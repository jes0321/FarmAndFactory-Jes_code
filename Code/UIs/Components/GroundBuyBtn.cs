using Code.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Works.JES.Code.Farms;

namespace Works.JES.Code.UIs.Components
{
    public class GroundBuyBtn : MonoBehaviour,IUIElement<int,UnityAction>
    {
        [SerializeField] private Button buyBtn;
        [SerializeField] private TextMeshProUGUI priceText;
        public void EnableFor(int price,UnityAction callback)
        {
            priceText.text = price.ToString() + "$";
            gameObject.SetActive(true);
            buyBtn.onClick.RemoveAllListeners();
            buyBtn.onClick.AddListener(callback);
        }

        public void Disable()
        {
            buyBtn.onClick.RemoveAllListeners();
            gameObject.SetActive(false);
        }
    }
}