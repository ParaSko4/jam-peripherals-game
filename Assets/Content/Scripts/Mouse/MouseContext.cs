using UnityEngine;

namespace Jam.Mouse
{
    public class MouseContext
    {
        private GameObject selectedEntity;
        
        public GameObject SelectedEntity => selectedEntity;
        public bool SelectedEntityExist => selectedEntity != null;

        public void SelectEntity(GameObject entity)
        {
            Debug.Log($"[{typeof(MouseContext)}] SelectEntity: {entity.name}");

            selectedEntity = entity;
        }

        public void DeselectEntity()
        {
            Debug.Log($"[{typeof(MouseContext)}] DeselectEntity: {selectedEntity.name}");

            selectedEntity = null;
        }
    }
}