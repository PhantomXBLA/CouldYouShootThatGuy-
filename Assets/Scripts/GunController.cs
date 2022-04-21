using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    GameObject weapon;
    Camera mainCamera;



    public AudioSource fireSound;
    // Start is called before the first frame update
    void Start()
    {
        weapon = GameObject.Find("Weapon");
        mainCamera = Camera.main;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        fireSound.Play();


        Ray screenRay = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        if (Physics.Raycast(screenRay, out RaycastHit hit, 500f))
        {
            Vector3 hitDirection = hit.point - mainCamera.transform.position;
            Debug.DrawRay(mainCamera.transform.position, hitDirection.normalized * 500, Color.red, 1);
        }

    }

}
