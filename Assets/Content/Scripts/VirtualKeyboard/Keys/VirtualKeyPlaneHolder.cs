using Jam.UI.VirtualKeyboard;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Jam.VirtualKeyboard.Keys
{
    public class VirtualKeyPlaneHolder : MonoBehaviour
    {
        public event Action<VirtualKeyPlanePresenter> DragBegin;
        public event Action<VirtualKeyPlanePresenter> DragEnd;

        [SerializeField] private List<VirtualKeyPlanePresenter> planeKeys;

        public IEnumerable<VirtualKeyPlanePresenter> PlaneKeys => planeKeys;

        private void Awake()
        {
            Subscribe();
        }

        private void Start()
        {
            ResetKeys();
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }

        public VirtualKeyPlanePresenter GetKey(VirtualKeyboardKey key)
        {
            return planeKeys.Find(p => p.Key == key);
        }

        public void ResetKeys()
        {
            foreach (var planeKey in planeKeys)
            {
                planeKey.ResetKey();
            }
        }

        private void Subscribe()
        {
            foreach (var planeKey in planeKeys)
            {
                planeKey.KeyBeginDrag += OnDragBegin;
                planeKey.KeyEndDrag += OnDragEnd;
            }
        }

        private void Unsubscribe()
        {
            foreach (var planeKey in planeKeys)
            {
                planeKey.KeyBeginDrag -= OnDragBegin;
                planeKey.KeyEndDrag -= OnDragEnd;
            }
        }

        private void OnDragBegin(VirtualKeyPlanePresenter key)
        {
            DragBegin?.Invoke(key);
        }

        private void OnDragEnd(VirtualKeyPlanePresenter key)
        {
            DragEnd?.Invoke(key);
        }
    }
}