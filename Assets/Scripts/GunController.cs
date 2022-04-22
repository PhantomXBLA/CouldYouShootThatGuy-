using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GunController : MonoBehaviour
{
    GameObject weapon;
    Camera mainCamera;
    Rigidbody rigidbody;
    TextMeshProUGUI ammoLabel;

    Animator weaponAnimator;

    public LayerMask layers;

    public GameObject brokenEnemy;

    int magazineSizeMax = 8;
    int currentAmmo;
    AudioSource levelAudio;

    Rigidbody playerRigidbody;

    LevelWinCheck leveWinCheck;


    public AudioSource fireSound;
    // Start is called before the first frame update
    void Start()
    {
        weapon = GameObject.Find("Weapon");
        mainCamera = Camera.main;
        rigidbody = GetComponent<Rigidbody>();
        weaponAnimator = GetComponent<Animator>();

        playerRigidbody = GameObject.Find("Player").GetComponent<Rigidbody>();
        ammoLabel = GameObject.Find("AmmoCount").GetComponent<TextMeshProUGUI>();

        levelAudio = GameObject.Find("LevelMusic").GetComponent<AudioSource>();
        leveWinCheck = GameObject.Find("WinPlatform").GetComponent<LevelWinCheck>();

        currentAmmo = magazineSizeMax;
        ammoLabel.text = currentAmmo.ToString();

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        if(currentAmmo > 0)
        {
            fireSound.Play();
            //weaponAnimator.SetBool("weaponFiring", true);
            weaponAnimator.Play("Fire");
            //Invoke("resetAnimState", .01f);

            //rigidbody.AddForce(new Vector3(0, 0, -1), ForceMode.Impulse);

            Ray screenRay = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

            if (Physics.Raycast(screenRay, out RaycastHit hit ,100f, layers))
            {
                Vector3 hitDirection = hit.point - mainCamera.transform.position;

                Debug.DrawRay(mainCamera.transform.position, hitDirection.normalized * 500, Color.red, 1);

                if(hit.transform.gameObject.tag == "Enemy")
                {



                    Instantiate(brokenEnemy, hit.transform.position, hit.transform.rotation);
                    hit.transform.gameObject.SetActive(false);
                    leveWinCheck.enemiesKilled++;

                    if (SceneManager.GetActiveScene().buildIndex == 1)
                    {
                        Level1GameManager level1GameManager = GameObject.Find("GameManager").GetComponent<Level1GameManager>();

                        level1GameManager.enemyShot();
                    }

                }

                if (hit.transform.gameObject.tag == "Headshot")
                {
                    Debug.Log(hit.transform.gameObject);
                    Time.timeScale = 0.1f;
                    fireSound.pitch = Time.timeScale * 3;
                    levelAudio.pitch = Time.timeScale * 6;
                    playerRigidbody.interpolation = RigidbodyInterpolation.Interpolate;
                    Instantiate(brokenEnemy, hit.transform.position, hit.transform.rotation);
                    hit.transform.parent.gameObject.SetActive(false);
                    leveWinCheck.enemiesKilled++;

                    if (SceneManager.GetActiveScene().buildIndex == 1)
                    {
                        Level1GameManager level1GameManager = GameObject.Find("GameManager").GetComponent<Level1GameManager>();

                        level1GameManager.enemyShot();
                    }

                    Invoke("returnToNormal", .2f);

                }
            }

            currentAmmo--;
            ammoLabel.text = currentAmmo.ToString();
        }


    }

    void returnToNormal()
    {
        Time.timeScale = 1f;
        fireSound.pitch = Time.timeScale;
        levelAudio.pitch = Time.timeScale;
        playerRigidbody.interpolation = RigidbodyInterpolation.None;
    }

    void resetAnimState()
    {
        weaponAnimator.SetBool("weaponFiring", false);
    }

}
