using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Math
{
    public static bool valueIsBetween(float value, float bMin, float bMax)
    {
        if (value > bMin && value < bMax)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
