using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
     // Public:
    public float MaxSpeed { get { return maxSpeed; } set { maxSpeed = value; } }
    public float MinSpeed { get { return minSpeed; } set { minSpeed = value; } }
    public float Acceleration { get { return acceleration; } set { acceleration = value; } }
    public float Friction { get { return friction; } set { friction = value; } }
    public Vector2 CurrentVelocity { get { return velocityVector; } }
    public bool IsGrounded { get { return isGrounded; } }

    // Private:
    [SerializeField] private float maxSpeed;
    [SerializeField] private float minSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float friction;
    private Vector2 velocityVector;
    private bool accelerationToggle = false;

    // Values which define the confines of our ground detection.
    [SerializeField] private float groundCheckDist;
    [SerializeField] private LayerMask groundLayer;
    private RaycastHit2D hit;
    private bool isGrounded = false;

    #region Buffering.
    private Vector2 bufferedForce;
    private bool buffer_AddForce = false;
    #endregion

    #region References.
    private Rigidbody2D rb;
    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        DetectGround();
        UpdateVelocity();
    }

    float test;

    private void UpdateVelocity()
    {
        velocityVector = rb.velocity;

        if (accelerationToggle && Mathf.Abs(velocityVector.x) < maxSpeed)
            velocityVector += Vector2.right * acceleration * Time.fixedDeltaTime;

        if (Mathf.Abs(velocityVector.x) > minSpeed)
            velocityVector += Vector2.right * (velocityVector.x / -Mathf.Abs(velocityVector.x)) * friction * Time.fixedDeltaTime;

        if (buffer_AddForce)
        {
            velocityVector += bufferedForce;
            buffer_AddForce = false;
        }

        rb.velocity = velocityVector;
    }

    private void DetectGround()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDist, groundLayer);      
    }

    public void ToggleAcceleration(bool toggle)
    {
        accelerationToggle = toggle;
    }

    public void ChangeVelocity(Vector2 velocity)
    {
        rb.velocity = velocity;
    }

    public void AddForce(Vector2 force)
    {
        bufferedForce = force;

        buffer_AddForce = true;
    }
}
