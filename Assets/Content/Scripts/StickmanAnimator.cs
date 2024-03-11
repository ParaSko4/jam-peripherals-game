using System;
using UnityEngine;


namespace Content.Scripts
{
    public class StickmanAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody _rb;
        private readonly int _speedHash = Animator.StringToHash("Speed");

        private void Start()
        {
            _rb.velocity = Vector3.zero;
        }

        private void Update()
        {
            _animator.SetFloat(_speedHash, _rb.velocity.magnitude);
        }

        private void OnValidate()
        {
            _animator ??= GetComponent<Animator>();
        }
    }
}
