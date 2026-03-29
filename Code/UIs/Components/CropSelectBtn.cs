using Code.UI;
using JES.Code.System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Works.JES.Code.Farms;

namespace Works.JES.Code.UIs.Components
{
    public class CropSelectBtn : MonoBehaviour, IUIElement<CropData, CropGround,bool>
    {
        [SerializeField] private Image icon;
        [SerializeField] private Button selectBtn;
        [SerializeField] private CurrencySO currencySO;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI priceText;
        [SerializeField] private SectionPopupUI popupUI;
        [SerializeField] private GameObject panel;

        private CropGround _ground;
        private CropData _data;

        public void EnableFor(CropData item, CropGround ground,bool canBuy)
        {
            _data = item;
            _ground = ground;
            gameObject.SetActive(true);
            icon.sprite = item.Icon;
            nameText.text = item.Name;
            priceText.text = item.BuyPrice.ToString()+"$";
            if (currencySO.Money.Value < item.BuyPrice)
            {
                selectBtn.interactable = false;
            }
            else
            {
                selectBtn.interactable = true;
                selectBtn.onClick.RemoveAllListeners();
                selectBtn.onClick.AddListener(BuyClick);
            }
            
            string sectionText = $"{item.Name}\n" +
                                 $"필요 온도: {item.GrowTemper.x}~{item.GrowTemper.y}\n" +
                                 $"성장 시간: {item.GrowTime}초\n" +
                                 $"판매 가격: {item.SellPrice}$";
            popupUI.SectionText=sectionText;
            if (canBuy == false)
            {
                panel.SetActive(true);
                selectBtn.interactable = false;
            }
        }

        private void BuyClick()
        {
            FarmManager.Instance.Deselect();
            currencySO.Money.Value -= _data.BuyPrice;
            Vector3 pos = _ground.transform.position;
            pos.y += 1;
            var obj = Instantiate(_data.CropPrefab, pos, Quaternion.identity);
            _ground.PlantingCrop = obj.GetComponent<Crop>().Initialize(_ground, _data);
        }

        public void Disable()
        {
            panel.SetActive(false);
            gameObject.SetActive(false);
            selectBtn.onClick.RemoveAllListeners();
        }
    }
}