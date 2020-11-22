using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SocialPlatforms.Impl;

[RequireComponent(typeof(ButtonBorderDisplayer))]
public class HUDManager : MonoBehaviour
{
    //Exposed variables
    [SerializeField] GameObject HUDGroup;
    [SerializeField] Text money;
    [SerializeField] Text wave;
    [SerializeField] Text descriptionText;
    [SerializeField] GameObject[] livesHUDIcons;

    ButtonBorderDisplayer borderDisplyer;

    void Awake()
    {
        //Reference
        borderDisplyer = GetComponent<ButtonBorderDisplayer>();
    }

    #region Public
    public void UpdateMoneyCount(int amount)
    {
        //Update the text that needs to displayed
        money.text = amount.ToString();
    }

    public void UpdateWave(int value)
    {
        //Update wave text
        wave.text = value.ToString();
    }

    public void UpdateLivesDisplay(int lives)
    {
        //Reveal heart icons corresponding the lives left
        for (int i = 0; i < livesHUDIcons.Length; i++)
        {
            livesHUDIcons[i].SetActive(lives > i ? true : false);
        }
    }

    public void DisplayDebugText(string text)
    {
        //Erase debug text's text box
        descriptionText.text = text;
    }

    public void EnterPlacementMode(TowerTypes mode)
    {
        //Display a certain border
        borderDisplyer.EnterPlacementMode(mode);
        DisplayDebugText("Click on a platform to spawn tower!");
    }

    public void ExitPlacementMode ()
    {
        //Hide current border
        borderDisplyer.ExitPlacementMode();
    }

    public void HideDebugText ()
    {
        DisplayDebugText("");
    }
    #endregion
}