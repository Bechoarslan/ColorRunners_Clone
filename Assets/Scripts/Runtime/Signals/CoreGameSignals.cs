using System;
using Runtime.Enums;
using Runtime.Enums.Collectable;
using Runtime.Extentions;
using Runtime.Keys;
using Runtime.Managers;
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
        
        
        public UnityAction<short> onSetCollectableScore = delegate {  };
        public UnityAction onPlayerInteractWithEndArea = delegate {  };
        public UnityAction onPlayerExitInteractWithEndArea = delegate {  };
        public UnityAction onSetPlayerScale = delegate {  };
        
        public Func<short> onSendCollectableScore = delegate { return 0; };
        
        
        public UnityAction<bool> onSetPlayerAnimation = delegate {  };
        
        
      
        
       
      
        
       
        
    }
}