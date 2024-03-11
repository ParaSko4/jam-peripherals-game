using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Jam;
using UnityEngine;
using Zenject;

namespace Content.Scripts.Mouse
{
    public class MousePointer : MonoBehaviour
    {
        [SerializeField] private LayerMask defaultMask;
        [SerializeField] private LayerMask groundOnly;
        [SerializeField] [Range(0f, 5f)] private float heightAboveGround;
        private PointerConfig _cfg;
        private MousePresenter _mousePresenter;
        private SelectedContext _selectedCtx;
        private LayerMask _currentMask;
        private bool _isHoldingItem = false;
        private bool _isButtonPressed=false;
        private CancellationTokenSource _ctx;
        private CameraService _cameraService;
        private bool _enabled;
        
        public GameObject SelectedEntity => _selectedCtx.currentItem.GameObject();
        public bool SelectedEntityExist => _selectedCtx.currentItem != null;

        public void DeselectEntity() => _selectedCtx.currentItem = null;
        
        [Inject]
        public void Construct(CameraService cameraService, PointerConfig config)
        {
            _cameraService = cameraService;
            _cfg = config;
            _selectedCtx = new SelectedContext();
            _currentMask = defaultMask;
            _mousePresenter = new MousePresenter(_cfg.CursorViewData);
            _enabled = true;
        }

        private void Update()
        {
            if(!_enabled) return;
            
            var (succes, position, pointedObject) = _cameraService.CheckRaycastFromMousePosition(_currentMask);

            if (!succes) return;
            
            position.y = 0;

            _selectedCtx.pointedItem = pointedObject.TryGetComponent(out ISelectableEntity selectable) ? selectable : null;

            if (Input.GetKeyDown(_cfg.PrimaryKey))
            {
                _isButtonPressed = true;
                if (_selectedCtx.pointedItem != null)
                {
                    if (_selectedCtx.currentItem != _selectedCtx.pointedItem)
                    {
                        _selectedCtx.currentItem?.DisableSelection();
                        _selectedCtx.currentItem = _selectedCtx.pointedItem;
                    }
                    _selectedCtx.currentItem?.Select();
                    
                    BuildCancellationToken();
                    HoldCountDown(_selectedCtx.currentItem,_ctx);
                }
            }

            if (Input.GetKeyUp(_cfg.PrimaryKey))
            {
                _isButtonPressed = false;
                
                if (_isHoldingItem)
                    DropItem();
                else
                {
                    _selectedCtx.pointedItem?.Click();
                    if(_ctx!=null)
                        _ctx.Cancel();
                }
            }

            MoveHandledItem(position);
            UpdateView();
        }

        private void MoveHandledItem(Vector3 position)
        {
            if (_selectedCtx.handleItem == null || !_isHoldingItem) return;
            position.y = heightAboveGround;
            _selectedCtx.handleItem.GameObject().transform.position = position;
        }

        public void SendObjectFromUI(ISelectableEntity obj)
        {
            if(!_isButtonPressed) return;
            var newItem = Instantiate(obj.GameObject()).GetComponent<ISelectableEntity>();
            newItem.GameObject().SetActive(true);
            HandleItem(newItem);
        }

        private void HandleItem(ISelectableEntity selected)
        {
            _mousePresenter.ChangeCursorVisibility(false);
            _currentMask = groundOnly;
            _isHoldingItem = true;
            _selectedCtx.handleItem = selected;
            _selectedCtx.handleItem.Select();
            _selectedCtx.handleItem.Handle();
        }

        private void DropItem()
        {
            _isHoldingItem = false;
            _currentMask = defaultMask;
            _selectedCtx.handleItem.Drop();
            _selectedCtx.handleItem = null;
            _mousePresenter.ChangeCursorVisibility(true);
        }

        private void UpdateView()
        {
            if(_selectedCtx.pointedItem==null)
                _mousePresenter.ChangeCursorView(MouseViewState.DEFAULT);
            else
                _mousePresenter.ChangeCursorView(MouseViewState.POINTER);
        }

        private void BuildCancellationToken()
        {
            if(_ctx!=null)
                if(!_ctx.IsCancellationRequested)
                    _ctx.Cancel();
            _ctx = new CancellationTokenSource();
        }

        private async void HoldCountDown(ISelectableEntity selected, CancellationTokenSource ctx)
        {
            if(_isButtonPressed==false) return;

            await UniTask.Delay(_cfg.HandleDelay, cancellationToken:ctx.Token).SuppressCancellationThrow();
            if (_isButtonPressed == true)
            {
                if(selected!= _selectedCtx.pointedItem) return;
                HandleItem(selected);
            }
        }
        
        [HideInInspector]
        public class SelectedContext
        {
            public ISelectableEntity pointedItem;
            public ISelectableEntity currentItem;
            public ISelectableEntity handleItem = null;
        }
    }
}
