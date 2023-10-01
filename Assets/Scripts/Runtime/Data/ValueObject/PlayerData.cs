using System;
using Unity.VisualScripting;

namespace Runtime.Data.ValueObject
{
    [Serializable]
    public struct PlayerData
    {
        public PlayerMovementData MovementData;
    }

    [Serializable]
    public struct PlayerMovementData
    {
        public float ForwardSpeed;
        public float SidewaysSpeed;
    }
}