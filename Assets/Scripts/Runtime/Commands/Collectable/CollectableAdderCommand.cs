using System.Collections.Generic;
using Runtime.Data.ValueObject;
using Runtime.Managers;
using UnityEngine;

namespace Runtime.Commands.Collectable
{
    public class CollectableAdderCommand
    {
        private  StackData _stackData;
        private  List<GameObject> _collectableList;
        private  StackManager _stackManager;
        public CollectableAdderCommand(ref StackData stackData, ref List<GameObject> collectableList, StackManager stackManager)
        {
            _stackData = stackData;
            _collectableList = collectableList;
            _stackManager = stackManager;
        }

        public void Execute(GameObject collectableGameObject)
        {
            collectableGameObject.transform.SetParent(_stackManager.transform);
            _collectableList.Add(collectableGameObject);
            if (_collectableList.Count > _stackData.StackLimit) collectableGameObject.SetActive(true);
            
            var newCollectablePos = _collectableList[^1].transform.position;
            
            collectableGameObject.transform.localPosition = new Vector3(newCollectablePos.x
                ,newCollectablePos.y,newCollectablePos.z - _stackData.StackOffset * _collectableList.Count * 2);
            
                
            
            
        }
    }
}