using System.Collections.Generic;
using Runtime.Data.ValueObject;
using UnityEngine;

namespace Runtime.Commands.Collectable
{
    public class CollectableLerpMovementCommand
    {
        private StackData _stackData;
        private List<GameObject> _collectableList;
        public CollectableLerpMovementCommand(ref StackData stackData, ref List<GameObject> collectableList)
        {
            _stackData = stackData;
            _collectableList = collectableList;
        }

        public void Execute(ref Transform playerManager)
        {
            if (_collectableList.Count <= 0) return;
            var playerManagerPos = playerManager.position;
            var firstCollectablePos = _collectableList[0].transform.localPosition;
            var lerpPosX = Mathf.Lerp(firstCollectablePos.x, playerManagerPos.x, _stackData.StackLerpXDelay);
            var lerpPosY = Mathf.Lerp(firstCollectablePos.y, playerManagerPos.y, 1);
            var lerpPosZ = Mathf.Lerp(firstCollectablePos.z, playerManagerPos.z - _stackData.StackOffset, _stackData.StackLerpZDelay);
            _collectableList[0].transform.localPosition = new Vector3(lerpPosX, lerpPosY, lerpPosZ);
            _collectableList[0].transform.LookAt(new Vector3(playerManagerPos.x,
                _collectableList[0].transform.position.y,playerManagerPos.z));

            for (var i = 1; i < _collectableList.Count; i++)
            {
                var copyPos = _collectableList[i].transform.localPosition;
                var newPos = _collectableList[i - 1].transform.localPosition;
                lerpPosX = Mathf.Lerp(copyPos.x, newPos.x, _stackData.StackLerpXDelay);
                lerpPosY = Mathf.Lerp(copyPos.y, newPos.y, _stackData.StackLerpYDelay);
                lerpPosZ = Mathf.Lerp(copyPos.z, newPos.z - _stackData.StackOffset, _stackData.StackLerpZDelay);
                copyPos = new Vector3(lerpPosX, lerpPosY, lerpPosZ);
                _collectableList[i].transform.LookAt(new Vector3(newPos.x,copyPos.y,newPos.z));
            }

        }
    }
}