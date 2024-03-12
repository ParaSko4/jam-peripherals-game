using Cysharp.Threading.Tasks;
using DG.Tweening;
using Jam.VirtualKeyboard;
using Jam.VirtualKeyboard.Keys;
using System;
using UnityEngine;

namespace Jam.UI.VirtualKeyboard
{
    public class VirtualKeyButtonPresenter : MonoBehaviour
    {
        public event Action<VirtualKeyboardKey> ButtonAchivedPlane;
        public event Action<VirtualKeyButtonPresenter> KeyBeginDrag;
        public event Action<VirtualKeyButtonPresenter> KeyEndDrag;

        [SerializeField] private VirtualKeyboardKey key;
        [SerializeField] private float showDuration;
        [Space]
        [SerializeField] private GameObject buttonContainer;
        [SerializeField] private DraggableVirtualKey draggableVirtualKey;

        private Tween showTween;

        private Vector3 defaultButtonScale;

        public VirtualKeyboardKey Key => key;

        private void Awake()
        {
            Setup();
            Subscribe();
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }

        public void ResetKey()
        {
            buttonContainer.gameObject.SetActive(true);
            StopShowAnimation();
            draggableVirtualKey.ResetKey();

            buttonContainer.transform.localScale = defaultButtonScale;
        }

        public void ActivateDragAfterReturningFromPlane()
        {
            buttonContainer.gameObject.SetActive(true);

            ShowButtonAnimation();

            draggableVirtualKey.StartCustomDragAfterReturningFromPlane();
        }

        private void Setup()
        {
            defaultButtonScale = buttonContainer.transform.localScale;
        }

        private void Subscribe()
        {
            draggableVirtualKey.DragEnd += OnDragEnd;
            draggableVirtualKey.DragBegin += OnDragBegin;
            draggableVirtualKey.KeyAchivePlane += OnPlaneAchivedInDrag;
        }

        private void Unsubscribe()
        {
            draggableVirtualKey.DragEnd -= OnDragEnd;
            draggableVirtualKey.DragBegin -= OnDragBegin;
            draggableVirtualKey.KeyAchivePlane -= OnPlaneAchivedInDrag;
        }

        private void OnDragBegin()
        {
            KeyBeginDrag?.Invoke(this);
        }

        private void OnDragEnd()
        {
            KeyEndDrag?.Invoke(this);
        }

        private async void OnPlaneAchivedInDrag()
        {
            ButtonAchivedPlane?.Invoke(key);

            Debug.Log($"[{typeof(DraggableVirtualKey)} - {key}]: Plane achived");

            await HideButtonAnimationAsync();

            buttonContainer.gameObject.SetActive(false);
        }

        private void ShowButtonAnimation()
        {
            StopShowAnimation();

            showTween = buttonContainer.transform.DOScale(defaultButtonScale, showDuration);
        }

        private async UniTask HideButtonAnimationAsync()
        {
            StopShowAnimation();

            showTween = buttonContainer.transform.DOScale(Vector3.zero, showDuration);

            await showTween.AsyncWaitForCompletion();
        }

        private void StopShowAnimation()
        {
            if (showTween.IsActive())
            {
                showTween.Kill();
            }
        }
    }
}