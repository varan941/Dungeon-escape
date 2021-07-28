using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause_menu : MonoBehaviour
{
    public static bool GameIsPaused = false; // в начаеле игра не на паузе
    public GameObject _pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused == true)
            {
                Resume();
            }
            else
            {
                Pause();
            }

        }

    }

    public void Resume()
    {
        _pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        _pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void LoadMenu() // чтоб вернутся в глав меню
    {
        SceneManager.LoadScene("Main_menu");
        Time.timeScale = 1.0f;
    }

    
}
