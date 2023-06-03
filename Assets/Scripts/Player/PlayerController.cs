using System.Collections;
using Player.Input;
using Player.PlayerFiniteStateMachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private Animator _animator;
        [SerializeField] private PlayerInputHandler _inputHandler;
        [SerializeField] private PlayerStateMachine _stateMachine;
        [SerializeField] private AnimationEventHandler _animationEventHandler;
        
        public Rigidbody2D Rb => rb;
        public Animator Animator => _animator;
        public PlayerInputHandler InputHandler => _inputHandler;
        public AnimationEventHandler AnimationEventHandler => _animationEventHandler;

        public readonly int SightXParam = Animator.StringToHash("sightX");
        public readonly int SightYParam = Animator.StringToHash("sightY");
        public readonly int MoveParam = Animator.StringToHash("move");
        public readonly int IdleParam = Animator.StringToHash("idle");
        public readonly int AttackParam = Animator.StringToHash("attack");
        public readonly int HitParam = Animator.StringToHash("hit");
        public readonly int DeathParam = Animator.StringToHash("death");

        [SerializeField] private float _moveSpeed;
        public float MoveSpeed => _moveSpeed;
    }
}