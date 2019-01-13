using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Reuseable class for movement with custom acceleration, gravity, friction & ground detection.

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
     // Public:
    public bool IsGrounded { get { return isGrounded; } }
    public Vector2 CurrentVelocity { get { return velocityVector; } }

    // Private:

    // These variables will be adjusted through a parent class.
    private float maxSpeed;
    private float minSpeed;
    private float friction;
    private Vector2 direction;

    // Private values which we'll update the object's velocity with.
    private Vector2 velocityVector;
    private Vector2 accelerationVector;
    private Vector2 frictionVector;
    private Vector2 gravityVector;

    // Values which define the confines of our ground detection.
    [SerializeField] private float groundCheckDist;
    [SerializeField] private LayerMask groundLayer;
    private RaycastHit2D hit;
    private bool isGrounded = false;


    #region References.
    private Rigidbody2D rb;
    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        GroundCheck();

        Accelerate();
        UpdateVelocity();
    }

    /// <summary>
    /// Calculate and add acceleration to the velocity based on the acceleration vector.
    /// </summary>
    private void Accelerate()
    {
        velocityVector += accelerationVector * Time.fixedDeltaTime;

        if (Mathf.Abs(velocityVector.x) > maxSpeed)
            velocityVector.x = direction.x * maxSpeed;
    }

    /// <summary>
    /// A simple ground check which utilizes a raycast.
    /// </summary>
    private void GroundCheck()
    {
        hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDist, groundLayer);

        if (hit)
            isGrounded = true;
        else
            isGrounded = false;
    }

    /// <summary>
    /// Update the rigidbody's velocity with our custom values.
    /// </summary>
    private void UpdateVelocity()
    {
        frictionVector = -direction * friction;

        //velocityVector += frictionVector * Time.fixedDeltaTime;

        //if (!isGrounded)
        //    velocityVector += gravityVector * Time.fixedDeltaTime;


        //if (Mathf.Abs(velocityVector.x) < (minSpeed))
        //    velocityVector.x = direction.x * minSpeed;

        rb.AddForce(accelerationVector + frictionVector, ForceMode2D.Force);
    }

    /// <summary>
    /// Define a max speed for this object.
    /// </summary>
    /// <param name="maxSpeed"></param>
    public void SetMaxSpeed(float maxSpeed)
    {
        this.maxSpeed = maxSpeed;
    }

    /// <summary>
    /// Define an initial velocity and a direction for this object.
    /// </summary>
    /// <param name="initialSpeed"></param>
    /// <param name="direction"></param>
    public void SetInitialVelocity(float initialSpeed, Vector2 direction)
    {
        this.minSpeed = initialSpeed;
        this.direction = direction;

        rb.velocity = direction * initialSpeed;
    }

    /// <summary>
    /// Define a friction constant for this object.
    /// </summary>
    /// <param name="friction"></param>
    public void SetFriction(float friction)
    {
        this.friction = friction;
    }
    
    /// <summary>
    /// Define gravity for this object.
    /// </summary>
    /// <param name="gravity"></param>
    public void SetGravity(Vector2 gravity)
    {
        this.gravityVector = gravity;
    }

    /// <summary>
    /// Change/add acceleration to this object. 
    /// </summary>
    /// <param name="toggle">Responsible for toggling acceleration.</param>
    /// <param name="acceleration"></param>
    public void AddAcceleration(int toggle, float acceleration)
    {
        accelerationVector = direction * acceleration * toggle;
    }

    /// <summary>
    /// Add a force in terms of a velocity change to this object.
    /// </summary>
    /// <param name="force"></param>
    public void Addforce(Vector2 force)
    {
        if (force.y != 0)
            velocityVector.y = 0;

        velocityVector += force;
    }
}
