using System;
using Runtime.Signals;
using Sirenix.OdinInspector;
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
            PlayerSignals.Instance.onGetPlayerColor += OnGetPlayerColor;
        }

        
        private Color OnGetPlayerColor()
        {
            return playerRenderer.material.color;
        }

        
        private void OnGetGateColor(Color value)
        {
            playerRenderer.material.color = value;
        }


        private void OnSendStackScoreToPlayerText(short stackValue)
        {
            stackText.text = stackValue.ToString();
        }

        private void UnSubscribeEvents()
        {
            PlayerSignals.Instance.onSendStackScoreToPlayerText -= OnSendStackScoreToPlayerText;
            GateSignals.Instance.onGetGateColor -= OnGetGateColor;
            PlayerSignals.Instance.onGetPlayerColor -= OnGetPlayerColor;

        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }


        
    }
}