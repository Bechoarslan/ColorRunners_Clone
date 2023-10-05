using System.Collections.Generic;
using Runtime.Data.ValueObject;
using Runtime.Signals;
using UnityEditor;
using UnityEngine;

namespace Runtime.Commands.Stack
{
    public class StackMoverCommand
    {
        private StackData _stackData;
        public StackMoverCommand(ref StackData stackData)
        {
            _stackData = stackData;
        }

        public void Execute(float directionX, List<GameObject> collectableStack)
        {
            float direct = Mathf.Lerp(collectableStack[0].transform.position.x, directionX, _stackData.LerpSpeed);
            collectableStack[0].transform.localPosition = new Vector3(direct, 0.053f, 0.335f);
            StackItemsLerpMove(collectableStack);
            
        }

        private void StackItemsLerpMove(List<GameObject> collectableStack)
        {
            for (int i = 1; i < collectableStack.Count; i++)
            {
                Vector3 pos = collectableStack[i].transform.localPosition;
                pos.x = collectableStack[i - 1].transform.localPosition.x;
                float direct = Mathf.Lerp(collectableStack[i].transform.localPosition.x, pos.x, _stackData.LerpSpeed);
                collectableStack[i].transform.localPosition = new Vector3(direct, pos.y, pos.z);

            }
            
        }
    }
}