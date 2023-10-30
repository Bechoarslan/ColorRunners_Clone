using Runtime.Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class EnvironmentSignals : MonoSingleton<EnvironmentSignals>
    {
        public UnityAction<GameObject> onPlayerInteractWithEnvironment = delegate {  };
        public UnityAction<GameObject> onPlayerStayInteractWithEnvironment = delegate{  };
        public UnityAction onPlayerExitInteractWithEnvironment = delegate {  };
        public UnityAction<GameObject> onPlayerPaintEnvironment = delegate {  };
        
    }
}