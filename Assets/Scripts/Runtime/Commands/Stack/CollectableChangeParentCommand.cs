using System.Collections.Generic;
using DG.Tweening;
using Runtime.Managers;
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

        public void Execute(GameObject collectableObject, Transform colorAreaObj, bool isColorsSame)
        {
            var colorAreaManager = colorAreaObj.GetComponentInParent<ColorAreaManager>();
            var miniGameManager = colorAreaManager.GetComponentInParent<MiniGameManager>();
            _collectableList.Remove(collectableObject);
            if (isColorsSame)
            {
                colorAreaManager.correctColorList.Add(collectableObject);
            }
            else
            {
                colorAreaManager.falseColorList.Add(collectableObject);
            }
            collectableObject.transform.parent = colorAreaObj;
            _collectableList.TrimExcess();
            if (_collectableList.Count <= 1)
            {
                DOVirtual.DelayedCall(1f,() =>
                { 
                    miniGameManager.PlayDrone();

                });
               
                
            }
        }
    }
}