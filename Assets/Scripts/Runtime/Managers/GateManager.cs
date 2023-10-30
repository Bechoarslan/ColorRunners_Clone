using System;
using Runtime.Commands.Gate;
using Runtime.Data.UnityObject;
using Runtime.Enums.Color;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Managers
{
    public class GateManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        
        [SerializeField] private ColorType colorType;
        [SerializeField] private Renderer gateRenderer;

        #endregion

        #region Private Variables

        private CD_Color _colorData;
        private readonly string _colorDataPath= "Data/CD_Color";
        private GateSetColorCommand _gateSetColorCommand;

        #endregion

        #endregion

        private void Awake()
        {
            _colorData = GetColorData();
            Init();
            
        }
        
        private CD_Color GetColorData() => Resources.Load<CD_Color>(_colorDataPath);
        
        private void Init()
        {
            _gateSetColorCommand = new GateSetColorCommand(ref gateRenderer);
            SetColorData();
        }
        private void SetColorData()
        {
            _gateSetColorCommand.Execute(_colorData.gateColor[(int)colorType]);
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CollectableSignals.Instance.onCollectableInteractWithGate += OnCollectableInteractWithCollectable;
        }

        private void OnCollectableInteractWithCollectable(GameObject gateObject)
        {
            if(gateObject.GetInstanceID() == gameObject.GetInstanceID())
            {
                CollectableSignals.Instance.onSendGateColorType?.Invoke(colorType);
            }
               
            
        }


        private void UnSubscribeEvents()
        {
            CollectableSignals.Instance.onCollectableInteractWithGate -= OnCollectableInteractWithCollectable;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}