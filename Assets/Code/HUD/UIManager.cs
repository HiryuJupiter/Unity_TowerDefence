using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("Null group gameObjects")]
    [SerializeField] GameObject reloadPrompt;
    [SerializeField] GameObject ammoDisplay;
    [SerializeField] GameObject lossScreen;
    [SerializeField] GameObject newHighScoreMessage;

    [Header("UI elements")]
    [SerializeField] Text bulletCount; 
    [SerializeField] Text highScore;
    [SerializeField] Text killCounts;
    [SerializeField] GameObject[] livesIcons;

    [Header("Canvas group")]
    [SerializeField] CanvasGroup onHurtRedFlash;

    void Awake()
    {
        //Lazy singleton
        instance = this;
    }

    void Start()
    {
        //Hide unnecessary elements
        lossScreen.SetActive(false);
    }

    void Update()
    {
        //Fade out the red-flashing effect when player is hurt.
        if (onHurtRedFlash.alpha > 0f)
        {
            onHurtRedFlash.alpha -= Time.deltaTime * 5f;
        }
    }

    public void UpdateBulletCount (int bullet, int maxBullets)
    {
        //When told to update bullet count display, show a "reload" warning...
        //...when there is no bullets left.
        if (bullet > 0)
        {
            bulletCount.text = bullet + " / " + maxBullets;
            reloadPrompt.SetActive(false);
            ammoDisplay.SetActive(true);
        }
        else
        {
            ammoDisplay.SetActive(false);
            reloadPrompt.SetActive(true);
        }
    }

    public void GameOver (int score)
    {
        //Hide unnecessary elements and show highscore
        UpdateHighScore(score);
        lossScreen.SetActive(true);
        reloadPrompt.SetActive(false);
        ammoDisplay.SetActive(false);
    }

    public void UpdateLives (int currentLives)
    {
        //Reveal heart icons corresponding the lives left
        for (int i = 0; i < livesIcons.Length; i++)
        {
            livesIcons[i].SetActive(currentLives > i ? true : false);
        }
    }

    public void UpdateKillCount (int kills)
    {
        //Update skill score display
        killCounts.text = kills.ToString().PadLeft(2, '0');
    }

    public void OnHurtFlashRedBorder ()
    {
        //When hurt, set red borders to visible
        onHurtRedFlash.alpha = 1f;
    }

    void UpdateHighScore (int currentHighScore)
    {
        //Load highscore
        int prevHighscore = HighScore.LoadHighScore();
        Debug.Log("prev highscore " + prevHighscore);

        //Save high score if it is higher than previous highscore
        if(currentHighScore > prevHighscore)
        {
            HighScore.SaveHighscore(currentHighScore);
            newHighScoreMessage.SetActive(true);
        }

        //Display the highscore with some paddings to the left
        string paddedString = currentHighScore.ToString().PadLeft(4, '0');
        highScore.text = paddedString;
    }
}
