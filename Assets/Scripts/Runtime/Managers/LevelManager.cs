using System;
using _Modules.SaveModule.Scripts.Data;
using DG.Tweening;
using Managers;
using Runtime.Commands.Level;
using Runtime.Data.UnityObject;
using Runtime.Enums;
using Runtime.Keys;
using Runtime.Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Runtime.Managers
{
    public class LevelManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public int LevelData;

        

        #endregion
      

        #region Serialized Variables

        [Header("Holder")] [SerializeField] internal GameObject levelHolder;

        #endregion

        #region Private Variables

        private LevelLoaderCommand _levelLoader;
        private LevelDestroyerCommand _levelDestroyer;
        private GameData _gameData;
        [ShowInInspector] private int _levelId;

        #endregion

        #endregion

        private void Awake()
        {
            GetReferences();
            Init();
        }

        private void Init()
        {
            _levelLoader = new LevelLoaderCommand(ref levelHolder);
            _levelDestroyer = new LevelDestroyerCommand(ref levelHolder);
        }

        private void GetReferences()
        {
            LevelData = GetLevelCount();
        }

        private int GetLevelCount()
        {
            return _levelId % Resources.Load<CD_Level>("Data/CD_Level").Levels.Count;
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitialize += OnLevelInitialize;
            CoreGameSignals.Instance.onClearActiveLevel += OnClearActiveLevel;
            CoreGameSignals.Instance.onNextLevel += OnNextLevel;
            CoreGameSignals.Instance.onRestartLevel += OnRestartLevel;
            CoreGameSignals.Instance.onGetLevelID += GetLevelID;
            SaveSignals.Instance.onGetLevelDatas += OnGetLevelDatas;
            SaveSignals.Instance.onLoadLevelDatas += OnLoadLevelData;
           
           
            
        }

       
        private void OnClearActiveLevel()
        {
            _levelDestroyer.Execute();
        }

        private void OnLevelInitialize()
        {
            var newLevelData = GetLevelCount();
                _levelLoader.Execute(newLevelData);
                Debug.LogWarning("Executed");
            
        }

        private void OnLoadLevelData(LevelDataParams levelData)
        {
           
            _levelId = levelData.Level;
            Debug.LogWarning("LevelId" + _levelId);
            
        }

        private LevelDataParams OnGetLevelDatas()
        {
            return new LevelDataParams { Level = _levelId };
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitialize -= OnLevelInitialize;
            CoreGameSignals.Instance.onClearActiveLevel -= OnClearActiveLevel;
            CoreGameSignals.Instance.onGetLevelID -= GetLevelID;
            CoreGameSignals.Instance.onNextLevel -= OnNextLevel;
            CoreGameSignals.Instance.onRestartLevel -= OnRestartLevel;
            SaveSignals.Instance.onGetLevelDatas -= OnGetLevelDatas;
            SaveSignals.Instance.onLoadLevelDatas -= OnLoadLevelData;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }


        private int GetLevelID()
        {
            return _levelId;
        }


        private void OnNextLevel()
        {
            _levelId++;
            CoreGameSignals.Instance.onClearActiveLevel?.Invoke();
            CoreGameSignals.Instance.onReset?.Invoke();
            CoreGameSignals.Instance.onLevelInitialize?.Invoke();
            SaveSignals.Instance.onSaveData?.Invoke();
            
        }

        private void OnRestartLevel()
        {
            CoreGameSignals.Instance.onClearActiveLevel?.Invoke();
            CoreGameSignals.Instance.onReset?.Invoke();
            CoreGameSignals.Instance.onLevelInitialize?.Invoke();
        }

        private void Start()
        {
            OnLevelInitialize();
        }
    }
}