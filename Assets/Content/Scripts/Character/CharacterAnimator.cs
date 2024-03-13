using UnityEngine;

namespace Content.Scripts.Character
{
    public class CharacterAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private CharacterController _characterController;
        private readonly int _speedHash = Animator.StringToHash("Speed");
        

        private void Update()
        {
            _animator.SetFloat(_speedHash, GroundedMagnitude(_characterController.velocity));
        }

        private void OnValidate()
        {
            _animator ??= GetComponent<Animator>();
        }

        private float GroundedMagnitude(Vector3 original)
        {
            return Mathf.Sqrt(original.x * original.x + original.z * original.z);
        }
    }
}
