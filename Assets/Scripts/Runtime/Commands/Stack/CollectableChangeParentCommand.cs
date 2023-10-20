using System.Collections.Generic;
using Runtime.Signals;
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
            if (_collectableList.Count <= 1)
            {
                MiniGameSignals.Instance.onMiniGameAreaStartDroneRoutine?.Invoke(colorAreaTransform.transform.parent.gameObject.transform.parent.gameObject);
                
                
            }
        }
    }
}