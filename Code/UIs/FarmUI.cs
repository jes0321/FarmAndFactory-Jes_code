using System;
using Code.Core.EventSystems;
using UnityEngine;
using Works.JES.Code.Farms;
using Works.JES.Code.UIs.Container;

namespace Works.JES.Code.UIs
{
    public class FarmUI : MonoBehaviour
    {
        [SerializeField] private PlantCropUI plantCropUI;
        [SerializeField] private CropUI cropUI;

        private void Awake()
        {
            Reset();
        }

        private void Reset()
        {
            plantCropUI.Disable();
            cropUI.Disable();
        }

        private void Start()
        {
            GameEventBus.AddListener<SelectCropEvent>(OnSelectCrop);
            GameEventBus.AddListener<SelectCropGroundEvent>(OnSelectCropGround);
            GameEventBus.AddListener<DeselectCropGroundEvent>(OnDeselectCropGround);
            GameEventBus.AddListener<DeselectCropEvent>(OnDeselectCrop);
        }

        private void OnDestroy()
        {
            GameEventBus.RemoveListener<SelectCropEvent>(OnSelectCrop);
            GameEventBus.RemoveListener<SelectCropGroundEvent>(OnSelectCropGround);
            GameEventBus.RemoveListener<DeselectCropGroundEvent>(OnDeselectCropGround);
            GameEventBus.RemoveListener<DeselectCropEvent>(OnDeselectCrop);
        }

        private void OnDeselectCrop(DeselectCropEvent obj)
        {
            Reset();
        }

        private void OnDeselectCropGround(DeselectCropGroundEvent obj)
        {
            Reset();
        }

        private void OnSelectCropGround(SelectCropGroundEvent obj)
        {
            Reset();
            plantCropUI.EnableFor(obj.CropGround);
        }

        private void OnSelectCrop(SelectCropEvent obj)
        {
            Reset();
            cropUI.EnableFor(obj.Crop);
        }
    }
}