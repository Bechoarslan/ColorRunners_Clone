using DG.Tweening;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Commands.MiniGame
{
    public class MiniGamePlayDroneCommand
    {
        private GameObject _dronePrefab;
        public MiniGamePlayDroneCommand(ref GameObject dronePrefab)
        {
            _dronePrefab = dronePrefab;
        }

        public void Execute()
        {
            var dronePos = _dronePrefab.transform.position;
            var droneFirstPosition = new Vector3(dronePos.x,dronePos.y,dronePos.z);
            var droneSecondPosition = new Vector3(dronePos.x, dronePos.y + 3, dronePos.z);
            var droneThirdPosition = new Vector3(dronePos.x - 13, droneSecondPosition.y, dronePos.z);
            var droneFourthPosition = new Vector3(droneThirdPosition.x, droneFirstPosition.y, droneFirstPosition.z);
            _dronePrefab.transform.DOMove(droneFirstPosition, 1f).OnComplete((() =>
            {
                _dronePrefab.transform.DOMove(droneSecondPosition, 1f).OnComplete((() =>
                {
                    _dronePrefab.transform.DOMove(droneThirdPosition, 2f).OnComplete((() =>
                    {

                        _dronePrefab.transform.DOMove(droneFourthPosition, 1f);

                    }));
                }));
            } ));

           
            

        }
    }
}