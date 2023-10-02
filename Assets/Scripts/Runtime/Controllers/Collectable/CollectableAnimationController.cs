using System;
using Runtime.Enums;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Controllers.Collectable
{
    public class CollectableAnimationController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Animator collectableAnimator;

        #endregion

        #endregion
        

        internal void SetAnimationState(PlayerAnimationStates run)
        {
            collectableAnimator.SetTrigger(run.ToString());
        }
    }
}