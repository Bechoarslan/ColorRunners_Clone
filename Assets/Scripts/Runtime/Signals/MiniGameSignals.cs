
using System.Collections.Generic;
using Runtime.Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class MiniGameSignals : MonoSingleton<MiniGameSignals>
    {
        
        public UnityAction<GameObject,List<GameObject>,Transform> onColorAreaInteractWithCollectable = delegate {  };
        public UnityAction<GameObject,Transform> onColorAreaSendCollectableToHolder = delegate {  };
        public UnityAction onColorAreaInteractWithPlayerManager = delegate {  };

        
        
        
        
    }
}