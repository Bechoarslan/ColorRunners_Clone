using System;
using Runtime.Commands.ColorCheck;
using Runtime.Data.UnityObject;
using Runtime.Data.ValueObject;
using Runtime.Enums.Color;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Managers
{
    public class ColorAreaManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Renderer colorAreaRenderer;
        [SerializeField] private ColorType colorType;
        [SerializeField] private Transform colorCheckHolder;
        

        #endregion
        
        private ColorData _colorData;
        private readonly string _colorDataPath = "Data/CD_Color";

        private ColorCheckSetColorCommand _colorCheckSetColorCommand;
        private ColorCheckColorIsSameCommand _colorCheckCheckColorIsSameCommand;

        #endregion


        private void Awake()
        {
            _colorData = GetColorData();
            Init();
        }
        
        private ColorData GetColorData() => Resources.Load<CD_Color>(_colorDataPath).groundColor[(int)colorType];
        private void Init()
        {
            _colorCheckSetColorCommand = new ColorCheckSetColorCommand(ref colorAreaRenderer, _colorData);
            _colorCheckCheckColorIsSameCommand = new ColorCheckColorIsSameCommand();
            _colorCheckSetColorCommand.Execute();
        }
        
        internal ColorType SendColorType() => colorType;

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            MiniGameSignals.Instance.onCheckColorTypes += OnCheckColorTypes;
        }

        private void OnCheckColorTypes(GameObject collectableObject, GameObject colorAreaManager)
        {
            _colorCheckCheckColorIsSameCommand.Execute(collectableObject, colorAreaManager);
        }

        private void UnSubscribeEvents()
        {
            
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}