using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

            switch (SceneManager.GetActiveScene().buildIndex)
            {
                case 1:
                    SceneManager.LoadScene(2);
                    break;
                case 2:
                    SceneManager.LoadScene(3);
                    break;
                case 3:
                    SceneManager.LoadScene(4);
                    break;

            }

        }
    }
}
