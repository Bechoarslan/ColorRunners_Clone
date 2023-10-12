
using Runtime.Commands.Input;
using Runtime.Data.UnityObject;
using Runtime.Data.ValueObject;
using Runtime.Enums;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Managers
{
    
    public class InputManager : MonoBehaviour
    {
        
        #region Self Variables

        #region Public Variables

        public Vector3? MousePosition;

        #endregion
        
        #region Serialized Variables
        [SerializeField] private bool _isAvailableForTouch;
        [SerializeField] private bool _isFirstTimeTouchTaken;
        [SerializeField] private InputManager inputManager;
        

        #endregion

        #region Private Variables
        [Header("Data")] private InputData _data;
        private bool _isTouching;
        private float _currentVelocity; //ref type
        private Vector3 _moveVector; //ref type
        private InputDraggedOverCommand _inputDraggedOverCommand;
        private InputStartedDraggedCommand _inputStartedDraggedCommand;
        private InputDraggingCommand _inputDraggingCommand;

        #endregion

        #endregion

        private void Awake()
        {
            _data = GetInputData();
            Init();
            
        }

        private void Init()
        {
            _inputDraggedOverCommand = new InputDraggedOverCommand(ref _moveVector);
            _inputStartedDraggedCommand = new InputStartedDraggedCommand(ref _isFirstTimeTouchTaken, ref inputManager);
            _inputDraggingCommand = new InputDraggingCommand(ref _data, ref _moveVector, ref _currentVelocity,ref inputManager);
        }

        private InputData GetInputData() => Resources.Load<CD_Input>("Data/CD_Input").Data;
        

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onChangeGameStates += OnChangeGameState;
        }
        private void OnPlay()
        {
            _isAvailableForTouch = true;
        }
       

        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onChangeGameStates -= OnChangeGameState;
        }

        private void OnChangeGameState(GameStates gameState)
        {
            if (gameState == GameStates.Idle)
            {
                
            }
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }


        private void Update()
        {
            if (!_isAvailableForTouch) return;
            if (Input.GetMouseButtonUp(0))
            {
                _isTouching = false;
                _inputDraggedOverCommand.Execute();
                
            }

            if (Input.GetMouseButtonDown(0))
            {
                _isTouching = true;
                _inputStartedDraggedCommand.Execute();
            }

            if (Input.GetMouseButton(0))
            {
                if (_isTouching)
                {
                    if (MousePosition != null)
                    {
                       _inputDraggingCommand.Execute();
                    }
                }
            }
        }
        

        private void OnReset()
        {
            _isTouching = false;
            _isFirstTimeTouchTaken = false;
            _isAvailableForTouch = false;
        }
    }
}