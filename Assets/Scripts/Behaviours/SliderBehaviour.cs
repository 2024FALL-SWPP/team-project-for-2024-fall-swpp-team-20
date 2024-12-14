using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum SliderType { 
    Volume,
    Sensitivity
}

public class SliderBehaviour : MonoBehaviour
{
    [SerializeField] private SliderType sliderType;

    private Slider slider;
    private Text sliderText;
    private void OnEnable()
    {
        if (slider == null) slider = GetComponent<Slider>();
        if (sliderText == null) sliderText = GetComponentInChildren<Text>();
        SetSlider();
    }
    public void SetSlider() {
        Debug.Log(slider.value);
        switch (sliderType) { 
            case SliderType.Volume:
                slider.value = GameManager.GetInstance().volume * 100f;
                break;
            case SliderType.Sensitivity:
                slider.value = GameManager.GetInstance().sensitivity;
                break;
            default:
                Debug.LogError("Invalid or null type");
                break;
        }
        SetNumber();
    }

    public void SetValue() {
        SetNumber();
        switch (sliderType) { 
            case SliderType.Volume:
                GameManager.GetInstance().SetVolume(slider.value);
                break;
            case SliderType.Sensitivity:
                GameManager.GetInstance().SetSensitivity(slider.value);
                break;
            default:
                Debug.LogError("Invalid or null type");
                break;
        }
    }

    private void SetNumber() {
        sliderText.text = $"{(int)slider.value}";
    }
}
