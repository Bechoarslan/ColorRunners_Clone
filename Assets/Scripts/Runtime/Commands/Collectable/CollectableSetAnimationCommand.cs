using Runtime.Enums.Collectable;
using UnityEngine;

namespace Runtime.Commands.Collectable
{
    public class CollectableSetAnimationCommand
    {
        private Animator _collectableAnimator;
        public CollectableSetAnimationCommand(ref Animator collectableAnimator)
        {
            _collectableAnimator = collectableAnimator;
        }

        public void Execute(CollectableAnimationStates animState)
        {
            _collectableAnimator.SetTrigger(animState.ToString());
        }
    }
}