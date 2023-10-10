using System.Collections.Generic;
using DG.Tweening;
using Runtime.Enums;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Commands.Stack
{
    public class RemoverOnStackCommand
    {
        private List<GameObject> _stackList;
        private readonly GameObject _levelHolder;

        public RemoverOnStackCommand(ref List<GameObject> collectableList)
        {
           _stackList = collectableList;
           _levelHolder = GameObject.Find("LevelHolder");
        }

        public void Execute(GameObject collectableGameObject, CollectableAnimationCommand collectableAnimationCommand)
        {
            if (_stackList.Count <= 0)
            {
                CoreGameSignals.Instance.onLevelFailed?.Invoke();
            }
            else
            {
                var destroyedObject= _stackList[^1];
                _stackList.Remove(destroyedObject);
                collectableGameObject.SetActive(false);
                collectableAnimationCommand.Execute(ref destroyedObject, PlayerAnimationStates.Died);
                destroyedObject.transform.SetParent(_levelHolder.transform);
                DOVirtual.DelayedCall(1.5f, () =>
                {
                    destroyedObject.SetActive(false);
                });
                _stackList.TrimExcess();
            }
        }
    }
}