using Runtime.Interfaces;
using Runtime.Managers;
using UnityEngine;

namespace Runtime.Commands.Level
{
    public class LevelDestroyerCommand : ICommand
    {
        private GameObject _levelHolder;
        public LevelDestroyerCommand(ref GameObject levelHolder)
        {
            _levelHolder = levelHolder;
        }

        public void Execute()
        {
            Object.Destroy(_levelHolder.transform.GetChild(0).gameObject);
        }
    }
}