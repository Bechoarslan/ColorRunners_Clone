
using System;
using DG.Tweening;
using Runtime.Controllers;
using Runtime.Controllers.Player;
using Runtime.Data.UnityObject;
using Runtime.Data.ValueObject;
using Runtime.Enums;
using Runtime.Enums.MiniGame;
using Runtime.Keys;
using Runtime.Signals;
using Sirenix.OdinInspector;
using TMPro;
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
        [SerializeField] private float scaleValue = 0.1f;
        [SerializeField] private ParticleSystem particle;
        [SerializeField] private TextMeshPro scoreText;
        

        [SerializeField] private int _playerScore;

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
            CoreGameSignals.Instance.onChangeGameStates += playerMovementController.ChangeGameState;
            InputSignals.Instance.onInputDragged += OnInputDragged;
            InputSignals.Instance.onInputReleased += playerMovementController.OnInputReleased;
            InputSignals.Instance.onInputTaken += playerMovementController.OnInputTaken;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onSetCollectableScore += OnSetCollectableScore;
            CoreGameSignals.Instance.onLevelFailed += () =>  playerMovementController.IsReadyToPlay(false);
            MiniGameSignals.Instance.onMiniGameAreaSendToMiniGameTypeToListeners += OnMiniGameAreaSendToMiniGameTypeToListeners
                ;
            MiniGameSignals.Instance.onPlayerReadyToGo += playerMovementController.OnPlayerExitInteractWithMiniGameArea;
            MiniGameSignals.Instance.onPlayerExitInteractWithMiniGameArea += OnPlayerExitInteractWithMiniGameArea;
            CoreGameSignals.Instance.onSetPlayerScale += OnSetPlayerScale;
            CoreGameSignals.Instance.onPlayerExitInteractWithEndArea += OnPlayerExitInteractWithEndArea;
            CoreGameSignals.Instance.onSetPlayerAnimation += playerMeshController.SetPlayerAnimation;
            EnvironmentSignals.Instance.onPlayerPaintEnvironment += OnPlayerPaintEnvironment;


        }

        private void OnPlayerExitInteractWithMiniGameArea()
        {
            playerMovementController.OnPlayerExitInteractWithMiniGameArea();
        }

        private void OnMiniGameAreaSendToMiniGameTypeToListeners(MiniGameType type)
        {
            playerMovementController.OnMiniGameAreaSendToMiniGameTypeToListeners(type);
            if (type == MiniGameType.Drone)
            {
                scoreText.gameObject.SetActive(false);
            }
        }

        private void OnSetCollectableScore(short scoreValue)
        {
            scoreText.text = scoreValue.ToString();
          
        }

        [Button]
        private void OnPlayerPaintEnvironment()
        {
            particle.Play();
            
            var score = CoreGameSignals.Instance.onSendCollectableScore?.Invoke();
            score -= 1;
            CoreGameSignals.Instance.onSetCollectableScore?.Invoke((short)score);
            
            
        }

        private void OnSetPlayerScale()
        {
                var playerScale = transform.localScale;
                playerScale = new Vector3(
                    Mathf.Clamp((playerScale.x + scaleValue), 0.8f, 2f),
                    Mathf.Clamp((playerScale.y + scaleValue), 0.8f, 2f),
                    Mathf.Clamp((playerScale.z + scaleValue), 0.8f, 2f)
                );
                transform.localScale = playerScale;
            
        }

        private void OnPlayerExitInteractWithEndArea()
        {
            CoreGameSignals.Instance.onChangeGameStates?.Invoke(GameStates.Idle);
            playerMeshController.gameObject.SetActive(true);
            _playerScore += 5;
            
        }

       

        private void OnPlay()
        {
            scoreText.gameObject.SetActive(true);
            playerMovementController.IsReadyToPlay(true);
            CoreGameSignals.Instance.onChangeGameStates?.Invoke(GameStates.Run);
        }

        private void OnInputDragged(HorizontalInputParams inputParams)
        {
            playerMovementController.UpdateInputParams(inputParams);
        }

        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onChangeGameStates -= playerMovementController.ChangeGameState;
            InputSignals.Instance.onInputDragged -= OnInputDragged;
            InputSignals.Instance.onInputReleased -= playerMovementController.OnInputReleased;
            InputSignals.Instance.onInputTaken -= playerMovementController.OnInputTaken;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onSetCollectableScore -= OnSetCollectableScore;
            CoreGameSignals.Instance.onLevelFailed -= () =>  playerMovementController.IsReadyToPlay(false);
            MiniGameSignals.Instance.onMiniGameAreaSendToMiniGameTypeToListeners -= OnMiniGameAreaSendToMiniGameTypeToListeners
                ;
            MiniGameSignals.Instance.onPlayerReadyToGo -= playerMovementController.OnPlayerExitInteractWithMiniGameArea;
            MiniGameSignals.Instance.onPlayerExitInteractWithMiniGameArea -= OnPlayerExitInteractWithMiniGameArea;
            CoreGameSignals.Instance.onSetPlayerScale -= OnSetPlayerScale;
            CoreGameSignals.Instance.onPlayerExitInteractWithEndArea -= OnPlayerExitInteractWithEndArea;
            CoreGameSignals.Instance.onSetPlayerAnimation -= playerMeshController.SetPlayerAnimation;
            EnvironmentSignals.Instance.onPlayerPaintEnvironment -= OnPlayerPaintEnvironment;

        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
        
        
    }
}