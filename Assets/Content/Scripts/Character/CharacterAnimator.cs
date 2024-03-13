using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Content.Scripts.Character
{
    public class CharacterAnimator : MonoBehaviour
    {
        private const string JUMP_START_STATE = "JumpStart";
        private const string JUMP_END_STATE = "JumpEnd";

        private event Action _jumpPushAnimationEvent;
        private event Action _jumpHighestPointAnimationEvent;
        
        [SerializeField] private Animator _animator;
        [SerializeField] private MovementComponent _movementComponent;
        private readonly int _speedHash = Animator.StringToHash("Speed");

        private void Update()
        {
            _animator.SetFloat(_speedHash, GroundedMagnitude(_movementComponent.CharacterController.velocity));
        }
        
        private float GroundedMagnitude(Vector3 original)
        {
            return Mathf.Sqrt(original.x * original.x + original.z * original.z);
        }

        private void OnLand()
        {
            _animator.CrossFadeInFixedTime(JUMP_END_STATE, 0.1f);
        }
        
        public void StartJumpAnimation(Action onJumped, Func<UniTask<bool>> landingCheck)
        {
            _animator.CrossFadeInFixedTime(JUMP_START_STATE, 0.3f);

            void OnJumped()
            {
                onJumped?.Invoke();
                _jumpPushAnimationEvent -= OnJumped;
            }

            async void OnHighestPoint()
            {
                var result = await UniTask.FromResult(await landingCheck.Invoke());

                if (result)
                {
                    OnLand();
                }
                _jumpHighestPointAnimationEvent -= OnHighestPoint;
            }

            _jumpPushAnimationEvent += OnJumped;
            _jumpHighestPointAnimationEvent += OnHighestPoint;
        }
        
#region AnimationEvents

        public void OnJumpHighestPoint()
        {
            _jumpHighestPointAnimationEvent?.Invoke();
        }

        public void OnJumpPush()
        {
            _jumpPushAnimationEvent?.Invoke();
        }
        
#endregion
        
        
        private void OnValidate()
        {
            _animator ??= GetComponent<Animator>();
        }
    }
}
