using Runtime.Enums;
using Runtime.Managers;
using UnityEngine;

namespace Runtime.Commands.Stack
{
    public class CollectableAnimationCommand
    {
        public void Execute(ref GameObject collectableGameObject, PlayerAnimationStates playerAnimationStates)
        {
            var collectableManager = collectableGameObject.GetComponent<CollectableManager>();
            collectableManager.OnSetCollectableAnimationState(playerAnimationStates);
            
        }
    }
}