using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SocialPlatforms.Impl;

public class HUDManager : MonoBehaviour
{
    public static HUDManager instance;

    [SerializeField] GameObject HUDGroup;
    [SerializeField] Text money;
    [SerializeField] Text wave;
    [SerializeField] GameObject[] livesHUDIcons;
    void Awake()
    {
        instance = this;
    }

    #region Public
    public void UpdateMoneyCount (int amount)
    {
        money.text = amount.ToString();
    }

    public void UpdateWave(int value)
    {
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
    #endregion
}