using Jam.VirtualKeyboard.Keys;
using System.Collections.Generic;
using Zenject;

namespace Jam.VirtualKeyboard
{
    public class VirtualKeyboardExecuter
    {
        private Dictionary<VirtualKeyboardKeys, VirtualKey> virtualKeys = new();

        [Inject]
        public VirtualKeyboardExecuter(KeyFactory keyFactory)
        {
            virtualKeys = new()
            {
                [VirtualKeyboardKeys.UpArrow] = keyFactory.CreateKey<UpArrowKey>(),
                [VirtualKeyboardKeys.DownArrow] = keyFactory.CreateKey<DownArrowKey>(),
                [VirtualKeyboardKeys.LeftArrow] = keyFactory.CreateKey<LeftArrowKey>(),
                [VirtualKeyboardKeys.RightArrow] = keyFactory.CreateKey<RightArrowKey>(),
                [VirtualKeyboardKeys.CapsLock] = keyFactory.CreateKey<CapsLockKey>(),
                [VirtualKeyboardKeys.Backspace] = keyFactory.CreateKey<BackspaceKey>(),
            };
        }

        public void ExecuteKeyboardKey(VirtualKeyboardKeys virtualKeyboardKey)
        {
            virtualKeys[virtualKeyboardKey].ExecuteKeyboardAction();
        }

        public void ExecuteBoardKey(VirtualKeyboardKeys virtualKeyboardKey)
        {
            virtualKeys[virtualKeyboardKey].ExecuteBoardAction();
        }

        public VirtualKey GetKey(VirtualKeyboardKeys virtualKeyboardKey)
        {
            if (virtualKeys.ContainsKey(virtualKeyboardKey))
            {
                return virtualKeys[virtualKeyboardKey];
            }

            return null;
        }
    }
}