using System;
using Runtime.Enums.Collectable;
using Runtime.Enums.Color;
using Runtime.Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class CollectableSignals : MonoSingleton<CollectableSignals>
    {
        public UnityAction<GameObject> onCollectableInteractWithCollectable = delegate{  };
        
        
        public UnityAction<GameObject> onCollectableInteractWithGate = delegate{  };
        public UnityAction<ColorType> onSendGateColorType = delegate {  };
        
        
        
        
        
        
        public UnityAction<GameObject,GameObject> onCheckCollectablesColors = delegate { };
        public UnityAction<bool> onSendIsSameColorCondition = delegate {  };
        
        
        public UnityAction<CollectableAnimationStates> onSetCollectableAnimation = delegate{  };
        
        
        
        
        
    }
}