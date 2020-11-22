using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    const int SceneIndex_MainMenu = 0;
    const int StartingHealth = 3; //Max health
    const int StartingMoney = 100; 

    //Variables
    public static GameManager Instance;

    //References
    [SerializeField] PauseMenu pauseMenu;
    UIManager ui;

    //Stats
    int wavesCompleted;
    int money = StartingMoney;
    int lives = StartingHealth;

    //Properties
    public static GameStates gameState { get; private set; } = GameStates.Standby;
    public static TowerPlacementModes PlacementMode { get; private set; } = TowerPlacementModes.None;

    #region MonoBehavior

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        ui = UIManager.Instance;

        ui.DisplayWave(0);
        ui.DisplayLives(lives);
        ui.DisplayMoney(money);
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

        if (PlacementMode == TowerPlacementModes.None && 
            (Input.GetKeyDown(KeyCode.Escape)))
        {
            pauseMenu.TogglePause();
        }
        else if (PlacementMode != TowerPlacementModes.None &&
            (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1)))
        {
            PlacementMode = TowerPlacementModes.None;
            ui.ExitSpawningMode();
        }
    }
    #endregion

    #region Public - stats change
    public void StartWave(int currentWave)
    {
        ui.DisplayWave(currentWave);
        wavesCompleted = currentWave - 1;
        gameState = GameStates.WaveStarted;
    }

    public void AddMoney(int value)
    {
        money += value;
        ui.DisplayMoney(money);
    }

    public void ReduceLife()
    {
        if (gameState == GameStates.WaveStarted)
        {
            ui.DisplayLives(lives);
            ui.OnDamaged();
            if (--lives <= 0)
            {
                GameOver();
            }
        }
    }
    #endregion

    #region Public - events
    public void WaveFinished()
    {
        gameState = GameStates.Standby;
    }

    public void SetTowerPlacementMode(TowerPlacementModes mode)
    {
        PlacementMode = mode;
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(SceneIndex_MainMenu);
    }
    #endregion

    void GameOver()
    {
        ui.GameOver(wavesCompleted);
        gameState = GameStates.GameOverScoreboard;
    }
}