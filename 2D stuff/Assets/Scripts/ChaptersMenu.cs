
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;



public class ChaptersMenu : MonoBehaviour
{
    private Button chapter2;


    void Start ()
    {
        if (GameController.gameController.CheckLastUnlocked () == 1)
        {
            chapter2 = GameObject.Find("Chapter2But").GetComponent<Button> ();
            chapter2.interactable = false;
        }
    }


    private void LoadLevel (int level)
    {
        GameController.gameController.UsedChapterSelection ();
        SceneManager.LoadScene (level);
    }
}
