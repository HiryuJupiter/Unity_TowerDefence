using UnityEngine;
using System.Collections;

public class DestroyAfter : MonoBehaviour
{
    [SerializeField] float WaitTime = 0.2f;
    IEnumerator Start()
    {
        //Destroy this gameobject after some time
        yield return new WaitForSeconds(WaitTime);
        Destroy(gameObject);
    }
}