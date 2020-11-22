using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour
{
    public bool HasTower { get; private set;}
    Dummy tower;

    Material material;
    Color defaultColor;

    void Awake()
    {
        material = GetComponent<Renderer>().material;
        defaultColor = material.color;
    }


    public void PlaceTower (Dummy tower)
    {
        this.tower = tower;
        material.color = Color.grey;
    }

    public void SellTower ()
    {
        tower = null;
        material.color = defaultColor;
    }
}