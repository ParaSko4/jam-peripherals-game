using UnityEngine;
using Zenject;

namespace Jam.VirtualKeyboard
{
    public class VirtualKeyboardPresenter : MonoBehaviour
    {
  
        private VirtualKeyboardExecuter virtualKeyboardExecuter;

        [Inject]
        public void Construct( VirtualKeyboardExecuter virtualKeyboardExecuter)
        {
            this.virtualKeyboardExecuter = virtualKeyboardExecuter;
        }

        private void Update()
        {

            if (Input.GetKey(KeyCode.UpArrow))
            {
                virtualKeyboardExecuter.ExecuteKeyboardKey(VirtualKeyboardKeys.UpArrow);
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                virtualKeyboardExecuter.ExecuteKeyboardKey(VirtualKeyboardKeys.DownArrow);
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                virtualKeyboardExecuter.ExecuteKeyboardKey(VirtualKeyboardKeys.LeftArrow);
            }

            if (Input.GetKey(KeyCode.RightArrow))
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
    }
}