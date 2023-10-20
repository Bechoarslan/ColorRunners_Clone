using System.Collections.Generic;
using Runtime.Controllers.Collectable;
using Runtime.Data.ValueObject;
using Runtime.Enums.Collectable;
using Runtime.Managers;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Commands.Stack
{
    public class CollectableAdderCommand
    {
        private readonly StackData _stackData;
        private readonly List<GameObject> _collectableList;
        private readonly StackManager _stackManager;
        public CollectableAdderCommand(ref StackData stackData, ref List<GameObject> collectableList, StackManager stackManager)
        {
            _stackData = stackData;
            _collectableList = collectableList;
            _stackManager = stackManager;
        }

        public void Execute(GameObject collectableGameObject)
        {
            collectableGameObject.tag = "Collected";
            collectableGameObject.transform.parent = _stackManager.transform;
            _collectableList.Add(collectableGameObject);
            if (_collectableList.Count > _stackData.StackLimit) { collectableGameObject.SetActive(false);}
            
            var newCollectablePos = _collectableList[^1].transform.position;
            
            collectableGameObject.transform.localPosition = new Vector3(newCollectablePos.x
                ,newCollectablePos.y,newCollectablePos.z - _stackData.StackOffset * _collectableList.Count * 2);
            CoreGameSignals.Instance.onSetCollectableScore?.Invoke((short)_collectableList.Count);
            
        }
    }
}