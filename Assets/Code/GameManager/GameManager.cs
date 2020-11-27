using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Consts
    private const int SceneIndex_MainMenu = 0;
    private const int StartingHealth = 3; //Max health
    private const int StartingMoney = 100; 

    //Static
    public static GameManager Instance;

    //Exposed variables
    [SerializeField] private PauseMenu pauseMenu;

    //References
    private PlacementManager towerPlacer;
    private UIRendererManager ui;

    //Stats
    private int wavesCompleted;
    private int money = StartingMoney;
    private int lives = StartingHealth;

    //Properties
    public static GameStates gameState { get; private set; } = GameStates.Standby;
    private bool IsInPlacementMode => towerPlacer.IsInPlacementMode;

    #region MonoBehavior
    private void Awake()
    {
        //Lazy singleton
        Instance = this;
    }

    private void Start()
    {
        //Reference
        ui = UIRendererManager.Instance;
        towerPlacer = PlacementManager.Instance;

        //Initialize UI display for HUD items
        ui.DisplayWave(0);
        ui.DisplayLives(lives);
        ui.DisplayMoney(money);
    }

    private void Update()
    {
        //Debugs
        if (Input.GetKeyDown(KeyCode.H))
        {
            ReduceLife();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            AddMoney(100);
        }

        //Toggle pause when player pressed Escape
        if (!IsInPlacementMode && (Input.GetKeyDown(KeyCode.Escape)))
        {
            pauseMenu.TogglePause();
        }
    }
    #endregion

    #region Public - stats change
    public void StartWave(int currentWave)
    {
        //Update UI wave count
        ui.DisplayWave(currentWave);
        wavesCompleted = currentWave - 1;
        gameState = GameStates.WaveStarted;
    }

    public void AddMoney(int value)
    {
        //Increment money
        money += value;
        ui.DisplayMoney(money);
    }

    public void ReduceLife()
    {
        //If game is still running, reduce life points.
        if (gameState == GameStates.WaveStarted)
        {
            --lives;
            ui.DisplayLives(lives);
            ui.OnDamaged();
            //Game is over when no more hp left.
            if (lives <= 0)
            {
                GameOver();
            }
        }
    }
    #endregion

    #region Public - events
    //Public methods for UI buttons
    public void WaveFinished()
    {
        gameState = GameStates.Standby;
    }

    public void Restart()
    {
        //Load current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ToMainMenu()
    {
        //Load main menu scene
        SceneManager.LoadScene(SceneIndex_MainMenu);
    }
    #endregion

    private void GameOver()
    {
        //Tell ui manager we're done and let it decide how to clean up the interface
        ui.GameOver(wavesCompleted);
        gameState = GameStates.GameOverScoreboard;
    }

    //void GameLost ()
    //{
    //    ui.GameLost(wavesCompleted);
    //}
}