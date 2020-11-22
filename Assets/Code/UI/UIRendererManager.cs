using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(HUDManager))]
[RequireComponent(typeof(OnHurtHUDBorder))]
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
        highScoreBoard.DisplayHighscore(score);
    }

    public void DisplayLives(int lives)
    {
        hud.UpdateLivesDisplay(lives);
    }

    public void DisplayWave(int lives)
    {
        hud.UpdateWave(lives);
    }

    public void DisplayMoney(int amount)
    {
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
        hud.EnterPlacementMode(mode);
    }

    public void ExitTowerPlacementMode()
    {
        hud.HideDebugText();
    }
}