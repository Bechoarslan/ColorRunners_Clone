using Runtime.Enums;
using Runtime.Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class MiniGameSignals : MonoSingleton<MiniGameSignals>
    {
        public UnityAction<MiniGameType> onGetMiniGameType = delegate{  };
        public UnityAction<GameObject> onInteractionWithCollectable = delegate {  };
        public UnityAction<GameObject> onExitInteractionWithCollectable = delegate {  };
    }
}