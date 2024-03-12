using Jam.Mouse;
using UnityEngine;
using Zenject;

namespace Jam.VirtualKeyboard
{
    public class VirtualKeyboardPresenter : MonoBehaviour
    {
        [SerializeField] private LayerMask entityLayer = default;

        private MouseContext mouseContext;
        private CameraService cameraService;
        private VirtualKeyboardExecuter virtualKeyboardExecuter;

        [Inject]
        public void Construct(MouseContext mouseContext, CameraService cameraService, VirtualKeyboardExecuter virtualKeyboardExecuter)
        {
            this.mouseContext = mouseContext;
            this.cameraService = cameraService;
            this.virtualKeyboardExecuter = virtualKeyboardExecuter;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var obj = cameraService.ThrowRaycastFromMousePosition(entityLayer);

                if (obj != null)
                {
                    mouseContext.SelectEntity(obj);
                }
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                virtualKeyboardExecuter.ExecuteKey(VirtualKeyboardKey.UpArrow);
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                virtualKeyboardExecuter.ExecuteKey(VirtualKeyboardKey.DownArrow);
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                virtualKeyboardExecuter.ExecuteKey(VirtualKeyboardKey.LeftArrow);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                virtualKeyboardExecuter.ExecuteKey(VirtualKeyboardKey.RightArrow);
            }

            if (Input.GetKeyDown(KeyCode.CapsLock))
            {
                virtualKeyboardExecuter.ExecuteKey(VirtualKeyboardKey.CapsLock);
            }

            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                virtualKeyboardExecuter.ExecuteKey(VirtualKeyboardKey.Backspace);
            }
        }
    }
}