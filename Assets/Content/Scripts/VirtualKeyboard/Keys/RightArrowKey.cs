using Content.Scripts.Mouse;
using UnityEngine;
using Zenject;

namespace Jam.VirtualKeyboard.Keys
{
    public class RightArrowKey : VirtualKey
    {
        private const float SPEED = 3f;

        private MousePointer _mousePointer;

        [Inject]
        public RightArrowKey(MousePointer mouseContext)
        {
            _mousePointer = mouseContext;
        }

        public override void ExecuteBoardAction()
        {

        }

        public override void ExecuteKeyboardAction()
        {
            if (_mousePointer.SelectedEntityExist)
                _mousePointer.SelectedEntity.transform.AddZ(Time.deltaTime * SPEED);
        }
    }
}