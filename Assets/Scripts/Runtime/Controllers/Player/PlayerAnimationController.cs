
using Runtime.Enums;
using UnityEngine;
namespace Runtime.Controllers.Player
{
    public class PlayerAnimationController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Animator animator;

        #endregion

        #endregion

        public void SetAnimationState(PlayerAnimationStates animState)
        {
            animator.SetTrigger(animState.ToString());
        }

        public void OnReset()
        {
            animator.SetTrigger(PlayerAnimationStates.Idle.ToString());
        }
    }
}