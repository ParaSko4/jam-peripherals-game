using Jam.Mouse;
using UnityEngine;
using Zenject;

namespace Jam.VirtualKeyboard.Keys
{
    public class RightArrowKey : VirtualKey
    {
        private const float SPEED = 3f;

        private MouseContext mouseContext;

        [Inject]
        public RightArrowKey(MouseContext mouseContext)
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