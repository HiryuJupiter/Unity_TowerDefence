using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    const int SceneIndex_MainMenu = 0;
    const int StartingHealth = 3; //Max health

    //Variables
    public static GameManager instance;    

    //References
    UIManager ui;

    //Properties
    public static GameStates gameState { get; private set; } = GameStates.Standby;
    public int lives { get; private set; } = StartingHealth;
    public int Money { get; private set; } 
    public int Wave { get; private set; }

    #region MonoBehavior

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        ui = UIManager.Instance;

        ui.UpdateWave(Wave);
        ui.UpdateLives(lives);
        ui.UpdateMoney(Money);
    }

    void Update()
    {
        //Debugs
        if (Input.GetKeyDown(KeyCode.H))
        {
            ReduceLife();
        }
        if (Input.GetKeyDown(KeyCode.M))
            AddMoney(100);
    }
    #endregion

    #region Public - stats change
    public void StartWave()
    {
        ui.UpdateWave(++Wave);
        gameState = GameStates.WaveStarted;
    }

    public void AddMoney(int value)
    {
        Money += value;
        ui.UpdateMoney(Money);
    }

    public void ReduceLife()
    {
        if (gameState == GameStates.WaveStarted)
        {
            ui.UpdateLives(lives);
            if (--lives <= 0)
            {
                GameOver();
            }
        }
    }
    #endregion

    #region Public - events
    public void WaveFinished ()
    {
        gameState = GameStates.Standby;
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(SceneIndex_MainMenu);
    }
    #endregion

    void GameOver ()
    {
        ui.GameOver(Wave);
        gameState = GameStates.GameOverScoreboard;
    }
}