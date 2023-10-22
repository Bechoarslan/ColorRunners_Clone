
using System.Collections.Generic;
using Runtime.Commands.ColorCheck;
using Runtime.Controllers.ColorCheck;
using Runtime.Data.UnityObject;
using Runtime.Data.ValueObject;
using Runtime.Enums.Collectable;
using Runtime.Enums.Color;
using Runtime.Enums.MiniGame;
using Runtime.Signals;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

namespace Runtime.Managers
{
    public class ColorAreaManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        [ShowInInspector]public List<GameObject> correctColorList = new List<GameObject>();
        [ShowInInspector]public List<GameObject> falseColorList = new List<GameObject>();

        #endregion
        
        #region Serialized Variables

        [SerializeField] private Renderer colorAreaRenderer;
        [SerializeField] private ColorType colorType;

        #endregion
        
        private ColorData _colorData;
        private readonly string _colorDataPath = "Data/CD_Color";

        private ColorCheckSetColorCommand _colorCheckSetColorCommand;
        private ColorAreaSetListOfCollectableCommand _colorAreaSetListOfCollectableCommand;
        private MiniGameType _miniGameType;

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
            // _colorAreaSetListOfCollectableCommand =
            //     new ColorAreaSetListOfCollectableCommand(ref correctColorList, ref falseColorList,ref correctCollectableHolder,ref falseCollectableHolder);
             _colorCheckSetColorCommand.Execute();
            
        }
        

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

        internal ColorType SendColorType() => colorType;

        
    }
}