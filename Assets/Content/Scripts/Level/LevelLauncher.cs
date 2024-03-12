using System;
using System.Collections.Generic;
using Content.Scripts.Character;
using EasyButtons;
using Unity.Mathematics;
using UnityEngine;

namespace Content.Scripts.Level
{
    public class LevelLauncher : MonoBehaviour
    {
        public CharacterEntity Player;
        public int restartLevelDelay;
        public LevelSetup _levelSetup;
        public int currentLevelIndex=0;
        public float PlayerDropHeigh =0f;
        private Level _currentLevel = null;
        
        [Button]
        public void SetupLevel(int levelId)
        {
            if (_currentLevel != null)
            {
                UnsubscribeFromCurrentLevel();
                GameObject.Destroy(_currentLevel);
            }
              
            if (levelId < _levelSetup.Levels.Count)
            {
                currentLevelIndex = levelId;
                _currentLevel =Instantiate(_levelSetup.Levels[currentLevelIndex], transform.position, quaternion.identity) ;
                SubscribeOnCurrentLevel();
                _currentLevel.Setup(restartLevelDelay);
            }
        }

        public void LaunchLevel()
        {
            _currentLevel.EnableFinishTrigger();
            DropPlayer();
        }

        public void FinishLevel()
        {
            UnsubscribeFromCurrentLevel();
            _currentLevel.DisableFinishTrigger();
            currentLevelIndex++;
        }

        private void UnsubscribeFromCurrentLevel()
        {
            if(_currentLevel==null) return;
            _currentLevel.OnLevelReady -= LaunchLevel;
            _currentLevel.OnLevelFinish -= FinishLevel;
        }

        private void SubscribeOnCurrentLevel()
        {
            if(_currentLevel==null) return;
            _currentLevel.OnLevelReady += LaunchLevel;
            _currentLevel.OnLevelFinish += FinishLevel;
        }

        [Button]
        public async void ResetLevel()
        {
            //Player enable movement
            _currentLevel.Reset();
            DropPlayer();
        }

        public void DropPlayer()
        {
            var dropPos = _currentLevel.StartPos.position;
            dropPos.y = PlayerDropHeigh;
            Player.transform.position = dropPos;
            //Player enable movement
        }

        private void OnEnable() => SubscribeOnCurrentLevel();

        private void OnDisable() => UnsubscribeFromCurrentLevel();
    }
}
