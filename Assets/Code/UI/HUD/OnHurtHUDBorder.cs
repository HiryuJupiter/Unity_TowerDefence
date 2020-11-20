using UnityEngine;
using System.Collections;

public class OnHurtHUDBorder : MonoBehaviour
{
    [SerializeField] CanvasGroup onHurtRedFlash;

    void Awake()
    {
        onHurtRedFlash.alpha = 0f;
    }

    void Update()
    {
        //Fade out the red-flashing effect when player is hurt.
        if (onHurtRedFlash.alpha > 0f)
        {
            onHurtRedFlash.alpha -= Time.deltaTime * 5f;
        }
    }

    public void FlashRed()
    {
        //When hurt, set red borders to visible
        onHurtRedFlash.alpha = 1f;
    }
}