using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(HUDManager))]
[RequireComponent(typeof(OnHurtHUDBorder))]
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    HUDManager hud;
    OnHurtHUDBorder hurtBorder;
    HighscoreBoard highScoreBoard;
    GameManager gameManager;

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

    void Start()
    {
        gameManager = GameManager.instance;
    }
    #endregion

    #region Public
    public void GameOver(int score)
    {
        highScoreBoard.DisplayHighscore(score);
    }

    public void ToMainMenu()
    {
        gameManager.ToMainMenu();
    }

    public void UpdateLives(int lives)
    {
        hud.UpdateLivesDisplay(lives);
    }

    public void UpdateWave(int lives)
    {
        hud.UpdateWave(lives);
    }

    public void UpdateMoney(int amount)
    {
        hud.UpdateMoneyCount(amount);
    }

    public void OnDamaged()
    {
        //When hurt, set red borders to visible
        hurtBorder.FlashRed();
    }

    public void EnterSpawningMode_Tower1() => EnterSpawningMode(TowerPlacementModes.Tower1);
    public void EnterSpawningMode_Tower2() => EnterSpawningMode(TowerPlacementModes.Tower2);

    public void ExitSpawningMode()
    {
        //To exit spawning mode, we tell the spawn button manager to hide all borders 
        hud.EnterPlacementMode(TowerPlacementModes.None);
        gameManager.SetTowerPlacementMode(TowerPlacementModes.None);
    }
    #endregion

    void EnterSpawningMode(TowerPlacementModes mode)
    {
        if (GameManager.PlacementMode != mode)
        {
            //Entering a spawning mode for spawning towers
            Debug.Log("EnterSpawningMode :" + mode);
            hud.EnterPlacementMode(mode);
            gameManager.SetTowerPlacementMode(mode);
        }
    }
}