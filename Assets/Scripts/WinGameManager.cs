using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGameManager : MonoBehaviour
{
    public GameObject brokenEnemy;
    GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        enemy = GameObject.Find("Low Poly Bad Guy");

        Invoke("DestroyEnemy", 2.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DestroyEnemy()
    {
        Instantiate(brokenEnemy, enemy.transform.position, enemy.transform.rotation);
        enemy.SetActive(false);
    }
}
