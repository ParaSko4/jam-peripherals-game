using System;
using UnityEngine;

namespace Content.Scripts.SelectablesEntities
{
    public class WallEntity : MonoBehaviour, ISelectableEntity
    {
        [SerializeField] private OutlineComponent _outlineComponent;
        
        public GameObject GameObject() => gameObject;

        public Type GetObjectType() => GetType();

        public void Select()
        {
            _outlineComponent.OutlinedOn();
        }

        public void DisableSelection()
        {
            _outlineComponent.OutlinedOff();
        }
    }
}
