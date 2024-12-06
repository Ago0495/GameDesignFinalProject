using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    void Start()
    {
        // Initialize slider with the volume from GameManager
        float currentVolume = GameManager.GetGameVolume();

        if (volumeSlider != null)
        {
            volumeSlider.value = currentVolume;
            volumeSlider.onValueChanged.AddListener(UpdateVolume);
        }
    }

    public void UpdateVolume(float value)
    {
        // Update the volume in GameManager
        GameManager.SetGameVolume(value);

    }

    void OnDestroy()
    {
        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.RemoveListener(UpdateVolume);
        }
    }
}
