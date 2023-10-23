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
        private Transform _transform;
        public CollectableChangeParentCommand(ref List<GameObject> collectableList, Transform transform)
        {
            _collectableList = collectableList;
            _transform = transform;
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
            Debug.LogWarning(_collectableList.Count);
            if (_collectableList.Count > 0) return;
            MiniGameSignals.Instance.onPlayDroneAnimation?.Invoke();
            MiniGameSignals.Instance.onPlayMiniGameDroneArea?.Invoke(_collectableList,_transform);

              
               
                
            }
        }
    }
