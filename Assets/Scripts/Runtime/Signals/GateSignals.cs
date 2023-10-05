
using System;

using Runtime.Extentions;
using UnityEngine;
using UnityEngine.Events;


namespace Runtime.Signals
{
    public class GateSignals : MonoSingleton<GateSignals>
    {
        
       public UnityAction<Color> onGetGateColor = delegate {  };
       
        
    }
}