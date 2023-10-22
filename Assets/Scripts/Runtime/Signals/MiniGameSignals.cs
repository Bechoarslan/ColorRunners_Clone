
using System;
using System.Collections.Generic;
using Runtime.Enums.Color;
using Runtime.Enums.MiniGame;
using Runtime.Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class MiniGameSignals : MonoSingleton<MiniGameSignals>
    {
        public UnityAction<GameObject> onPlayerInteractWithMiniGameArea = delegate {  };
        public UnityAction onPlayerExitInteractWithMiniGameArea = delegate {  };
        public UnityAction<MiniGameType> onMiniGameAreaSendToMiniGameTypeToListeners = delegate {  }; 
        public UnityAction<GameObject,Transform> onCollectableInteractWithCollectableArea = delegate {  };
        
        
        
        
        
        
        
      

        
        
        
        
    }

}