using System.Collections.Generic;
using Runtime.Data.ValueObject;
using Runtime.Enums.Collectable;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Commands.Stack
{
    public class CollectableSetVisibleUnVisibleCollectableCommand
    {
        private readonly List<GameObject> _collectableList;
        private readonly StackData _stackData;
        public CollectableSetVisibleUnVisibleCollectableCommand(ref List<GameObject> collectableList,
            ref StackData stackData)
        {
            _collectableList = collectableList;
            _stackData = stackData;
        }

        public void Execute(bool condition)
        {
            switch (condition)
            {
                case true:
                    for (int i = _stackData.StackLimit; i < _collectableList.Count  ; i++)
                    {
                        _collectableList[i].SetActive(true);
                        CollectableSignals.Instance.onSetCollectableAnimation?.Invoke(_collectableList[i],CollectableAnimationStates.Run);
                    }
                    break;
                
            }
        }
    }
}