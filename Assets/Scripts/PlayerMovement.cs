using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    float walkSpeed = 7;
    [SerializeField]
    float runSpeed = 10;
    [SerializeField]
    float jumpForce = 5;

    float mouseX;
    float mouseY;

    //public float aimSensitivity = 0.05f;

    Vector3 moveDirection = Vector3.zero;
    Vector2 lookInput = Vector2.zero;
    Vector2 inputVector = Vector2.zero;

    public float aimSensitivity = 0.05f;
    private float XRotation = 0.0f;

    Transform playerTransform;

    public GameObject weapon;

    Quaternion oldRotation;

    float weaponSwayAmount = 400;
    float slideVelocity = 5;

    Rigidbody rigidbody;
    CapsuleCollider capsuleCollider;



    // Start is called before the first frame update
    void Start()
    {
        playerTransform = this.transform;
        rigidbody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        weapon = GameObject.Find("Weapon");
        oldRotation = weapon.transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {


        //Debug.Log(weapon.transform.rotation);

        

        if (!(inputVector.magnitude > 0)) moveDirection = Vector3.zero;
        moveDirection = transform.forward * inputVector.y + transform.right * inputVector.x;

        Vector3 movementDirection = moveDirection * (walkSpeed * Time.deltaTime);
        transform.position += movementDirection;
    }

    public void OnSlide(InputValue value)
    {
        if (value.isPressed)
        {
            capsuleCollider.height /= 2;
            rigidbody.AddForce(transform.forward * 5, ForceMode.VelocityChange);
        }

        if (!value.isPressed)
        {
            capsuleCollider.height *= 2;
        }




    }

    public void OnFire()
    {
        weapon.GetComponent<GunController>().Shoot();

        Debug.Log("click");
    }

    public void OnMovement(InputValue value)
    {
        inputVector = value.Get<Vector2>();
    }

        public void OnLook(InputValue value)
    {
        mouseX = value.Get<Vector2>().x * aimSensitivity;
        mouseY = value.Get<Vector2>().y * aimSensitivity;

        XRotation -= mouseY;
        XRotation = Mathf.Clamp(XRotation, -90.0f, 90.0f);

        Camera.main.transform.localRotation = Quaternion.Euler(XRotation, 0.0f, 0.0f);
        playerTransform.Rotate(Vector3.up * mouseX);

        Quaternion desiredRotation = weapon.transform.rotation * Quaternion.Inverse(oldRotation);

        Quaternion deltaRotation = desiredRotation.normalized;

        //Debug.Log(deltaRotation.y);

        weapon.transform.localRotation = Quaternion.Euler(Vector3.Lerp(
            new Vector3(weapon.transform.rotation.x, weapon.transform.rotation.y, weapon.transform.rotation.z), 
            new Vector3(weapon.transform.rotation.x, deltaRotation.y * weaponSwayAmount, weapon.transform.rotation.z), 1f));



        oldRotation = weapon.transform.rotation;
    }
}
