using UnityEngine;

namespace Runtime.Commands.ColorCheck
{
    public class ColorAreaCheckPlayerColorCommand
    {
        private readonly Renderer _colorCheckAreaRenderer;
        private bool _isPlayerColorCorrect;
        public ColorAreaCheckPlayerColorCommand(ref Renderer colorCheckAreaRenderer, ref bool isPlayerColorCorrect)
        {
            _colorCheckAreaRenderer = colorCheckAreaRenderer;
        }


        public void Execute(Color playerColor)
        {
            Debug.LogWarning("Executed ===> playerColor");
            _isPlayerColorCorrect = _colorCheckAreaRenderer.material.color == playerColor;
        }
    }
}