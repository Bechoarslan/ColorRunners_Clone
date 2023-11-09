using System;
using System.Collections.Generic;
using Runtime.Data.ValueObject;
using Runtime.Keys;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Managers
{
    public class SaveManager : MonoBehaviour
    {
        private void Awake()
        {
            SetReferences();
        }

        private void SetReferences()
        {
            LoadGame();
        }

        private void LoadGame()
        {
          
            SaveSignals.Instance.onLoadEnvironmentDatas?.Invoke(new EnvironmentDataParams
            {
                BuildDatas = ES3.KeyExists("BuildDatas") ? ES3.Load<Dictionary<int,AreaData>>("BuildDatas") : new Dictionary<int, AreaData>(),
                CityLevel =  ES3.KeyExists("CityLevel") ? ES3.Load<int>("CityLevel") : 0,
                Score = ES3.KeyExists("Score") ? ES3.Load<int>("Score") : 0,
                CompletedArea = ES3.KeyExists("CompletedArea") ? ES3.Load<int>("CompletedArea") : 0
            });
            
            
            SaveSignals.Instance.onLoadLevelDatas?.Invoke(new LevelDataParams
            {
                Level = ES3.KeyExists("Level") ? ES3.Load<int>("Level") : 0,
                
            });
           

            if (ES3.KeyExists("Level"))
            {
                Debug.Log(ES3.Load<int>("Level"));
            }

            
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            SaveSignals.Instance.onSaveData += OnSaveData;
        }

        private void OnSaveData()
        {
            SaveGame(SaveSignals.Instance.onGetEnvironmetDatas(),SaveSignals.Instance.onGetLevelDatas());
        }

        private void SaveGame(EnvironmentDataParams environmentData, LevelDataParams levelData)
        {
            if (levelData.Level != null)
            {
                ES3.Save("Level",levelData.Level);
            }
            if (environmentData.CityLevel != null)
            {
                ES3.Save("CityLevel",environmentData.CityLevel);
            }

            if (environmentData.BuildDatas != null)
            {
                ES3.Save("BuildDatas",environmentData.BuildDatas);
            }

            if (environmentData.Score != null)
            {
                ES3.Save("Score",environmentData.Score);
                
            }

            if (environmentData.Score != null)
            {
                ES3.Save("CompletedArea",environmentData.CompletedArea);
            }
        }

        private void UnSubscribeEvents()
        {
            SaveSignals.Instance.onSaveData -= OnSaveData;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}