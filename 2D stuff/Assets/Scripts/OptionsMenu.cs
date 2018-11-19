using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using UnityEngine;



public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    Resolution[] resolutions;

    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private Dropdown resolutionDropdown, graphicsDropdown;
    [SerializeField] private Slider volumeSlider;

    private string settingsLocation;

    private bool fullscreenSave;
    private int resolutionSave, graphicsSave;
    private float volumeSave;


    void Awake()
    {
        settingsLocation = Application.persistentDataPath + "/Settings.dat";

        PlayerSettings adjustments = new PlayerSettings();

        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        resolutionSave = 0;

        for (int i = 0; i < resolutions.Length; i += 1)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                resolutionSave = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = resolutionSave;
        resolutionDropdown.RefreshShownValue();

        /*if (GameController.gameController.noSettings == true)
        {
            DefaultSettings ();
        }*/

        if (File.Exists (settingsLocation) == false)
        {
            adjustments = CreateDefault ();
        }
        else
        {
            adjustments = LoadSettings ();
        }

        MatchSettings (adjustments);
    }


    private PlayerSettings CreateDefault ()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter ();
        PlayerSettings data = new PlayerSettings ();
        FileStream file = File.Create (settingsLocation);

        data.fullscreen = Screen.fullScreen;
        fullscreenSave = data.fullscreen;
        data.resolution = resolutionSave;
        audioMixer.GetFloat ("volume", out data.masterAudio);
        volumeSave = data.masterAudio;
        data.graphics = QualitySettings.GetQualityLevel ();
        graphicsSave = data.graphics;

        binaryFormatter.Serialize (file, data);
        file.Close ();

        return data;
    }


    private PlayerSettings LoadSettings ()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Open (settingsLocation, FileMode.Open);
        PlayerSettings data = (PlayerSettings) binaryFormatter.Deserialize (file);
        file.Close ();

        SetFullscreen (data.fullscreen);
        SetResolution (data.resolution);
        SetVolume (data.masterAudio);
        SetQuality (data.graphics);

        Debug.Log(data.fullscreen);
        Debug.Log(data.resolution);
        Debug.Log(data.masterAudio);
        Debug.Log(data.graphics);

        return data;
    }


    private void MatchSettings (PlayerSettings adjustments)
    {
        fullscreenToggle.isOn = adjustments.fullscreen;
        resolutionDropdown.value = adjustments.resolution;
        resolutionDropdown.RefreshShownValue ();
        volumeSlider.value = adjustments.masterAudio;
        graphicsDropdown.value = adjustments.graphics;
        graphicsDropdown.RefreshShownValue();
    }


    public void SetFullscreen (bool fullscreen)
    {
        Screen.fullScreen = fullscreen;

        fullscreenSave = fullscreen;
    }


    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution;
        if (resolutionIndex < resolutions.Length)
        {
            resolution = resolutions[resolutionIndex];
        }
        else
        {
            resolutionIndex = 0;
            resolution = resolutions[resolutionIndex];
        }
        Screen.SetResolution (resolution.width, resolution.height, Screen.fullScreen);

        resolutionSave = resolutionIndex;
    }


    public void SetVolume (float volume)
    {
        audioMixer.SetFloat ("volume", volume);

        volumeSave = volume;
    }


    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel (qualityIndex);

        graphicsSave = qualityIndex;
    }


    public void SaveChanges ()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter ();
        PlayerSettings data = new PlayerSettings ();
        FileStream file = File.Create (settingsLocation);

        data.fullscreen = fullscreenSave;
        data.resolution = resolutionSave;
        data.masterAudio = volumeSave;
        data.graphics = graphicsSave;

        binaryFormatter.Serialize (file, data);
        file.Close ();
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



[Serializable] class PlayerSettings
{
    public bool fullscreen;
    public int resolution, graphics;
    public float masterAudio;
}

