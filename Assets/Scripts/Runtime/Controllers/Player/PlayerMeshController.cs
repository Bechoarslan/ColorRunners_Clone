using System;
using Runtime.Signals;
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

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            PlayerSignals.Instance.onSendStackScoreToPlayerText += OnSendStackScoreToPlayerText;
            GateSignals.Instance.onGetGateColor += OnGetGateColor;
        }

        private void OnGetGateColor(Color arg0)
        {
            playerRenderer.material.color = arg0;
        }


        private void OnSendStackScoreToPlayerText(short stackValue)
        {
            stackText.text = stackValue.ToString();
        }

        private void UnSubscribeEvents()
        {
            PlayerSignals.Instance.onSendStackScoreToPlayerText -= OnSendStackScoreToPlayerText;
            GateSignals.Instance.onGetGateColor -= OnGetGateColor;
           
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}