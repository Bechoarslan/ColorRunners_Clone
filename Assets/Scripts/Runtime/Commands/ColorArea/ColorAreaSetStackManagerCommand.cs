using System.Collections.Generic;
using DG.Tweening;
using Runtime.Enums.Collectable;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Commands.ColorCheck
{
    public class ColorAreaSetStackManagerCommand
    {
        private List<GameObject> _correctColorList;
        public ColorAreaSetStackManagerCommand(ref List<GameObject> correctColorList)
        {
            _correctColorList = correctColorList;
        }


        public void Execute(List<GameObject> collectableList, Transform stackManagerTransform)
        {
            if (_correctColorList.Count <= 0) return;
            for (var i = _correctColorList.Count; i > 0  ; i--)
            {
                var collectableObject = _correctColorList[i - 1];
                var collectableRenderer = collectableObject.GetComponentInChildren<SkinnedMeshRenderer>();
                collectableRenderer.material.DOFloat(4.5f,"_OutlineWidth",1f);
                collectableObject.transform.parent = stackManagerTransform;
                _correctColorList.Remove(collectableObject);
                collectableList.Add(collectableObject);
                _correctColorList.TrimExcess();
                CollectableSignals.Instance.onSetCollectableAnimation?.Invoke(collectableObject,CollectableAnimationStates.Run);
                
            }
            CoreGameSignals.Instance.onSetCollectableScore?.Invoke((short)collectableList.Count);
            MiniGameSignals.Instance.onPlayerReadyToGo?.Invoke();
            
            
        }
    }

}