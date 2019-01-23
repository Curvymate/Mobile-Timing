using UnityEngine;

[CreateAssetMenu]
public class Rule : ScriptableObject
{
    public ObjectType objectType;
    public SObject sObject;
    public Rule[] link;

    public Range spacing;
}

public enum ObjectType { PLATFORM, LAUNCHER, HOOK, ENEMY }