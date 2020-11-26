using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour
{
    //Variables
    public bool HasTower { get; private set;}

    private Dummy tower;
    private Material material;
    private Color defaultColor;

    private void Awake()
    {
        //Reference
        material = GetComponent<Renderer>().material;
        defaultColor = material.color;
    }

    public void PlaceTower (Dummy tower)
    {
        //When a tower is placed, set the platform to grey color
        this.tower = tower;
        material.color = Color.grey;
    }

    public void SellTower ()
    {
        //When a tower is sold, return the platforms color
        tower = null;
        material.color = defaultColor;
    }
}