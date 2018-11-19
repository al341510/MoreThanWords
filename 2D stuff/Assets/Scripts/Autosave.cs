
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;



public class Autosave : MonoBehaviour
{
    //private int delay;


	void Awake ()
    {
        if (GameController.gameController.save == true)
        {
            GameController.gameController.Save ();
        }
        
        /*currentScene = x;
        if (Directory.Exists(Application.persistentDataPath + "/SavedData") == false)
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/SavedData");
        }

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/SavedData/level.txt");
        binaryFormatter.Serialize(file, currentScene);
        file.Close();

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            delay = 2;

            StartCoroutine(LoadLevelAfterDelay(delay));
        }*/
    }


    /*IEnumerator LoadLevelAfterDelay(int delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }*/
}
