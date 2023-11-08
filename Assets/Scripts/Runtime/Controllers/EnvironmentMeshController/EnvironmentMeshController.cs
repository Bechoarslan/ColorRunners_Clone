using Runtime.Data.ValueObject;
using UnityEngine;

namespace Runtime.Controllers.EnvironmentMeshController
{
    public class EnvironmentMeshController : MonoBehaviour
    {

        #region Self Variables

        #region Private Variables

        private BuildData _buildData;

        #endregion

        #endregion
        public void GetEnvironmentData(BuildData buildData)
        {
            _buildData = buildData;
        }
    }
}