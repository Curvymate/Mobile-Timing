using UnityEngine;
using System;

[Serializable]
public class Range
{
    public Vector2 min;
    public Vector2 max;
}

public class Utility
{
    public static Color GetRandomColor()
    {
        Color ret = (Color)UnityEngine.Random.Range(0, Enum.GetValues(typeof(Color)).Length);

        return ret;
    }
}