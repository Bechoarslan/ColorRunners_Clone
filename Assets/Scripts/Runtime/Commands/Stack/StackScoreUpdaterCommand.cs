using System.Collections.Generic;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Commands.Stack
{
    public class StackScoreUpdaterCommand
    {
        private List<GameObject> _collectableList;
        private short _totalListScore;
        public StackScoreUpdaterCommand(ref List<GameObject> collectableList)
        {
            _collectableList = collectableList;
        }

        public void Execute()
        {
            _totalListScore = 0;
            foreach (var item in _collectableList)
            {
                _totalListScore += 1;
            }
            PlayerSignals.Instance.onSendStackScoreToPlayerText?.Invoke(_totalListScore);


        }
    }
}