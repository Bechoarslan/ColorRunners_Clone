
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
        
        public UnityAction<GameObject,List<GameObject>,Transform> onDroneColorAreaInteractWithCollectable = delegate {  };
        public UnityAction<GameObject,Transform> onDroneColorAreaSendCollectableToHolder = delegate {  };
        public UnityAction onColorAreaInteractWithPlayerManager = delegate {  };
        public UnityAction<GameObject> onMiniGameAreaStartDroneRoutine = delegate {  };
        
        public UnityAction<GameObject> onMiniGameAreaInteractWithCollectable = delegate {};
        public UnityAction<MiniGameType> onSendMiniGameAreaTypeToListeners = delegate {  };
        public UnityAction onDroneAreaControlPatrolEnd = delegate {  };
        
        
        public UnityAction<ColorType,GameObject,GameObject> onCheckColorCollectableForColorArea = delegate {  };
        
        public UnityAction<List<GameObject>,Transform>  onSetCollectableListToStackManager = delegate {  };
        public UnityAction onSetPlayerMovementReady = delegate {  };

        
        
        
        
    }

}