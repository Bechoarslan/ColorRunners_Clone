using System.Collections.Generic;
using Runtime.Data.ValueObject;
using Runtime.Enums.Collectable;
using Runtime.Enums.Environemnt;
using UnityEngine;

namespace Runtime.Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_BuildData", menuName = "ColorRunners/CD_BuilData", order = 0)]
    public class CD_BuildData : ScriptableObject
    {
        public List<BuildData> EnvironmentDataList;
    }
}