using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
   [SerializeField] private Slider volumeSlider;
   [SerializeField] private AudioSource audioSource;

    void Start()
    {
        if (audioSource != null && volumeSlider != null)
        {
            volumeSlider.value = audioSource.volume;
            volumeSlider.onValueChanged.AddListener(UpdateVolume);
        }
    }
    public void UpdateVolume(float value)
    {
        if (audioSource != null)
        {
            audioSource.volume = value;
        }
    }
    void OnDestroy()
    {
        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.RemoveListener(UpdateVolume);
        }
    }
}
