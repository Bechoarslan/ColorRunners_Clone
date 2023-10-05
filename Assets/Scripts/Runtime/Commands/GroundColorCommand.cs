using UnityEngine;

namespace Runtime.Commands
{
    public class GroundColorCommand
    {
        private readonly Renderer _groundRenderer;
        public GroundColorCommand(ref Renderer groundRenderer)
        {
            _groundRenderer = groundRenderer;
        }

        public void Execute(Color groundColor)
        {
            _groundRenderer.material.color = groundColor;
        }
    }
}