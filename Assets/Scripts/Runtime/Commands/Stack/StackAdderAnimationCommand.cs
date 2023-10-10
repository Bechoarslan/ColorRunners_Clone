using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Runtime.Data.ValueObject;
using UnityEngine;

namespace Runtime.Commands.Stack
{
    public class StackAdderAnimationCommand
    {
        private List<GameObject> _collectableList;
        private StackData _stackData;
        public StackAdderAnimationCommand(ref List<GameObject> collectableList, ref StackData stackData)
        {
            _collectableList = collectableList;
            _stackData = stackData;
            
        }

        public IEnumerator Execute()
        {
            for (int i = 0; i <= _collectableList.Count - 1; i++)
            {
                var index = i;
                _collectableList[index].transform
                    .DOScale(new Vector3(1, _stackData.StackScaleValue, 1), _stackData.StackScaleDelay)
                    .SetEase(Ease.Flash);
                _collectableList[index].transform.DOScale(new Vector3(1f, 1f, 1f), _stackData.StackScaleDelay)
                    .SetDelay(_stackData.StackAnimDuration).SetEase(Ease.Flash);
                yield return new WaitForSeconds(_stackData.StackAnimDuration / 3);
            }
        }
    }
}