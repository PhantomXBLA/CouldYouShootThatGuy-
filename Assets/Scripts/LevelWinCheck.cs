using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelWinCheck : MonoBehaviour
{
    GameObject[] allEnemies;
    int enemiesToKill;
    public int enemiesKilled = 0;
    Timer timer;
    // Start is called before the first frame update
    void Start()
    {
        allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        timer = GameObject.Find("Timer").GetComponent<Timer>();

        enemiesToKill = allEnemies.Length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && enemiesKilled >= enemiesToKill)
        {
            Debug.Log("yes");

            timer.timerStop = true;
            
        }
    }
}
