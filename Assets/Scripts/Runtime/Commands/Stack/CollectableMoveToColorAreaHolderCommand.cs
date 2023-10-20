
using System.Collections.Generic;
using DG.Tweening;
using Runtime.Enums.Collectable;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Commands.Stack
{
    public class CollectableMoveToColorAreaHolderCommand
    {
        private List<GameObject> _collectableList;
        public CollectableMoveToColorAreaHolderCommand(ref List<GameObject> collectableList)
        {
            _collectableList = collectableList;
        }

        public void Execute(GameObject collectableObject, Transform colorAreaHolder)
        {
            var collectableRenderer = collectableObject.GetComponentInChildren<SkinnedMeshRenderer>();
            var randomNumber = Random.Range(-1.2f, 1.2f);
            var newPos = new Vector3(colorAreaHolder.position.x,collectableObject.transform.position.y,colorAreaHolder.position.z + randomNumber);
            collectableObject.transform.DOMove(newPos, 1f).SetEase(Ease.Linear).OnComplete(() =>
                {
                    collectableRenderer.material.DOFloat(0,"_OutlineWidth",1f);
                    CollectableSignals.Instance.onSetCollectableAnimation?.Invoke(collectableObject, CollectableAnimationStates.Idle);
                     
                    
                }
                    );
        }

        
    }
}