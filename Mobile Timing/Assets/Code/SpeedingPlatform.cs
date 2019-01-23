using UnityEngine;

public class SpeedingPlatform : SObject
{
    [SerializeField] private PlatformColor[] _colorProfiles;
    private PlatformColor _currentColorProfile;

    private bool _targetInContact = false;

    public override void Init(Movement target, IColor targetColorProfile)
    {
        base.Init(target, targetColorProfile);

        _currentColorProfile = _colorProfiles[Random.Range(0, _colorProfiles.Length)];

        sr.sprite = _currentColorProfile.sprite;
    }

    private new void Update()
    {
        if (_targetInContact)
        {
            if (_targetColorProfile.color == _currentColorProfile.color)
                _targetMovement.ToggleAcceleration(true);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.CompareTo("Player") == 0)
        {
            _targetInContact = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag.CompareTo("Player") == 0)
        {
            _targetMovement.ToggleAcceleration(false);
            _targetInContact = false;
        }
    }
}