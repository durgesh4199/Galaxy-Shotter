using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadSinglePlayerScene()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadCoopScene()
    {
        SceneManager.LoadScene(2);
    }
    public void OnExit()
    {    
        Application.Quit();  
    }

}

