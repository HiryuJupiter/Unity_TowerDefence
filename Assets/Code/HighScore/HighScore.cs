using UnityEngine;
using System.Collections;

public static class HighScore
{
    private const string Key_HighScore = "HighScore";

    public static void SaveHighscore (int score)
    {
        //Save to playerpref
        PlayerPrefs.SetInt(Key_HighScore, score);
    }

    public static int LoadHighScore ()
    {
        //Load from player pref
        return PlayerPrefs.GetInt(Key_HighScore, 0);
    }
}