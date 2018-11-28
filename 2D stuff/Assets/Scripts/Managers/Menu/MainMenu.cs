using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class MainMenu : MonoBehaviour
{
    /*public static MainMenu instance;
    [SerializeField] private Button load;
    [SerializeField] private Button chapters;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this);

        // The player won't be able to interact with the 'Continue' and 'Chapters' buttons if there is no save file in the system.
        if (File.Exists (Application.persistentDataPath + "/SavedData/level.txt") == false)
        {
            load = GameObject.Find("ContinueBut").GetComponent<Button>();
            load.interactable = false;
            chapters = GameObject.Find("ChaptersBut").GetComponent<Button>();
            chapters.interactable = false;
        }
    }*/


    public void PlayGame()
    {
        GameController.gameController.NormalPlay();
        SceneManager.LoadScene (1);
    }


    public void ContinueGame()
    {
        GameController.gameController.Load();
    }


    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit ();
    }


    /*public void LoadGame()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + "/SavedData/level.txt") == true)
        {
            FileStream file = File.Open(Application.persistentDataPath + "/SavedData/level.txt", FileMode.Open);
            SceneManager.LoadScene((string) binaryFormatter.Deserialize(file));
            file.Close();
        }
    }*/
}
