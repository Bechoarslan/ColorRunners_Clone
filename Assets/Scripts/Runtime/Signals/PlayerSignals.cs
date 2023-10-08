using System;
using System.Drawing;
using Runtime.Enums;
using Runtime.Extentions;
using UnityEngine;
using UnityEngine.Events;
using Color = UnityEngine.Color;

namespace Runtime.Signals
{
    public class PlayerSignals : MonoSingleton<PlayerSignals>
    {
        public UnityAction<bool> onPlayConditionChanged = delegate{  };
        public UnityAction<bool> onMoveConditionChanged = delegate{  };
        
        public UnityAction<PlayerAnimationStates>  onPlayerAnimationChanged = delegate{  };
        public UnityAction<short> onSendStackScoreToPlayerText = delegate{  };
        
        
        
        public Func<Color> onGetPlayerColor = delegate{ return Color.white; };
        public UnityAction onPlayerSettledToMiniGameArea = delegate {  };
        public UnityAction onPlayerExitMiniGameArea = delegate {  };
        
        
        
        
        
        
        
    }
}