using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(HUDManager))]
[RequireComponent(typeof(OnHurtHUDBorder))]
[RequireComponent(typeof(HighscoreBoard))]
public class UIRendererManager : MonoBehaviour
{
    public static UIRendererManager Instance;

    HUDManager hud;
    OnHurtHUDBorder hurtBorder;
    HighscoreBoard highScoreBoard;

    #region MonoBehavior
    void Awake()
    {
        //Lazy singleton
        Instance = this;

        //Reference
        hud = GetComponent<HUDManager>();
        hurtBorder = GetComponent<OnHurtHUDBorder>();
        highScoreBoard = GetComponent<HighscoreBoard>();
    }
    #endregion

    #region Public
    public void GameOver(int score)
    {
        //Display highscore when games over
        highScoreBoard.DisplayHighscore(score);
    }

    public void DisplayLives(int lives)
    {
        //Delegate lives update to HUD manager
        hud.UpdateLivesDisplay(lives);
    }

    public void DisplayWave(int lives)
    {
        //Delegate waves count update to HUD manager
        hud.UpdateWave(lives);
    }

    public void DisplayMoney(int amount)
    {
        //Delegate money display to HUD manager
        hud.UpdateMoneyCount(amount);
    }

    public void OnDamaged()
    {
        //When hurt, set red borders to visible
        hurtBorder.FlashRed();
    }
    #endregion

    public void TowerPlacementMode(TowerTypes mode)
    {
        //Tell HDU manager we're in placement mode
        hud.EnterPlacementMode(mode);
    }

    public void ExitTowerPlacementMode()
    {
        //Tell HUD we're no longer in placement mode
        hud.ExitPlacementMode();
        hud.HideDebugText();
    }
}