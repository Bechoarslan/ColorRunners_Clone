using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Runtime.Enums.Collectable;
using Runtime.Managers;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Commands.ColorCheck
{
    public class ColorAreaDestroyFalseCollectableCommand
    {
        private List<GameObject> _falseColorList;
        private DOTweenAnimation _dotTweenAnimation;
        private List<GameObject> _correctColorList;
       
        public ColorAreaDestroyFalseCollectableCommand(ref List<GameObject> falseColorList,
            ref DOTweenAnimation dotTweenAnimation, ref List<GameObject> correctColorList)
        {
            _falseColorList = falseColorList;
            _dotTweenAnimation = dotTweenAnimation;
            _correctColorList= correctColorList;
            
            
        }

        public void Execute()
        {
            if (_correctColorList.Count > 0) return;
            
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