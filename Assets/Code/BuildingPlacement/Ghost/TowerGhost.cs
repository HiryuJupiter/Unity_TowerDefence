using UnityEngine;
using System.Collections;

public class TowerGhost : MonoBehaviour
{
    //Variables
    private Renderer renderer;
    private Material material;
    private Color colorUnavaiable;
    private Color colorReveal;

    private void Awake()
    {
        //Reference
        renderer = GetComponentInChildren<Renderer>();
        material = renderer.material;

        //Cache colors
        colorUnavaiable = Color.red;
        colorUnavaiable.a = 0.2f;
        colorReveal = Color.green;
        colorReveal.a = 0.2f;
    }

    //Expression body methods for self documenting code
    public void SetVisibility (bool isVisible) => renderer.enabled = isVisible;
    public void SetPlacementAvailability (bool canPlace) => material.color = canPlace ? colorReveal : colorUnavaiable;
    public void SetPosition (Vector3 position) => transform.position = position;
}