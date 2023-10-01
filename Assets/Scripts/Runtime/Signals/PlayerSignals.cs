using Runtime.Extentions;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class PlayerSignals : MonoSingleton<PlayerSignals>
    {
        public UnityAction<bool> onPlayConditionChanged = delegate{  };
        public UnityAction<bool> onMoveConditionChanged = delegate{  };
    }
}