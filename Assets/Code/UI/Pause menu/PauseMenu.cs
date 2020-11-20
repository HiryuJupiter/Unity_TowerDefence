using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public CanvasGroup canvas_Pause;
    public GameObject PauseButton;

    bool isPaused = false;

    void Start()
    {
        SetPause(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    #region Public - buttons
    public void TogglePause()
    {
        SetPause(isPaused = !isPaused);
    }

    public void PauseMenu_QuitGame()
    {
        SetPause(isPaused = false);
    }
    #endregion

    #region Pause logic
    void SetPause(bool pause)
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