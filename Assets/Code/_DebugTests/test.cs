using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int[] array = { 1, 2, 3, 4, 5 };
        int result = array.Aggregate((a, b) => b + a);
        // 1 + 2 = 3
        // 3 + 3 = 6
        // 6 + 4 = 10
        // 10 + 5 = 15
        Debug.Log(result);

        int[] array2= { 1, 2, 3, 4, 5 };
        int result2 = array.Aggregate((a, b) => a + b);
        // 1 + 2 = 3
        // 3 + 3 = 6
        // 6 + 4 = 10
        // 10 + 5 = 15
        Debug.Log(result2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
