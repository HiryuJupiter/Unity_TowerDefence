using UnityEngine;
using System.Collections;

public class OnHurtHUDBorder : MonoBehaviour
{
    [SerializeField] private CanvasGroup onHurtRedFlash;

    private void Awake()
    {
        onHurtRedFlash.alpha = 0f;
    }

    private void Update()
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