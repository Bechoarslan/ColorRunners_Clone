using System;
using Runtime.Commands.ColorCheckArea;
using Runtime.Controllers.ColorCheckAreaMeshController;
using Runtime.Data.UnityObject;
using Runtime.Data.ValueObject;
using Runtime.Enums.Color;
using Runtime.Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Runtime.Managers
{
    public class ColorCheckAreaManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private ColorCheckAreaMeshController colorCheckAreaMeshController;
        [SerializeField] private ColorType colorType;

        #endregion

        #region Private Variables

        [ShowInInspector] private ColorData _colorData;
        private readonly string colorDataPath = "Data/CD_Color";
        private ColorCheckAreaCheckColor _colorCheckAreaCheckColor;

        #endregion

        #endregion

        private void Awake()
        {
            _colorData = GetColorData();
            SendDataToController();
            Init();
        }

        private void Init()
        {
            _colorCheckAreaCheckColor = new ColorCheckAreaCheckColor();
        }

        private void SendDataToController() => colorCheckAreaMeshController.SetData(_colorData);
        private ColorData GetColorData() => Resources.Load<CD_Color>(colorDataPath).GroundColors[(int)colorType];

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlayerInteractionWithColorCheckArea += OnPlayerInteractionWithColorCheckArea;
        }

        private void OnPlayerInteractionWithColorCheckArea(GameObject colorAreaGameObject)
        {
            _colorCheckAreaCheckColor.Execute(colorAreaGameObject);
        }

        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onPlayerInteractionWithColorCheckArea -= OnPlayerInteractionWithColorCheckArea;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
        
        internal ColorType SendColorType() => colorType;
    }
}