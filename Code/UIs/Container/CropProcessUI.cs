using System;
using System.Linq;
using Code.Core.EventSystems;
using Code.Factories.Crops;
using Code.Test;
using Settings.Input;
using TMPro;
using UnityEngine;
using Works.JES.Code.Farms;

namespace Works.JES.Code.UIs.Container
{
    public class CropProcessUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI nameText, processText;
        [SerializeField] private LayerMask whatIsSelect;
        [SerializeField] private InputSO inputSO;

        private bool _isOn = false;
        private TestCrop _testCrop;
        private void Awake()
        {
            _isOn = false;
            nameText.text = "";
            processText.text = "";
            GameEventBus.AddListener<SelectingFactoryCropEvent>(HandleSelect);
        }

        private void HandleSelect(SelectingFactoryCropEvent obj)
        {
            if (obj.IsSelected)
            {
                _isOn = true;
                _testCrop = obj.crop;
                nameText.text = _testCrop.CropData.Name;
                SetProcessText();
                _testCrop.ChangeProcessEvent.AddListener(HandleChange);
                _testCrop.OnSell += HandleSell;
            }
        }

        private void HandleSell(ICrop obj)
        {
            if (_testCrop != null)
            {
                _testCrop.ChangeProcessEvent.RemoveListener(HandleChange);
                _testCrop.OnSell -= HandleSell;
            }
            _isOn = false;
            nameText.text = "";
            processText.text = "";
        }

        private void HandleChange()
        {
            if (_testCrop == null) return;
            SetProcessText();
        }

        private void SetProcessText()
        {
            string processStr = "";
            foreach (CropProcess progress in _testCrop.Item.Progresss)
            {
                processStr += ProcessToString(progress) + "\n";
            }
            processText.text = processStr;
        }

        private void Update()
        {
            if (_isOn && Input.GetMouseButtonDown(0))
            {
                _testCrop.OnSell -= HandleSell;
                _testCrop?.ChangeProcessEvent.RemoveListener(HandleChange);
                _testCrop?.Deselect();
                _isOn = false;
                nameText.text = "";
                processText.text = "";
            }
        }

        private string ProcessToString(CropProcess processes)
        {
            string result = "";
            switch (processes)
            {
                case CropProcess.Dry:
                    result = "<color=#C2A675>건조됨</color>";
                    break;
                case CropProcess.Fire:
                    result = "<color=#FF5C39>불에탐</color>";
                    break;
                case CropProcess.Ice:
                    result = "<color=#5DD9DD>얼어붙음</color>";
                    break;
                case CropProcess.Wet:
                    result = "<color=#3FA9F5>젖음</color>";
                    break;
                case CropProcess.Shock:
                    result = "<color=#FFD93D>감전됨</color>";
                    break;
            }
            return result;
        }
        private void FixedUpdate()
        {
            if (inputSO.GetMousePosition(out RaycastHit hit, whatIsSelect))
            {
                if (hit.collider.TryGetComponent(out TestCrop crop))
                {
                    _testCrop = crop;
                    crop.Select();
                }
            }
        }
    }
}