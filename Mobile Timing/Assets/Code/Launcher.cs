using UnityEngine;

public class Launcher : InteractableObstacle
{
    private static string targetTag = "Player";

    [SerializeField] private float verticalForce;
    private Movement target;

    public InteractableObstacle ProceedingHook { get { return proceedingHook; } }
    private InteractableObstacle proceedingHook;

    private void Start()
    {
        target = Player.instance.Movement;
    }

    public void ChangeProceedingHook(InteractableObstacle hook)
    {
        proceedingHook = hook;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == targetTag)
        {
            Player.instance.ChangeHook(proceedingHook);
            target.Addforce(Vector2.up * verticalForce);
        }
    }
}