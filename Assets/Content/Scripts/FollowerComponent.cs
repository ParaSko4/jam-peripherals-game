using EasyButtons;
using UnityEngine;

namespace Content.Scripts
{
    public class FollowerComponent : MonoBehaviour
    {
        [SerializeField] private float _maxSpeed = 75f;
        [SerializeField] private float _stoppingDistance =1.5f;
        [SerializeField] private float _slowDownDistance = 4f;
        [SerializeField] private Rigidbody _selfRb;
        
        private Transform _currentTarget;
        private bool _readyToFollow;
        private float _currentSpeed;
        
        private void FixedUpdate()
        {
            if (_readyToFollow)
            {
                _selfRb.velocity = CalculateSlowedDownVelocity();
                RotateTowardsTarget();
            }
        }

        private Vector3 CalculateSlowedDownVelocity()
        {
            var dir = _currentTarget.position - transform.position;

            if (dir.magnitude<=_stoppingDistance)
            {
                _currentSpeed = 0f;
                return Vector3.zero;
            }
                
            _currentSpeed = Mathf.Lerp(0, _maxSpeed, dir.magnitude/_slowDownDistance);
            return dir.normalized * _currentSpeed * Time.fixedDeltaTime;
        }

        private void RotateTowardsTarget()
        {
            var dir = new Vector3(_currentTarget.position.x, transform.position.y, _currentTarget.position.z)  - transform.position;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir.normalized), Time.fixedDeltaTime * 10f);
        }
        
        [Button]
        public void SetTarget(Transform target)
        {
            _currentTarget = target;
            if (_currentTarget!=null)
            {
                _readyToFollow = true;
            }
        }

        [Button]
        public void ReleaseCurrentTarget()
        {
            _readyToFollow = false;
            _currentTarget = null;
            _currentSpeed = 0f;
        }
        
        private void OnValidate()
        {
            _selfRb = GetComponent<Rigidbody>();
            _selfRb ??= gameObject.AddComponent<Rigidbody>();
            _selfRb.drag = 2f;
            _selfRb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationZ |
                              RigidbodyConstraints.FreezeRotationX;
            _selfRb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        }
    }
}
