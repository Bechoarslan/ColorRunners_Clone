
using Runtime.Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class ColorAreaSignals :  MonoSingleton<ColorAreaSignals>
    {
        public UnityAction<bool> onIsPlayerColorCorrect = delegate {  };
        public UnityAction<Transform> onSendMiniGameHolder = delegate {  };

    }
}