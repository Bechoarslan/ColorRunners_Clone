using System;
using DG.Tweening;
using Runtime.Data.UnityObject;
using Runtime.Data.ValueObject;
using Runtime.Enums.Environemnt;
using Runtime.Signals;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.XR;

namespace Runtime.Managers
{
    public class BuildManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private EnvironmentEnum environmentType;
        [SerializeField] private TextMeshPro buildCost;
        [SerializeField] private TextMeshPro gardenCost;
        [SerializeField] private GameObject buildObject;
        [SerializeField] private GameObject gardenObject;
        [SerializeField] private int areaId;

        #endregion

        #region Private Variables
        
        [ShowInInspector] private BuildData _buildData;
        [ShowInInspector]private AreaData _areaData;
        [ShowInInspector]private GameObject _buildManagerObject;

        #endregion

        #endregion


        private void Awake()
        {
            GetReferences();
        }

        private void GetReferences()
        {
            _buildData = GetData();
            buildCost.text = _buildData.EnvironmentBuildCost.ToString();
            gardenCost.text = _buildData.EnvironmentGardenCost.ToString();
        }

        private BuildData GetData()
        {
            return Resources.Load<CD_BuildData>("Data/CD_BuildData").EnvironmentDataList[(int)environmentType];
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            EnvironmentSignals.Instance.onPlayerStayInteractWithEnvironment += OnPlayerStayInteractWithEnvironment;
            EnvironmentSignals.Instance.onPlayerInteractWithEnvironment += OnPlayerInteractWithEnvironment;
            EnvironmentSignals.Instance.onPrepareEnvironmentWithSave += OnPrepareEnvironmentWithSave;
            EnvironmentSignals.Instance.onRefreshEnvironmentData += OnRefreshEnvironmentData;
            EnvironmentSignals.Instance.onCityComplete += OnCityComplete;
            
        }

        private void OnCityComplete()
        {
            _buildData.EnvironmentMaterial.DOFloat(0, "_Saturation", 0f);
            _buildData.GardenMaterial.DOFloat(0, "_Saturation", 0f);
        }


        private void OnRefreshEnvironmentData()
        {
            _areaData = (AreaData)EnvironmentSignals.Instance.onGetAreaData?.Invoke(areaId);
            SetMaterials();
            SetTexts();
            ChangeCostAppearance();
        }

        private void OnPrepareEnvironmentWithSave()
        {
            EnvironmentSignals.Instance.onSetAreaData?.Invoke(areaId,_areaData);
        }

        private void OnPlayerInteractWithEnvironment(GameObject buildManager)
        {
            _buildManagerObject = buildManager;
        }

        private void OnPlayerStayInteractWithEnvironment( )
        {
            if (_buildManagerObject != gameObject) return;
            switch (_areaData.EnvironmentStageType)
            {
                case StageType.Build:
                    _areaData.BuildValue++;
                    SetTexts();
                    SetMaterials();
                    if (_buildData.EnvironmentBuildCost == _areaData.BuildValue) ChangeState();
                    break;
                case StageType.Garden:
                    _areaData.GardenValue++;
                    SetTexts();
                    SetMaterials();
                    if (_buildData.EnvironmentGardenCost == _areaData.GardenValue) ChangeState();
                    break;
                case StageType.Complete:
                    break;
                default:
                    break;
                
            }
        }

        private void ChangeState()
        {
            if (_areaData.EnvironmentStageType == StageType.Build)
            {
                _areaData.EnvironmentStageType = StageType.Garden;
                ChangeCostAppearance();
            }
            else if (_areaData.EnvironmentStageType == StageType.Garden)
            {
                
                _areaData.EnvironmentStageType = StageType.Complete;
                ChangeCostAppearance();
                EnvironmentSignals.Instance.onEnvironmentCompleted?.Invoke();
            }
            else
            {
                ChangeCostAppearance();
            }
        }

        private void ChangeCostAppearance()
        {
            switch (_areaData.EnvironmentStageType)
            {
                case StageType.Build:
                    buildObject.SetActive(true);
                    gardenObject.SetActive(false);
                    break;
                case StageType.Garden:
                    buildObject.SetActive(false);
                    gardenObject.SetActive(true);
                    break;
                case StageType.Complete:
                    buildObject.SetActive(false);
                    gardenObject.SetActive(false);
                    break;
            }
        }

        private void SetMaterials()
        {
            switch (_areaData.EnvironmentStageType)
            {
                case StageType.Build:
                    _buildData.EnvironmentMaterial.DOFloat(2 / (_buildData.EnvironmentBuildCost / _areaData.BuildValue),
                        "_Saturation", 0.5f);
                    break;
                case StageType.Garden:
                    _buildData.GardenMaterial.DOFloat(2 / (_buildData.EnvironmentGardenCost / _areaData.GardenValue), 
                        "_Saturation", 0.5f);
                    break;
                case StageType.Complete:
                    _buildData.EnvironmentMaterial.DOFloat(2 / (_buildData.EnvironmentBuildCost / _areaData.BuildValue),
                        "_Saturation", 0.5f);
                    _buildData.GardenMaterial.DOFloat(2 / (_buildData.EnvironmentGardenCost / _areaData.GardenValue), 
                        "_Saturation", 0.5f);
                    break;
            }
        }

        private void SetTexts()
        {
            switch (_areaData.EnvironmentStageType) 
            {
                case StageType.Build:
                    buildCost.text = (_buildData.EnvironmentBuildCost - _areaData.BuildValue).ToString();
                    break;
                case StageType.Garden:
                    gardenCost.text = (_buildData.EnvironmentGardenCost - _areaData.GardenValue).ToString();
                    break;
                
            }
        }

        private void UnSubscribeEvents()
        {
            EnvironmentSignals.Instance.onPlayerStayInteractWithEnvironment -= OnPlayerStayInteractWithEnvironment;
            EnvironmentSignals.Instance.onPlayerInteractWithEnvironment -= OnPlayerInteractWithEnvironment;
            EnvironmentSignals.Instance.onPrepareEnvironmentWithSave -= OnPrepareEnvironmentWithSave;
            EnvironmentSignals.Instance.onRefreshEnvironmentData -= OnRefreshEnvironmentData;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}