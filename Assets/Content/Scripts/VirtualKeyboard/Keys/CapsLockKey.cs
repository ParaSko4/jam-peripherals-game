using DG.Tweening;
using Jam.Mouse;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Jam.VirtualKeyboard.Keys
{
    public class CapsLockKey : VirtualKey
    {
        private const float DURATION = 0.3f;
        private const float SCALE_PERCENTAGE = 0.3f;

        private Dictionary<GameObject, CapsLockData> changableObjects = new();
        private MouseContext mouseContext;

        [Inject]
        public CapsLockKey(MouseContext mouseContext)
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

            CapsLockData capsLockData;

            if (changableObjects.ContainsKey(mouseContext.SelectedEntity) == false)
            {
                capsLockData = changableObjects[mouseContext.SelectedEntity] = new CapsLockData(mouseContext.SelectedEntity);
            }
            else
            {
                capsLockData = changableObjects[mouseContext.SelectedEntity];

                if (capsLockData.ScaleTween.IsActive())
                {
                    capsLockData.ScaleTween.Kill();
                }
            }

            var scale = capsLockData.ObjectInDefaultScaleState ?
                capsLockData.StartScale * (1f - SCALE_PERCENTAGE) : capsLockData.StartScale;

            var tween = mouseContext.SelectedEntity.transform.DOScale(scale, DURATION);

            capsLockData.SetScaleTween(tween);
            capsLockData.ObjectInDefaultScaleState = !capsLockData.ObjectInDefaultScaleState;
        }
    }
}