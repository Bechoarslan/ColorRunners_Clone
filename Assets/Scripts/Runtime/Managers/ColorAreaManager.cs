
using System;
using Runtime.Commands.ColorCheck;
using Runtime.Controllers.ColorArea;
using Runtime.Data.UnityObject;
using Runtime.Data.ValueObject;
using Runtime.Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Runtime.Managers
{
    public class ColorAreaManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialize Variables
        [SerializeField] private int colorCheckAreaId;
        [SerializeField] private ColorAreaMeshController _colorAreaMeshController;

        #endregion

        #region Private Variables
        private ColorData _colorData;
        private readonly string _colorDataPath = "Data/CD_Color";
        [ShowInInspector] private bool _isPlayerColorCorrect;
        
        
        #endregion

        #endregion

        private void Awake()
        {
            _colorData = GetColorData();
            SendColorDataToMesh();
        }

        private ColorData GetColorData() => Resources.Load<CD_Color>(_colorDataPath).groundAreaColors[colorCheckAreaId];

        private void SendColorDataToMesh()
        {
            _colorAreaMeshController.GetColorData(_colorData);
            
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onInteractionWithColorCheckArea += OnInteractionWithColorCheckArea;
            ColorAreaSignals.Instance.onIsPlayerColorCorrect += OnIsPlayerColorCorrect;
        }

        private void OnIsPlayerColorCorrect(bool condition)
        {
            _isPlayerColorCorrect = condition;
        }
        

        private void OnInteractionWithColorCheckArea(GameObject colorCheckObject)
        {
            if (colorCheckObject.GetInstanceID() == gameObject.GetInstanceID())
            {
                _colorAreaMeshController.CheckPlayerColor(PlayerSignals.Instance.onGetPlayerColor.Invoke());
            }
        }
        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onInteractionWithColorCheckArea -= OnInteractionWithColorCheckArea;
            ColorAreaSignals.Instance.onIsPlayerColorCorrect -= OnIsPlayerColorCorrect;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        
    }
}