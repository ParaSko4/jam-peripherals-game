using Jam.VirtualKeyboard.Keys;
using System.Collections.Generic;
using Zenject;

namespace Jam.VirtualKeyboard
{
    public class VirtualKeyboardExecuter
    {
        private Dictionary<VirtualKeyboardKey, VirtualKey> virtualKeys = new();

        [Inject]
        public VirtualKeyboardExecuter(KeyFactory keyFactory)
        {
            virtualKeys = new()
            {
                [VirtualKeyboardKey.UpArrow] = keyFactory.CreateKey<UpArrowKey>(),
                [VirtualKeyboardKey.DownArrow] = keyFactory.CreateKey<DownArrowKey>(),
                [VirtualKeyboardKey.LeftArrow] = keyFactory.CreateKey<LeftArrowKey>(),
                [VirtualKeyboardKey.RightArrow] = keyFactory.CreateKey<RightArrowKey>(),
                [VirtualKeyboardKey.CapsLock] = keyFactory.CreateKey<CapsLockKey>(),
                [VirtualKeyboardKey.Backspace] = keyFactory.CreateKey<BackspaceKey>(),
            };
        }

        public void ExecuteKey(VirtualKeyboardKey virtualKeyboardKey)
        {
            virtualKeys[virtualKeyboardKey].Execute();
        }

        public VirtualKey GetKey(VirtualKeyboardKey virtualKeyboardKey)
        {
            if (virtualKeys.ContainsKey(virtualKeyboardKey))
            {
                return virtualKeys[virtualKeyboardKey];
            }

            return null;
        }

        public void ResetKeys()
        {
            foreach (var value in virtualKeys.Values)
            {
                value.IsKeyBindedToPlayer = true;
            }
        }
    }
}