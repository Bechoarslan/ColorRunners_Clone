
using System;
using Runtime.Commands;
using Runtime.Commands.Gate;
using Runtime.Controllers.Gate;
using Runtime.Signals;
using UnityEngine;


namespace Runtime.Managers
{
    public class GateManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Renderer gateRenderer;
        [SerializeField] private Color gateColor;
        
        #endregion

        #region Private Variables

        private GateChangeColorCommand _gateChangeColorCommand;

        #endregion
        
        #endregion

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _gateChangeColorCommand = new GateChangeColorCommand(ref gateRenderer);
            _gateChangeColorCommand.Execute(gateColor);
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
                GateSignals.Instance.onGetGateColor?.Invoke(gateColor);
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