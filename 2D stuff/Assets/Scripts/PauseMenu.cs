using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;



public class PauseMenu : MonoBehaviour
{
    public static bool paused = false;
    [SerializeField] private GameObject pauseMenu, mainPauseMenu, optionsPauseMenu;


    void Awake()
    {
        pauseMenu.SetActive(false);
        mainPauseMenu.SetActive(false);
        optionsPauseMenu.SetActive(false);
    }


    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape) == true)
        {
            if (paused == true)
            {
                Resume ();
            }
            else
            {
                Pause ();
            }
        }
	}


    public void Resume ()
    {
        pauseMenu.SetActive (false);
        mainPauseMenu.SetActive (false);
        optionsPauseMenu.SetActive (false);
        Time.timeScale = 1f;
        paused = false;
    }


    private void Pause ()
    {
        pauseMenu.SetActive (true);
        mainPauseMenu.SetActive (true);
        optionsPauseMenu.SetActive (false);
        Time.timeScale = 0f;
        paused = true;
    }


    public void RestartLevel ()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
    }


    public void LoadMenu ()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene (0);
    }


    public void QuitGame ()
    {
        Debug.Log ("Quit");
        Application.Quit ();
    }
}
