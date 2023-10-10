using System;
using Runtime.Enums;
using Runtime.Enums.Color;
using Runtime.Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class CollectableSignals : MonoSingleton<CollectableSignals>
    {
        public UnityAction<PlayerAnimationStates> onSetCollectableAnimationState = delegate{  };
        public UnityAction<bool> onIsPlayerColorSame = delegate{  };
        
    }
}