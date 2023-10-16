using RootMotion;
using Runtime.Enums.MiniGame;
using Runtime.Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class MiniGameSignals : MonoSingleton<MiniGameSignals>
    {
        
        public UnityAction<MiniGameType> onSendMiniGameAreaType = delegate{  };
        public UnityAction<GameObject> onCollectableInteractWithColorCheckArea = delegate {  };
        public UnityAction<GameObject> onCollectableExitFromColorCheckArea = delegate {  };
        public UnityAction<GameObject,GameObject> onCheckColorTypes = delegate{  };
        public UnityAction<GameObject,GameObject> onCollectableIsNotSameColorWithColorArea = delegate{  };
        
        
    }
}