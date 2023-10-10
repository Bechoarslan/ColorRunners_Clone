
using Runtime.Data.UnityObject;
using Runtime.Enums.Color;
using TMPro;
using UnityEngine;



namespace Runtime.Controllers.Player
{
    public class PlayerMeshController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private TextMeshPro stackText;
        [SerializeField] private Renderer playerRenderer;
        
        #endregion

        #endregion
        
        internal void SetColorData(CD_Color colorData, ColorType colorType)
        {
            playerRenderer.material.color = colorData.PlayerColors[(int)colorType].material.color;
        }
        
        internal void OnSendStackScoreToPlayerText(short stackValue)
        {
            stackText.text = stackValue.ToString();
        }
        internal ColorType OnGetPlayerColor(ColorType colorType)
        {
            return colorType;
        }

        
    }
}