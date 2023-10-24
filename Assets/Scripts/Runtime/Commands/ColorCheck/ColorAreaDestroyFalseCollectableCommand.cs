using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Runtime.Enums.Collectable;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Commands.ColorCheck
{
    public class ColorAreaDestroyFalseCollectableCommand
    {
        private List<GameObject> _falseColorList;
        private DOTweenAnimation _dotTweenAnimation;
       
        public ColorAreaDestroyFalseCollectableCommand(ref List<GameObject> falseColorList,
            ref DOTweenAnimation dotTweenAnimation)
        {
            _falseColorList = falseColorList;
            _dotTweenAnimation = dotTweenAnimation;
            
            
        }

        public void Execute()
        {
            if(_falseColorList.Count <= 0) return;
            for (var i = _falseColorList.Count; i > 0  ; i--)
            {
                var colObj = _falseColorList[i - 1];
                _falseColorList.Remove(colObj);
                CollectableSignals.Instance.onSetCollectableAnimation?.Invoke(colObj,CollectableAnimationStates.Died);
                DOVirtual.DelayedCall(0.5f, () =>
                {
                    colObj.SetActive(false);
                });
                _falseColorList.TrimExcess();

            }
            
            _dotTweenAnimation.DOPlay();

            

        }
    }
}