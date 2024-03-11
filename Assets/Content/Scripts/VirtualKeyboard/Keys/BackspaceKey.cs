using Content.Scripts.Mouse;
using UnityEngine;
using Zenject;

namespace Jam.VirtualKeyboard.Keys
{
    public class BackspaceKey : VirtualKey
    {
        private MousePointer _mousePointer;

        [Inject]
        public BackspaceKey(MousePointer mouseContext)
        {
            _mousePointer = mouseContext;
        }

        public override void ExecuteBoardAction()
        {

        }

        public override void ExecuteKeyboardAction()
        {
            if (_mousePointer.SelectedEntityExist == false)
            {
                return;
            }

            if (_mousePointer.SelectedEntityExist)
            {
                GameObject.Destroy(_mousePointer.SelectedEntity);
                _mousePointer.DeselectEntity();
            }
        }
    }
}