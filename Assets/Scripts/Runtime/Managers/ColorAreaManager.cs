
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

        [ShowInInspector]private List<GameObject> correctColorList = new List<GameObject>();
        [ShowInInspector]private List<GameObject> falseColorList = new List<GameObject>();

        #endregion
        
        #region Serialized Variables

        [SerializeField] private Renderer colorAreaRenderer;
        [SerializeField] private ColorType colorType;
        [SerializeField] private ColorAreaPhysicsController colorAreaPhysicsController;
        [SerializeField] private Transform correctCollectableHolder;
        [SerializeField] private Transform falseCollectableHolder;
        
        

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
            _colorAreaSetListOfCollectableCommand =
                new ColorAreaSetListOfCollectableCommand(ref correctColorList, ref falseColorList,ref correctCollectableHolder,ref falseCollectableHolder);
            _colorCheckSetColorCommand.Execute();
        }
        

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
           MiniGameSignals.Instance.onSendMiniGameAreaTypeToListeners += OnSendMiniGameAreaTypeToListeners;
           MiniGameSignals.Instance.onCheckColorCollectableForColorArea += OnCheckColorCollectableForColorArea;
           MiniGameSignals.Instance.onSetCollectableListToStackManager += OnSetCollectableListToStackManager;
        }

        private void OnSetCollectableListToStackManager(List<GameObject> collectableList, Transform stackManagerTransform)
        {
            for (var i = correctCollectableHolder.childCount; i > 0  ; i--)
            {
                var collectableObject = correctCollectableHolder.GetChild(i - 1).gameObject;
                collectableObject.transform.parent = stackManagerTransform;
                correctColorList.Remove(collectableObject);
                collectableList.Add(collectableObject);
                correctColorList.TrimExcess();
                CollectableSignals.Instance.onSetCollectableAnimation?.Invoke(collectableObject,CollectableAnimationStates.Run);
            } 
            MiniGameSignals.Instance.onSetPlayerMovementReady?.Invoke();
            
        }
        


        private void OnCheckColorCollectableForColorArea(ColorType collectableType, GameObject colorAreaObject,GameObject collectableObject)
        {
            if(colorAreaObject.GetInstanceID() != gameObject.GetInstanceID()) return;
            _colorAreaSetListOfCollectableCommand.Execute(colorType, collectableType, collectableObject);
          
           
            
        }

        private void OnSendMiniGameAreaTypeToListeners(MiniGameType miniGameType)
        {
            _miniGameType = miniGameType;
            colorAreaPhysicsController.GetMiniGameType(miniGameType);
        }

        


        private void UnSubscribeEvents()
        {
            MiniGameSignals.Instance.onSendMiniGameAreaTypeToListeners -= OnSendMiniGameAreaTypeToListeners;
            MiniGameSignals.Instance.onCheckColorCollectableForColorArea -= OnCheckColorCollectableForColorArea;
            MiniGameSignals.Instance.onSetCollectableListToStackManager -= OnSetCollectableListToStackManager;
           
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}