using DG.Tweening;
using System.Collections.Generic;
using Content.Scripts.Mouse;
using UnityEngine;
using Zenject;

namespace Jam.VirtualKeyboard.Keys
{
    public class CapsLockKey : VirtualKey
    {
        private const float DURATION = 0.3f;
        private const float SCALE_PERCENTAGE = 0.3f;

        private Dictionary<GameObject, CapsLockData> changableObjects = new();
        private MousePointer _mousePointer;

        [Inject]
        public CapsLockKey(MousePointer mouseContext)
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

            CapsLockData capsLockData;

            if (changableObjects.ContainsKey(_mousePointer.SelectedEntity) == false)
            {
                capsLockData = changableObjects[_mousePointer.SelectedEntity] = new CapsLockData(_mousePointer.SelectedEntity);
            }
            else
            {
                capsLockData = changableObjects[_mousePointer.SelectedEntity];

                if (capsLockData.ScaleTween.IsActive())
                {
                    capsLockData.ScaleTween.Kill();
                }
            }

            var scale = capsLockData.ObjectInDefaultScaleState ?
                capsLockData.StartScale * (1f - SCALE_PERCENTAGE) : capsLockData.StartScale;

            var tween = _mousePointer.SelectedEntity.transform.DOScale(scale, DURATION);

            capsLockData.SetScaleTween(tween);
            capsLockData.ObjectInDefaultScaleState = !capsLockData.ObjectInDefaultScaleState;
        }
    }
}