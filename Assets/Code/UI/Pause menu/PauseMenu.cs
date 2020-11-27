﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public CanvasGroup canvas_Pause;
    public GameObject PauseButton;
    public bool isPaused { get; private set; }

    private void Start()
    {
        
        SetPause(false);
    }

    #region Public - buttons
    public void TogglePause()
    {
        //Set pause to the opposite of its current value
        SetPause(isPaused = !isPaused);
    }

    public void PauseMenu_QuitGame()
    {
        SetPause(isPaused = false);
    }
    #endregion

    #region Pause logic
    private void SetPause(bool pause)
    {
        //Debug.Log("Set pause " + isPaused);
        isPaused = pause;
        if (isPaused)
        {
            Time.timeScale = 0f;
            CanvasGroupUtil.RevealCanvasGroup(canvas_Pause);
        }
        else
        {
            Time.timeScale = 1f;
            CanvasGroupUtil.HideCanvasGroup(canvas_Pause);
        }
    }
    #endregion
}