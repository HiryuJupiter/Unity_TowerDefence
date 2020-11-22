using UnityEngine;
using System.Collections;

public class TowerGhost : MonoBehaviour
{
    Renderer renderer;
    Material material;

    Color colorUnavaiable;
    Color colorReveal;

    void Awake()
    {
        renderer = GetComponentInChildren<Renderer>();
        material = renderer.material;

        colorUnavaiable = Color.red;
        colorUnavaiable.a = 0.2f;
        colorReveal = Color.green;
        colorReveal.a = 0.2f;
    }

    public void SetVisibility (bool isVisible) => renderer.enabled = isVisible;
    public void SetPlacementAvailability (bool canPlace) => material.color = canPlace ? colorReveal : colorUnavaiable;
    public void SetPosition (Vector3 position) => transform.position = position;
}