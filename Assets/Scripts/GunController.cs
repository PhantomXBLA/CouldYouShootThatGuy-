using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunController : MonoBehaviour
{
    GameObject weapon;
    Camera mainCamera;
    Rigidbody rigidbody;
    TextMeshProUGUI ammoLabel;


    int magazineSizeMax = 8;
    int currentAmmo;


    public AudioSource fireSound;
    // Start is called before the first frame update
    void Start()
    {
        weapon = GameObject.Find("Weapon");
        mainCamera = Camera.main;
        rigidbody = GetComponent<Rigidbody>();
        ammoLabel = GameObject.Find("AmmoCount").GetComponent<TextMeshProUGUI>();

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

           //rigidbody.AddForce(new Vector3(0, 0, -1), ForceMode.Impulse);

            Ray screenRay = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

            if (Physics.Raycast(screenRay, out RaycastHit hit, 500f))
            {
                Vector3 hitDirection = hit.point - mainCamera.transform.position;
                Debug.DrawRay(mainCamera.transform.position, hitDirection.normalized * 500, Color.red, 1);
            }

            currentAmmo--;
            ammoLabel.text = currentAmmo.ToString();
        }


    }

}
