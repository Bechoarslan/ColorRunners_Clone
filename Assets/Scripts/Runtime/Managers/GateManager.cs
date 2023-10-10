
using System;
using Runtime.Commands.Gate;
using Runtime.Controllers.Gate;
using Runtime.Data.UnityObject;
using Runtime.Data.ValueObject;
using Runtime.Enums.Color;
using Runtime.Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Runtime.Managers
{
    public class GateManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private ColorType colorType;
        [SerializeField] private GateMeshController gateMeshController;

        #endregion

        #region Private Variables

        [ShowInInspector] private ColorData _colorData;
        private readonly string _dataPath = "Data/CD_Color";
        private SendGateColorDataCommand _sendGateColorDataCommand;

        #endregion

        #endregion

        private void Awake()
        {
            _colorData = GetColorData();
            SendDataToControllers();
            Init();

        }
        
        private ColorData GetColorData() => Resources.Load<CD_Color>(_dataPath).GateColors[(int) colorType];
        private void Init()
        {
            _sendGateColorDataCommand = new SendGateColorDataCommand();
        }

        private void SendDataToControllers()
        {
            gateMeshController.GetColorData(_colorData);
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlayerInteractionWithGate += OnPlayerInteractionWithGate;
        }

        internal ColorType OnGetGateColorType()
        {
            return colorType;
        }

        private void OnPlayerInteractionWithGate(GameObject gateGameObject)
        {
            _sendGateColorDataCommand.Execute(gateGameObject);
        }

        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onPlayerInteractionWithGate -= OnPlayerInteractionWithGate;
        }

        private void OnDisable()
        {
           UnSubscribeEvents();
        }
    }

   
}