using UnityEngine;

namespace Runtime.Commands.Collectable
{
    public class CollectableChangeColorCommand
    {
        private readonly Renderer _collectableRenderer;
        public CollectableChangeColorCommand(ref Renderer collectableRenderer)
        {
            _collectableRenderer = collectableRenderer;
        }

        public void Execute(Color value)
        {
            _collectableRenderer.material.color = value;
        }
    }
}