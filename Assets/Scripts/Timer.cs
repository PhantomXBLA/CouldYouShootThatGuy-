using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    TextMeshProUGUI timerLabel;
    float timeLimit = 10;
    AudioSource timerSound;
    float pitch = 1;
    PlayerMovement playerController;

    public bool timerStop = false;
    // Start is called before the first frame update
    void Start()
    {
        timerLabel = GetComponent<TextMeshProUGUI>();
        timerSound = GetComponent<AudioSource>();
        playerController = GameObject.Find("Player").GetComponent<PlayerMovement>();

        InvokeRepeating("countdown", 1, 1);
        InvokeRepeating("flashCountdown", 1, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void countdown()
    {
        if (!timerStop)
        {
            timerSound.pitch = pitch;
            timeLimit -= 1;
            timerLabel.text = timeLimit.ToString("F0");

            if (timeLimit == 0)
            {
                //timeLimit = 0;
                CancelInvoke("countdown");
                CancelInvoke("flashCountdown");
                endGame();
            }
            //timerSound.Play();
            pitch += 0.025f;
        }

    }

    void flashCountdown()
    {

        timerLabel.enabled = !timerLabel.enabled; 
    }

    void endGame()
    {
        Debug.Log("Game Over!");
        playerController.GameOver();
    }
}
