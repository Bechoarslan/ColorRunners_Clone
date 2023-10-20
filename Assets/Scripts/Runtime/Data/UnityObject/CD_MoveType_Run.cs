using System.Diagnostics.Eventing.Reader;
using Runtime.Data.ValueObject;
using Runtime.Keys;
using UnityEngine;

namespace Runtime.Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_MoveType_Run", menuName = "ColorRunners/CD_MoveType_Run", order = 0)]
    public class CD_MoveType_Run : CD_Move
    {
        public override void DoMovement(ref float _colorAreaSpeed,
            ref bool _isReadyToMove,
            ref Rigidbody _rigidbody,
            ref HorizontalInputParams _inputParams,
            ref PlayerMovementData _playerMovementData)
        {
            if (_isReadyToMove)
                SwerveMove(ref _colorAreaSpeed,
                    ref _rigidbody,
                    ref _playerMovementData,
                    ref _inputParams);
            else
                StopSideways(ref _colorAreaSpeed,
                    ref _rigidbody,
                    ref _playerMovementData);
        }

        private void SwerveMove(ref float _colorAreaSpeed,
            ref Rigidbody _rigidbody,
            ref PlayerMovementData _playerMovementData,
            ref HorizontalInputParams _inputParams)
        {
            _rigidbody.velocity = new Vector3(
                _inputParams.Values.x * _playerMovementData.SidewaysSpeed,
                _rigidbody.velocity.y,
                _playerMovementData.ForwardSpeed * _colorAreaSpeed);


            _rigidbody.position = new Vector3(
                Mathf.Clamp(_rigidbody.position.x,
                    -_inputParams.HorizontalInputClampSides.x,
                    _inputParams.HorizontalInputClampSides.x),
                _rigidbody.position.y,
                _rigidbody.position.z);
        }

        private void StopSideways(ref float _colorAreaSpeed,
            ref Rigidbody _rigidbody,
            ref PlayerMovementData _playerMovementData)
        {
            _rigidbody.velocity =
                new Vector3(0,
                    _rigidbody.velocity.y,
                    _playerMovementData.ForwardSpeed * _colorAreaSpeed);
            _rigidbody.angularVelocity = Vector3.zero;
        }
    }
}