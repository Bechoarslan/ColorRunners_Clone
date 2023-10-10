using System;
using Runtime.Data.ValueObject;
using Runtime.Enums.Color;
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

        public void GetColorData(ColorData colorData)
        {
            gateRenderer.material.color = colorData.material.color;
        }

        
    }
}