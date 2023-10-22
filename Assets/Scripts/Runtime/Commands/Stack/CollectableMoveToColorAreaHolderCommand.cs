
using System.Collections.Generic;
using DG.Tweening;
using Runtime.Enums.Collectable;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Commands.Stack
{
    public class CollectableMoveToColorAreaHolderCommand
    {
        

       

        public void Execute(GameObject collectableObj, Transform colorAreaObj)
        {
            
            var collectableRenderer = collectableObj.GetComponentInChildren<SkinnedMeshRenderer>();
            collectableObj.GetComponentInChildren<CapsuleCollider>().enabled = false;
            var randomNumber = Random.Range(-1.2f, 1.2f);
            
                var newPos = new Vector3(colorAreaObj.position.x,collectableObj.transform.position.y, colorAreaObj.position.z+ randomNumber);
                
                collectableObj.transform.DOMove(newPos, 1f).SetEase(Ease.Linear).OnComplete(() =>
                    {
                        collectableRenderer.material.DOFloat(0,"_OutlineWidth",1f);
                        CollectableSignals.Instance.onSetCollectableAnimation?.Invoke(collectableObj, CollectableAnimationStates.Idle);
                     
                    
                    }
                );
            
        }
    }
}