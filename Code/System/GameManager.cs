using Code.Core;
using Code.Placements;
using Settings.Input;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using NotImplementedException = System.NotImplementedException;

namespace JES.Code.System
{
    public class GameManager : CanvasGroupUI
    {
        [SerializeField] private InputSO inputSO;
        [SerializeField] private CurrencySO currencySo;
        private bool _isOn;
        [SerializeField] private Slider slider;
        [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private int EndingMoney = 10000000;
        private string _volumeParameter = "MASTER";
        private readonly string _saveKey = "MASTER_VOLUME";
        public static bool IsOverUI = false;
        protected override void Awake()
        {
            base.Awake();
            inputSO.OnEscapeKeyPressedEvent += HandleESC;
            float savedVolume = PlayerPrefs.GetFloat(_saveKey, 1f);
            slider.value = savedVolume;
            slider.onValueChanged.AddListener(HandleValueChange);
            HandleValueChange(savedVolume);
            currencySo.Money.OnValueChanged+= HandleMoneyChange;
        }

        private void HandleMoneyChange(int prev, int next)
        {
            if (next >= 10000000)
            {
                SceneManager.LoadScene("EndScene");
            }
        }

        private void HandleValueChange(float arg0)
        {
            audioMixer.SetFloat(_volumeParameter, Mathf.Log10(arg0) * 20);
            PlayerPrefs.SetFloat(_saveKey, arg0);
            PlayerPrefs.Save();
        }

        private void OnDestroy()
        {
            inputSO.OnEscapeKeyPressedEvent -= HandleESC;
            currencySo.Money.OnValueChanged-= HandleMoneyChange;
            slider.onValueChanged.RemoveAllListeners();
        }

        private void Update()
        {
            IsOverUI = UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
        }

        private void HandleESC()
        {
            if(PlacementManager.Instance.IsPlaceMode) return;
            
            CanvasOnOff(!_isOn);
            _isOn = !_isOn;
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}