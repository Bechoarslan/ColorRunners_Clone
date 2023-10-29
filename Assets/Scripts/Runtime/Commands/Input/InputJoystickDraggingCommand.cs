using Runtime.Keys;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Commands.Input
{
    public class InputJoystickDraggingCommand
    {
        private Vector3 _joystickPos;
        private Vector3 _moveVector;
        private Joystick _joystick;
        public InputJoystickDraggingCommand(ref Vector3 joystickPos, ref Vector3 moveVector, ref Joystick joystick)
        {
            _joystickPos = joystickPos;
            _moveVector = moveVector;
            _joystick = joystick;
        }

        public void Execute()
        {
            _joystickPos = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical);
            _moveVector = _joystickPos;
            InputSignals.Instance.onInputDragged?.Invoke(new HorizontalInputParams()
            {
                Values = _moveVector
            });
        }
    }
}