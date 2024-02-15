using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Scripts.Views.Models
{
    [Serializable]
    public class ButtonModel
    {
        public string nameSave;
        public Button button;
        public Image image;
        public Text text;
        [ShowIf("ButtonImageNotNull")] public List<Sprite> sprites;
        [ShowIf("ButtonImageNotNull")] public List<Color> colors;
        [ShowIf("text")] public List<string> texts;

        private bool ButtonImageNotNull()
        {
            return button != null && image != null;
        }
    }
}