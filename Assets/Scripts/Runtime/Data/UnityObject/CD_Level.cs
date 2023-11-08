using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Level", menuName = "ColorRunners/CD_Level", order = 0)]
    public class CD_Level : ScriptableObject
    {
        public List<int> Levels = new List<int>();
    }
}