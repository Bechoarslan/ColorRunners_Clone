
using System.Collections;
using Runtime.Commands.Collectable;
using Runtime.Controllers;
using Runtime.Data.UnityObject;
using Runtime.Data.ValueObject;
using Runtime.Enums;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Managers
{

    public class CollectableManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        private Color _collectableRenderer;
        [SerializeField] private Animator collectableAnimator;
        [SerializeField] private int collectableColorId;
        #endregion

        #region Serialized Variables

        [SerializeField] private CollectableMeshController collectableMeshController;
        

        #endregion

        #region Private Variables

        private ColorData _colorData;
        private readonly string _colorDataPath = "Data/CD_Color";
        private CollectableAnimationCommand _collectableAnimationCommand;

        #endregion

        #endregion

        private void Awake()
        {
            _colorData = GetColorData();
            SendDataToMesh();
            Init();
        }

        private ColorData GetColorData() =>
            Resources.Load<CD_Color>("Data/CD_Color").collectableColors[collectableColorId];
        private void SendDataToMesh()
        {
            collectableMeshController.GetColorData(_colorData);
        }
        
        private void Init()
        {
            _collectableAnimationCommand = new CollectableAnimationCommand(ref collectableAnimator);
        }


        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CollectableSignals.Instance.onCheckCollectableIsCurrent += OnCheckCollectableIsCurrent;
            GateSignals.Instance.onGetGateColor += collectableMeshController.SetGateColorForCollectable;
            MiniGameSignals.Instance.onInteractionWithCollectable += OnInteractionWithCollectable;
            MiniGameSignals.Instance.onExitInteractionWithCollectable += OnExitInteractionWithCollectable;
            
            
            
        }

        private void OnExitInteractionWithCollectable(GameObject collectableObject)
        {
            if (collectableObject.GetInstanceID() == gameObject.GetInstanceID())
            {
                _collectableAnimationCommand.Execute(PlayerAnimationStates.Run);
                
            }
        }

        private void OnInteractionWithCollectable(GameObject collectableObject)
        {
            if (collectableObject.GetInstanceID() == gameObject.GetInstanceID())
            {
                _collectableAnimationCommand.Execute(PlayerAnimationStates.HideWalk);
                
            }
        }
        private void OnCheckCollectableIsCurrent(GameObject collectableGameObject)
        {

            if (collectableGameObject != gameObject)
                return;

            _collectableAnimationCommand.Execute(PlayerAnimationStates.Run);


        }

        

        

        private void UnSubscribeEvents()
        {
            CollectableSignals.Instance.onCheckCollectableIsCurrent -= OnCheckCollectableIsCurrent;
            GateSignals.Instance.onGetGateColor -= collectableMeshController.SetGateColorForCollectable;
            MiniGameSignals.Instance.onInteractionWithCollectable -= OnInteractionWithCollectable;
            MiniGameSignals.Instance.onExitInteractionWithCollectable -= OnExitInteractionWithCollectable;
        }

        
        

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

     

    }
}