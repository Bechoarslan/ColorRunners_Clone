using System;
using Runtime.Commands.Collectable;
using Runtime.Controllers.Collectable;
using Runtime.Data.UnityObject;
using Runtime.Data.ValueObject;
using Runtime.Enums;
using Runtime.Enums.Color;
using Runtime.Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Runtime.Managers
{
    public class CollectableManager : MonoBehaviour
    {
        #region Self Variables

        #region Self Variables

        [SerializeField] private CollectablePhysicsController collectablePhysicsController;
        [SerializeField] private CollectableMeshController collectableMeshController;
        [SerializeField] private CollectableAnimationController collectableAnimationController;
        [SerializeField] private ColorType colorType;
        #endregion

        #region Private Variables

        [ShowInInspector] private CD_Color _colorData;
        
        private readonly string _colorDataPath = "Data/CD_Color";
        private readonly string _collected = "Collected";
        
        #endregion

        #endregion

        private void Awake()
        {
            _colorData = GetColorData();
            SendDataToControllers();
        }
        
        private void SendDataToControllers()
        {
            collectableMeshController.GetColorData(_colorData,colorType);
        }

        private CD_Color GetColorData() => Resources.Load<CD_Color>(_colorDataPath);

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CollectableSignals.Instance.onSetCollectableAnimationState += OnSetCollectableAnimationState;
            PlayerSignals.Instance.onSetPlayerColor += OnSetPlayerColor;
        }

        private void OnSetPlayerColor(ColorType gateColorType)
        {
            if (collectablePhysicsController.CompareTag(_collected))
            {
                collectableMeshController.GetColorData(_colorData,gateColorType);
            }
            
        }

        internal void OnGetCollectableColor(ColorType playerColor)
        {
            collectableMeshController.OnGetColorCollectable(playerColor,colorType);
        }
        
        internal void OnSetCollectableAnimationState(PlayerAnimationStates state)
        {
            collectableAnimationController.SetAnimationState(state);
        }
        

        private void UnSubscribeEvents()
        {
            CollectableSignals.Instance.onSetCollectableAnimationState -= OnSetCollectableAnimationState;
            PlayerSignals.Instance.onSetPlayerColor -= OnSetPlayerColor;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}