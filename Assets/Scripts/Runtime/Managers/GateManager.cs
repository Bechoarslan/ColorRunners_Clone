
using System;
using Runtime.Controllers.Gate;
using Runtime.Signals;
using UnityEngine;


namespace Runtime.Managers
{
    public class GateManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        [SerializeField] private GateMeshController gateMeshController;
        [SerializeField] private Color gateColor;
        
        #endregion
        
        #endregion

        private void Awake()
        {
            gateMeshController.SetGateColor(gateColor);
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