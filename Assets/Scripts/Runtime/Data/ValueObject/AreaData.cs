using System;
using Runtime.Enums.Environemnt;
using UnityEngine.Serialization;

namespace Runtime.Data.ValueObject
{
    [Serializable]
    public struct AreaData
    {
        public float GardenValue;
        public float BuildValue;
         public StageType EnvironmentStageType;
    }
}