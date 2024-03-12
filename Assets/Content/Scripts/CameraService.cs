using UnityEngine;
using Zenject;

namespace Jam
{
    public class CameraService
    {
        private Camera camera;

        private RaycastHit lastHit;

        public RaycastHit LastHit => lastHit;

        [Inject]
        public CameraService(Camera camera)
        {
            this.camera = camera;
        }
        
        public GameObject ThrowRaycastFromMousePosition(LayerMask mask = default)
        {
            var ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out lastHit, Mathf.Infinity, mask))
            {
                return lastHit.transform.gameObject;
            }

            return null;
        }
    }
}