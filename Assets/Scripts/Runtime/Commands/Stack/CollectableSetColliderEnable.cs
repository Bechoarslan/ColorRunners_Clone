using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Commands.Stack
{
    public class CollectableSetColliderEnable
    {
        private  List<GameObject> _collectableLists;
        public CollectableSetColliderEnable(ref List<GameObject> collectableList)
        {
            _collectableLists = collectableList;
        }

        public void Execute()
        {
            foreach (var col in _collectableLists)
            {
                Debug.LogWarning(col.GetComponentInChildren<CapsuleCollider>().name);
                col.GetComponentInChildren<CapsuleCollider>().enabled = true;
                
            }
        }
    }
}