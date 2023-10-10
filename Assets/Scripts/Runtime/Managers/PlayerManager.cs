using System;
using DG.Tweening;
using Runtime.Controllers.Player;
using Runtime.Data.UnityObject;
using Runtime.Data.ValueObject;
using Runtime.Enums;
using Runtime.Enums.Color;
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
        [SerializeField] private ColorType colorType;
       

        #endregion

        #region Private Variables
        private PlayerData _data;
        private CD_Color _colorData;
        private readonly string _dataPath = "Data/CD_Player";
        private readonly string _colorDataPath = "Data/CD_Color";
        #endregion

        #endregion


        private void Awake()
        {
            _data = GetPlayerData();
            _colorData = GetColorData();
            SendPlayerDataToControllers();
        }

        private void SendPlayerDataToControllers()
        {
            movementController.SetMovementData(_data.MovementData);
            meshController.SetColorData(_colorData, colorType);
        }

        private PlayerData GetPlayerData() => Resources.Load<CD_Player>(_dataPath).Data;
        private CD_Color GetColorData() => Resources.Load<CD_Color>(_colorDataPath);

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
            PlayerSignals.Instance.onSetPlayerAnimationState += animationController.SetAnimationState;
            PlayerSignals.Instance.onGetPlayerColor += () => meshController.OnGetPlayerColor(colorType);
            PlayerSignals.Instance.onSendStackScoreToPlayerText += meshController.OnSendStackScoreToPlayerText;
            PlayerSignals.Instance.onSetPlayerColor += OnSetPlayerColor;
            ColorCheckSignals.Instance.onSendCheckAreaHolderTransform += OnSendCheckAreaHolderTransform;


        }

        private void OnSendCheckAreaHolderTransform(Transform holderTransform)
        {
            movementController.OnSendPlayerToCheckAreaHolder(holderTransform);
        }


        private void OnSetPlayerColor(ColorType gateColor)
        {
            meshController.SetColorData(_colorData, gateColor);
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
            CoreGameSignals.Instance.onReset -= OnReset;
            PlayerSignals.Instance.onSetPlayerAnimationState -= animationController.SetAnimationState;
            PlayerSignals.Instance.onGetPlayerColor -= () => meshController.OnGetPlayerColor(colorType);
            PlayerSignals.Instance.onSendStackScoreToPlayerText -= meshController.OnSendStackScoreToPlayerText;
            PlayerSignals.Instance.onSetPlayerColor -= OnSetPlayerColor;

            
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