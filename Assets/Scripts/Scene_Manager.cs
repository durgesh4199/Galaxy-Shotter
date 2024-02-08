using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoBehaviour
{
    private UIManager uiGO;
    public bool isCoopModeActive = false;
    // Start is called before the first frame update
    void Start()
    {
        uiGO = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        LoadCurrentScene();
        LoadMainScene();
        OnExit();
    }

    private void LoadCurrentScene()
    {
        if (uiGO.gameOver == true && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    private void LoadMainScene()
    {
        if (uiGO.gameOver == true && Input.GetKeyDown(KeyCode.T))
        {
            SceneManager.LoadScene(0);
        }
    }
    private void OnExit()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();    
        }
    }
}
