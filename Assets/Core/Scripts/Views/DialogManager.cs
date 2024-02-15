using System.Collections.Generic;
using Core.Scripts.Views.Models;
using DG.Tweening;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Core.Scripts.Views
{
    public class DialogManager : MonoBehaviour
    {
        [SerializeField] private float _animDuration = 0.2f;
        [SerializeField] private List<DialogModel> _dialogModels;

        private void Start()
        {
            _dialogModels.ForEach((item, index) =>
            {
                item.dialogContent.SetActive(false);
                item.buttonOpen.onClick.AddListener(() => ShowHideDialog(index));
                item.buttonClose.onClick.AddListener(() => ShowHideDialog(index));
            });
        }

        private void OnDestroy()
        {
            _dialogModels.ForEach(item =>
            {
                if (item.buttonOpen != null)
                    item.buttonOpen.onClick.RemoveAllListeners();
                if (item.buttonClose != null)
                    item.buttonClose.onClick.RemoveAllListeners();
            });
        }

        public void ShowHideDialog(int index)
        {
            if (_dialogModels[index].isOpen)
            {
                HideDialog(index);
            }
            else
            {
                ShowDialog(index);
            }
        }

        public void AddListener(GameObject dialogContent, Button buttonOpen, Button buttonClose)
        {
            _dialogModels.Add(new DialogModel(dialogContent, buttonOpen, buttonClose));
            buttonOpen.onClick.AddListener(() => ShowHideDialog(_dialogModels.Count - 1));
            buttonClose.onClick.AddListener(() => ShowHideDialog(_dialogModels.Count - 1));
        }

        public void AddListener(GameObject dialogContent, Button buttonOpen, Button buttonClose, UnityAction action)
        {
            _dialogModels.Add(new DialogModel(dialogContent, buttonOpen, buttonClose));
            buttonOpen.onClick.AddListener(action);
            buttonClose.onClick.AddListener(action);
        }

        private void ShowDialog(int index)
        {
            _dialogModels[index].dialogContent.transform.localScale = Vector3.zero;
            _dialogModels[index].dialogContent.SetActive(true);
            _dialogModels[index].dialogContent.transform.DOScale(Vector3.one, _animDuration);
            _dialogModels[index].isOpen = true;
        }

        private void HideDialog(int index)
        {
            _dialogModels[index].dialogContent.transform.DOScale(Vector3.zero, _animDuration)
                .OnComplete(() => { gameObject.SetActive(false); });
            _dialogModels[index].isOpen = false;
        }
    }
}