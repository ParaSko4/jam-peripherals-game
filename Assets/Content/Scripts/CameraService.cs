using UnityEngine;
using Zenject;

namespace Jam
{
    public class CameraService
    {
        private Camera camera;

        [Inject]
        public CameraService(Camera camera)
        {
            this.camera = camera;
        }
        
        public GameObject ThrowRaycastFromMousePosition(LayerMask mask = default)
        {
            var ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit, Mathf.Infinity, mask))
            {
                return hit.transform.gameObject;
            }

            return null;
        }
    }
}