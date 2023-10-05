using UnityEngine;

namespace Runtime.Commands.Gate
{
    public class GateChangeColorCommand
    {
        private readonly Renderer _gateRenderer;
        public GateChangeColorCommand(ref Renderer gateRenderer)
        {
            _gateRenderer = gateRenderer;
        }


        public void Execute(Material colorDataMaterial)
        {
            _gateRenderer.material = colorDataMaterial;
        }
    }
}