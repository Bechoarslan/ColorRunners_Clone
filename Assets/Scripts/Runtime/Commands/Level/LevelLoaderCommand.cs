using Runtime.Interfaces;
using Runtime.Managers;
using Unity.VisualScripting;
using UnityEngine;

namespace Runtime.Commands.Level
{
    public class LevelLoaderCommand : ICommand
    {
        private GameObject _levelHolder;
        public LevelLoaderCommand(ref GameObject levelHolder)
        {
            _levelHolder = levelHolder;
        }
        
        public void Execute(int _LevelID)
        {

            Object.Instantiate(Resources.Load<GameObject>($"Prefabs/LevelPrefabs/level {_LevelID}"), _levelHolder.transform);
        }
    }
}