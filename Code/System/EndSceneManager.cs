using System;
using TMPro;
using UnityEngine;

namespace JES.Code.System
{
    public class EndSceneManager : MonoBehaviour
    {
        [SerializeField] private CurrencySO currencySo;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private string[] messages;
        private void Awake()
        {
            float temper = currencySo.Temper;
            int index = Mathf.Clamp((int)((temper - 20) / 10), 0, messages.Length - 1);
            text.SetText(messages[index]);
        }
    }
}