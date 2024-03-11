using Jam.Mouse;
using UnityEngine;
using Zenject;

namespace Jam.VirtualKeyboard.Keys
{
    public class LeftArrowKey : VirtualKey
    {
        private const float SPEED = -3f;

        private MouseContext mouseContext;

        [Inject]
        public LeftArrowKey(MouseContext mouseContext)
        {
            this.mouseContext = mouseContext;
        }

        public override void ExecuteBoardAction()
        {

        }

        public override void ExecuteKeyboardAction()
        {
            mouseContext.SelectedEntity?.transform.AddZ(Time.deltaTime * SPEED);
        }
    }
}