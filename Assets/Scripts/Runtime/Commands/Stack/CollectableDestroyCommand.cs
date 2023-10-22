using System.Collections.Generic;
using DG.Tweening;
using Runtime.Enums.Collectable;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Commands.Stack
{
    public class CollectableDestroyCommand
    {
        private List<GameObject> _collectableList;
        public CollectableDestroyCommand(ref List<GameObject> collectableList)
        {
            _collectableList = collectableList;
        }

        public void Execute(List<GameObject> collectableObject)
        {
            foreach (var colObj in collectableObject)
            {
                CollectableSignals.Instance.onSetCollectableAnimation?.Invoke(colObj,CollectableAnimationStates.Run);
                DOVirtual.DelayedCall(0.5f, () =>
                {
                    colObj.SetActive(false);
                });
              
                
               
            }
            CoreGameSignals.Instance.onSetCollectableScore?.Invoke((short)_collectableList.Count);
           
        }
    }
}