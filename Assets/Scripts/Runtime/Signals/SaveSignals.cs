using System;
using Runtime.Extentions;
using Runtime.Keys;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class SaveSignals : MonoSingleton<SaveSignals>
    {
        public UnityAction onSaveData = delegate {  };
        public Func<EnvironmentDataParams> onGetEnvironmetDatas = delegate { return default; };
        public UnityAction<EnvironmentDataParams> onLoadEnvironmentDatas = delegate { };
        
        public Func<LevelDataParams> onGetLevelDatas = delegate { return default; };
        public UnityAction<LevelDataParams> onLoadLevelDatas = delegate{  };
        
        
        
    }
}