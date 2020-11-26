using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class NumericUtil
{
    public static int SignAllowingZero (float value)
    {
        if (value > 0.1f)
            return 1;
        else if (value < -0.1f)
            return -1;
        else
            return 0;
    }


    public static float GetMaximumValueAboveZero(List<int> valueList)
    {
        //Btw, If you just want the max value then use Linq.valueList.Max();
        float max = 0f;
        foreach (int value in valueList)
        {
            if (value > max)
                max = value;
        }
        max *= 1.2f;
        return max;
    }

    public static float GetMinimumValue(List<int> valueList)
    {
        float min = Mathf.Infinity;
        foreach (int value in valueList)
        {
            if (value < min)
                min = value;
        }
        min = Mathf.Clamp(min * 0.8f, 0, min);
        return min;
    }
}