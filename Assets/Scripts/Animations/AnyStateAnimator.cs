using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces;
using UnityEngine;

namespace Animations
{
    // TODO: улучшить архитектуру вызова анимаций
    public class AnyStateAnimator : MonoBehaviour
    {
        public event Action OnAnimationFinished; 

        private IAnimated _animated;
        private Animator _animator;

        private List<string> _animationTransitionTags;

        private string _currentAnimation;

        public void Initialize(IAnimated animated)
        {
            _animated = animated;
            _animator = animated.Animator;
            InitializeAnimations(animated.AnimationTransitionTags);
        }

        private void InitializeAnimations(List<string> animationTransitionTags)
        {
            foreach (var animationTransitionTag in animationTransitionTags)
            {
                if (!_animationTransitionTags.Contains(animationTransitionTag))
                {
                    _animationTransitionTags.Add(animationTransitionTag);
                }
            }
        }


        public void Awake()
        {
            _animationTransitionTags = new List<string>();
        }


        public void PlayAnimation(string transitionTag)
        {
            if (_animationTransitionTags.Contains(transitionTag))
            {
                if (_currentAnimation != null)
                    _animator.SetBool(_currentAnimation, false);
    
                _animator.SetBool(transitionTag, true);
                _currentAnimation = transitionTag;

            }
        }

        private void OnAnimationFinishedTrigger()
        {
            OnAnimationFinished?.Invoke();
        }
    }
}