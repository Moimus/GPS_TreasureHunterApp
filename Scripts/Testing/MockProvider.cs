using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MockProvider
{
    /// <summary>
    /// censored for privacy reasons, insert desired coordinates
    /// </summary>
    /// <returns>List of mock targets</returns>
    [System.Obsolete]
    public static List<Vector2> getMockTargets()
    {
        List<Vector2> targets = new List<Vector2>()
        {
            new Vector2(0.0f, 0.0f),
            new Vector2(0.0f, 0.0f),
            new Vector2(0.0f, 0.0f)
        };

        return targets;
    }
}
