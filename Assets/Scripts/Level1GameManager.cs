using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Level1GameManager : MonoBehaviour
{
    TextMeshProUGUI CYSTGText;
    // Start is called before the first frame update
    void Start()
    {
        CYSTGText = GameObject.Find("CYSTG").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void enemyShot()
    {
        CYSTGText.text = "Thanks";
    }
}
