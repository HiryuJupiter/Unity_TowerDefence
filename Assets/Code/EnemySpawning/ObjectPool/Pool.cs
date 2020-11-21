using UnityEngine;
using System.Collections.Generic;

public class Pool
{
    Vector3 offscreen = new Vector3(-100, -100, -100);

    List<GameObject> active = new List<GameObject>();
    List<GameObject> inactive = new List<GameObject>();
    [SerializeField] GameObject prefab;

    //Constructor
    public Pool(GameObject prefab)
    {
        this.prefab = prefab;
    }

    public GameObject Spawn()
    {
        GameObject p;
        if (inactive.Count > 0)
        {
            //If object pool is not empty, then take an object from the pool and make it active
            p = inactive[0];
            p.SetActive(true);
            inactive.RemoveAt(0);
            
        }
        else
        {
            //If object pool is empty, then spawn a new object.
            p = GameObject.Instantiate(prefab, offscreen, Quaternion.identity);
            active.Add(p);
        }
        return p;
    }

    public void Despawn(GameObject obj)
    {
        inactive.Add(obj);
        active.Remove(obj);
        obj.SetActive(false);
    }
}