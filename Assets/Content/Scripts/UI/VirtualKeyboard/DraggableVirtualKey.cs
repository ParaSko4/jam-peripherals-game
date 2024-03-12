using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Jam.VirtualKeyboard.Keys
{
    public class DraggableVirtualKey : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public event Action KeyAchivePlane;
        public event Action DragBegin;
        public event Action DragEnd;

        [SerializeField] private LayerMask planeLayer;
        [SerializeField] private float moveDuration;

        private Tween moveToStartTween;
        private CameraService cameraService;
        private RectTransform rectTransform;

        private Vector2 defaultAnchorPosition;
        private bool dragActive;
        private bool planeAchived;
        private bool customDragHandler;

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
            DragHandler();
            SearchPlane();
        }
        
        public void StartCustomDragAfterReturningFromPlane()
        {
            planeAchived = false;
            customDragHandler = true;

            StartDrag();
        }

        public void ResetKey()
        {
            dragActive = false;
            planeAchived = false;
            customDragHandler = false;

            rectTransform.anchoredPosition = defaultAnchorPosition;

            StopMovementAnimation();
        }

        private void Setup()
        {
            rectTransform = GetComponent<RectTransform>();

            defaultAnchorPosition = rectTransform.anchoredPosition;
        }

        private void DragHandler()
        {
            if (customDragHandler == false)
            {
                return;
            }

            DragMovement();
        }

        private void SearchPlane()
        {
            if (dragActive == false || planeAchived)
            {
                return;
            }

            if (cameraService.ThrowRaycastFromMousePosition(planeLayer) != null)
            {
                planeAchived = true;

                KeyAchivePlane?.Invoke();
            }
        }

        private void StartDrag()
        {
            dragActive = true;

            StopMovementAnimation();

            DragBegin?.Invoke();
        }

        private void DragMovement()
        {
            if (dragActive == false)
            {
                return;
            }

            rectTransform.position = Input.mousePosition;
        }

        private void StopDrag()
        {
            if (dragActive == false)
            {
                return;
            }

            dragActive = false;
            customDragHandler = false;

            DragEnd?.Invoke();

            if (planeAchived)
            {
                return;
            }

            StartMoveToDefaultPositionAnimation();
        }

        private void StartMoveToDefaultPositionAnimation()
        {
            moveToStartTween = rectTransform.DOAnchorPos(defaultAnchorPosition, moveDuration);
        }

        private void StopMovementAnimation()
        {
            if (moveToStartTween.IsActive())
            {
                moveToStartTween.Kill();
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (planeAchived)
            {
                return;
            }

            StartDrag();
        }

        public void OnDrag(PointerEventData eventData)
        {
            DragMovement();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            StopDrag();
        }
    }
}