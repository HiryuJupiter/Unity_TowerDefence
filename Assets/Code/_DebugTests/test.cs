using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetFacing(Quaternion.LookRotation(Vector3.left, Vector3.up));

            Debug.Log("1 :");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("2 :");
            SetFacing(Quaternion.LookRotation(Vector3.right, Vector3.up));
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("3 :");
            SetFacing(Quaternion.LookRotation(Vector3.up, Vector3.up));
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Debug.Log("4 :");
            SetFacing(Quaternion.LookRotation(Vector3.down, Vector3.up));
        }
    }

    public void SetFacing(Quaternion facing)
    {
        transform.localRotation = facing;
    }
}
