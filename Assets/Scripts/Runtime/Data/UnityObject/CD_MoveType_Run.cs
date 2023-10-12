using System.Diagnostics.Eventing.Reader;
using Runtime.Data.ValueObject;
using Runtime.Keys;
using UnityEngine;

namespace Runtime.Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_MoveType_Run", menuName = "ColorRunners/CD_MoveType_Run", order = 0)]
    public class CD_MoveType_Run : CD_Move
    {
        public override void DoMovement(ref float colorAreaSpeed, ref bool isReadyToMove, ref Rigidbody rigidbody,
            ref HorizontalInputParams inputParams, ref PlayerMovementData playerMovementData)
        {
            if (isReadyToMove)
            {
                RunMove(ref colorAreaSpeed, ref rigidbody, ref inputParams, ref playerMovementData);
            }
            else
            {
                StopSideWays(ref colorAreaSpeed, ref rigidbody, ref playerMovementData);
            }
        }

        private void RunMove(ref float colorAreaSpeed, ref Rigidbody rigidbody, ref HorizontalInputParams inputParams, ref PlayerMovementData playerMovementData)
        {
            rigidbody.velocity = new Vector3(
                inputParams.Values.x * playerMovementData.SidewaysSpeed,
                Mathf.Clamp(rigidbody.velocity.y,
                    -inputParams.HorizontalInputClampSides.y,
                    inputParams.HorizontalInputClampSides.y),
                playerMovementData.ForwardSpeed * colorAreaSpeed);


            
            rigidbody.position = new Vector3(
                Mathf.Clamp(rigidbody.position.x,
                    -inputParams.HorizontalInputClampSides.x,
                    inputParams.HorizontalInputClampSides.x),
                rigidbody.position.y,
                rigidbody.position.z);
         
        }

        private void StopSideWays(ref float colorAreaSpeed, ref Rigidbody rigidbody, ref PlayerMovementData playerMovementData)
        {
            rigidbody.velocity =
                new Vector3(0,
                    rigidbody.velocity.y,
                    playerMovementData.ForwardSpeed * colorAreaSpeed);
            rigidbody.angularVelocity = Vector3.zero;
        }
    }
}