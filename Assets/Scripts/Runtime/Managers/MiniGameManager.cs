using System;
using DG.Tweening;
using Runtime.Commands.MiniGame;
using Runtime.Enums.MiniGame;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Managers
{
    public class MiniGameManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private MiniGameType miniGameType;
        [SerializeField] private GameObject dronePrefab;
        [SerializeField] private DOTweenPath dronePath;

        #endregion

        #region Private Variables

        private MiniGamePlayDroneCommand _miniGamePlayDroneCommand;

        #endregion
        

        #endregion

        private void Awake()
        {
            Init();
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
           MiniGameSignals.Instance.onMiniGameAreaInteractWithCollectable += OnMiniGameAreaInteractWithCollectable;
           MiniGameSignals.Instance.onMiniGameAreaStartDroneRoutine += OnMiniGameAreaStartDroneRoutine;
            
        }

        private void OnMiniGameAreaStartDroneRoutine(GameObject miniGameObject)
        {
            if(miniGameObject.GetInstanceID() != gameObject.GetInstanceID()) return;
            _miniGamePlayDroneCommand.Execute();



        }

        private void OnMiniGameAreaInteractWithCollectable(GameObject miniGameObject)
        {
            Debug.LogWarning("Executed ===> OnMiniGameAreaInteractWithCollectable");
            if (miniGameObject.GetInstanceID() != gameObject.GetInstanceID()) return;
            MiniGameSignals.Instance.onSendMiniGameAreaTypeToListeners?.Invoke(miniGameType); 
            
        }


        private void UnSubscribeEvents()
        {
            MiniGameSignals.Instance.onMiniGameAreaInteractWithCollectable -= OnMiniGameAreaInteractWithCollectable;
            MiniGameSignals.Instance.onMiniGameAreaStartDroneRoutine -= OnMiniGameAreaStartDroneRoutine;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}