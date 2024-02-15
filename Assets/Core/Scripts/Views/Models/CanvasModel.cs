using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Scripts.Views.Models
{
    [Serializable]
    public class CanvasModel
    {
        public Button button;
        public CanvasGroup canvasGroup;

        public CanvasModel(Button button, CanvasGroup canvasGroup)
        {
            this.button = button;
            this.canvasGroup = canvasGroup;
        }
    }
}