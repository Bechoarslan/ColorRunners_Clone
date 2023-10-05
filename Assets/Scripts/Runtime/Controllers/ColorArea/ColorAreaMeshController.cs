using System;
using Runtime.Data.ValueObject;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Controllers.ColorArea
{
    public class ColorAreaMeshController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Renderer colorAreaRenderer;
        
        #endregion

        #region Private Variables

        private ColorData _colorData;

        #endregion

        #endregion

        private void Start()
        {
            colorAreaRenderer.material = _colorData.material;
        }

        public void GetColorData(ColorData colorData)
        {
            _colorData = colorData;
        }

        public void CheckPlayerColor(Color playerColor)
        {
           
            if (colorAreaRenderer.material.color == playerColor)
            {
                ColorAreaSignals.Instance.onIsPlayerColorCorrect?.Invoke(true);
            }
            else
            {
                ColorAreaSignals.Instance.onIsPlayerColorCorrect?.Invoke(false);
            }
        }
    }
}