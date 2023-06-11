using System;
using Player;
using UnityEngine;

namespace ComponentSystem.Components
{
    public class VisualsCoreComponent : CoreComponent
    {
        public event Action<float> OnTurnStart;
        public event Action OnTurnFinish;
        
        private SpriteRenderer _spriteRenderer;
        private Animator _animator;
        private AnimationEventHandler _animationEventHandler;
        
        private int _activeAnimatorParam;

        private readonly int _turnAnimatorParam = Animator.StringToHash("turn");
        
        protected override void Awake()
        {
            base.Awake();
            _spriteRenderer = GetComponentInParent<SpriteRenderer>();
            _animator = GetComponentInParent<Animator>();
            _animationEventHandler = GetComponentInParent<AnimationEventHandler>();

            _animationEventHandler.OnTurnFinish += FinishTurn;
        }

        public override void Turn()
        {
            OnTurnStart?.Invoke(10f);
            SetAnimation(_turnAnimatorParam);
        }

        private void FinishTurn()
        {
            _spriteRenderer.flipX = !_spriteRenderer.flipX;
            OnTurnFinish?.Invoke();
        }
        
        public void SetAnimation(int hashedParam)
        {
            if (hashedParam == _activeAnimatorParam) return;
            
            _animator.SetBool(_activeAnimatorParam, false);
            _animator.SetBool(hashedParam, true);
            _activeAnimatorParam = hashedParam;
        }
    }
}