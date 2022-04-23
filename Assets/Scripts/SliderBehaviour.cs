using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderBehaviour : MonoBehaviour
{
    float Sensitivity;
    Slider sensSlider;
    TextMeshProUGUI sensitivityValueLabel;
    // Start is called before the first frame update
    void Start()
    {
        sensSlider = GetComponent<Slider>();
        sensitivityValueLabel = GameObject.Find("SensitivityValueLabel").GetComponent<TextMeshProUGUI>();

        sensitivityValueLabel.text = sensSlider.value.ToString("F2");

        if (PlayerPrefs.HasKey("Sensitivity"))
        {
            sensSlider.value = PlayerPrefs.GetFloat("Sensitivity");
        }
        else
        {
            sensSlider.value = 0.1f;
            Sensitivity = sensSlider.value;
            PlayerPrefs.SetFloat("Sensitivity", Sensitivity);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SliderChange()
    {
        Sensitivity = sensSlider.value;
        sensitivityValueLabel.text = sensSlider.value.ToString("F2");
        PlayerPrefs.SetFloat("Sensitivity", Sensitivity);
    }
}
