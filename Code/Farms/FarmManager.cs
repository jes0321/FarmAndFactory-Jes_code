using System;
using System.Collections.Generic;
using Code.Core;
using Code.Core.EventSystems;
using JES.Code.System;
using Settings.Input;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Works.JES.Code.Farms
{
    public class FarmManager : MonoSingleton<FarmManager>
    {
        [SerializeField] private LayerMask whatIsSelectable;
        [SerializeField] private InputSO inputSO;

        private ISelectable _currentSelected;
        private List<Crop> _crops = new List<Crop>();

        private void Awake()
        {
            inputSO.OnClickEvent += HandleOnClick;
            GameEventBus.AddListener<PlantCropEvent>(HandlePlantCrop);
        }

        private void OnDestroy()
        {
            inputSO.OnClickEvent -= HandleOnClick;
            GameEventBus.RemoveListener<PlantCropEvent>(HandlePlantCrop);
        }

        public void Disaster()
        {
            if (_crops.Count <= 0) return;
            Deselect();
            int maxDisaster = Mathf.Clamp(Mathf.CeilToInt(_crops.Count * 0.2f), 2, 15);
            int randIndex = UnityEngine.Random.Range(1, maxDisaster);
            while (randIndex > 0)
            {
                if (_crops.Count <= 0) return;
                int index = UnityEngine.Random.Range(0, _crops.Count);
                Crop crop = _crops[index];
                _crops.RemoveAt(index);
                crop.Destroy();
                randIndex--;
            }
        }

        private void HandlePlantCrop(PlantCropEvent obj)
        {
            if (obj.IsPlanted)
            {
                _crops.Add(obj.Crop);
            }
            else
            {
                _crops.Remove(obj.Crop);
            }
        }

        private void HandleOnClick(bool obj)
        {
            if (!obj) return;
            if (GameManager.IsOverUI) return;
            Deselect();
            if (inputSO.GetMousePosition(out RaycastHit hitInfo, whatIsSelectable))
            {
                if (hitInfo.collider.TryGetComponent(out ISelectable selectable))
                {
                    if (_currentSelected == selectable)
                    {
                        return;
                    }

                    _currentSelected = selectable;
                    selectable.Select();
                }
            }
        }

        public void Deselect()
        {
            if (_currentSelected is UnityEngine.Object uo && uo == null)
            {
                // 이미 파괴된 오브젝트
                _currentSelected = null;
            }
            else
            {
                _currentSelected?.Deselect();
                _currentSelected = null;
            }
        }
    }
}