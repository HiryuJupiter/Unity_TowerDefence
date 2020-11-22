using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonBorderDisplayer : MonoBehaviour
{
    //Variables
    [SerializeField] Image[] buttonBorders;

    int currentlyRevealed = -1;

    Dictionary<TowerTypes, int> modeToIndexLookUp;

    void Awake()
    {
        //Look up table
        modeToIndexLookUp = new Dictionary<TowerTypes, int>()
        {
            {TowerTypes.Tower1, 0 },
            {TowerTypes.Tower2, 1 },
        };

        //Hide all borders
        foreach (var b in buttonBorders)
        {
            b.enabled = false;
        }
    }

    #region Public
    public void EnterPlacementMode(TowerTypes mode)
    {
        //First hide currently revealed borders then revela the new one
        HideCurrentBorder();
        RevealButtonBorder(modeToIndexLookUp[mode]);
    }

    public void ExitPlacementMode ()
    {
        HideCurrentBorder();
    }

    #endregion
    void RevealButtonBorder(int buttonIndex)
    {
        //Hide the currently active border then reveal the new highlight border
        HideCurrentBorder();

        SetBorderVisibility(buttonIndex, true);
    }

    void HideCurrentBorder()
    {
        //If we're currently in a spawning state, then a border is active and we will now deactivate it. 
        if (currentlyRevealed != -1)
        {
            SetBorderVisibility(currentlyRevealed, false);
            currentlyRevealed = -1;
        }
    }

    void SetBorderVisibility(int index, bool isVisible)
    {
        //Set border's visibility 
        buttonBorders[index].enabled = isVisible;
        if (isVisible)
        {
            currentlyRevealed = index;
        }
    }
}