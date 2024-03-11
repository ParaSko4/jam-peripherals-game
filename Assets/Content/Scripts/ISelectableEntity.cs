using System;
using UnityEngine;

namespace Content.Scripts
{
    public interface ISelectableEntity
    {
        public void EnableHighlight()
        {
        }

        public void DisableHighlight()
        {
        }

        public void Select()
        {
        }

        public void DisableSelection()
        {
        }

        public void Click()
        {
        }

        public void Handle()
        {
        }

        public void Drop()
        {
        }

        public GameObject GameObject();
        public Type GetObjectType();
    }
}