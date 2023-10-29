using Runtime.Data.ValueObject;
using Runtime.Keys;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Runtime.Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Joystick", menuName = "ColorRunners/CD_Joystick", order = 0)]
    public class CD_MoveType_Joystick : CD_Move
    {
        public override void DoMovement(ref float colorAreaSpeed, ref
                bool isReadyToMove, ref Rigidbody rigidbody,
            ref HorizontalInputParams inputParams, ref PlayerMovementData playerMovementData)
        {
            JoyStickMove(ref rigidbody,ref playerMovementData,ref inputParams);
        }

        private void JoyStickMove(ref Rigidbody rigidbody,
            ref PlayerMovementData playerMovementData, ref HorizontalInputParams inputParams)
        {
            Vector3 _playerMovement = new Vector3(inputParams.Values.x * playerMovementData.JoystickSpeed, 0
                , inputParams.Values.z * playerMovementData.JoystickSpeed);
            rigidbody.velocity = _playerMovement;
            if(_playerMovement != Vector3.zero)
            {
                Quaternion _newDirect = Quaternion.LookRotation(_playerMovement);
                rigidbody.transform.GetChild(0).rotation = _newDirect;
            }

        }
    }
}