using System;
using DG.Tweening;
using Runtime.Controllers.Player;
using Runtime.Data.UnityObject;
using Runtime.Data.ValueObject;
using Runtime.Enums;
using Runtime.Keys;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private PlayerMovementController movementController;
        [SerializeField] private PlayerAnimationController animationController;
        [SerializeField] private PlayerMeshController meshController;

        #endregion

        #region Private Variables

        private PlayerData _data;
        private readonly string _dataPath = "Data/CD_Player";
        private Transform _miniGameHolder;

        #endregion

        #endregion


        private void Awake()
        {
            _data = GetPlayerData();
            SendPlayerDataToControllers();
        }

        private void SendPlayerDataToControllers()
        {
            movementController.SetMovementData(_data.MovementData);
        }

        private PlayerData GetPlayerData() => Resources.Load<CD_Player>(_dataPath).Data;

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            InputSignals.Instance.onInputTaken += () => PlayerSignals.Instance.onMoveConditionChanged?.Invoke(true);
            InputSignals.Instance.onInputReleased += () => PlayerSignals.Instance.onMoveConditionChanged?.Invoke(false);
            InputSignals.Instance.onInputDragged += OnInputDragged;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onLevelSuccessful +=
                () => PlayerSignals.Instance.onPlayConditionChanged?.Invoke(true);
            CoreGameSignals.Instance.onLevelFailed +=
                () => PlayerSignals.Instance.onPlayConditionChanged?.Invoke(false);
            CoreGameSignals.Instance.onReset += OnReset;
            MiniGameSignals.Instance.onGetMiniGameType += OnGetMiniGameType;
            CoreGameSignals.Instance.onExitInteractionWithMiniGameArea += OnExitInteractionWithMiniGameArea;
            ColorAreaSignals.Instance.onSendMiniGameHolder += OnSendMiniGameHolder;


        }

        private void OnSendMiniGameHolder(Transform miniGameHolder)
        {
            _miniGameHolder = miniGameHolder;
            
        }


        private void OnExitInteractionWithMiniGameArea()
        {
            PlayerSignals.Instance.onPlayerAnimationChanged?.Invoke(PlayerAnimationStates.Run);
            movementController.SetForwardSpeed(_data.MovementData.NormalSpeed);
        }

        private void OnGetMiniGameType(MiniGameType miniGameType)
        {
            if (miniGameType == MiniGameType.Turret)
            {
               
                PlayerSignals.Instance.onPlayerAnimationChanged?.Invoke(PlayerAnimationStates.HideWalk);
                movementController.SetForwardSpeed(_data.MovementData.SlowSpeed);
                
            }

            if (miniGameType == MiniGameType.Drone)
            {
                PlayerSignals.Instance.onPlayConditionChanged?.Invoke(false);
                movementController.SetPositionToMiniGameHolder(_miniGameHolder);
                
                
            }
        }


        private void OnPlay()
        {
            PlayerSignals.Instance.onPlayConditionChanged?.Invoke(true);
            
        }

        private void OnInputDragged(HorizontalnputParams inputValues)
        {
            movementController.UpdateInputValue(inputValues);
        }
        
       


        private void UnSubscribeEvents()
        {
            InputSignals.Instance.onInputTaken -= () => PlayerSignals.Instance.onMoveConditionChanged?.Invoke(true);
            InputSignals.Instance.onInputReleased -= () => PlayerSignals.Instance.onMoveConditionChanged?.Invoke(false);
            InputSignals.Instance.onInputDragged -= OnInputDragged;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onLevelSuccessful -=
                () => PlayerSignals.Instance.onPlayConditionChanged?.Invoke(true);
            CoreGameSignals.Instance.onLevelFailed -=
                () => PlayerSignals.Instance.onPlayConditionChanged?.Invoke(false);
            CoreGameSignals.Instance.onExitInteractionWithMiniGameArea -= OnExitInteractionWithMiniGameArea;
            MiniGameSignals.Instance.onGetMiniGameType -= OnGetMiniGameType;
            CoreGameSignals.Instance.onReset -= OnReset;
            
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        internal void SetStackPosition()
        {
            var position = transform.position;
            Vector2 pos = new Vector2(position.x, position.z);
            StackSignals.Instance.onStackFollowPlayer?.Invoke(pos);
        }


        private void OnReset()
        {
            movementController.OnReset();
            animationController.OnReset();
            
        }

    }
}