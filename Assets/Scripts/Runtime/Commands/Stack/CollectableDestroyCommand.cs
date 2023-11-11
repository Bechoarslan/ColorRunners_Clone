using System.Collections.Generic;
using DG.Tweening;
using Runtime.Enums.Collectable;
using Runtime.Enums.MiniGame;
using Runtime.Managers;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Commands.Stack
{
    public class CollectableDestroyCommand
    {
        private readonly List<GameObject> _collectableList;
        public CollectableDestroyCommand(ref List<GameObject> collectableList)
        {
            _collectableList = collectableList;
        }


        public void Execute(GameObject colObj)
        {
            if (_collectableList.Count <= 0)
            {
                CoreGameSignals.Instance.onLevelFailed?.Invoke();
                return;
            }
            var getLastCollectable = _collectableList[^1];
            _collectableList.Remove(getLastCollectable);
            var pool = Object.FindObjectOfType<PoolManager>().GetComponentInChildren<Transform>();
            getLastCollectable.SetActive(false);
            getLastCollectable.transform.parent = pool;
            CoreGameSignals.Instance.onSetCollectableScore.Invoke((short)_collectableList.Count);
            

        }
    }
}