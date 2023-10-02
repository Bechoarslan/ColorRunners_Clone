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

        #endregion

        #endregion

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            PlayerSignals.Instance.onSendStackScoreToPlayerText += OnSendStackScoreToPlayerText;
        }

        private void OnSendStackScoreToPlayerText(short stackValue)
        {
            stackText.text = stackValue.ToString();
        }

        private void UnSubscribeEvents()
        {
            PlayerSignals.Instance.onSendStackScoreToPlayerText -= OnSendStackScoreToPlayerText;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}