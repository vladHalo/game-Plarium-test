using UnityEngine;
using UnityEngine.UI;

namespace Core.Scripts.Bots.Player.View
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Text _borderText;
        [SerializeField] private GameObject _borderMenu;

        public void ActiveBorderMenu(bool value)
        {
            _borderMenu.SetActive(value);
        }

        public void SetTextBorder(float index)
        {
            _borderText.text = index.ToString("##");
        }
    }
}