using System;
using Code.CarbonSystem;
using Code.Core;
using Code.Core.EventSystems;
using GondrLib.Dependencies;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JES.Code.System
{
    public class TemperManager : MonoSingleton<TemperManager>
    {
        [Inject] private CarbonCollector _collector;
        private float _plusTemper = 0;
        public int OverCarbon { get; private set; }

        public float Temper => 20 + _plusTemper;
        [SerializeField] private TextMeshProUGUI temperText,dayText,transText;
        private int _day = 1;
        [SerializeField] private CurrencySO currencySo;

        private void Awake()
        {
            temperText?.SetText(Temper.ToString("F1") + "℃");
            transText?.SetText("+0.0℃");
            dayText.SetText("DAY 1");
            GameEventBus.AddListener<CarbonEmissionChangeEvent>(HandleCarbon);
            GameEventBus.AddListener<DayTimerEvent>(HandleDayTimer);
            _collector.OnCurrentCarbonChanged += HandleCarbonChanged;
        }

        private void HandleCarbonChanged(float obj)
        {
            float plus = GetPlusTemper(obj);
            plus -= _plusTemper;
            char  sign = plus >= 0 ? '+' : '-';
            plus = Math.Abs(plus);
            transText?.SetText(sign+plus.ToString("F1") + "℃");
        }

        private void OnDestroy()
        {
            GameEventBus.RemoveListener<CarbonEmissionChangeEvent>(HandleCarbon);
            GameEventBus.RemoveListener<DayTimerEvent>(HandleDayTimer);
        }

        private void HandleDayTimer(DayTimerEvent obj)
        {
            dayText.SetText($"DAY {++_day}");
        }

        private void HandleCarbon(CarbonEmissionChangeEvent obj)
        {
            if (obj.totalCarbon < 1500f) return;
            _plusTemper = obj.totalCarbon-1500f;
            OverCarbon = (int)(_plusTemper / 100);
            _plusTemper = OverCarbon * 0.2f;
            temperText?.SetText(Temper.ToString("F1") + "℃");
            transText.SetText(0.ToString("F1") + "℃");
            currencySo.Temper = Temper;
            if (Temper >= 50)
            {
                SceneManager.LoadScene("BadEndScene");
            }
        }
        public float GetPlusTemper(float carbon)
        {
            if (carbon < 1500f) return 0;
            float plus = (carbon - 1500f) / 100 * 0.2f;
            return plus;
        }
    }
}