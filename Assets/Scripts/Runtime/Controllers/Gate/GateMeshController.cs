using System;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Controllers.Gate
{
    public class GateMeshController : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

        private new Renderer _gateRenderer;

        #endregion
        #endregion

        private void Awake()
        {
            GetReferences();
        }

        private void GetReferences()
        {
            _gateRenderer = GetComponent<Renderer>();
        }

        private void OnEnable()
        {
            GateSignals.Instance.onSetGateColor += OnSetGateColor;
        }

        private void OnSetGateColor(Color32 value)
        {
            _gateRenderer.material.color = value;
        }
    }
}