using System;
using Code.UI;
using JES.Code.System;
using UnityEngine;
using Works.JES.Code.Farms;
using Works.JES.Code.UIs.Components;

namespace Works.JES.Code.UIs.Container
{
    public class PlantCropUI : MonoBehaviour,IUIElement<CropGround>
    {
        [SerializeField] private GameObject plantCrop,select;
        [SerializeField] private Transform content;
        [SerializeField] private CropDataList cropDataList;
        [SerializeField] private GroundBuyBtn groundBuyBtn;
        [SerializeField] private CurrencySO currencySO;
        [SerializeField] private CropSelectBtn cropSelectBtnPrefab;
        private static int _groundPrice = 500;
        private CropGround _cropGround;

        private void Awake()
        {
            _groundPrice = 500;
        }

        public void EnableFor(CropGround commandable)
        {
            if (commandable.PlantingCrop != null) return;
            _cropGround = commandable;
            gameObject.SetActive(true);
            if (commandable.CanPlant == false)
            {
                plantCrop.SetActive(true);
                groundBuyBtn.EnableFor(_groundPrice,BuyGround);
            }
            else
            {
                foreach (Transform obj in content)
                {
                    Destroy(obj.gameObject);
                }
                float temper = TemperManager.Instance.Temper;
                foreach (var data in cropDataList.GetCropsList())
                {
                    CropSelectBtn btn = Instantiate(cropSelectBtnPrefab, content);
                    bool canBuy = temper >= data.GrowTemper.x && temper <= data.GrowTemper.y;
                    btn.EnableFor(data, commandable,canBuy);
                }
                plantCrop.SetActive(false);
                select.SetActive(true);
                groundBuyBtn.Disable();
            }
        }

        public void Disable()
        {
            gameObject.SetActive(false);
            plantCrop.SetActive(false);
            select.SetActive(false);
        }

        private void BuyGround()
        {
            if (currencySO.Money.Value < _groundPrice) return;
            _cropGround.EnablePlanting();
            FarmManager.Instance.Deselect();
            currencySO.Money.Value -= _groundPrice;
            _groundPrice =(int)(_groundPrice*1.2f);
        }
    }
}