using Runtime.Data.ValueObject;
using Runtime.Keys;
using Runtime.Managers;
using Runtime.Signals;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Runtime.Commands.Input
{
    public class InputDraggingCommand
    {
        private readonly InputData _data;
        private Vector3 _moveVector;
        private float _currentVelocity;
        private readonly InputManager _inputManager;
       

        public InputDraggingCommand(ref InputData data, ref Vector3 moveVector, ref float currentVelocity,
            ref InputManager inputManager)
        {
            _data = data;
            _moveVector = moveVector;
            _currentVelocity = currentVelocity;
            _inputManager = inputManager;
            
        }

        public void Execute()
        {
            var mouseDeltaPos = UnityEngine.Input.mousePosition - _inputManager.MousePosition.Value;
            if (mouseDeltaPos.x > _data.HorizontalInputSpeed)
                _moveVector.x = _data.HorizontalInputSpeed / 10f * mouseDeltaPos.x;
            else if (mouseDeltaPos.x < -_data.HorizontalInputSpeed)
                _moveVector.x = -_data.HorizontalInputSpeed / 10f * -mouseDeltaPos.x;
            else
                _moveVector.x = Mathf.SmoothDamp(_moveVector.x, 0f, ref _currentVelocity,
                    _data.HorizontalInputClampStopValue);

            _inputManager.MousePosition = UnityEngine.Input.mousePosition;

            InputSignals.Instance.onInputDragged?.Invoke(new HorizontalInputParams()
            {
                Values = _moveVector,
                HorizontalInputClampSides = new Vector2(_data.HorizontalInputClampNegativeSides.x,
                    _data.HorizontalInputClampNegativeSides.y)
            });
            
        }
    }
}