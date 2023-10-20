using Runtime.Data.ValueObject;
using UnityEngine;

namespace Runtime.Controllers.Collectable
{
    public class CollectableMeshController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Renderer collectableRenderer;

        #endregion

        #endregion
        public void GetColorDataFromManager(ColorData collectableColorData)
        {
            collectableRenderer.material.color = collectableColorData.material.color;
            
        }
    }
    
    
}