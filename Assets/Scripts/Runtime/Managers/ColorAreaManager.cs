
using System;
using Runtime.Commands.ColorCheck;
using Runtime.Controllers.ColorArea;
using Runtime.Data.UnityObject;
using Runtime.Data.ValueObject;
using Runtime.Enums;
using Runtime.Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Runtime.Managers
{
    public class ColorAreaManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialize Variables
        [SerializeField] private int colorCheckAreaId; 
        [SerializeField] private ColorAreaMeshController colorAreaMeshController;
        [SerializeField] private Transform miniGameHolder;
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
            colorAreaMeshController.GetColorData(_colorData);
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
                colorAreaMeshController.CheckPlayerColor(PlayerSignals.Instance.onGetPlayerColor.Invoke());
                ColorAreaSignals.Instance.onSendMiniGameHolder?.Invoke(miniGameHolder);
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