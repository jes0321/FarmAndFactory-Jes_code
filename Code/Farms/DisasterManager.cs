using System;
using System.Threading.Tasks;
using Code.Core;
using Code.Core.EventSystems;
using JES.Code.System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Works.JES.Code.Farms
{
    public class DisasterManager : CanvasGroupUI
    {
        [SerializeField] private TextMeshProUGUI disasterText;
        [SerializeField] private string[] disasterMessages;
        
        protected override void Awake()
        {
            base.Awake();
            disasterText.text = "";
            GameEventBus.AddListener<DayTimerEvent>(HandleDay);
        }

        private void OnDestroy()
        {
            GameEventBus.RemoveListener<DayTimerEvent>(HandleDay);
        }

        private async void HandleDay(DayTimerEvent obj)
        {
            float timer = Random.Range(120, 320);
            await Awaitable.WaitForSecondsAsync(timer);
            int carbon = TemperManager.Instance.OverCarbon;
            int rand = Random.Range(0, 101);
            if (carbon > 0 && rand < carbon)
            {
                await Debuging();
            }
        }
        
        [ContextMenu("Debuging")]
        private async Task Debuging()
        {
            int msgIndex = Random.Range(0, disasterMessages.Length);
            disasterText.text =$"%%{disasterMessages[msgIndex]} 발생%%";
            CanvasOnOff(true);
            FarmManager.Instance.Disaster();
            await Awaitable.WaitForSecondsAsync(4f);
            CanvasOnOff(false);
        }
    }
}