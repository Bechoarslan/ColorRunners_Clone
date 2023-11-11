
using Cinemachine;
using Runtime.Enums;
using Runtime.Signals;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

namespace Runtime.Managers
{
    public class CameraManager : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

        [ShowInInspector] private float3 _initialPosition;
        [SerializeField] private CinemachineStateDrivenCamera stateDrivenCamera;
        [SerializeField] private Animator cameraAnimator;

        #endregion

        #endregion
        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            transform.position = _initialPosition;
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onReset += OnReset;    
            CameraSignals.Instance.onSetCinemachineTarget += OnSetCinemachineTarget;
            CameraSignals.Instance.onChangeCameraState += OnChangeCameraState;
            CoreGameSignals.Instance.onPlayerExitInteractWithEndArea += OnPlayerExitInteractWithEndArea;
        }

        private void OnPlayerExitInteractWithEndArea()
        {
            OnChangeCameraState(CameraStates.MiniGame);
        }

        private void OnChangeCameraState(CameraStates state)
        {
            cameraAnimator.SetTrigger(state.ToString());
            
        }

        private void OnSetCinemachineTarget()
        {
            var target = GameObject.FindObjectOfType<PlayerManager>().transform;
           stateDrivenCamera.Follow = target;
           

        }
        
        

        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onReset -= OnReset;
             CameraSignals.Instance.onSetCinemachineTarget -= OnSetCinemachineTarget;
            CameraSignals.Instance.onChangeCameraState -= OnChangeCameraState;
            CoreGameSignals.Instance.onPlayerExitInteractWithEndArea -= OnPlayerExitInteractWithEndArea;
        }

        private void OnReset()
        {
            transform.position = _initialPosition;
            OnSetCinemachineTarget();
            
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}