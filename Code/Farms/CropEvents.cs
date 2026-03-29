using Code.Core.EventSystems;
using Code.Test;
using NUnit.Framework.Internal;

namespace Works.JES.Code.Farms
{
    public class CropEvents
    {
        public static PlantCropEvent PlantCropEvent = new PlantCropEvent();
        public static SelectingFactoryCropEvent SelectingFactoryCropEvent = new SelectingFactoryCropEvent();
        public static GetCropEvent GetCropEvent = new GetCropEvent();
        public static SelectCropEvent SelectCropEvent = new SelectCropEvent();
        public static DeselectCropEvent DeselectCropEvent = new DeselectCropEvent();
        public static SelectCropGroundEvent SelectCropGroundEvent = new SelectCropGroundEvent();
        public static DeselectCropGroundEvent DeselectCropGroundEvent = new DeselectCropGroundEvent();
    }

    public class PlantCropEvent : GameEvent
    {
        public Crop Crop;
        public bool IsPlanted = false;
        public PlantCropEvent Init(Crop crop, bool isplanted)
        {
            Crop = crop;
            IsPlanted = isplanted;
            return this;
        }
    }
    public class GetCropEvent : GameEvent
    {
        public CropData ItemData;
        public GetCropEvent Init(CropData itemData)
        {
            ItemData = itemData;
            return this;
        }
    }

    public class SelectCropEvent : GameEvent
    {
        public Crop Crop;
        public SelectCropEvent Init(Crop crop)
        {
            Crop = crop;
            return this;
        }
    }

    public class DeselectCropEvent : GameEvent
    {
        public Crop Crop;
        public DeselectCropEvent Init(Crop crop)
        {
            Crop = crop;
            return this;
        }
    }
    public class SelectCropGroundEvent : GameEvent
    {
        public CropGround CropGround;
        public SelectCropGroundEvent Init(CropGround cropGround)
        {
            CropGround = cropGround;
            return this;
        }
    }
    public class DeselectCropGroundEvent : GameEvent
    {
        public CropGround CropGround;
        public DeselectCropGroundEvent Init(CropGround cropGround)
        {
            CropGround = cropGround;
            return this;
        }
    }
    public class SelectingFactoryCropEvent : GameEvent
    {
        public TestCrop crop;
        public bool IsSelected = false;
        public SelectingFactoryCropEvent Init(TestCrop itemData,bool isselected)
        {
            crop = itemData;
            IsSelected = isselected;
            return this;
        }
    }
}