
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
        public UnityAction onPlayerReadyToGo = delegate {  };
        public UnityAction<MiniGameType> onMiniGameAreaSendToMiniGameTypeToListeners = delegate {  }; 
        public UnityAction<GameObject,Transform> onCollectableInteractWithCollectableArea = delegate {  };
        public UnityAction<GameObject> onCollectableExitInteractWithColorArea = delegate {  };
        public UnityAction onPlayerExitInteractWithMiniGameArea = delegate {  };
        
        public UnityAction<List<GameObject>,Transform> onPlayMiniGameDroneArea = delegate {  };
        public UnityAction onCheckCollectableListIsEmpty = delegate {  };
        
        public UnityAction<List<GameObject>,bool> onTurretMiniGamePlay = delegate {  };




        public UnityAction<GameObject> onPlayDroneAnimation = delegate {  };
        
      

        
        
        
        
    }

}