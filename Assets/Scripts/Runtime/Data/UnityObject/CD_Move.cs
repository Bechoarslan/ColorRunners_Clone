using Runtime.Data.ValueObject;
using Runtime.Keys;
using UnityEngine;

namespace Runtime.Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Move", menuName = "ColorRunners/CD_Move", order = 0)]
    public abstract class CD_Move : ScriptableObject
    {
        public abstract void DoMovement(ref float colorAreaSpeed, ref bool isReadyToMove, ref Rigidbody rigidbody,
            ref HorizontalInputParams inputParams, ref PlayerMovementData playerMovementData);

    }
}