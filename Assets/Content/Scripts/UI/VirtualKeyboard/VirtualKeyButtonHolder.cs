using Jam.VirtualKeyboard;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Jam.UI.VirtualKeyboard
{
    public class VirtualKeyButtonHolder : MonoBehaviour
    {
        public event Action<VirtualKeyButtonPresenter> DragBegin;
        public event Action<VirtualKeyButtonPresenter> DragEnd;

        [SerializeField] private List<VirtualKeyButtonPresenter> keyButtons;

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

        public VirtualKeyButtonPresenter GetKeyButton(VirtualKeyboardKey key)
        {
            return keyButtons.Find(b => b.Key == key);
        }

        public void ResetKeys()
        {
            foreach (var keyButton in keyButtons)
            {
                keyButton.ResetKey();
            }
        }

        private void Subscribe()
        {
            foreach(var keyButton in keyButtons)
            {
                keyButton.KeyBeginDrag += OnDragBegin;
                keyButton.KeyEndDrag += OnDragEnd;
            }
        }

        private void Unsubscribe()
        {
            foreach (var keyButton in keyButtons)
            {
                keyButton.KeyBeginDrag -= OnDragBegin;
                keyButton.KeyEndDrag -= OnDragEnd;
            }
        }

        private void OnDragBegin(VirtualKeyButtonPresenter key)
        {
            DragBegin?.Invoke(key);
        }

        private void OnDragEnd(VirtualKeyButtonPresenter key)
        {
            DragEnd?.Invoke(key);
        }
    }
}