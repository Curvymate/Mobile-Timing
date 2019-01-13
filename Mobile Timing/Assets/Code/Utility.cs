using UnityEngine;
using System;

[CreateAssetMenu]
public class Rule : ScriptableObject
{
    public ObjectType objectType;
    public InteractableObstacle platform;
    public Rule[] link;

    public Range spacing;
}

public enum ObjectType { PLATFORM, LAUNCHER, HOOK }

[Serializable]
public class Range
{
    public Vector2 min;
    public Vector2 max;
}