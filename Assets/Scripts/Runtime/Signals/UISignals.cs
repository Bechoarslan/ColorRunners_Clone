
using Runtime.Extentions;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class UISignals : MonoSingleton<UISignals>
    {
        public UnityAction onUpdateThrowableCount = delegate { };
        public UnityAction onUpdateLeftEnemyCount = delegate { };
        public UnityAction onSetNewLevelValue = delegate { };
    }
}