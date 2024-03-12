using DG.Tweening;
using System;
using UnityEngine;
using Zenject;

namespace Jam.VirtualKeyboard.Keys
{
    public class VirtualKeyPlanePresenter : MonoBehaviour
    {
        public event Action<VirtualKeyboardKey> KeyOutOfPlane;
        public event Action<VirtualKeyPlanePresenter> KeyBeginDrag;
        public event Action<VirtualKeyPlanePresenter> KeyEndDrag;

        [SerializeField] private VirtualKeyboardKey key;
        [SerializeField] private LayerMask planeLayer;
        [SerializeField] private float showDuration;
        [Space]
        [SerializeField] private GameObject keyObject;

        private Tween showTween;
        private CameraService cameraService;

        private Vector3 defaultScale;
        private bool outOfPlane;

        public VirtualKeyboardKey Key => key;

        [Inject]
        public void Construct(CameraService cameraService)
        {
            this.cameraService = cameraService;
        }

        private void Awake()
        {
            Setup();
        }

        private void Update()
        {
            DetectOutOfPlaneArea();
        }

        public void ResetKey()
        {
            outOfPlane = false;

            HideInstantly();
        }

        public void Show()
        {
            StopTween();

            cameraService.ThrowRaycastFromMousePosition(planeLayer);

            keyObject.transform.position = cameraService.LastHit.point;

            showTween = keyObject.transform.DOScale(defaultScale, showDuration);
        }

        public void Hide()
        {
            StopTween();

            showTween = keyObject.transform.DOScale(Vector3.zero, showDuration);
        }

        public void ShowInstantly()
        {
            StopTween();

            keyObject.transform.localScale = defaultScale;
        }

        public void HideInstantly()
        {
            StopTween();

            keyObject.transform.localScale = Vector3.zero;
        }

        private void Setup()
        {
            defaultScale = transform.localScale;
        }

        private void DetectOutOfPlaneArea()
        {
            if (cameraService.ThrowRaycastFromMousePosition(planeLayer) == null)
            {
                outOfPlane = true;

                KeyOutOfPlane?.Invoke(key);
            }
        }

        private void StopTween()
        {
            if (showTween.IsActive())
            {
                showTween.Kill();
            }
        }
    }
}