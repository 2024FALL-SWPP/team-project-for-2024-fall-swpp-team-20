using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum SliderType
{
    BGMVolume,
    SFXVolume,
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
    public void SetSlider()
    {
        switch (sliderType)
        {
            case SliderType.BGMVolume:
                if (slider.value != GameManager.GetInstance().GetBGMVolume())
                {
                    slider.value = GameManager.GetInstance().GetBGMVolume() * 100f;
                }
                break;
            case SliderType.SFXVolume:
                if (slider.value != GameManager.GetInstance().GetSFXVolume()) 
                {
                    slider.value = GameManager.GetInstance().GetSFXVolume() * 100f;
                }
                break;
            case SliderType.Sensitivity:
                if (slider.value != GameManager.GetInstance().GetSensitivity())
                {
                    slider.value = GameManager.GetInstance().GetSensitivity();
                }
                break;
            default:
                Debug.LogError("Invalid or null type");
                break;
        }
        SetNumber();
    }

    public void SetValue()
    {
        SetNumber();
        switch (sliderType)
        {
            case SliderType.BGMVolume:
                GameManager.GetInstance().SetBGMVolume(slider.value);
                break;
            case SliderType.SFXVolume:
                GameManager.GetInstance().SetSFXVolume(slider.value);
                break;
            case SliderType.Sensitivity:
                GameManager.GetInstance().SetSensitivity(slider.value);
                break;
            default:
                Debug.LogError("Invalid or null type");
                break;
        }
    }

    private void SetNumber()
    {
        sliderText.text = $"{(int)slider.value}";
    }
}
