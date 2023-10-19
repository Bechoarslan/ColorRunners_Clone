
using System.Collections.Generic;
using Runtime.Commands.ColorCheck;
using Runtime.Data.UnityObject;
using Runtime.Data.ValueObject;
using Runtime.Enums.Color;

using UnityEngine;

namespace Runtime.Managers
{
    public class ColorAreaManager : MonoBehaviour
    {
        #region Self Variables

        
        #region Serialized Variables

        [SerializeField] private Renderer colorAreaRenderer;
        [SerializeField] private ColorType colorType;
        
        

        #endregion
        
        private ColorData _colorData;
        private readonly string _colorDataPath = "Data/CD_Color";

        private ColorCheckSetColorCommand _colorCheckSetColorCommand;

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
            _colorCheckSetColorCommand.Execute();
        }
        
        internal ColorType SendColorType() => colorType;

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
           
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