using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces;
using UnityEngine;

namespace Animations
{
    public class AnyStateAnimator : MonoBehaviour
    {
        public event Action OnAnimationFinished; 

        private IAnimated _animated;
        private Animator _animator;
        
        private List<string> _animatorParams;

        private string _currentAnimation;

        public void Initialize(IAnimated animated)
        {
            _animated = animated;
            _animator = animated.Animator;
            InitializeParams();
        }


        private void InitializeParams()
        {
            _animatorParams = new List<string>();
            foreach (var parameter in _animator.parameters)
            {
                _animatorParams.Add(parameter.name);
            }
        }


        public void PlayAnimation(string stateName)
        {
            var stateHash = Animator.StringToHash(stateName);
            if (_animator.HasState(0, stateHash))
            {
                _animator.Play(stateHash);
            }
            else
            {
                Debug.LogWarning($"No {stateName} state in {gameObject.name} animator!");
            }
        }


        public void ChangeAnimationSpeed(string stateParam, float speedMultiplier)
        {
            if (_animatorParams.Contains(stateParam))
            {
                _animator.SetFloat(stateParam, speedMultiplier);
            }
            else
            {
                Debug.LogWarning($"No {stateParam} param in {gameObject.name} animator!");
            }
        }
        

        private void OnAnimationFinishedTrigger()
        {
            OnAnimationFinished?.Invoke();
        }
    }
}