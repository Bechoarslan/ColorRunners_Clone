using System;
using UnityEngine;

namespace Runtime.Data.ValueObject
{
    [Serializable]
    public struct PoolData
    {
        public string ObjName;
        public GameObject Pref;
        public int ObjectCount;

    }
}