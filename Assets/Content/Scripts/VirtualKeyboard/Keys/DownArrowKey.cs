using Content.Scripts.Mouse;
using UnityEngine;
using Zenject;

namespace Jam.VirtualKeyboard.Keys
{
    public class DownArrowKey : VirtualKey
    {
        private const float SPEED = 3f;
        
        private MousePointer _mousePointer;

        [Inject]
        public DownArrowKey(MousePointer mousePointer)
        {
            _mousePointer = mousePointer;
        }

        public override void ExecuteBoardAction()
        {

        }

        public override void ExecuteKeyboardAction()
        {
            if(_mousePointer.SelectedEntityExist)
                _mousePointer.SelectedEntity.transform.AddX(Time.deltaTime * SPEED);
        }
    }
}