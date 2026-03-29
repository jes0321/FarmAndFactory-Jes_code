using System;
using Code.Core.EventSystems;
using EPOOutline;
using UnityEngine;

namespace Works.JES.Code.Farms
{
    public class CropGround : MonoBehaviour,ISelectable
    {
        [SerializeField] private Outlinable outlinable;
        [SerializeField] private GameObject can, cant;
        [SerializeField] private bool isOn = false;
        public bool CanPlant { get; private set; } = false;
        public Crop PlantingCrop { get; set; } = null;
        public bool IsSelected { get; private set; }

        private void Start()
        {
            outlinable.enabled = false;
            if (isOn)
            {
                EnablePlanting();
            }
        }
        public void Select()
        {
            outlinable.enabled = true;
            GameEventBus.RaiseEvent(CropEvents.SelectCropGroundEvent.Init(this));
            IsSelected = true;                
        }
        public void EnablePlanting()
        {
            CanPlant = true;
            can.SetActive(true);
            cant.SetActive(false);
        }

        public void Deselect()
        {
            GameEventBus.RaiseEvent(CropEvents.DeselectCropGroundEvent.Init(this));
            outlinable.enabled = false;
            IsSelected = false;
        }
    }
}