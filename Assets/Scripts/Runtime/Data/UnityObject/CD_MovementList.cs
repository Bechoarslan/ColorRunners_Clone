using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_MovementList", menuName = "ColorRunners/CD_MovementList", order = 0)]
    public class CD_MovementList : ScriptableObject
    {
        public List<CD_Move> MoveTypeList;
    }
}