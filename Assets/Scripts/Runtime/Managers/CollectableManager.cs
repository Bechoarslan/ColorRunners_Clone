
using System.Collections;
using Runtime.Commands.Collectable;
using Runtime.Enums;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Managers
{

    public class CollectableManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables



        [SerializeField] private Renderer collectableRenderer;
        [SerializeField] private Animator collectableAnimator;

        #endregion

        #region Serialized Variables

        private CollectableChangeColorCommand _collectableChangeColorCommand;
        private CollectableAnimationCommand _collectableAnimationCommand;

        #endregion

        #endregion

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _collectableChangeColorCommand = new CollectableChangeColorCommand(ref collectableRenderer);
            _collectableAnimationCommand = new CollectableAnimationCommand(ref collectableAnimator);
        }


        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CollectableSignals.Instance.onCheckCollectableIsCurrent += OnCheckCollectableIsCurrent;
            GateSignals.Instance.onGetGateColor += SetCollactableColor;
            
            
        }
        

        private void SetCollactableColor(Color value)
        {
            _collectableChangeColorCommand.Execute(value);
        }

        private void UnSubscribeEvents()
        {
            CollectableSignals.Instance.onCheckCollectableIsCurrent -= OnCheckCollectableIsCurrent;
            GateSignals.Instance.onGetGateColor -= SetCollactableColor;
        }

        private void OnCheckCollectableIsCurrent(GameObject collectableGameObject)
        {

            if (collectableGameObject != gameObject)
                return;

            _collectableAnimationCommand.Execute(PlayerAnimationStates.Run);


        }

        

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

     

    }
}