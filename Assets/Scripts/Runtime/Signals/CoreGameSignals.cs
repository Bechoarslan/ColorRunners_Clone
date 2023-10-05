using System;
using Runtime.Enums;
using Runtime.Extentions;
using Runtime.Keys;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class CoreGameSignals : MonoSingleton<CoreGameSignals>
    {
        public UnityAction<GameStates> onChangeGameStates = delegate { };
        public UnityAction<int> onLevelInitialize = delegate { };
        public UnityAction onClearActiveLevel = delegate { };
        public UnityAction<GameSaveDataParams> onSaveGameData = delegate { };
        public UnityAction onLevelSuccessful = delegate { };
        public UnityAction onLevelFailed = delegate { };
        public UnityAction onNextLevel = delegate { };
        public UnityAction onRestartLevel = delegate { };
        public UnityAction onPlay = delegate { };
        public UnityAction onReset = delegate { };
        public Func<int> onGetLevelID = delegate { return 0; };
        
        
        public UnityAction<GameObject> onInteractionWithGate = delegate {  };
        
       
        
    }
}