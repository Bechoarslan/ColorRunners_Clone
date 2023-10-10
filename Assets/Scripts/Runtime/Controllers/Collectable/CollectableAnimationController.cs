using Runtime.Enums;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Controllers.Collectable
{
    public class CollectableAnimationController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Animator animator;

        #endregion

        #endregion
        

        internal void SetAnimationState(PlayerAnimationStates state)
        {
                animator.SetTrigger(state.ToString());
                
        }


        
    }
}