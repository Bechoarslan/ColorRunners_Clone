
using DG.Tweening;
using Runtime.Enums.Collectable;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Commands.Stack
{
    public class CollectableMoveToColorAreaHolderCommand
    {
        public void Execute(GameObject collectableObject, Transform colorAreaHolder)
        {
            var randomNumber = Random.Range(-1.2f, 1.2f);
            var newPos = new Vector3(colorAreaHolder.position.x,collectableObject.transform.position.y,colorAreaHolder.position.z + randomNumber);
            collectableObject.transform.DOMove(newPos, 1f).OnComplete(() =>
                {
                    CollectableSignals.Instance.onSetCollectableAnimation?.Invoke(collectableObject, CollectableAnimationStates.Idle);
                }
                    );
        }

        
    }
}