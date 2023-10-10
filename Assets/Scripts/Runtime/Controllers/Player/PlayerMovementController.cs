using System;
using DG.Tweening;
using Runtime.Data.ValueObject;
using Runtime.Keys;
using Runtime.Managers;
using Runtime.Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Runtime.Controllers.Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private new Rigidbody rigidbody;
        [SerializeField] private PlayerManager manager;
        [SerializeField] private float waitForSecond;

        #endregion

        #region Private Variables

        private PlayerMovementData _data;
        [ShowInInspector] private bool _isReadyToMove, _isReadyToPlay,_isReadyToStack;
        [ShowInInspector] private float _inputValue;
        [ShowInInspector] private Vector2 _clampValues;
        [ShowInInspector] private bool _onExitMiniGame;
       
        #endregion

        #endregion
        internal void SetMovementData(PlayerMovementData dataMovementData)
        {
            _data = dataMovementData;

        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            PlayerSignals.Instance.onPlayConditionChanged += OnPlayConditionChanged;
            PlayerSignals.Instance.onMoveConditionChanged += OnMoveConditionChanged;
           
           
        }
        
        private void OnMoveConditionChanged(bool condition) => _isReadyToMove = condition;
        private void OnPlayConditionChanged(bool condition) => _isReadyToPlay = condition;
        
        private void UnSubscribeEvents()
        {
            PlayerSignals.Instance.onPlayConditionChanged -= OnPlayConditionChanged;
            PlayerSignals.Instance.onMoveConditionChanged -= OnMoveConditionChanged;
            
        }
        

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
        
        public void UpdateInputValue(HorizontalnputParams inputParams)
        {
            _inputValue = inputParams.HorizontalInputValue;
            _clampValues = inputParams.HorizontalInputClampSides;
        }
        private void FixedUpdate()
        {
            if (_isReadyToPlay)
            {
                
                if (_isReadyToMove)
                {
                    Move();
                }
                else
                {
                    StopSideways();
                }
            }
            else
            {
                Stop();
            }
        }
        private void Update()
        {
            manager.SetStackPosition();
        }
        private void Move()
        {
            var velocity = rigidbody.velocity;
            velocity = new Vector3(_inputValue * _data.SidewaysSpeed, velocity.y, _data.ForwardSpeed);
            rigidbody.velocity = velocity;
            Vector3 position;
            position = new Vector3(Mathf.Clamp(rigidbody.position.x, _clampValues.x, _clampValues.y),
                (position = rigidbody.position).y, position.z);
            rigidbody.position = position;
        }
        private void StopSideways()
        {
            rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, _data.ForwardSpeed);
            rigidbody.angularVelocity = Vector3.zero;
        }
        private void Stop()
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }
        public void OnReset()
        {
            Stop();
            _isReadyToMove = false;
            _isReadyToPlay = false;
        }
        public void SetForwardSpeed(float movementDataSlowSpeed)
        {
            _data.ForwardSpeed = movementDataSlowSpeed;
        }
        
        internal void OnSendPlayerToCheckAreaHolder(Transform holderTransform)
        {
            if (holderTransform == null) return;
            var randomPos = Random.Range(-1.2f, 1.2f);
            var playerPos = rigidbody.position;
            var holderPos = holderTransform.position;
            var movePos = new Vector3(holderPos.x, playerPos.y, holderPos.z + randomPos);
            transform.DOMove(movePos, 1f);
        }
    }
    }
    
