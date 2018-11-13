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

    private Button load;
    private Button chapters;

    private string saveLocation;

    public bool chapterSelected;

    /*public int level;
    public float masterAudio;
    public float effectsAudio;
    public float musicAudio;
    public bool fullscreen;
    public int graphics;
    public int resolutionX;
    public int resolutionY;*/


    void Awake()
    {
        saveLocation = Application.persistentDataPath + "/PlayerInfo.dat";

        chapterSelected = false;

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
            chapters = GameObject.Find("ChaptersBut").GetComponent<Button>();
            chapters.interactable = false;
        }
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

        PlayerData data = new PlayerData();
        data.level = SceneManager.GetActiveScene().buildIndex;
        /*data.masterAudio = masterAudio;
        data.effectsAudio = masterAudio;
        data.musicAudio = masterAudio;
        data.fullscreen = fullscreen;
        data.graphics = graphics;
        data.resolutionX = resolutionX;
        data.resolutionY = resolutionY;*/

        binaryFormatter.Serialize(file, data);
        file.Close();
    }


    public void Load()
    {
        if (File.Exists(saveLocation) == true)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter ();
            FileStream file = File.Open(saveLocation, FileMode.Open);

            PlayerData data = (PlayerData)binaryFormatter.Deserialize(file);
            file.Close();

            SceneManager.LoadScene(data.level);
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


    public void Delete()
    {
        if (File.Exists (saveLocation) == true)
        {
            File.Delete(saveLocation);
        }
    }


    public int CheckLastUnlocked()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Open(saveLocation, FileMode.Open);

        PlayerData data = (PlayerData)binaryFormatter.Deserialize(file);
        file.Close();

        return data.level;
    }


    public void UsedChapterSelection ()
    {
        chapterSelected = true;
    }
}



[Serializable] class PlayerData
{
    public int level;
    /*public float masterAudio;
    public float effectsAudio;
    public float musicAudio;
    public bool fullscreen;
    public int graphics;
    public int resolutionX;
    public int resolutionY;*/
}
