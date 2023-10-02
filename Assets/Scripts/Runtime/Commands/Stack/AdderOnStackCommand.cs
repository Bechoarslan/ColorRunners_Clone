using System.Collections.Generic;
using Runtime.Data.ValueObject;
using Runtime.Managers;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Commands.Stack
{
    public class AdderOnStackCommand
    {
        private StackManager _stackManager;
        private List<GameObject> _collectableList;
        private StackData _stackData;
        public AdderOnStackCommand(StackManager stackManager, ref List<GameObject> collectableList, ref StackData stackData)
        {
            _stackManager = stackManager;
            _collectableList = collectableList;
            _stackData = stackData;
        }

        public void Execute(GameObject collectableGameObject)
        {
            if (_collectableList.Count <= 0)
            {
                
                _collectableList.Add(collectableGameObject);
                collectableGameObject.transform.SetParent(_stackManager.transform);
                collectableGameObject.transform.localPosition = new Vector3(0f, 1f, 0.335f);
                CollectableSignals.Instance.onCheckCollectableIsCurrent?.Invoke(collectableGameObject);
            }
            else
            {
             
                collectableGameObject.transform.SetParent(_stackManager.transform);
                Vector3 newPos = _collectableList[_collectableList.Count - 1].transform.localPosition;
                newPos.z -= _stackData.CollectableOffsetInStack;
                collectableGameObject.transform.localPosition = newPos;
                _collectableList.Add(collectableGameObject);
                CollectableSignals.Instance.onCheckCollectableIsCurrent?.Invoke(collectableGameObject);
            }
        }
    }
}