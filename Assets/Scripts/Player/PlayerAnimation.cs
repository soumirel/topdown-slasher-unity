using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [Header("Component References")] 
    [SerializeField] private Animator _animator;
    
    private static readonly int X = Animator.StringToHash("X");
    private static readonly int Y = Animator.StringToHash("Y");
    private static readonly int Move = Animator.StringToHash("Move");
    private static readonly int Shot = Animator.StringToHash("Shot");

    public void UpdateMovementAnimation(Vector2 direction)
    {
        if (direction.sqrMagnitude != 0)
        {
            if (!_animator.GetBool(Move))
            {
                _animator.SetBool(Move, true);
            }
            _animator.SetFloat(X, direction.x);
            _animator.SetFloat(Y, direction.y);
        }
        else
        {
            _animator.SetBool(Move, false);
        }
    }


    public void PlayAttackAnimation()
    {
        _animator.SetTrigger(Shot);
    }
}
