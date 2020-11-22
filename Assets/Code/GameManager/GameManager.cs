using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    const int SceneIndex_MainMenu = 0;
    const int StartingHealth = 3; //Max health
    const int StartingMoney = 100; 

    public static GameManager Instance;

    [SerializeField] PauseMenu pauseMenu;

    //References
    PlacementManager towerPlacer;
    UIRendererManager ui;

    //Stats
    int wavesCompleted;
    int money = StartingMoney;
    int lives = StartingHealth;

    //Properties
    public static GameStates gameState { get; private set; } = GameStates.Standby;

    bool IsInPlacementMode => towerPlacer.PlacementMode != PlacementModes.None;

    #region MonoBehavior
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        ui = UIRendererManager.Instance;
        towerPlacer = PlacementManager.Instance;

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

        if (!IsInPlacementMode && (Input.GetKeyDown(KeyCode.Escape)))
        {
            pauseMenu.TogglePause();
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