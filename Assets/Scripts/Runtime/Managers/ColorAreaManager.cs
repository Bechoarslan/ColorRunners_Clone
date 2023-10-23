
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Runtime.Commands.ColorCheck;
using Runtime.Commands.Stack;
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
        private ColorAreaSetStackManagerCommand _colorAreaSetStackManagerCommand;
        private ColorAreaDestroyFalseCollectableCommand _colorAreaDestroyFalseCollectableCommand;
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
             _colorCheckSetColorCommand.Execute();
             _colorAreaSetStackManagerCommand = new ColorAreaSetStackManagerCommand(ref correctColorList);
             _colorAreaDestroyFalseCollectableCommand = new ColorAreaDestroyFalseCollectableCommand(ref falseColorList);
            
        }
        

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
           MiniGameSignals.Instance.onPlayMiniGameDroneArea += OnPlayMiniGameDroneArea;
        }

        private void OnPlayMiniGameDroneArea(List<GameObject> collectableList,Transform stackManagerTransform)
        {
            StartCoroutine(OnPlayDroneMiniGame(collectableList,stackManagerTransform));

        }
        


        private void UnSubscribeEvents()
        {
           MiniGameSignals.Instance.onPlayMiniGameDroneArea -= OnPlayMiniGameDroneArea;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private IEnumerator OnPlayDroneMiniGame(List<GameObject> collectableList,Transform stackManagerTransform)
        {
            yield return new WaitForSeconds(4f);
            _colorAreaDestroyFalseCollectableCommand.Execute();
            yield return new WaitForSeconds(1f);
            _colorAreaSetStackManagerCommand.Execute(collectableList,stackManagerTransform);
            yield return new WaitForSeconds(0.001f);
            MiniGameSignals.Instance.onCheckCollectableListIsEmpty?.Invoke();

        }

        internal ColorType SendColorType() => colorType;

        
    }
}