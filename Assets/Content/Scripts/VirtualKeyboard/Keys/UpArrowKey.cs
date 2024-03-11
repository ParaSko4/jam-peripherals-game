using Jam.Mouse;
using UnityEngine;
using Zenject;

namespace Jam.VirtualKeyboard.Keys
{
    public class UpArrowKey : VirtualKey
    {
        private const float SPEED = -3f;

        private MouseContext mouseContext;

        [Inject]
        public UpArrowKey(MouseContext mouseContext)
        {
            this.mouseContext = mouseContext;
        }

        public override void ExecuteBoardAction()
        {

        }

        public override void ExecuteKeyboardAction()
        {
            mouseContext.SelectedEntity.transform.AddX(Time.deltaTime * SPEED);
        }
    }
}