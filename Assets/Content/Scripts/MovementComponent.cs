using System;
using EasyButtons;
using UnityEngine;

namespace Content.Scripts
{
    public class MovementComponent : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float _speed =5f;
        [SerializeField] private float _rotationSpeed = 360f;
        private Vector3 _currentInput;
        private Vector3 _prevInput;
        
        
        public void Move()
        {
            if (_currentInput!=Vector3.zero)
            {
                _characterController.Move(_currentInput * _speed *Time.deltaTime);
            }
        }
        
        private void Update()
        {
            SetMovementInput();
            Look();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Look()
        {
            if (_currentInput!=Vector3.zero)
            {
                var dir = new Vector3(transform.position.x + _currentInput.x, transform.position.y,
                    transform.position.z + _currentInput.z);
                
                var rot = Quaternion.LookRotation(dir - transform.position, Vector3.up);
                
                transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * _rotationSpeed);
            }
        }
        
        private void SetMovementInput()
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            _currentInput = new Vector3(x, 0f, z);
        }

        private void OnValidate()
        {
            _characterController ??= GetComponent<CharacterController>();
        }
    }
}
