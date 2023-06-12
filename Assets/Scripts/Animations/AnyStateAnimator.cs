using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Animations
{
    // TODO: улучшить архитектуру вызова анимаций
    public class AnyStateAnimator : MonoBehaviour
    {
        private IAnimated _animated;
        private Animator _animator;

        private Dictionary<string, AnyStateAnimation> _anyStateAnimations;

        private AnyStateAnimation _currentAnimation;

        public void Initialize(IAnimated animated)
        {
            _animated = animated;
            _animator = animated.Animator;
            InitializeAnimations(animated.AnyStateAnimations);
        }

        private void InitializeAnimations(List<AnyStateAnimation> animations)
        {
            foreach (var anyStateAnimation in animations)
            {
                _anyStateAnimations.TryAdd
                (
                    anyStateAnimation.TransitionTag,
                    anyStateAnimation
                );
            }
        }


        public void Awake()
        {
            _anyStateAnimations = new Dictionary<string, AnyStateAnimation>();
        }


        public void PlayAnimation(string transitionTag)
        {
            if (_anyStateAnimations.TryGetValue(transitionTag, out var anyStateAnimation))
            {
                if (_currentAnimation != null && _currentAnimation.Priority > anyStateAnimation.Priority)
                    return;

                if (_currentAnimation != null)
                    _animator.SetBool(_currentAnimation.TransitionTag, false);
    
                _animator.SetBool(anyStateAnimation.TransitionTag, true);
                _currentAnimation = anyStateAnimation;

            }
        }
    }
}