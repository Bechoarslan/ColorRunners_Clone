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
            
            var droneFirstPosition = new Vector3(6, 4, 9);
            var droneSecondPosition = new Vector3(-5, 4, 9);
            var droneThirdPosition = new Vector3(-5, -7, 9);
            _dronePrefab.transform.DOMove(droneFirstPosition, 1f).OnComplete((() =>
            {
                _dronePrefab.transform.DOMove(droneSecondPosition, 1f).OnComplete((() =>
                {
                    _dronePrefab.transform.DOMove(droneThirdPosition, 3f).OnComplete((() =>
                    {
                        
                        

                    }));
                }));
            } ));

           
            

        }
    }
}