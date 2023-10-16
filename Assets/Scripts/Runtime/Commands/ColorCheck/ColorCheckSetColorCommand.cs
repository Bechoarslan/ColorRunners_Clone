using Runtime.Data.ValueObject;
using UnityEngine;

namespace Runtime.Commands.ColorCheck
{
    public class ColorCheckSetColorCommand
    {
        private readonly Renderer _colorAreaRenderer;
        private readonly ColorData _colorData;
        public ColorCheckSetColorCommand(ref Renderer colorAreaRenderer, ColorData colorData)
        {
            _colorAreaRenderer = colorAreaRenderer;
            _colorData = colorData;
        }

        public void Execute()
        {
            _colorAreaRenderer.material.color = _colorData.material.color;
        }
    }
}