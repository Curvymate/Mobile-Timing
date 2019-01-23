using UnityEngine;
using System;

[Serializable]
public struct PlatformColor
{
    public Color color;
    public Sprite sprite;
}

public enum Color { RED, GREEN, BLUE, YELLOW };