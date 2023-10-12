using Runtime.Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class CollectableSignals : MonoSingleton<CollectableSignals>
    {
        public UnityAction<GameObject> onCollectableInteractWithCollectable = delegate{  };
        
    }
}