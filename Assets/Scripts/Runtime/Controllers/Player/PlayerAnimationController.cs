using System;
using Runtime.Enums;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Controllers.Player
{
    public class PlayerAnimationController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Animator playerAnimator;

        #endregion

        #endregion

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            PlayerSignals.Instance.onPlayerAnimationChanged += OnPlayerAnimationChanged;
        }

        private void OnPlayerAnimationChanged(PlayerAnimationStates state)
        {
           playerAnimator.SetTrigger(state.ToString());
        }

        private void UnSubscribeEvents()
        {
            PlayerSignals.Instance.onPlayerAnimationChanged -= OnPlayerAnimationChanged;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        public void OnReset()
        {
            PlayerSignals.Instance.onPlayerAnimationChanged?.Invoke(PlayerAnimationStates.Idle);
        }
    }
}