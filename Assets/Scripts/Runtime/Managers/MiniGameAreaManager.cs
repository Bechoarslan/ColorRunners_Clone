using System;
using Runtime.Enums;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Controllers.MiniGame
{
    public class MiniGameAreaManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private MiniGameType miniGameType;
        [SerializeField] private GameObject turret;
        [SerializeField] private GameObject drone;

        #endregion

        #endregion

        private void Awake()
        {
            SetMiniGameType();
        }

        private void SetMiniGameType()
        {
            if (miniGameType == MiniGameType.Drone)
            {
                turret.SetActive(false);
                drone.SetActive(true);
            }

            if (miniGameType == MiniGameType.Turret)
            {
                turret.SetActive(true);
                drone.SetActive(false);
            }
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onInteractionWithMiniGameArea += OnInteractionWithMiniGameArea;
        }

        private void OnInteractionWithMiniGameArea(GameObject miniGameArea)
        {
            if (miniGameArea.GetInstanceID() == gameObject.GetInstanceID())
            {
                MiniGameSignals.Instance.onGetMiniGameType?.Invoke(miniGameType);
            }
        }


        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onInteractionWithMiniGameArea -= OnInteractionWithMiniGameArea;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}