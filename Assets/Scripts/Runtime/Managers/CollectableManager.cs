using System;
using Runtime.Commands.Collectable;
using Runtime.Controllers;
using Runtime.Controllers.Collectable;
using Runtime.Data.UnityObject;
using Runtime.Data.ValueObject;
using Runtime.Enums.Color;
using Runtime.Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Runtime.Managers
{
    public class CollectableManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private ColorType colorType;
        [SerializeField] private CollectableMeshController collectableMeshController;

        #endregion

        #region Private Variables

        [ShowInInspector] private CD_Color _collectableColorData;
        private CollectableCheckColorCommand _collectableCheckColorCommand;

        private bool isSame;
        

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

        private void Init()
        {
            _collectableCheckColorCommand = new CollectableCheckColorCommand();
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
        }

        [Button]
        private void OnSendGateColorType(ColorType gateColorType)
        {
            Debug.LogWarning("Executed ===> Returned");
            if (gateColorType == colorType) return;
            Debug.LogWarning("Executed ===> OnSendGateColorType");
            SendColorDataToController(_collectableColorData.collectableColor[(int)gateColorType]);
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
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}