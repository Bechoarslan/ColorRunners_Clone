using System.Collections.Generic;
using Runtime.Enums.Collectable;
using UnityEngine;

namespace Runtime.Commands.Stack
{
    public class CollectableSetAnimationOnPlayCommand
    {
        private CollectableSetAnimationStateCommand _collectableSetAnimationStateCommand;
        public CollectableSetAnimationOnPlayCommand(ref CollectableSetAnimationStateCommand collectableSetAnimationStateCommand)
        {
            _collectableSetAnimationStateCommand = collectableSetAnimationStateCommand;
        }

        public void Execute(List<GameObject> collectableList)
        {
            foreach (var collectable in collectableList)
            {
                _collectableSetAnimationStateCommand.Execute(collectable,CollectableAnimationStates.Run);
            }
        }
    }
}