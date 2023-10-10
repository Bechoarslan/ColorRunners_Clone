using Runtime.Data.ValueObject;
using UnityEngine;

namespace Runtime.Commands.ColorCheck
{
    public class ColorAreaSetCollorCommand
    {
        private Renderer _colorCheckAreaRenderer;
        public ColorAreaSetCollorCommand(ref Renderer colorCheckAreaRenderer)
        {
            _colorCheckAreaRenderer = colorCheckAreaRenderer;
        }

        public void Execute(Material colorData)
        {
            _colorCheckAreaRenderer.material = colorData;
        }
    }
}