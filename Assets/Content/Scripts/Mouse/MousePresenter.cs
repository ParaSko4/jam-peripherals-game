using System.Collections.Generic;
using UnityEngine;

namespace Content.Scripts.Mouse
{
    public class MousePresenter 
    {
        private List<PointerViewData> _cursorView = new List<PointerViewData>();
        private CursorMode _cursorMode = CursorMode.Auto;
        private Vector2 _hotSpot = Vector2.zero;
        public MouseViewState CurrentViewState { get; private set; }

        public MousePresenter(List<PointerViewData> CursorView)
        {
            _cursorView = CursorView;
            CurrentViewState = MouseViewState.NONE;
            ChangeCursorView(MouseViewState.DEFAULT);
        }
        
        public void ChangeCursorView(MouseViewState viewState)
        {
            if(CurrentViewState == viewState) return;
            CurrentViewState = viewState;
            var view= _cursorView.Find(v => v.State == CurrentViewState).View;
            Cursor.SetCursor(view,_hotSpot,_cursorMode);
        }

        public void ChangeCursorVisibility(bool visibility) => Cursor.visible = visibility;
    }

    public enum MouseViewState
    {
        NONE,
        DEFAULT,
        POINTER
    }
}