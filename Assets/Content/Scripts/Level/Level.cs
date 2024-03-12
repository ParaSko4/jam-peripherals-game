using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using EasyButtons;
using UnityEngine;

namespace Content.Scripts.Level
{
    public class Level : MonoBehaviour
    {
        public event Action OnLevelFinish;
        public event Action OnLevelReady;
        [SerializeField] private Transform _startPos;
        [SerializeField] private LevelFinishTrigger _finishTrigger;
        [SerializeField] private List<Transform> _movableObjects = new List<Transform>();
        private int _resetDelay = 0;
        private float _itemScaleDuration = 0f;
        public Transform StartPos => _startPos;
        private Dictionary<Transform, Vector3> _defaultPosition = new Dictionary<Transform, Vector3>();
        public bool IsReady { get; private set; }

        [Button]
        public void Setup(int resetDelay)
        {
            _resetDelay = resetDelay;
            _itemScaleDuration = (float) _resetDelay / 1000;
            foreach (var item in _movableObjects)
            {
                _defaultPosition.Add(item, item.position);
            }
            
            IsReady = true;
            OnLevelReady?.Invoke();
        }

        [Button]
        public async void Reset()
        {
            if(!IsReady) return;
            IsReady = false;
            DisableFinishTrigger();
           
            foreach (var item in _movableObjects)
            {
                MoveLevelItemToDefault(item, _defaultPosition[item]);
            }

            EnableFinishTrigger();
            await UniTask.Delay(_resetDelay).SuppressCancellationThrow();
            IsReady = true;
        }

        public void DisableFinishTrigger()
        {
            _finishTrigger.Enabled = false;
            _finishTrigger.OnPlayerReachFinish -= FinishLevel;
        }

        public void EnableFinishTrigger()
        {
            _finishTrigger.Enabled = true;
            _finishTrigger.OnPlayerReachFinish += FinishLevel;
        }

        private void FinishLevel()
        {
            _finishTrigger.OnPlayerReachFinish -= FinishLevel;
            _finishTrigger.Enabled = false;
            OnLevelFinish?.Invoke();
        }

        public void OnDrawGizmos()
        {
            if (_startPos != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(_startPos.position,0.2f);
            }
        }

        private void MoveLevelItemToDefault(Transform item, Vector3 defPos)
        {
            var defaultScale = item.localScale;
            item.DOScale(Vector3.zero, _itemScaleDuration/2).OnComplete(() =>
            {
                item.position = defPos;
                item.DOScale(defaultScale, _itemScaleDuration / 2);
            });
        }
    }
}
