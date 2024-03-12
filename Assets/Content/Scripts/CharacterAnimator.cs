using UnityEngine;

namespace Content.Scripts
{
    public class CharacterAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private CharacterController _characterController;
        private readonly int _speedHash = Animator.StringToHash("Speed");
        

        private void Update()
        {
            _animator.SetFloat(_speedHash, _characterController.velocity.magnitude);
        }

        private void OnValidate()
        {
            _animator ??= GetComponent<Animator>();
        }
    }
}
