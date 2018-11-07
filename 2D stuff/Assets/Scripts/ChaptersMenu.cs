using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;


public class ChaptersMenu : MonoBehaviour
{
    [SerializeField] private Button chapter2;
    BinaryFormatter binaryFormatter = new BinaryFormatter();


    private void Awake()
    {
        if ((string) binaryFormatter.Deserialize(File.Open(Application.persistentDataPath + "/SavedData/level.txt", FileMode.Open)) == "Level1")
        {
            chapter2 = GameObject.Find("Chapter2But").GetComponent<Button>();
            chapter2.interactable = false;
        }
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene(1);
    }


    public void LoadLevel2()
    {
        SceneManager.LoadScene(2);
    }
}
