using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField] Text _ScoreText;
    [SerializeField] Image liveImgP1;
    [SerializeField] Image liveImgP2;
    [SerializeField] Sprite[] livesSprite;
    [SerializeField] GameObject gameoverText;
    [SerializeField] GameObject restartOnR;
    [SerializeField] GameObject gameplayInfo;
    [SerializeField] public bool gameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    public void disableInfoText()
    {
        gameplayInfo.SetActive(false);
    }
    public void UpdateScore(int PlayerScore)
    {
        _ScoreText.text = "Score: " + PlayerScore;
    }
    public void LifeLeftUpdaterP1(int CurrentLife)
    {
        if (CurrentLife >= 0)
        {
            liveImgP1.sprite = livesSprite[CurrentLife];
        }    
        if (CurrentLife < 1)
        {
            StartCoroutine(GameoverFlickerRoutine());
            restartOnR.SetActive(true);
            gameOver = true;
        }
    }
    public void LifeLeftUpdatorP2(int P2CurrentLife)
    {
        if (P2CurrentLife >= 0)
        {
            liveImgP2.sprite = livesSprite[P2CurrentLife];
        }
    }
    IEnumerator GameoverFlickerRoutine()
    {
        while (true)
        {
            gameoverText.SetActive(true);
            yield return new WaitForSeconds(0.4f);
            gameoverText.SetActive(false);
            yield return new WaitForSeconds(0.4f);
        }
    }

}
