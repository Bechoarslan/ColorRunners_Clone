using System;
using System.Collections.Generic;
using Runtime.Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class StackSignals : MonoSingleton<StackSignals>
    {
        public UnityAction<GameObject> onInteractionWithCollectable = delegate{ };
        public UnityAction<Vector2> onStackFollowPlayer = delegate{  };
        
        
      
        
    }
}