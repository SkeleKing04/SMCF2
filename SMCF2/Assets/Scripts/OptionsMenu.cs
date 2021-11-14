using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public AudioSource audioSource;
    public Slider volumeSlider;
    public Toggle fullscreenToggle;
    public void applyChanges()
    {
        audioSource.volume = volumeSlider.value;
        switch (fullscreenToggle.isOn)
        {
            case true:
                Screen.SetResolution(1920, 1080, FullScreenMode.ExclusiveFullScreen, 60);
                break;
            case false:
                //Screen.fullScreen = false;
                Screen.SetResolution(1280, 720, FullScreenMode.Windowed, 60);
                break;
        }
    }
}
