//using Tabtale.TTPlugins;

using System;
using _Modules.SaveModule.Scripts.Data;

using Managers;
using Runtime.Enums;
using Runtime.Extentions;
using Runtime.Signals;
using Signals;
using UnityEditor.VersionControl;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    #region Self Variables

    #region Public Variables

    [SerializeField] private GameStates gameStates;

    #endregion
    
    #endregion

    protected override void Awake()
    {
        Application.targetFrameRate = 60;
        //TTPCore.Setup();
    }

    private void Start()
    {
        gameStates = GameStates.Run;
    }

    private void OnEnable()
    {
        CoreGameSignals.Instance.onChangeGameStates += OnChangeGameState;
    }

    private void OnDisable()
    {
        CoreGameSignals.Instance.onChangeGameStates -= OnChangeGameState;
    }
    
    private void OnChangeGameState(GameStates newState)
    {
        gameStates = newState;
       
    }

   
}