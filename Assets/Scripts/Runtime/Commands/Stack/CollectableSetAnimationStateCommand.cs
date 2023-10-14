using Runtime.Enums.Collectable;
using Runtime.Managers;
using UnityEngine;

namespace Runtime.Commands.Stack
{
    public class CollectableSetAnimationStateCommand
    {
        public void Execute(GameObject collectableObject, CollectableAnimationStates collectableAnimationStates)
        {
            var manager = collectableObject.GetComponent<CollectableManager>();
            manager.OnSetCollectableAnimation(collectableAnimationStates);
        }
    }
}