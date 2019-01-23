using UnityEngine;

public class Launcher : SObject
{
    [SerializeField] private float verticalForce;
    private string _targetTag = "Player";

    private void Launch()
    {
        Vector2 newVelocity;
        newVelocity.x = _targetMovement.CurrentVelocity.x;
        newVelocity.y = 0;

        _targetMovement.ChangeVelocity(newVelocity);

        _targetMovement.AddForce(Vector2.up * verticalForce);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.CompareTo(_targetTag) == 0)
        {
            Player.instance.ChangeHook(Hook.GetClosestHook_Horizontal(position));
            Launch();
        }
    }
}