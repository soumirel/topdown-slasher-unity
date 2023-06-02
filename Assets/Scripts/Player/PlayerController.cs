using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    [Header("Sub Behaviours")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerAnimation playerAnimation;

    [Header("Input Settings")]
    public PlayerInput playerInput;
    [SerializeField] private float movementSmoothingSpeed = 1f;
    private Vector3 _rawInputMovement;
    private Vector3 _smoothInputMovement;
    
    private string _currentControlScheme = "";

    public void Start()
    {
        _currentControlScheme = playerInput.currentControlScheme;
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        Vector2 inputMovement = value.ReadValue<Vector2>();
        _rawInputMovement = new Vector3(inputMovement.x, inputMovement.y, 0);
    }

    public void OnAttack(InputAction.CallbackContext value)
    {
        if(value.started)
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
    
    //INPUT SYSTEM AUTOMATIC CALLBACKS --------------

    //This is automatically called from PlayerInput, when the input device has changed
    //(IE: Keyboard -> Xbox Controller)
    public void OnControlsChanged()
    {

        if(playerInput.currentControlScheme != _currentControlScheme)
        {
            _currentControlScheme = playerInput.currentControlScheme;
        }
    }

    //This is automatically called from PlayerInput, when the input device has been disconnected and can not be identified
    //IE: Device unplugged or has run out of batteries



    public void OnDeviceLost()
    {
        
    }


    public void OnDeviceRegained()
    {
        StartCoroutine(WaitForDeviceToBeRegained());
    }

    IEnumerator WaitForDeviceToBeRegained()
    {
        yield return new WaitForSeconds(0.1f);

    }
    
}