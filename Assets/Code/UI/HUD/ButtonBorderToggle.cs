using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonBorderToggle : MonoBehaviour
{
    //Variables
    [SerializeField] Image[] buttonBorders;

    int currentlyRevealed = -1;

    #region Public
    public void RevealButtonBorder(int buttonIndex)
    {
        //Hide the currently active border then reveal the new highlight border
        HideCurrentBorder();

        SetBorderVisibility(buttonIndex, true);
    }

    public void HideButtonBorder()
    {
        //Hide border then set the current mode to none
        HideCurrentBorder();
    }
    #endregion

    void HideCurrentBorder()
    {
        //If we're currently in a spawning state, then a border is active and we will now deactivate it. 
        if (currentlyRevealed != -1)
        {
            SetBorderVisibility(currentlyRevealed, false);
            currentlyRevealed = -1;
        }
    }

    void SetBorderVisibility (int index, bool isVisible)
    {
        buttonBorders[index].enabled = isVisible;
        if (isVisible)
        {
            currentlyRevealed = index;
        }
    }
}