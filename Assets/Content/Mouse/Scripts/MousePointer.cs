using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Content.Mouse.Scripts
{
    public class MousePointer : MonoBehaviour
    {
        private const KeyCode MouseButton = KeyCode.Mouse0;
        [SerializeField] private LayerMask defaultMask;
        [SerializeField] private LayerMask groundOnly;
        [SerializeField] private Camera cam;
        [SerializeField] [Range(0f, 5f)] private float heightAboveGround;
        [SerializeField] private int handleItemTimer = 0;
        [SerializeField] private Transform itemHandler;
        private LayerMask _currentMask;
        private ISelectable _currentPointedItem;
        private ISelectable _currentSelected;
        private ISelectable _handledItem = null;
        private bool _isHoldingItem;
        private bool _isButtonPressed=false;
        private CancellationTokenSource _ctx;

        [SerializeField] private GameObject TEST_FROM_UI;

        private void Awake()
        {
            _currentMask = defaultMask;
        }

        public void Init(Camera levelCamera)
        {
            cam = levelCamera;
        }

        private void Update()
        {
            var (succes, position, pointedObject) = GetMousePosition();

            if (!succes) return;
            
            position.y = 0;
            itemHandler.position = position;

            _currentPointedItem = pointedObject.TryGetComponent(out ISelectable selectable) ? selectable : null;

            if (Input.GetKeyDown(MouseButton))
            {
                _isButtonPressed = true;
                if (_currentPointedItem != null)
                {
                    if (_currentSelected != _currentPointedItem)
                    {
                        _currentSelected?.DisableSelection();
                        _currentSelected = _currentPointedItem;
                    }
                    _currentSelected?.Select();
                
                    if(_ctx!=null)
                        if(!_ctx.IsCancellationRequested)
                            _ctx.Cancel();
                    _ctx = new CancellationTokenSource();
                    HoldCountDown(_currentSelected,_ctx);
                }
                // if (_currentSelected?.GetObjectType() == typeof(CUBE_TEST))
            }

            if (Input.GetKeyUp(MouseButton))
            {
                _isButtonPressed = false;
                if (_isHoldingItem)
                {
                    _isHoldingItem = false;
                    _currentMask = defaultMask;
                    _handledItem.Drop();
                    _handledItem = null;
                    Cursor.visible = true;
                }
                else
                {
                    _currentPointedItem?.Click();
                    if(_ctx!=null)
                        _ctx.Cancel();
                }
            }

            MoveHandledItem(position);
            
            //TEST
            if(Input.GetKeyDown(KeyCode.Space))
                SendObjectFromUI(TEST_FROM_UI.GetComponent<ISelectable>());
        }

        private void MoveHandledItem(Vector3 position)
        {
            if (_handledItem == null || !_isHoldingItem) return;
            position.y = heightAboveGround;
            _handledItem.GetObject().transform.position = position;
        }

        public void SendObjectFromUI(ISelectable obj)
        {
            if(!_isButtonPressed) return;
            var newItem = Instantiate(obj.GetObject()).GetComponent<ISelectable>();
            newItem.GetObject().SetActive(true);
            HandleItem(newItem);
        }

        private void HandleItem(ISelectable selected)
        {
            Cursor.visible = false;
            _currentMask = groundOnly;
            _isHoldingItem = true;
            _handledItem = selected;
            _handledItem.Select();
            _handledItem.Handle();
        }

        private async void HoldCountDown(ISelectable selected, CancellationTokenSource ctx)
        {
            if(_isButtonPressed==false) return;

            await UniTask.Delay(handleItemTimer, cancellationToken:ctx.Token).SuppressCancellationThrow();
            if (_isButtonPressed == true)
            {
                if(selected!= _currentPointedItem) return;
                
                HandleItem(selected);
            }
        }

        private (bool succes, Vector3 position, GameObject pointedObject) GetMousePosition()
        {
            var ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, _currentMask))
                return (succes: true, position: hitInfo.point, hitInfo.collider.gameObject);
            else
                return (succes: false, position: Vector3.zero, null);
        }
    }
}
