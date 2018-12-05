using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;



public class GameController : MonoBehaviour
{
    public static GameController gameController;

    [SerializeField] private GameObject mainMenu, optionsMainMenu, chaptersMainMenu, instructionsMainMenu;

    private Button load, chapters;

    private string saveLocation;
    //private string settingsLocation;

    [HideInInspector] public bool save;
    //[HideInInspector] public bool noSettings;

    /*public float masterAudio;
    public float effectsAudio;
    public float musicAudio;
    public bool fullscreen;
    public int graphics;
    public int resolutionX;
    public int resolutionY;*/


    void Awake()
    {
        saveLocation = Application.persistentDataPath + "/Level.dat";
        //settingsLocation = Application.persistentDataPath + "Settings.dat";

        save = true;

        optionsMainMenu.SetActive(true);
        mainMenu.SetActive(true);
        optionsMainMenu.SetActive(false);
        chaptersMainMenu.SetActive(false);
        instructionsMainMenu.SetActive (false);

        if (gameController == null)
        {
            DontDestroyOnLoad(gameObject);
            gameController = this;
        }
        else if (gameController != this)
        {
            Destroy(gameObject);
        }

        if (File.Exists(saveLocation) == false)
        {
            load = GameObject.Find("ContinueBut").GetComponent<Button>();
            load.interactable = false;
            /*chapters = GameObject.Find("ChaptersBut").GetComponent<Button>();
            chapters.interactable = false;
            noSettings = true;*/
        }
        /*else
        {
            noSettings = false;
            LoadSettings();
        }*/

        /*if (File.Exists(settingsLocation) == false)
        {
            noSettings = true;
        }*/
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) == true)
        {
            Delete();
        }
    }


    public void Save()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter ();
        FileStream file = File.Create (saveLocation);

        /*data.masterAudio = masterAudio;
        data.effectsAudio = masterAudio;
        data.musicAudio = masterAudio;
        data.fullscreen = fullscreen;
        data.graphics = graphics;
        data.resolutionX = resolutionX;
        data.resolutionY = resolutionY;*/

        binaryFormatter.Serialize(file, SceneManager.GetActiveScene().buildIndex);
        file.Close();
    }


    /*public void SaveAudio(float volume)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Create(saveLocation);

        PlayerData data = new PlayerData();

        data.masterAudio = volume;
    }*/

    /*public void SaveSettings(float volume, bool fullscreen, int graphics, int resolution)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Create(saveLocation);

        PlayerData data = new PlayerData();

        data.masterAudio = volume;
        data.fullscreen = fullscreen;
        data.graphics = graphics;
        data.resolution = resolution;
    }*/


    public void Load()
    {
        if (File.Exists(saveLocation) == true)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter ();
            FileStream file = File.Open(saveLocation, FileMode.Open);

            save = true;

            SceneManager.LoadScene((int) binaryFormatter.Deserialize(file));
            file.Close();
            /*level = data.level;
            masterAudio = data.masterAudio;
            effectsAudio = data.effectsAudio;
            musicAudio = data.musicAudio;
            fullscreen = data.fullscreen;
            graphics = data.graphics;
            resolutionX = data.resolutionX;
            resolutionY = data.resolutionY;*/
        }
    }


    /*private void LoadSettings()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Open(saveLocation, FileMode.Open);

        PlayerData data = (PlayerData)binaryFormatter.Deserialize(file);
        file.Close();

        data.masterAudio =;
        Screen.fullScreen = data.fullscreen;
        QualitySettings.SetQualityLevel (data.graphics);
    }*/


    public void Delete()
    {
        if (File.Exists (saveLocation) == true)
        {
            File.Delete(saveLocation);
            load = GameObject.Find("ContinueBut").GetComponent<Button>();
            load.interactable = false;
            //chapters = GameObject.Find("ChaptersBut").GetComponent<Button>();
            //chapters.interactable = false;
        }
    }


    public int CheckLastUnlocked()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Open (saveLocation, FileMode.Open);

        int last = (int) binaryFormatter.Deserialize(file);
        file.Close();

        return last;
    }


    public void UsedChapterSelection ()
    {
        save = false;
    }


    public void NormalPlay()
    {
        save = true;
    }
}
