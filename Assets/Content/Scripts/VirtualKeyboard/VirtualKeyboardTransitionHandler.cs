using Jam.UI.VirtualKeyboard;
using Jam.VirtualKeyboard.Keys;
using System;
using Zenject;

namespace Jam.VirtualKeyboard
{
    public class VirtualKeyboardTransitionHandler : IDisposable, IInitializable
    {
        private VirtualKeyboardExecuter executer;
        private VirtualKeyPlaneHolder planeKeyHolder;
        private VirtualKeyButtonHolder buttonKeyHolder;

        [Inject]
        public VirtualKeyboardTransitionHandler(VirtualKeyboardExecuter virtualKeyboardExecuter, 
            VirtualKeyPlaneHolder virtualKeyPlaneHolder,
            VirtualKeyButtonHolder virtualKeyButtonHolder)
        {
            executer = virtualKeyboardExecuter;
            planeKeyHolder = virtualKeyPlaneHolder;
            buttonKeyHolder = virtualKeyButtonHolder;
        }

        public void Initialize()
        {
            Subscribe();
        }

        public void Dispose()
        {
            Unsubscribe();
        }

        public void ResetKeys()
        {
            planeKeyHolder.ResetKeys();
            buttonKeyHolder.ResetKeys();

            executer.ResetKeys();
        }

        private void Subscribe()
        {
            buttonKeyHolder.DragBegin += OnButtonKeyBeginDrag;
            buttonKeyHolder.DragEnd += OnButtonKeyEndDrag;

            planeKeyHolder.DragBegin += OnPlaneKeyBeginDrag;
            planeKeyHolder.DragEnd += OnPlaneKeyEndDrag;
        }

        private void Unsubscribe()
        {
            buttonKeyHolder.DragBegin -= OnButtonKeyBeginDrag;
            buttonKeyHolder.DragEnd -= OnButtonKeyEndDrag;

            planeKeyHolder.DragBegin -= OnPlaneKeyBeginDrag;
            planeKeyHolder.DragEnd -= OnPlaneKeyEndDrag;
        }

        private void OnButtonKeyBeginDrag(VirtualKeyButtonPresenter buttonKey)
        {
            buttonKey.ButtonAchivedPlane += OnButtonAchivedPlane;
        }

        private void OnButtonKeyEndDrag(VirtualKeyButtonPresenter buttonKey)
        {
            buttonKey.ButtonAchivedPlane -= OnButtonAchivedPlane;
        }

        private void OnPlaneKeyBeginDrag(VirtualKeyPlanePresenter planeKey)
        {
            planeKey.KeyOutOfPlane += OnKeyOutOfPlane;
        }

        private void OnPlaneKeyEndDrag(VirtualKeyPlanePresenter planeKey)
        {
            planeKey.KeyOutOfPlane -= OnKeyOutOfPlane;
        }

        private void OnButtonAchivedPlane(VirtualKeyboardKey key)
        {
            var virtualKey = executer.GetKey(key);

            virtualKey.IsKeyBindedToPlayer = false;

            var planeKey = planeKeyHolder.GetKey(key);

            planeKey.Show();
            planeKey.KeyOutOfPlane += OnKeyOutOfPlane;
        }

        private void OnKeyOutOfPlane(VirtualKeyboardKey key)
        {
            var virtualKey = executer.GetKey(key);

            virtualKey.IsKeyBindedToPlayer = true;

            var planeKey = planeKeyHolder.GetKey(key);

            planeKey.Hide();
            planeKey.KeyOutOfPlane -= OnKeyOutOfPlane;

            var buttonKey = buttonKeyHolder.GetKeyButton(key);

            buttonKey.ActivateDragAfterReturningFromPlane();
        }
    }
}