using Runtime.Controllers.Gate;
using Runtime.Managers;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Commands.Gate
{
    public class SendGateColorDataCommand
    {
        public void Execute(GameObject gateGameObject)
        {
            var meshController = gateGameObject.GetComponent<GateManager>();
            PlayerSignals.Instance.onSetPlayerColor?.Invoke(meshController.OnGetGateColorType());
            
        }
    }
}