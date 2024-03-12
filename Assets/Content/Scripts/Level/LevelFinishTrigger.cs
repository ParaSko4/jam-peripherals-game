using System;
using UnityEditor;
using UnityEngine;

namespace Content.Scripts.Level
{
    [RequireComponent(typeof(BoxCollider))]
    public class LevelFinishTrigger : MonoBehaviour
    {
        public event Action OnPlayerReachFinish;
        public bool Enabled { get; set; }

        public void OnTriggerEnter(Collider other)
        {
            if (Enabled)
            {
                /*if(other.TryGetComponent<CharacterEntity>(out CharacterENtity player))
               OnPlayerReachFinish?.Invoke();*/
            }
           
        }
    }
}
