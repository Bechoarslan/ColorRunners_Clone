using System.Collections.Generic;
using Runtime.Data.ValueObject;
using UnityEngine;

namespace Runtime.Commands.Collectable
{
    public class CollectableLerpMovementCommand
    {
        private readonly StackData _stackData;
        private readonly List<GameObject> _collectableList;

        public CollectableLerpMovementCommand(ref StackData stackData, ref List<GameObject> collectableList)
        {
            _stackData = stackData;
            _collectableList = collectableList;
        }

        public void Execute(ref Transform playerManager)
        {
            if (_collectableList.Count > 0)
            {
                var directX = Mathf.Lerp(_collectableList[0].transform.localPosition.x, playerManager.position.x,
                    _stackData.StackLerpXDelay);
                var directY = Mathf.Lerp(_collectableList[0].transform.localPosition.y, playerManager.position.y - 0.6f, 1);
                var directZ = Mathf.Lerp(_collectableList[0].transform.localPosition.z,
                    playerManager.position.z - _stackData.StackOffset, _stackData.StackLerpZDelay);
                _collectableList[0].transform.localPosition = new Vector3(directX, directY, directZ);

                _collectableList[0]
                    .transform.LookAt(new Vector3(playerManager.transform.position.x,
                        _collectableList[0]
                            .transform.position.y,
                        playerManager.transform.position.z));

                for (var i = 1; i < _collectableList.Count; i++)
                {
                    var pos = _collectableList[i - 1].transform.localPosition;
                    directX = Mathf.Lerp(_collectableList[i].transform.localPosition.x, pos.x,
                        _stackData.StackLerpXDelay);
                    directY = Mathf.Lerp(_collectableList[i].transform.localPosition.y, pos.y,
                        _stackData.StackLerpYDelay);
                    directZ = Mathf.Lerp(_collectableList[i].transform.localPosition.z, pos.z - _stackData.StackOffset,
                        _stackData.StackLerpZDelay);
                    _collectableList[i].transform.localPosition = new Vector3(directX, directY, directZ);
                    _collectableList[i].transform
                        .LookAt(new Vector3(pos.x, _collectableList[i].transform.position.y, pos.z));
                }
            }
        }
    }
}