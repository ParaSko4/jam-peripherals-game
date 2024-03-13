using System;
using System.Threading;
using UnityEngine;
using Cysharp.Threading;
using Cysharp.Threading.Tasks;

namespace Content.Scripts.Character
{
    public class MovementComponent : MonoBehaviour
    {
        [field: SerializeField] public CharacterController CharacterController { get; private set; }
        [SerializeField] private CharacterAnimator _characterAnimator;
        [Header("MovementSettings")]
        [SerializeField] private float _speed =5f;
        [SerializeField] private float _rotationSpeed = 360f;
        [Header("JumpSettings")]
        [SerializeField] private float _customGravity = -20f;
        [SerializeField] private float _jumpHeight = 5f;
        [SerializeField] private float _jumpDistanceFactor = 2f;

        private CancellationTokenSource _jumpCts;
        
        private Vector3 _currentVelocity;
        private Vector3 _currentInput;
        private bool _inJumpSequence;
        
        
        public void SetMovementInput()
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            _currentInput = new Vector3(x, 0f, z);
        }
        
        private void Update()
        {
            if (CharacterController.isGrounded && _inJumpSequence==false)
            { 
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Jump();
                    return;
                }
                
                SetMovementInput();
                _currentVelocity = new Vector3(_currentInput.x, 0f, _currentInput.z);
                Look();
            } 
            ApplyGravity();
            Move();
        }

        private void Move()
        {
            CharacterController.Move(_currentVelocity * Time.deltaTime * _speed);
        }
        
        private void Look()
        {
            if (_currentInput!=Vector3.zero)
            {
                var currentPos = transform.position;
                
                var dir = new Vector3(currentPos.x + _currentInput.x, currentPos.y,
                    currentPos.z + _currentInput.z);
                
                var rot = Quaternion.LookRotation(dir - currentPos, Vector3.up);
                
                transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * _rotationSpeed);
            }
        }
        
        private void ApplyGravity()
        {
            _currentVelocity.y += _customGravity * Time.deltaTime;
        }

        private void Jump()
        {
            _inJumpSequence = true;
            ResetVelocity();
            void SetJumpedVelocity()
            {
                _currentVelocity = transform.forward* _jumpDistanceFactor;
                _currentVelocity.y = _jumpHeight;
            }
            
            _characterAnimator.StartJumpAnimation(SetJumpedVelocity, CheckLanding);
        }

        private async UniTask<bool> CheckLanding()
        {
            _jumpCts = new();
            await UniTask.WaitUntil(() => CharacterController.isGrounded, cancellationToken:_jumpCts.Token);
            _inJumpSequence = false;
            return true;
        }

        private void OnValidate()
        {
            CharacterController ??= GetComponent<CharacterController>();
        }

        private void ResetVelocity()
        {
            _currentVelocity = Vector3.zero;
            _currentInput = Vector3.zero;
        }

        private void OnDisable()
        {
            _jumpCts?.Cancel();
        }
    }
}
