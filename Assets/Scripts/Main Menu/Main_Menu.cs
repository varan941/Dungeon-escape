using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // бибилиот. для управ. сценами

public class Main_Menu : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadScene("Level 0"); //загружает сцену с именем 
    }

    public void LoadLevel(int n)
    {
        SceneManager.LoadScene("Level "+n.ToString());
    }


    public void QuitButton()
    {
        Application.Quit( );
    }

}
