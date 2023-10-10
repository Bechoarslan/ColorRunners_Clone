using System.Security.Claims;
using Runtime.Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class ColorCheckSignals : MonoSingleton<ColorCheckSignals>
    {
        public UnityAction<Transform> onSendCheckAreaHolderTransform = delegate{  };
    }
}