using Runtime.Enums;
using UnityEngine;

namespace Runtime.Commands.Collectable
{
    public class CollectableAnimationCommand
    {
        private readonly Animator _collectableAnimator;
        public CollectableAnimationCommand(ref Animator collectableAnimator)
        {
            _collectableAnimator = collectableAnimator;
        }

        public void Execute(PlayerAnimationStates state)
        {
            _collectableAnimator.SetTrigger(state.ToString());
        }
    }
}