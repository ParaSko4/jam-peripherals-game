using DG.Tweening;
using UnityEngine;

namespace Jam.VirtualKeyboard
{
    public class CapsLockData
    {
        private readonly GameObject changableObject;

        public CapsLockData(GameObject changableObject)
        {
            ObjectInDefaultScaleState = true;
            ScaleTween = null;
            StartScale = changableObject.transform.localScale;

            this.changableObject = changableObject;
        }

        public Tween ScaleTween { get; private set; }
        public GameObject ChangableObject => changableObject;
        public Vector3 StartScale { get; private set; }
        public bool ObjectInDefaultScaleState { get; set; }

        public void SetScaleTween(Tween scaleTween)
        {
            ScaleTween = scaleTween;
        }
    }
}