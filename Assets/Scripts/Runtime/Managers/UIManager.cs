﻿using Runtime.Enums;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Managers
{
    public class UIManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        #endregion

        #region Private Variables

        #endregion

        #endregion

        private void OnEnable()
        {
            CoreGameSignals.Instance.onLevelInitialize += OnLevelInitialize;
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onLevelFailed += OnLevelFailed;
            CoreGameSignals.Instance.onLevelSuccessful += OnLevelSuccessful;

            OpenStartPanel();
        }

        private void OpenStartPanel()
        {
            CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Start, 1);
        }

        private void OnDisable()
        {
            CoreGameSignals.Instance.onLevelInitialize -= OnLevelInitialize;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onLevelFailed -= OnLevelFailed;
            CoreGameSignals.Instance.onLevelSuccessful -= OnLevelSuccessful;
        }


        private void OnLevelInitialize()
        {
            CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Level, 0);
            UISignals.Instance.onSetNewLevelValue?.Invoke();
          
        }

        public void OnPlay()
        {
            CoreGameSignals.Instance.onPlay?.Invoke();
            CoreUISignals.Instance.onClosePanel?.Invoke(1);
            CameraSignals.Instance.onSetCinemachineTarget?.Invoke();
            CameraSignals.Instance.onChangeCameraState?.Invoke(CameraStates.Idle);
        }

        private void OnOpenWinPanel()
        {
            CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Win, 2);
        }

        private void OnOpenFailPanel()
        {
            CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Fail, 2);
        }

        public void OnNextLevel()
        {
            CoreGameSignals.Instance.onNextLevel?.Invoke();
            CoreGameSignals.Instance.onReset?.Invoke();
        }

        public void OnRestartLevel()
        {
            CoreGameSignals.Instance.onRestartLevel?.Invoke();
            CoreGameSignals.Instance.onReset?.Invoke();
        }

        private void OnLevelFailed()
        {
            OnOpenFailPanel();
            
        }

        private void OnLevelSuccessful()
        {
            OnOpenWinPanel();
        }

        private void OnReset()
        {
            CoreUISignals.Instance.onCloseAllPanels?.Invoke();
            CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Start, 1);
        }
    }
}