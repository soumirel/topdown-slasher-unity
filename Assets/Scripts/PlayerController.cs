using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;

    [SerializeField] private float movementSmoothingSpeed = 1f;
    private Vector3 _rawInputMovement;
    private Vector3 _smoothInputMovement;

    public void OnMovement(InputAction.CallbackContext value)
    {
        Vector2 inputMovement = value.ReadValue<Vector2>();
        _rawInputMovement = new Vector3(inputMovement.x, inputMovement.y, 0);
    }

    void Update()
    {
        CalculateMovementInputSmoothing();
        UpdatePlayerMovement();
    }

    void CalculateMovementInputSmoothing()
    {
        _smoothInputMovement =
            Vector3.Lerp(_smoothInputMovement, _rawInputMovement, Time.deltaTime * movementSmoothingSpeed);
    }

    void UpdatePlayerMovement()
    {
        playerMovement.UpdateMovementData(_rawInputMovement);
    }
}