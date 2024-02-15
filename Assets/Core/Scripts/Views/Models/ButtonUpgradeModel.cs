using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Scripts.Views.Models
{
    [Serializable]
    public class ButtonUpgradeModel
    {
        public string nameSave;
        public int step;
        public Button button;
        public Sprite sprite;
        [ShowIf("ButtonSpriteNotNull")] public Text priceText;
        [ShowIf("ButtonSpriteNotNull")] public List<Image> images;

        private bool ButtonSpriteNotNull()
        {
            return button != null && sprite != null;
        }
    }
}