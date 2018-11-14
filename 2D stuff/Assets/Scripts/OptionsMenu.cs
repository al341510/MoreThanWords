using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;



public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    Resolution[] resolutions;
    [SerializeField] private Dropdown resolutionDropdown;

    /*private float volumeSave;
    private bool fullscreenSave;
    private int graphicsSave, resolutionSave;*/


    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolution = 0;

        for (int i = 0; i < resolutions.Length; i += 1)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolution = i;
                //resolutionSave = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolution;
        resolutionDropdown.RefreshShownValue();

        /*if (GameController.gameController.noSettings == true)
        {
            CreateDefault();
        }*/
    }


    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }


    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
        //volumeSave = volume;
    }


    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        //graphicsSave = qualityIndex;
    }


    public void SetFullscreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
        //fullscreenSave = fullscreen;
    }


    /*public void SaveChanges()
    {
        GameController.gameController.SaveSettings(volumeSave, fullscreenSave, graphicsSave, resolutionSave);
    }


    private void CreateDefault()
    {
        GameController.gameController.SaveSettings(0f, Screen.fullScreen, 3, resolutionSave);
    }*/
}

