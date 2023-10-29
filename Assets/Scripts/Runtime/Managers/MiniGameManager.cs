using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Runtime.Commands.MiniGame;
using Runtime.Controllers.Turret;
using Runtime.Enums.MiniGame;
using Runtime.Signals;
using Unity.VisualScripting;
using UnityEngine;

namespace Runtime.Managers
{
    public class MiniGameManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private MiniGameType miniGameType;
        [SerializeField] private GameObject dronePrefab;
        [SerializeField] private GameObject turretPrefab;
        [SerializeField] private TurretController turretController;
        

        #endregion

        #region Private Variables

        private MiniGamePlayDroneCommand _miniGamePlayDroneCommand;

        #endregion
        

        #endregion

        private void Awake()
        {
            Init();
            IsTurretOrDrone();

        }

        private void IsTurretOrDrone()
        {
            if (miniGameType == MiniGameType.Turret)
            {
                dronePrefab.SetActive(false);
                turretPrefab.SetActive(true);
            }
            else
            {
                dronePrefab.SetActive(true);
                turretPrefab.SetActive(false);
            }
                
        }

        private void Init()
        {
            _miniGamePlayDroneCommand = new MiniGamePlayDroneCommand(ref dronePrefab);
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            MiniGameSignals.Instance.onPlayerInteractWithMiniGameArea += OnPlayerInteractWithMiniGameArea;
            MiniGameSignals.Instance.onPlayDroneAnimation += PlayDrone;
            MiniGameSignals.Instance.onTurretMiniGamePlay += OnTurretMiniGamePlay;
            MiniGameSignals.Instance.onPlayerExitInteractWithMiniGameArea += OnPlayerExitInteractWithMiniGameArea;
            
        }

        private void OnPlayerExitInteractWithMiniGameArea()
        {
            turretController.isPlayerTargeted = false;
        }


        private void OnTurretMiniGamePlay(List<GameObject> collectableObj, bool condition)
        {
            if (condition) return;
            turretController.SetTarget(collectableObj[0].transform);
            turretController.isPlayerTargeted = true;
            DOVirtual.DelayedCall(1.5f, () =>
            {
                if ((bool)MiniGameSignals.Instance.onCheckColorAgainForTurretMiniGame?.Invoke()) return;
                turretController.Shoot();
                var destroyedCollectableObject = collectableObj[^1].gameObject;
                collectableObj.Remove(destroyedCollectableObject);
                destroyedCollectableObject.SetActive(false);
                var pool = FindObjectOfType<PoolManager>().GetComponentInChildren<Transform>();
                destroyedCollectableObject.transform.parent = pool;
                collectableObj.TrimExcess();
                CoreGameSignals.Instance.onSetCollectableScore?.Invoke((short)collectableObj.Count);
                

            });
           



        }
        


        private void OnPlayerInteractWithMiniGameArea(GameObject miniGameAreaObject)
        {
            if (miniGameAreaObject.GetInstanceID() != gameObject.GetInstanceID()) return;
            MiniGameSignals.Instance.onMiniGameAreaSendToMiniGameTypeToListeners?.Invoke(miniGameType);
        }


        private void UnSubscribeEvents()
        {
            MiniGameSignals.Instance.onPlayerInteractWithMiniGameArea -= OnPlayerInteractWithMiniGameArea;
            MiniGameSignals.Instance.onPlayDroneAnimation -= PlayDrone;
            MiniGameSignals.Instance.onTurretMiniGamePlay -= OnTurretMiniGamePlay;
            MiniGameSignals.Instance.onPlayerExitInteractWithMiniGameArea -= OnPlayerExitInteractWithMiniGameArea;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void PlayDrone(GameObject miniGameManager)
        {
            if(miniGameManager.GetInstanceID() != gameObject.GetInstanceID()) return;
            _miniGamePlayDroneCommand.Execute();
        }
    }
}