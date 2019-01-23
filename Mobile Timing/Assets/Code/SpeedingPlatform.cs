using UnityEngine;

public class SpeedingPlatform : SObject
{
    [SerializeField] private PlatformColor[] _colorProfiles;

    public PlatformColor currentColorProfile { get { return _currentColorProfile; } }
    private PlatformColor _currentColorProfile;

    private bool _targetInContact = false;

    public override void Init(Movement target)
    {
        base.Init(target);

        _currentColorProfile = _colorProfiles[Random.Range(0, _colorProfiles.Length)];

        sr.sprite = _currentColorProfile.sprite;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.CompareTo("Player") == 0)
            Player.instance.currentSpeedingPlatform = this;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.CompareTo("Player") == 0)
            Player.instance.currentSpeedingPlatform = null;
    }
}