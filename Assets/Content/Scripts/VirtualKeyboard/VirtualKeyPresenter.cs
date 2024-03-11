using EasyButtons;
using Jam.Mouse;
using UnityEngine;
using Zenject;

namespace Jam.VirtualKeyboard
{
    public class VirtualKeyboardPresenter : MonoBehaviour
    {
        [SerializeField] private LayerMask layerMask = default;
        [SerializeField] private VirtualKeyboardKeys virtualKeyboardKey = default;

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
                var obj = cameraService.ThrowRaycastFromMousePosition(layerMask);

                if (obj != null)
                {
                    mouseContext.SelectEntity(obj);
                }
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                virtualKeyboardExecuter.ExecuteKeyboardKey(VirtualKeyboardKeys.UpArrow);
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                virtualKeyboardExecuter.ExecuteKeyboardKey(VirtualKeyboardKeys.DownArrow);
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                virtualKeyboardExecuter.ExecuteKeyboardKey(VirtualKeyboardKeys.LeftArrow);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                virtualKeyboardExecuter.ExecuteKeyboardKey(VirtualKeyboardKeys.RightArrow);
            }

            if (Input.GetKeyDown(KeyCode.CapsLock))
            {
                virtualKeyboardExecuter.ExecuteKeyboardKey(VirtualKeyboardKeys.CapsLock);
            }

            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                virtualKeyboardExecuter.ExecuteKeyboardKey(VirtualKeyboardKeys.Backspace);
            }
        }

        [Button]
        public void Execute()
        {
            virtualKeyboardExecuter.ExecuteKeyboardKey(virtualKeyboardKey);
        }
    }
}