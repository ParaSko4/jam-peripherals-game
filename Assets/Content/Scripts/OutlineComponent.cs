using System.Collections.Generic;
using System.Linq;
using EasyButtons;
using UnityEngine;

namespace Content.Scripts
{
    public class OutlineComponent : MonoBehaviour
    {
        [SerializeField] private Renderer _renderer;
        [SerializeField] private Material _outlineMatOriginal;
        [SerializeField, HideInInspector] private List<Material> _currentMats;
        private Material _outlineMatInstance;

        private void Awake()
        {
            _outlineMatInstance = new Material(_outlineMatOriginal);
        }

        [Button]
        public void OutlinedOn()
        {
            if (_currentMats.Contains(_outlineMatInstance)==false) //safety
            {
                _currentMats.Add(_outlineMatInstance);
            }
            _renderer.SetSharedMaterials(_currentMats);
        }

        [Button]
        public void OutlinedOff()
        {
            _currentMats.Remove(_outlineMatInstance);
            _renderer.SetSharedMaterials(_currentMats);
        }

        private void OnValidate()
        {
            if (_renderer)
            {
                _currentMats = _renderer.sharedMaterials.ToList();
            }
        }
    }
}
