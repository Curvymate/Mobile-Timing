using UnityEngine;
using System;

public interface IColor
{
    Color color { get; }
}

[Serializable]
public struct PlatformColor
{
    public Color color;
    public Sprite sprite;
}

public enum Color { RED, GREEN, BLUE, YELLOW };