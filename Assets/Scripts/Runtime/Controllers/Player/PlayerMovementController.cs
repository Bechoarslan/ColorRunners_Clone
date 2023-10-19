using DG.Tweening;
using Runtime.Data.UnityObject;
using Runtime.Data.ValueObject;
using Runtime.Enums;
using Runtime.Keys;
using Unity.Mathematics;
using UnityEngine;

namespace Runtime.Controllers.Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Rigidbody playerRigidbody;
        [SerializeField] private CD_MovementList movementList;

        #endregion
        #region Private Variables

        private PlayerMovementData _playerData;
        private HorizontalInputParams _inputParams;
        private bool _isReadyToMove,_isReadyToPlay;
        private float3 _inputValues;
        private float2 _clampValues;
        private GameStates _gameStates;
        private float _colorAreaSpeed = 1;
        
        #endregion

        #endregion
        internal void GetDataFromPlayerManager(PlayerMovementData playerData)
        {
            _playerData = playerData;
        }
        
        internal void OnInputReleased()
        {
            _isReadyToMove = false;
        }

        internal void OnInputTaken()
        {
            _isReadyToMove = true;
        }

        internal void UpdateInputParams(HorizontalInputParams inputParams)
        {
            _inputParams = inputParams;
        }


        internal void IsReadyToPlay(bool isReadyToPlay)
        {
            _isReadyToPlay = isReadyToPlay;
        }

        private void FixedUpdate()
        {
            
            if (_isReadyToPlay)
            {
                movementList.MoveTypeList[(int)_gameStates].DoMovement(ref _colorAreaSpeed, ref _isReadyToMove,
                    ref playerRigidbody, ref _inputParams, ref _playerData);
            }
            else
            {
                Stop();
            }
            
        }

        private void Stop()
        {
            playerRigidbody.velocity = Vector3.zero;
            playerRigidbody.angularVelocity = Vector3.zero;
        }

        public void OnColorAreaInteractWithPlayerManager()
        {
            Stop();
            _colorAreaSpeed = 0;
        }
    }
}