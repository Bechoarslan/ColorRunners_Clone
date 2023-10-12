using System;
using UnityEngine;

namespace Runtime.Data.ValueObject
{
    [Serializable]
    public struct StackData
    {
        [Header("StacData"), Space(10)] 
        public int StackLimit;
        
        [Header("Animation Value"), Space(10)]
        public float StackAnimDuration;
        public float StackScaleValue;
        
        [Header("Lerp Value"), Space(10)]
        public float StackScaleDelay;
        public float StackLerpXDelay;
        public float StackLerpYDelay;
        public float StackLerpZDelay;
        public float StackOffset;
    }
}