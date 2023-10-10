using System.Collections.Generic;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Commands.Stack
{
    public class StackScoreUpdaterCommand
    {
        private readonly List<GameObject> _collectableList;
        private short _totalListScore;

        public StackScoreUpdaterCommand(ref List<GameObject> collectableList)
        {
            _collectableList = collectableList;

        }

        public void Execute(int collectableAddScore)
        {
            if (collectableAddScore < 0)
            {
                RemoveScore((short)collectableAddScore);
            }
            else
            {
                AddScore((short)collectableAddScore);
            }
            
            
        }

        private void RemoveScore(short collectableAddScore)
        {
            _totalListScore = 0;
            _totalListScore -= collectableAddScore;
            PlayerSignals.Instance.onSendStackScoreToPlayerText?.Invoke(_totalListScore);
        }

        private void AddScore(short collectableAddScore)
        {
            _totalListScore = 0;
            foreach (var item in _collectableList)
            {
                _totalListScore += collectableAddScore;

                PlayerSignals.Instance.onSendStackScoreToPlayerText?.Invoke(_totalListScore);


            }
        }
    }
}