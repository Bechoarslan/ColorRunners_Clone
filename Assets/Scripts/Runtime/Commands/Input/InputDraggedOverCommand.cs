using Runtime.Keys;
using Runtime.Managers;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Commands.Input
{
    public class InputDraggedOverCommand
    {
        private Vector3 _moveVector;
        public InputDraggedOverCommand(ref Vector3 moveVector)
        {
            _moveVector = moveVector;
        }
        

        public void Execute()
        {
            InputSignals.Instance.onInputReleased?.Invoke();
            _moveVector = Vector3.zero;
            InputSignals.Instance.onInputDragged?.Invoke(new HorizontalInputParams
            {
                Values = _moveVector
            });
        }
    }
}