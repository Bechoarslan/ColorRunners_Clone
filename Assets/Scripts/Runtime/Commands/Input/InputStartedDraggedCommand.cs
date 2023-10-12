
using Runtime.Managers;
using Runtime.Signals;
namespace Runtime.Commands.Input
{
  
    public class InputStartedDraggedCommand
    {
        private bool _isFirstTimeTouchTaken;
        private readonly InputManager _inputManager;
 
        public InputStartedDraggedCommand(ref bool isFirstTimeTouchTaken, ref InputManager inputManager
            )
        {
            _isFirstTimeTouchTaken = isFirstTimeTouchTaken;
            _inputManager = inputManager;
        
        }

        public void Execute()
        {
            InputSignals.Instance.onInputTaken?.Invoke();
            if (!_isFirstTimeTouchTaken)
            {
                _isFirstTimeTouchTaken = true;
                InputSignals.Instance.onFirstTimeTouchTaken?.Invoke();
            }


            
            _inputManager.MousePosition = UnityEngine.Input.mousePosition;
        }
    }
}