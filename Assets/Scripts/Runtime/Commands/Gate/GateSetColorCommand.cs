using Runtime.Data.UnityObject;
using Runtime.Data.ValueObject;
using UnityEngine;

namespace Runtime.Commands.Gate
{
    public class GateSetColorCommand
    {
        private readonly Renderer _gateRenderer;
        public GateSetColorCommand(ref Renderer gateRenderer)
        {
            _gateRenderer = gateRenderer;
        }

        public void Execute(ColorData colorData)
        {
            _gateRenderer.material.color = colorData.material.color;
        }
    }
}