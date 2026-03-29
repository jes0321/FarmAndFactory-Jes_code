using System;
using Code.Core.EventSystems;
using EPOOutline;
using UnityEngine;

namespace Works.JES.Code.Farms
{
    public class Crop : MonoBehaviour, ISelectable
    { 
        [SerializeField] private Outlinable outlinable;
        [SerializeField] private GameObject[] cropStage;
        private int _currentStageIndex = 0;

        private float _startTime;
        public bool IsCompleted { get; private set; } = false;
        [field: SerializeField] public CropData Data { get; private set; }

        private CropGround _ground;
        public Crop Initialize(CropGround ground,CropData data)
        {
            GameEventBus.RaiseEvent(CropEvents.PlantCropEvent.Init(this,true));
            Data = data;
            _ground = ground;
            outlinable.enabled = false;
            _startTime = Time.time;
            return this;
        }

        private void Update()
        {
            if (IsCompleted) return;
            float normalizedTime = (Time.time - _startTime) / Data.GrowTime;
            UpdateConstructionProgress(normalizedTime);
        }

        private void UpdateConstructionProgress(float progress)
        {
            if (progress < 0 || progress > 1 || _currentStageIndex >= cropStage.Length) return;

            if (_currentStageIndex == 0 && progress > 0.45f)
                ChangeConstructionStage(1);
            else if (_currentStageIndex == 1 && progress > 0.9f)
            {
                ChangeConstructionStage(2);
                IsCompleted = true;
            }
        }

        private void ChangeConstructionStage(int index)
        {
            if (index < 0 || index >= cropStage.Length) return;

            cropStage[_currentStageIndex].SetActive(false);
            _currentStageIndex = index;
            cropStage[_currentStageIndex].SetActive(true);
        }

        public void Destroy()
        {
            _ground.PlantingCrop = null;
            Destroy(gameObject);
        }
        public void Harvest()
        {
            if (!IsCompleted) return;
            _ground.PlantingCrop = null;
            FarmManager.Instance.Deselect();
            GameEventBus.RaiseEvent(CropEvents.PlantCropEvent.Init(this,false));
            GameEventBus.RaiseEvent(CropEvents.GetCropEvent.Init(Data));
            Destroy(gameObject);
        }
        public bool IsSelected { get; private set; }

        public void Select()
        {
            outlinable.enabled = true;
            IsSelected = true;
            GameEventBus.RaiseEvent(CropEvents.SelectCropEvent.Init(this));
        }

        public void Deselect()
        {
            outlinable.enabled = false;
            IsSelected = false;
            GameEventBus.RaiseEvent(CropEvents.DeselectCropEvent.Init(this));
        }
    }
}