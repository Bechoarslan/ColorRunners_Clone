using System;
using Runtime.Commands.Collectable;
using Runtime.Controllers;
using Runtime.Controllers.Collectable;
using Runtime.Data.UnityObject;
using Runtime.Data.ValueObject;
using Runtime.Enums.Collectable;
using Runtime.Enums.Color;
using Runtime.Enums.MiniGame;
using Runtime.Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Runtime.Managers
{
    public class CollectableManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] public ColorType colorType;
        [SerializeField] private CollectableMeshController collectableMeshController;
        [SerializeField] private Animator collectableAnimator;

        #endregion

        #region Private Variables

        [ShowInInspector] private CD_Color _collectableColorData;
        private CollectableCheckColorCommand _collectableCheckColorCommand;
        private CollectableSetAnimationCommand _collectableSetAnimationCommand;
        private MiniGameType _miniGameType;
       
       
      

        private bool _isSame;
        

        #endregion

        #endregion


        private void Awake()
        {
            _collectableColorData = GetColorData();
            SendColorDataToController(_collectableColorData.collectableColor[(int)colorType]);
            Init();
        }

        private void SendColorDataToController(ColorData collectableColor)
        {
           collectableMeshController.GetColorDataFromManager(collectableColor);
        }

        public void ChangePoolObjectColor(ColorType colorType)
        {
            SendColorDataToController(_collectableColorData.collectableColor[(int)colorType]);
        }

        private void Init()
        {
            _collectableCheckColorCommand = new CollectableCheckColorCommand();
            _collectableSetAnimationCommand = new CollectableSetAnimationCommand(ref collectableAnimator);
           
            
        }

        private CD_Color GetColorData() => Resources.Load<CD_Color>("Data/CD_Color");
        
        

        private void OnEnable()
        {
            SubscribeEvents();
        }
        

        private void SubscribeEvents()
        {
            CollectableSignals.Instance.onCheckCollectablesColors += OnCheckCollectablesColors;
            CollectableSignals.Instance.onSendGateColorType += OnSendGateColorType;
            CollectableSignals.Instance.onSetCollectableAnimation += OnSetCollectableAnimation;
           
            
        }


        private void OnSetCollectableAnimation(GameObject collectableObject,CollectableAnimationStates animState)
        {
            if(collectableObject.GetInstanceID() != gameObject.GetInstanceID()) return;
            _collectableSetAnimationCommand.Execute(animState);
        }

        
        private void OnSendGateColorType(ColorType gateColorType)
        {
            if (gateColorType == colorType) return;
            if (!gameObject.CompareTag("Collected")) return;
            SendColorDataToController(_collectableColorData.collectableColor[(int)gateColorType]);
            colorType = gateColorType;

        }

        private void OnCheckCollectablesColors(GameObject collectableObj, GameObject collectableManager)
        {
            if (collectableObj.GetInstanceID() != gameObject.GetInstanceID()) return;
            _collectableCheckColorCommand.Execute(collectableObj, collectableManager);
        }


        internal ColorType SendColorType() => colorType;
        


        private void UnSubscribeEvents()
        {
            CollectableSignals.Instance.onCheckCollectablesColors -= OnCheckCollectablesColors;
            CollectableSignals.Instance.onSendGateColorType -= OnSendGateColorType;
            CollectableSignals.Instance.onSetCollectableAnimation -= OnSetCollectableAnimation;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}