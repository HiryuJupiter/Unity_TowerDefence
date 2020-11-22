using UnityEngine;
using System.Collections;

public class TowerGhost : MonoBehaviour
{
    Renderer renderer;
    Material material;

    void Awake()
    {
        renderer = GetComponentInChildren<Renderer>();
        material = renderer.material;
    }

    public void SetVisibility (bool isVisible) => renderer.enabled = isVisible;
    public void SetColor (Color color) => material.color = color;
    public void SetPosition (Vector3 position) => transform.position = position;
}