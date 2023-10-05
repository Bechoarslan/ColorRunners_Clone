using System;
using Runtime.Data.ValueObject;
using UnityEngine;

namespace Runtime.Controllers
{
    public class CollectableMeshController : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

        private ColorData _colorData;

        #endregion

        #region Private Variables

        private Renderer _collectableRenderer;

        #endregion

        #endregion

        private void Awake()
        {
            _collectableRenderer = GetComponent<Renderer>();
        }

        public void GetColorData(ColorData colorData)
        {
            _colorData = colorData;
        }

        private void Start()
        {
            SetMeshColor();
        }

        private void SetMeshColor()
        {
            _collectableRenderer.material.color = _colorData.material.color;
        }

        public void SetGateColorForCollectable(Color value)
        {
            _collectableRenderer.material.color = value;
        }
    }
}