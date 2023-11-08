using System.Collections.Generic;
using Runtime.Data.ValueObject;
using UnityEngine;

namespace Runtime.Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_IdleData", menuName = "ColorRunners/CD_IdleData", order = 0)]
    public class CD_IdleData : ScriptableObject
    {
        public List<IdleData> IdleDataList;
    }
}