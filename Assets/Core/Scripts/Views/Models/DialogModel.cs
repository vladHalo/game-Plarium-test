using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Scripts.Views.Models
{
    [Serializable]
    public class DialogModel
    {
        public GameObject dialogContent;
        public Button buttonOpen;
        public Button buttonClose;
        public bool isOpen;

        public DialogModel(GameObject dialogContent, Button buttonOpen, Button buttonClose)
        {
            this.dialogContent = dialogContent;
            this.buttonOpen = buttonOpen;
            this.buttonClose = buttonClose;
            isOpen = false;
        }
    }
}