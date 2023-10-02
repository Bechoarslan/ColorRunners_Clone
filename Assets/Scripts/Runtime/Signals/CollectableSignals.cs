using Runtime.Enums;
using Runtime.Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class CollectableSignals : MonoSingleton<CollectableSignals>
    {
        public UnityAction<GameObject> onCheckCollectableIsCurrent;
        public UnityAction<PlayerAnimationStates> onCollectableAnimationStateChange;
        
    }
}