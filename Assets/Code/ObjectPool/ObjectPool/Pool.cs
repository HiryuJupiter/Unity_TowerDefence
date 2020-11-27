using UnityEngine;
using System.Collections.Generic;

public class Pool
{
    [SerializeField] private GameObject prefab;

    //Cache
    private Vector3 offscreen = new Vector3(-1000, -1000, -1000);

    //Pool lists
    private List<GameObject> inactives = new List<GameObject>();
    public List<GameObject> actives { get; private set; } = new List<GameObject>();

    //Constructor
    public Pool(GameObject prefab)
    {
        //Reference
        this.prefab = prefab;
    }



    public GameObject Spawn()
    {
        GameObject p;
        if (inactives.Count > 0)
        {
            //If object pool is not empty, then take an object from the pool and make it active
            p = inactives[0];
            p.SetActive(true);
            inactives.RemoveAt(0);
        }
        else
        {
            //If object pool is empty, then spawn a new object.
            p = GameObject.Instantiate(prefab, offscreen, Quaternion.identity);
            p.GetComponent<PoolObject>().SetPool(this);
            actives.Add(p);
        }
        return p;
    }

    public GameObject Spawn(Vector3 pos)
    {
        GameObject pf = Spawn();
        pf.GetComponent<PoolObject>().Respawned(pos);
        return pf;
    }

    public void Despawn(GameObject obj)
    {
        //Return to pool
        obj.transform.position = offscreen;
        inactives.Add(obj);
        actives.Remove(obj);
        obj.SetActive(false);
    }
}

/*
 
    //Note: please use OverlapCircleAll, which is more efficient, than the following
    //... methods that loops through 
    public Transform ClosestUnitToLocation(Vector3 center, float sqrRange)
    {
        Transform closest = null;
        float shortestDist = sqrRange;
        foreach (GameObject go in active)
        {
            if (Vector3.SqrMagnitude(center - go.transform.position) < shortestDist)
            {
                closest = go.transform;
                shortestDist = Vector3.SqrMagnitude(center - go.transform.position);
            }
        }
        return closest;
    }

    public List<Transform> AllUnitsInRange (Vector3 center, float sqrRange)
    {
        List<Transform> neighbors = new List<Transform>();
        foreach (GameObject go in active)
        {
            if (Vector3.SqrMagnitude(center - go.transform.position) < sqrRange)
            {
                neighbors.Add(go.transform);
            }
        }
        return neighbors;
    }
 */