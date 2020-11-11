using UnityEngine;

public static class CanvasGroupUtil
{
    public static void RevealCanvasGroup(CanvasGroup canvas)
    {
        //Reveal canvas and set it to interactable 
        canvas.alpha = 1f;
        canvas.interactable = true;
        canvas.blocksRaycasts = true;
    }

    public static void HideCanvasGroup(CanvasGroup canvas)
    {
        //Hide canvas and set it to uninteractable 
        canvas.alpha = 0f;
        canvas.interactable = false;
        canvas.blocksRaycasts = false;
    }
}