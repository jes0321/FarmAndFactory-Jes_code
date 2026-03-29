using System;
using JES.Code.System;
using TMPro;
using UnityEngine;

namespace Works.JES.Code.UIs
{
    public class MoneyText : MonoBehaviour
    {
        [SerializeField] private CurrencySO currencySO;
        [SerializeField] private TextMeshProUGUI moneyText;
        private void Awake()
        {
            currencySO.Money.OnValueChanged += OnMoneyChange;
            moneyText.text = currencySO.Money.Value.ToString()+"$";
        }
        private void OnDestroy()
        {
            currencySO.Money.OnValueChanged -= OnMoneyChange;
        }

        private void OnMoneyChange(int prev, int next)
        {
            moneyText.text = next.ToString()+"$";
        }
    }
}