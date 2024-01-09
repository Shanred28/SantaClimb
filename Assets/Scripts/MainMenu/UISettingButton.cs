using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenu
{
    public class UISettingButton : MonoBehaviour
    {
        [SerializeField] private Setting _setting;
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private TMP_Text _valueText;
        [SerializeField] private Image _previousImage;
        [SerializeField] private Image _nextImage;

        private void Start()
        {
            ApplyProperty(_setting);
        }

        public void SetNextValueSetting()
        {
            _setting?.SetNextValue();
            UpdateInfo();
            _setting?.Apply();
        }
        public void SetPreviousValueSetting()
        {
            _setting?.SetPreviousValue();
            UpdateInfo();
            _setting?.Apply();
        }

        private void UpdateInfo() 
        {
            _titleText.text = _setting.Title;
            _valueText.text = _setting.GetStringValue();

            _previousImage.enabled = !_setting.isMinValue;
            _nextImage.enabled = !_setting.isMaxValue;
        }

        public void ApplyProperty(ScriptableObject property)
        {
            if (property == null) return;
            if (property is Setting == false) return;

            _setting = property as Setting;

            UpdateInfo();
        }
    }
}
