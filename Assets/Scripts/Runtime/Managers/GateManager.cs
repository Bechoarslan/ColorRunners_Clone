
using System;
using System.Collections.Generic;
using Runtime.Commands;
using Runtime.Commands.Gate;
using Runtime.Controllers.Gate;
using Runtime.Data.UnityObject;
using Runtime.Data.ValueObject;
using Runtime.Signals;
using UnityEngine;


namespace Runtime.Managers
{
    public class GateManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Renderer gateRenderer;
        [SerializeField] private int gateId;
        
        #endregion

        #region Private Variables

        private GateChangeColorCommand _gateChangeColorCommand;
        private ColorData _colorData;
        private readonly string _colorDataPath = "Data/CD_Color";
        
        #endregion
        
        #endregion

        private void Awake()
        {
            _colorData = GetColorData();
            Init();
        }

        private ColorData GetColorData() => Resources.Load<CD_Color>(_colorDataPath).gateColors[gateId];
       

        private void Init()
        {
            _gateChangeColorCommand = new GateChangeColorCommand(ref gateRenderer);
            _gateChangeColorCommand.Execute(_colorData.material);
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onInteractionWithGate += OnInteractionWithGate;
        }

        private void OnInteractionWithGate(GameObject gateObject)
        {
            if (gateObject.GetInstanceID() == gameObject.GetInstanceID())
            {
                GateSignals.Instance.onGetGateColor?.Invoke(_colorData.material.color);
            }

        }

        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onInteractionWithGate -= OnInteractionWithGate;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}