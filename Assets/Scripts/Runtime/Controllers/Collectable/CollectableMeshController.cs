using Runtime.Data.UnityObject;
using Runtime.Data.ValueObject;
using Runtime.Enums.Color;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Controllers.Collectable
{
    public class CollectableMeshController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Renderer meshRenderer;

        #endregion

        #endregion
        
        internal void GetColorData(CD_Color colorData, ColorType colorType)
        {
            meshRenderer.material.color = colorData.PlayerColors[(int)colorType].material.color;
        }
        internal void OnGetColorCollectable(ColorType colorType, ColorType playerColor)
        {
            CollectableSignals.Instance.onIsPlayerColorSame?.Invoke(playerColor == colorType);
        }

        
    }
}