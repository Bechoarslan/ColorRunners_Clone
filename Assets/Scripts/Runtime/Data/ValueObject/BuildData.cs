using System;
using System.Collections.Generic;
using Runtime.Enums.Collectable;
using Runtime.Enums.Environemnt;
using UnityEngine;

namespace Runtime.Data.ValueObject
{
    [Serializable]
    public struct BuildData
    {
        public EnvironmentEnum EnvironmentType;
        public Material EnvironmentMaterial;
        public Material GardenMaterial;
        public int EnvironmentBuildCost;
        public int EnvironmentGardenCost;
    }
    

    
}