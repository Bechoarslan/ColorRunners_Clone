using System.Collections.Generic;
using Runtime.Data.ValueObject;
using UnityEngine;

namespace Runtime.Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Color", menuName = "ColorRunners/CD_Color", order = 0)]
    public class CD_Color : ScriptableObject
    {
        public List<ColorData> PlayerColors;
        public List<ColorData> GroundColors;
        public List<ColorData> GateColors;
    }
}