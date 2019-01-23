using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public static Player instance;

    // Public:
    public Color color { get { return _color; } }
    public SpeedingPlatform currentSpeedingPlatform { get { return _currentSpeedingPlatform; } set { _currentSpeedingPlatform = value; } }

    // Private:
    // Color Profile.
    private Color _color;

    // Speeding.
    private SpeedingPlatform _currentSpeedingPlatform;
    private bool _speed = false;

    // Grappeling.
    [Header("Grappel")]
    private Hook _currentHook;
    [SerializeField] private float _grappelForce;

    #region References.
    public Movement movement { get { return _movement; } }
    private Movement _movement;
    #endregion

    private new void Awake()
    {
        instance = this;

        _movement = GetComponent<Movement>();
    }

    private void Start()
    {
        Init();
    }

    private new void Update()
    {
        base.Update();
        Accelerate();

        if (Input.GetKeyDown(KeyCode.Space))
            Grappel();
    }

    private void Init()
    {
        _movement.ChangeVelocity(Vector2.right * _movement.MinSpeed);
    }

    private void Accelerate()
    {
        if (_currentSpeedingPlatform == null)
        {
            _speed = false;
        }

        if (_speed)
            _movement.ToggleAcceleration(true);
        else
            _movement.ToggleAcceleration(false);
    }

    private void Grappel()
    {
        if (_currentHook != null && !_movement.IsGrounded)
        {
            Vector2 hookDirection;
            hookDirection = (_currentHook.Position - position).normalized;

            Vector2 newVelocity;
            newVelocity.x = _movement.CurrentVelocity.x;
            newVelocity.y = 0;
            _movement.ChangeVelocity(newVelocity);

            _movement.AddForce(hookDirection * _grappelForce);

            _currentHook = null;
        }
    }

    public void ChangeHook(Hook hook)
    {
        _currentHook = hook;
    }

    public void ChangeColor(int color)
    {
        if (color > System.Enum.GetValues(typeof(Color)).Length - 1)
            color = 3;

        _color = (Color)color;

        if (_currentSpeedingPlatform != null)
        {
            if (_color == _currentSpeedingPlatform.currentColorProfile.color)
                _speed = true;
            else
            {
                Debug.Log("Player zapped ;(");
                _speed = false;
            }
        }
    }
}