using System;
using System.Numerics;
using Unity.Mathematics;
using Vector3 = UnityEngine.Vector3;

namespace Runtime.Data.ValueObject
{
    [Serializable]
    public struct InputData
    {
        public float HorizontalInputSpeed;
        public float2 HorizontalInputClampNegativeSides;
        public float HorizontalInputClampStopValue;
    }
}