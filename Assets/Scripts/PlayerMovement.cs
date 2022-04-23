using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    float walkSpeed = 7;
    [SerializeField]
    float runSpeed = 10;
    [SerializeField]
    float jumpForce = 1f;

    float maxVelocity = 6.25f;

    float mouseX;
    float mouseY;

    bool paused = false;
    Canvas pauseCanvas;

    Canvas gameOverCanvas;

    AudioSource levelMusic;

    //public float aimSensitivity = 0.05f;

    Vector3 moveDirection = Vector3.zero;
    Vector2 lookInput = Vector2.zero;
    Vector2 inputVector = Vector2.zero;

    bool isJumping = false;
    bool gameOver = false;

    public float aimSensitivity = 0.05f;
    private float XRotation = 0.0f;

    Transform playerTransform;

    public GameObject weapon;

    Quaternion oldRotation;

    float weaponSwayAmount = 400;
    float slideVelocity = 5;

    Rigidbody rigidbody;
    CapsuleCollider capsuleCollider;

    float capsuleCrouchHeight = 1;
    float capsuleHeight = 2;



    // Start is called before the first frame update
    void Start()
    {
        playerTransform = this.transform;
        rigidbody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        if (PlayerPrefs.HasKey("Sensitivity"))
        {
            aimSensitivity = PlayerPrefs.GetFloat("Sensitivity");
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        weapon = GameObject.Find("Weapon");
        oldRotation = weapon.transform.localRotation;

        pauseCanvas = GameObject.Find("PauseCanvas").GetComponent<Canvas>();
        gameOverCanvas = GameObject.Find("GameOverCanvas").GetComponent<Canvas>();
        pauseCanvas.enabled = false;
        gameOverCanvas.enabled = false;

        levelMusic = GameObject.Find("LevelMusic").GetComponent<AudioSource>();

        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {


        //Debug.Log(rigidbody.velocity.magnitude);

        

        if (!(inputVector.magnitude > 0)) moveDirection = Vector3.zero;
        moveDirection = transform.forward * inputVector.y + transform.right * inputVector.x;

        Vector3 movementDirection = moveDirection * (walkSpeed * Time.deltaTime);
        transform.position += movementDirection;

        if(rigidbody.velocity.magnitude > maxVelocity)
        {
            rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, maxVelocity);
        }
    }

    public void OnSlide(InputValue value)
    {
        if (value.isPressed && !isJumping)
        {
            capsuleCollider.height = capsuleCrouchHeight;
            rigidbody.AddForce(transform.forward * slideVelocity, ForceMode.VelocityChange);
        }

        if (!value.isPressed)
        {
            capsuleCollider.height = capsuleHeight;
        }




    }

    public void OnFire()
    {
        if (!paused)
        {
            weapon.GetComponent<GunController>().Shoot();
        }



        //Debug.Log("click");
    }

    public void OnRestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnMovement(InputValue value)
    {
        inputVector = value.Get<Vector2>();
    }

    public void OnLook(InputValue value)
    {
        if (!paused)
        {
            mouseX = value.Get<Vector2>().x * aimSensitivity;
            mouseY = value.Get<Vector2>().y * aimSensitivity;

            XRotation -= mouseY;
            XRotation = Mathf.Clamp(XRotation, -90.0f, 90.0f);

            Camera.main.transform.localRotation = Quaternion.Euler(XRotation, 0.0f, 0.0f);
            playerTransform.Rotate(Vector3.up * mouseX);

            Quaternion desiredRotation = weapon.transform.rotation * Quaternion.Inverse(oldRotation);

            Quaternion deltaRotation = desiredRotation.normalized;



            weapon.transform.localRotation = Quaternion.Euler(Vector3.Lerp(
                new Vector3(weapon.transform.rotation.x, weapon.transform.rotation.y, weapon.transform.rotation.z),
                new Vector3(weapon.transform.rotation.x, deltaRotation.y * weaponSwayAmount, weapon.transform.rotation.z), 1f));



            oldRotation = weapon.transform.rotation;
        }

        
    }

    public void OnJump(InputValue value)
    {
        if (!isJumping)
        {

            rigidbody.AddForce((transform.up + moveDirection) * jumpForce, ForceMode.Impulse);
            isJumping = true;

            //if (capsuleCollider.height != capsuleHeight)
            //{
            //    capsuleCollider.height = capsuleHeight;
            //}

        }

    }

    public void OnPause()
    {
        if (!paused && !gameOver)
        {
            pauseCanvas.enabled = true;
            Time.timeScale = 0;
            paused = true;
            levelMusic.Pause();
        }
        else
        {
            pauseCanvas.enabled = false;
            Time.timeScale = 1;
            paused = false;
            levelMusic.UnPause();
        }
    }

    public void GameOver()
    {
        if (!gameOver)
        {
            gameOverCanvas.enabled = true;
            Time.timeScale = 0;
            gameOver = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ground") && !isJumping) return;

        isJumping = false;
    }
}
