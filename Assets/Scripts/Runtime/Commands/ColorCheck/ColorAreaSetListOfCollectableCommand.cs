using System.Collections.Generic;
using Runtime.Enums.Color;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Commands.ColorCheck
{
    public class ColorAreaSetListOfCollectableCommand
    {
        private List<GameObject> _correctColorList;
        private List<GameObject> _falseColorList;
        private Transform _correctCollectableHolder;
        private Transform _falseCollectableHolder;
        

        public ColorAreaSetListOfCollectableCommand(ref List<GameObject> correctColorList, ref List<GameObject> falseColorList, ref Transform correctCollectableHolder, ref Transform falseCollectableHolder)
        {
            _correctColorList = correctColorList;
            _falseColorList = falseColorList;
            _correctCollectableHolder = correctCollectableHolder;
            _falseCollectableHolder = falseCollectableHolder;
        }

        public void Execute(ColorType colorType, ColorType collectableType, GameObject collectableObject)
        {
            if (collectableType == colorType)
            {
                
                
            }
            else
            {
               
            };
            
                
        }
    }
}