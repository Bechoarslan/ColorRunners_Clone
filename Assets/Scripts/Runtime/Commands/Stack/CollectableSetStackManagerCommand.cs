using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Runtime.Enums.Collectable;
using Runtime.Enums.MiniGame;
using Runtime.Managers;
using Runtime.Signals;
using Unity.VisualScripting;
using UnityEngine;

namespace Runtime.Commands.Stack
{
    public class CollectableSetStackManagerCommand
    {
        private List<GameObject> _collectableList;
        private Transform stackManagerTransform;
        public CollectableSetStackManagerCommand(ref List<GameObject> collectableList, Transform transform)
        {
            _collectableList = collectableList;
            stackManagerTransform = transform;
        }

        public void Execute(Transform colorAreaObj)
        { 
            var colorAreaCorrectColorList = colorAreaObj.GetComponentInParent<ColorAreaManager>().correctColorList;
            Debug.LogWarning(colorAreaCorrectColorList.Count);
            for (var i = colorAreaCorrectColorList.Count; i > 0  ; i--)
            {
                var collectableObject = colorAreaCorrectColorList[i - 1];
                collectableObject.transform.parent = stackManagerTransform;
                colorAreaCorrectColorList.Remove(collectableObject);
                _collectableList.Add(collectableObject);
                colorAreaCorrectColorList.TrimExcess();
                
                CollectableSignals.Instance.onSetCollectableAnimation?.Invoke(collectableObject,CollectableAnimationStates.Run);
                
            }
            
            MiniGameSignals.Instance.onPlayerExitInteractWithMiniGameArea.Invoke();
            MiniGameSignals.Instance.onMiniGameAreaSendToMiniGameTypeToListeners?.Invoke(MiniGameType.None);
            foreach (var col in _collectableList)
            {
                col.GetComponentInChildren<CapsuleCollider>().enabled = true;
                
            }
            
        
            
        }
    }

}