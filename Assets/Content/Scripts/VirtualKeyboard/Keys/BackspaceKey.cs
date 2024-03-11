using Jam.Mouse;
using UnityEngine;
using Zenject;

namespace Jam.VirtualKeyboard.Keys
{
    public class BackspaceKey : VirtualKey
    {
        private MouseContext mouseContext;

        [Inject]
        public BackspaceKey(MouseContext mouseContext)
        {
            this.mouseContext = mouseContext;
        }

        public override void ExecuteBoardAction()
        {

        }

        public override void ExecuteKeyboardAction()
        {
            if (mouseContext.SelectedEntityExist == false)
            {
                return;
            }

            GameObject.Destroy(mouseContext.SelectedEntity);

            mouseContext.DeselectEntity();
        }
    }
}