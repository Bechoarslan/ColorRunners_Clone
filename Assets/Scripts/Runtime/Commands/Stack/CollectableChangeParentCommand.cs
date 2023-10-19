using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Commands.Stack
{
    public class CollectableChangeParentCommand
    {
        private List<GameObject> _collectableList;
        public CollectableChangeParentCommand(ref List<GameObject> collectableList)
        {
            _collectableList = collectableList;
        }

        public void Execute(GameObject collectableObject, List<GameObject> newCollectableList, Transform colorAreaTransform)
        {
            _collectableList.Remove(collectableObject);
            newCollectableList.Add(collectableObject);
            collectableObject.transform.parent = colorAreaTransform.transform;
            _collectableList.TrimExcess();
        }
    }
}