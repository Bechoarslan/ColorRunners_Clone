using System;
using DG.Tweening;
using Runtime.Commands.MiniGame;
using Runtime.Enums.MiniGame;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Managers
{
    public class MiniGameManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private MiniGameType miniGameType;
        [SerializeField] private GameObject dronePrefab;
        [SerializeField] private DOTweenPath dronePath;

        #endregion

        #region Private Variables

        private MiniGamePlayDroneCommand _miniGamePlayDroneCommand;

        #endregion
        

        #endregion

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _miniGamePlayDroneCommand = new MiniGamePlayDroneCommand(ref dronePrefab);
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            MiniGameSignals.Instance.onPlayerInteractWithMiniGameArea += OnPlayerInteractWithMiniGameArea;
        }

        private void OnPlayerInteractWithMiniGameArea(GameObject miniGameAreaObject)
        {
            if (miniGameAreaObject.GetInstanceID() != gameObject.GetInstanceID()) return;
            MiniGameSignals.Instance.onMiniGameAreaSendToMiniGameTypeToListeners.Invoke(miniGameType);
        }


        private void UnSubscribeEvents()
        {
            MiniGameSignals.Instance.onPlayerInteractWithMiniGameArea -= OnPlayerInteractWithMiniGameArea;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        internal void PlayDrone()
        {
             _miniGamePlayDroneCommand.Execute();
             
        }
    }
}