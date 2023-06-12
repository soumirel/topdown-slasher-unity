using System;
using UnityEngine;

namespace Player
{
    public class PlayerVisuals : MonoBehaviour
    {
        public static readonly int IDLE = Animator.StringToHash("idle");
        public static readonly int MOVE = Animator.StringToHash("walk");
        public static readonly int TURN = Animator.StringToHash("turn");
        public static readonly int TURN_SPEED_MULTIPLIER = Animator.StringToHash("turn_speed_multiplier");

        public event Action OnTurnFinish;

        private Player _player;

        private SpriteRenderer _spriteRenderer;
        private Animator _animator;

        private int _activeAnimatorParam;

        public float TurnAnimationSpeedMultiplier { get; set; } = 1;
        

        public void Initialize(Player player)
        {
            _player = player;
            _spriteRenderer = player.GetComponent<SpriteRenderer>();
            _animator = player.GetComponent<Animator>();
        }

        public void Flip()
        {
            _spriteRenderer.flipX = !_spriteRenderer.flipX;
            _player.IsTurning = false;
            //OnTurnFinish?.Invoke();
        }
        
        public void SetAnimation(int hashedParam)
        {
            if (hashedParam == _activeAnimatorParam) return;
            
            if (hashedParam != 0)
            {
                _animator.SetBool(_activeAnimatorParam, false);
            }
            
            _animator.SetBool(hashedParam, true);
            _activeAnimatorParam = hashedParam;
        }


        public void Turn()
        {
            SetAnimation(TURN);
            //var stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
            //TurnAnimationSpeedMultiplier = _player.TurnSpeedSeconds / stateInfo.length;
            _animator.SetFloat(TURN_SPEED_MULTIPLIER, TurnAnimationSpeedMultiplier);
        }
    }
}