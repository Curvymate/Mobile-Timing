using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public static Player instance;

    // Public:

    // Private:
    [Header("Movement")]
    [SerializeField] private float maxSpeed;
    [SerializeField] private float initialSpeed;
    [SerializeField] private Vector2 direction;

    [SerializeField] private float acceleration;
    [SerializeField] private float friction;
    [SerializeField] private Vector2 gravity;

    private int accelerationSwitch = 0;

    [Header("Grappel")]
    private InteractableObstacle currentHook;
    [SerializeField] private float grappelAcceleration;

    // References.
    public Movement Movement { get { return movement; } }
    private Movement movement;

    private void Awake()
    {
        instance = this;

        movement = GetComponent<Movement>();
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        base.EntityUpdate();

        if (Input.GetMouseButton(0))
            ToggleAcceleration(1);
        else
            ToggleAcceleration(0);

        if (Input.GetKeyDown(KeyCode.Space))
            Grappel();

        Accelerate();
    }

    public void Init()
    {
        movement.SetMaxSpeed(this.maxSpeed);
        movement.SetFriction(this.friction);
        movement.SetGravity(this.gravity);

        movement.SetInitialVelocity(this.initialSpeed, this.direction);
    }

    private void Accelerate()
    {
        movement.AddAcceleration(accelerationSwitch, this.acceleration);
    }

    public void ToggleAcceleration(int toggle)
    {
        accelerationSwitch = toggle;
    }

    private void Grappel()
    {
        if (currentHook != null)
        {
            //if (position.y > currentHook.Position.y || position.x > currentHook.Position.x)
            //{
            //    currentHook = null;
            //    return;
            //}

            Vector2 currentVelocity;
            Vector2 force;

            currentVelocity = movement.CurrentVelocity;

            force = (currentHook.Position - position).normalized;

            movement.Addforce(currentVelocity + (force * grappelAcceleration * Time.fixedDeltaTime));
        }
    }

    public void ChangeHook(InteractableObstacle hook)
    {
        currentHook = hook;
    }
}
