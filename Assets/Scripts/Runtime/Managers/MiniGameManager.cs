using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Runtime.Commands.MiniGame;
using Runtime.Controllers.Turret;
using Runtime.Enums.MiniGame;
using Runtime.Signals;
using Sirenix.OdinInspector;
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
        [ShowInInspector] private bool _isPlayerExited;
        private MiniGamePlayTurretCommand _miniGamePlayTurretCommand;

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
            _miniGamePlayTurretCommand = new MiniGamePlayTurretCommand(ref turretController);
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
            _isPlayerExited = true;
        }


        private void OnTurretMiniGamePlay(List<GameObject> collectableObj, bool condition)
        {
            _miniGamePlayTurretCommand.Execute(_isPlayerExited, condition, collectableObj);

        }
        


        private void OnPlayerInteractWithMiniGameArea(GameObject miniGameAreaObject)
        {
            if (miniGameAreaObject.GetInstanceID() != gameObject.GetInstanceID()) return;
            MiniGameSignals.Instance.onMiniGameAreaSendToMiniGameTypeToListeners?.Invoke(miniGameType);
            _isPlayerExited = false;
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