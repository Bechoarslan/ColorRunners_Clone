
using System;
using Runtime.Controllers;
using Runtime.Controllers.Player;
using Runtime.Data.UnityObject;
using Runtime.Data.ValueObject;
using Runtime.Keys;
using Runtime.Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private PlayerMovementController playerMovementController;
        [SerializeField] private PlayerMeshController playerMeshController;

        #endregion

        #region Private Variables

        [ShowInInspector] private PlayerData _playerData;
        private readonly string _playerDataPath = "Data/CD_Player";

        #endregion

        #endregion

        private void Awake()
        {
            _playerData = GetPlayerData();
            SendDataToController();
        }
        private PlayerData GetPlayerData() => Resources.Load<CD_Player>(_playerDataPath).Data;
        private void SendDataToController()
        {
            playerMovementController.GetDataFromPlayerManager(_playerData.MovementData);
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            InputSignals.Instance.onInputDragged += OnInputDragged;
            InputSignals.Instance.onInputReleased += playerMovementController.OnInputReleased;
            InputSignals.Instance.onInputTaken += playerMovementController.OnInputTaken;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onSetCollectableScore += playerMeshController.SetCollectableScore;
            MiniGameSignals.Instance.onMiniGameAreaSendToMiniGameTypeToListeners +=
                playerMovementController.OnMiniGameAreaSendToMiniGameTypeToListeners;
            MiniGameSignals.Instance.onPlayerReadyToGo += playerMovementController.OnPlayerExitInteractWithMiniGameArea;


        }

        private void OnPlay()
        {
            playerMovementController.IsReadyToPlay(true);
        }

        private void OnInputDragged(HorizontalInputParams inputParams)
        {
            playerMovementController.UpdateInputParams(inputParams);
        }

        private void UnSubscribeEvents()
        {
            InputSignals.Instance.onInputDragged -= OnInputDragged;
            InputSignals.Instance.onInputReleased -= playerMovementController.OnInputReleased;
            InputSignals.Instance.onInputTaken -= playerMovementController.OnInputTaken;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            MiniGameSignals.Instance.onMiniGameAreaSendToMiniGameTypeToListeners -=
                playerMovementController.OnMiniGameAreaSendToMiniGameTypeToListeners;
            MiniGameSignals.Instance.onPlayerReadyToGo -= playerMovementController.OnPlayerExitInteractWithMiniGameArea;
           
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
        
        
    }
}