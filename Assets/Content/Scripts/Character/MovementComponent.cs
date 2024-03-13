using System;
using UnityEditor;
using UnityEngine;

namespace Content.Scripts.Character
{
    public class MovementComponent : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float _speed =5f;
        [SerializeField] private float _rotationSpeed = 360f;
        [SerializeField] private float _customGravity = -20f;
        [SerializeField] private float _jumpHeight = 5f;
        [SerializeField] private float _jumpDistanceFactor = 2f;
        
        private Vector3 _currentVelocity;
        private Vector3 _currentInput;
        
        
        public void SetMovementInput()
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            _currentInput = new Vector3(x, 0f, z);
        }
        
        private void Update()
        {
            SetMovementInput();
            
            if (_characterController.isGrounded)
            { 
                _currentVelocity = new Vector3(_currentInput.x, _currentVelocity.y, _currentInput.z);
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Jump();
                }
            }

            ApplyGravity();
            Move();
            Look();
        }

        private void Move()
        {
            _characterController.Move(_currentVelocity * Time.deltaTime);
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
            _currentVelocity *= _jumpDistanceFactor;
            _currentVelocity.y = _jumpHeight;
        }

        private void OnValidate()
        {
            _characterController ??= GetComponent<CharacterController>();
        }
    }
}
