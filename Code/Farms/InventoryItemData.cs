using System.Collections.Generic;

namespace Works.JES.Code.Farms
{
    public struct InventoryItemData
    {
        public int Count;
        public CropData Data;
        public List<CropProcess> Progresss;
    }
}