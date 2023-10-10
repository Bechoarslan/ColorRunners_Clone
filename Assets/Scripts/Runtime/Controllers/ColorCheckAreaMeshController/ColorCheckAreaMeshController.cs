using Runtime.Data.ValueObject;
using UnityEngine;

namespace Runtime.Controllers.ColorCheckAreaMeshController
{
    public class ColorCheckAreaMeshController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Renderer renderer;

        #endregion

        #endregion

        public void SetData(ColorData colorData)
        {
            renderer.material.color = colorData.material.color;
        }
    }
}