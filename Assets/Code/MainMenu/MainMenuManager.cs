using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MainMenuManager : MonoBehaviour
{
    //Const
    const string Key_MusicVol = "Music";
    const string Key_Master = "Master";
    
    const int SceneIndex_GamePlay = 1;

    //Fields
    [SerializeField] AudioMixer mixer;
    [SerializeField] Text highScore;
    [SerializeField] CanvasGroup MainMenu;
    [SerializeField] CanvasGroup OptionsMenu;
    [SerializeField] CanvasGroup AboutMenu;

    //Cache
    int highscore;

    #region MonoBehavior
    private void Awake()
    {
        //Reveal main menu
        CanvasGroupUtil.HideCanvasGroup(OptionsMenu);
        CanvasGroupUtil.HideCanvasGroup(AboutMenu);
        CanvasGroupUtil.RevealCanvasGroup(MainMenu);

        //Update highscore display
        highscore = HighScore.LoadHighScore();
        string paddedString = highscore.ToString().PadLeft(6, '0'); 
        highScore.text = paddedString;
    }
    #endregion

    #region Public - UI button interactions
    public void StartGame ()
    {
        //Load the game
        SceneManager.LoadScene(SceneIndex_GamePlay);
    }

    public void OpenOptionsMenu ()
    {
        //Hide the main menu and reveal the options menu
        CanvasGroupUtil.HideCanvasGroup(MainMenu);
        CanvasGroupUtil.RevealCanvasGroup(OptionsMenu);
    }

    public void OpenAbout()
    {
        //Hide the main menu and reveal the options menu
        CanvasGroupUtil.HideCanvasGroup(MainMenu);
        CanvasGroupUtil.RevealCanvasGroup(AboutMenu);
    }

    public void OptionsBackToMain ()
    {
        //Hide the options menu and reveal the main menu
        CanvasGroupUtil.HideCanvasGroup(OptionsMenu);
        CanvasGroupUtil.RevealCanvasGroup(MainMenu);
    }

    public void AboutBackToMain()
    {
        //Hide the options menu and reveal the main menu
        CanvasGroupUtil.HideCanvasGroup(AboutMenu);
        CanvasGroupUtil.RevealCanvasGroup(MainMenu);
    }

    public void QuitGame()
    {
        //Quits the game
        Application.Quit();
    }

    public void SetMasterVolumn(float amount)
    {
        //Set channel volumn inside the audio mixer
        mixer.SetFloat(Key_Master, Mathf.Log10(amount) * 20); //Convert linear slider value to a logarithmic value
    }

    public void SetMusicVolumn(float amount)
    {
        //Set channel volumn inside the audio mixer
        mixer.SetFloat(Key_MusicVol, Mathf.Log10(amount) * 20);
    }
    #endregion
}