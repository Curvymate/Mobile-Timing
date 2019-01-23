using UnityEngine;

public class SObject : Entity
{
    protected Movement _targetMovement;
    protected IColor _targetColorProfile;

    public virtual void Init(Movement target, IColor targetColorProfile)
    {
        this._targetMovement = target;
        this._targetColorProfile = targetColorProfile;
    }
}
