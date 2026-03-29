using System;
using Code.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Works.JES.Code.Farms;

namespace Works.JES.Code.UIs.Container
{
    public class CropUI : MonoBehaviour,IUIElement<Crop>
    {
        [SerializeField] private Image cropImage;
        [SerializeField] private TextMeshProUGUI nameText,btnText;
        [SerializeField] private Button harvestBtn;
        private Crop _crop;
        public void EnableFor(Crop item)
        {
            _crop = item;
            gameObject.SetActive(true);
            cropImage.sprite = item.Data.Icon;
            nameText.text = item.Data.Name;
            harvestBtn.interactable = item.IsCompleted;
            btnText.text = item.IsCompleted?"수확하기":"자라는중";
            harvestBtn.onClick.RemoveAllListeners();
            harvestBtn.onClick.AddListener(item.Harvest);
        }

        private void Update()
        {
            if (_crop.IsCompleted && harvestBtn.interactable == false)
            {
                btnText.text = "수확하기";
                harvestBtn.interactable = true;
            }
        }

        public void Disable()
        {
            gameObject.SetActive(false);
            harvestBtn.onClick.RemoveAllListeners();
        }
    }
}