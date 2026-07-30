using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UILevelCollectedItem : UIBaseItem
    {
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _countText;

        public void Setup(Sprite sprite, int count, int target)
        {
            _image.sprite = sprite;
            _countText.text = $"{count}/{target}";
            Show();
        }
    }
}