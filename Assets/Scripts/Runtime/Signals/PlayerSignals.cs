
using System;
using Runtime.Data.ValueObject;
using Runtime.Enums;
using Runtime.Enums.Color;
using Runtime.Extentions;
using UnityEngine;
using UnityEngine.Events;


namespace Runtime.Signals
{
    public class PlayerSignals : MonoSingleton<PlayerSignals>
    {
        public UnityAction<bool> onPlayConditionChanged = delegate{  };
        public UnityAction<bool> onMoveConditionChanged = delegate{  };
        public UnityAction<short> onSendStackScoreToPlayerText = delegate{  };
        
        
        public UnityAction<PlayerAnimationStates> onSetPlayerAnimationState = delegate {  };
        public Func<ColorType> onGetPlayerColor = delegate { return ColorType.Brown; };
        public UnityAction<ColorType> onSetPlayerColor = delegate {  };
        
    }
}