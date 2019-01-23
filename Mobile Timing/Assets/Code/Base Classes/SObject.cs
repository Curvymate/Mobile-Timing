using UnityEngine;

public class SObject : Entity
{
    protected Movement _targetMovement;

    public virtual void Init(Movement target)
    {
        this._targetMovement = target;
    }
}
