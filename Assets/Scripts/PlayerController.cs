using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerAnimation playerAnimation;

    [SerializeField] private float movementSmoothingSpeed = 1f;
    private Vector3 _rawInputMovement;
    private Vector3 _smoothInputMovement;

    public void OnMovement(InputAction.CallbackContext value)
    {
        Vector2 inputMovement = value.ReadValue<Vector2>();
        _rawInputMovement = new Vector3(inputMovement.x, inputMovement.y, 0);
    }

    public void OnAttack(InputAction.CallbackContext value)
    {
        print("OnAttack" + " " + value.performed);
        if(value.performed)
        {
            playerAnimation.PlayAttackAnimation();
        }
    }

    public void Update()
    {
        CalculateMovementInputSmoothing();
        UpdatePlayerMovement();
        UpdatePlayerAnimation();
    }

    private void CalculateMovementInputSmoothing()
    {
        _smoothInputMovement =
            Vector3.Lerp(_smoothInputMovement, _rawInputMovement, Time.deltaTime * movementSmoothingSpeed);
    }

    private void UpdatePlayerMovement()
    {
        playerMovement.UpdateMovementData(_rawInputMovement);
    }

    private void UpdatePlayerAnimation()
    {
        playerAnimation.UpdateMovementAnimation(_rawInputMovement);
    }
    
}