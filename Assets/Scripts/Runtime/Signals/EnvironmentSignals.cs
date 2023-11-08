using System;
using Runtime.Data.ValueObject;
using Runtime.Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class EnvironmentSignals : MonoSingleton<EnvironmentSignals>
    {
        public UnityAction<GameObject> onPlayerInteractWithEnvironment = delegate {  };
        public UnityAction onPlayerStayInteractWithEnvironment = delegate{  };
        public UnityAction onPlayerExitInteractWithEnvironment = delegate {  };
        public UnityAction onPlayerPaintEnvironment = delegate {  };
        public UnityAction onCityComplete = delegate {  };
        
        
        public UnityAction onEnvironmentCompleted = delegate {  };
        public UnityAction<int,AreaData> onSetAreaData = delegate {  };
        public Func<int,AreaData> onGetAreaData = delegate { return default;};
        
        public UnityAction onPrepareEnvironmentWithSave = delegate {  };
        public UnityAction onRefreshEnvironmentData = delegate {  };
        
    }
}