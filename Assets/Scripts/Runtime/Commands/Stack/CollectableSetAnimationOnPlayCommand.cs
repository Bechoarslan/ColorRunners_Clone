using System.Collections.Generic;
using Runtime.Enums.Collectable;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Commands.Stack
{
    public class CollectableSetAnimationOnPlayCommand
    {
        

        public void Execute(List<GameObject> collectableList)
        {
            foreach (var collectable in collectableList)
            {
                CollectableSignals.Instance.onSetCollectableAnimation(collectable, CollectableAnimationStates.Run);
            }
        }
    }
}