
using System;
using System.Collections.Generic;
using Runtime.Data.UnityObject;
using Runtime.Data.ValueObject;
using Runtime.Enums.Environemnt;
using Runtime.Keys;
using Runtime.Signals;
using Sirenix.OdinInspector;
using UnityEngine;


namespace Runtime.Managers
{
    public class EnvironmentManager : MonoBehaviour
    {

        #region Self Variables

        #region Serialized Variables

        [SerializeField] private GameObject cityHolder;



        #endregion


        #region Private Variables
        
        [ShowInInspector] private Dictionary<int,AreaData> _areaDic = new Dictionary<int, AreaData>();
        private int _cityLevel;
        private CD_IdleData _idleData;
        private int _completedArea;
        private bool _isLevelPlayable;
        private int _score;


        #endregion

        #endregion

        private void Awake()
        {
            GetReferences();
        }

        private void GetReferences()
        {
            _idleData = GetIdleData();
        }

        private CD_IdleData GetIdleData()
        {
            return Resources.Load<CD_IdleData>("Data/CD_IdleData");
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            EnvironmentSignals.Instance.onEnvironmentCompleted += OnEnvironmentCompleted;
            EnvironmentSignals.Instance.onSetAreaData += OnSetAreaData;
            EnvironmentSignals.Instance.onGetAreaData += OnGetAreaData;
            CoreGameSignals.Instance.onNextLevel += OnNextLevel;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onSendCollectableScore += OnSendCollectableScore;
            CoreGameSignals.Instance.onSetCollectableScore += OnSetCollectableScore;
            SaveSignals.Instance.onGetEnvironmetDatas += OnGetEnvironmetDatas;
            SaveSignals.Instance.onLoadEnvironmentDatas += OnLoadEnvironmentDatas;
        }

        private void OnLoadEnvironmentDatas(EnvironmentDataParams dataParams)
        {
            _areaDic = dataParams.BuildDatas;
            _cityLevel = dataParams.CityLevel;
            _score = dataParams.Score;
            _completedArea = dataParams.CompletedArea;
        }

        private EnvironmentDataParams OnGetEnvironmetDatas()
        {
            return new EnvironmentDataParams
            {
                BuildDatas = _areaDic,
                CityLevel = _cityLevel,
                Score = _score,
                CompletedArea = _completedArea
            };

        }

        private short OnSendCollectableScore()
        {
            return (short)_score;
        }

        private void OnSetCollectableScore(short score)
        {
            _score = score;
            
        }


        private void OnNextLevel()
        {
            if (_isLevelPlayable)
            {
                _areaDic.Clear();
                Destroy(cityHolder.transform.GetChild(0).gameObject);
                OnInitializeLevel();
                EnvironmentSignals.Instance.onCityComplete?.Invoke();
                _isLevelPlayable = false;
            }
            else
            {
                EnvironmentSignals.Instance.onPrepareEnvironmentWithSave?.Invoke();
            }
        }

        private void OnSetAreaData(int id, AreaData areaData)
        {
            if (_areaDic.ContainsKey(id))
                _areaDic[id] = areaData;
            else
                _areaDic.Add(id, areaData);
            SaveSignals.Instance.onSaveData?.Invoke();
        }

        private AreaData OnGetAreaData(int id)
        {
            return _areaDic.ContainsKey(id) ? _areaDic[id] : new AreaData();

        }

        private void OnEnvironmentCompleted()
        {
            _completedArea++;
            CityCompleteCheck();

        }

        private void CityCompleteCheck()
        {
            if (_completedArea == _idleData.IdleDataList[_cityLevel].BuildCount)
            {
                _cityLevel++;
                _isLevelPlayable = true;
                _completedArea = 0;
            }
                
        }


        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void UnSubscribeEvents()
        {
            EnvironmentSignals.Instance.onEnvironmentCompleted -= OnEnvironmentCompleted;
            EnvironmentSignals.Instance.onGetAreaData -= OnGetAreaData;
            EnvironmentSignals.Instance.onSetAreaData -= OnSetAreaData;
            CoreGameSignals.Instance.onNextLevel -= OnNextLevel;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onSendCollectableScore -= OnSendCollectableScore;
            CoreGameSignals.Instance.onSetCollectableScore -= OnSetCollectableScore;
            SaveSignals.Instance.onGetEnvironmetDatas -= OnGetEnvironmetDatas;
            SaveSignals.Instance.onLoadEnvironmentDatas -= OnLoadEnvironmentDatas;
        }

        private void Start()
        {
            OnInitializeLevel();
        }

        private void OnInitializeLevel()
        {
            Instantiate(Resources.Load<GameObject>("Prefabs/CityPrefabs/City 1"), cityHolder.transform);
        }
        
        private void OnPlay()
        {
            EnvironmentSignals.Instance.onRefreshEnvironmentData?.Invoke();
        }
    }
}