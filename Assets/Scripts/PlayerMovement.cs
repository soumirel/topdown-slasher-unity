using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Component References")] [SerializeField]
    private Rigidbody2D playerRigidbody;

    [Header("Movement Settings")] public float movementSpeed = 3f;

    private Vector3 _movementDirection;

    public void UpdateMovementData(Vector3 newMovementDirection)
    {
        _movementDirection = newMovementDirection;
    }

    void FixedUpdate()
    {
        MoveThePlayer();
    }

    void MoveThePlayer()
    {
        Vector3 movement = _movementDirection * (movementSpeed * Time.fixedDeltaTime);
        playerRigidbody.MovePosition(transform.position + movement);
    }
}