using System.Collections.Generic;
using DG.Tweening;
using Runtime.Managers;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Commands.Stack
{
    public class CollectableMoveToPlayerCommand
    {
        private readonly List<GameObject> _collectableList;
        public CollectableMoveToPlayerCommand(ref List<GameObject> collectableList)
        {
            _collectableList = collectableList;
        }


        public void Execute(Transform playerManagerTransform)
        {
            for (var i = _collectableList.Count ; i > 0 ; i--)
            {
                var pool = Object.FindObjectOfType<PoolManager>().GetComponentInChildren<Transform>();
                var collectableObj = _collectableList[i - 1];
                _collectableList.Remove(collectableObj);
                var playerPosition = playerManagerTransform.position;
                var goToPlayerPosition = new Vector3(playerPosition.x, collectableObj.transform.position.y,
                    playerPosition.z);
                collectableObj.transform.DOMove(goToPlayerPosition, 1.5f).OnComplete(() =>
                {
                    collectableObj.SetActive(false);
                    collectableObj.transform.parent = pool;
                    
                });
                CoreGameSignals.Instance.onSetPlayerScale?.Invoke();
            }
            _collectableList.TrimExcess();
        }
    }
}