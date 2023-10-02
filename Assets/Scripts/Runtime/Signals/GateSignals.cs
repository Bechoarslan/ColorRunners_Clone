using System;
using Runtime.Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class GateSignals : MonoSingleton<GateSignals>
    {
        public Func<Color32> onGetGateColor = delegate { return Color.white; };
        public UnityAction<Color32> onSetGateColor = delegate {  };
    }
}