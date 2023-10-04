using System;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Controllers.Gate
{
    public class GateMeshController : MonoBehaviour
    {
        #region Self Variables

        #region Serialize Variables

        [SerializeField] private Renderer gateRenderer;

       

        #endregion

        #endregion


        public void SetGateColor(Color gateColor)
        {
            gateRenderer.material.color = gateColor;
        }
    }
}