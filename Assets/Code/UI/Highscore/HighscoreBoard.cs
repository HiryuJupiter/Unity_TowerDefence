using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HighscoreBoard : MonoBehaviour
{
    [SerializeField] GameObject highscoreBoard;
    [SerializeField] Text highscoreText;
    [SerializeField] GameObject newHighScoreMessage;

    int highscore;

    void Awake()
    {
        highscoreBoard.SetActive(false);
    }

    public void DisplayHighscore (int score)
    {
        highscoreBoard.SetActive(true);
        highscore = score;
        CheckIfNewHighscore(score);

        StartCoroutine(PlayHighscoreAnimation());
    }

    IEnumerator PlayHighscoreAnimation ()
    {
        //Tick and increase the highscore text from 0 to the current score
        float score = 0;
        float t = 0;
        while (t < 2f)
        {
            t += Time.deltaTime;
            float t2 = t / 2f;
            score = Mathf.Lerp(0, highscore, t2);
            highscoreText.text = $"{Mathf.CeilToInt(score):0000}";
            yield return null;
        }

        highscoreText.text = $"{(int)highscore:0000}";
    }

    void CheckIfNewHighscore(int score)
    {
        //Load previous highscore
        int prevHighscore = HighScore.LoadHighScore();
        Debug.Log("prev highscore " + prevHighscore);

        //Save high score if it is higher than previous highscore
        if (score > prevHighscore)
        {
            HighScore.SaveHighscore(score);
            newHighScoreMessage.SetActive(true);
        }
    }
}