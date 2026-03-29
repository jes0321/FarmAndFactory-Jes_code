using System.Collections.Generic;
using UnityEngine;

namespace Works.JES.Code.Farms
{
    [CreateAssetMenu(fileName = "Crop Data List", menuName = "SO/Crop/List", order = 0)]
    public class CropDataList : ScriptableObject
    {
        [field:SerializeField] public CropData[] Crops { get; private set; }
        
        public List<CropData> GetCropsList()
        {
            List<CropData> result = new List<CropData>(Crops);
            result.Sort((a, b) => a.BuyPrice.CompareTo(b.BuyPrice));
            return result;
        }
    }
}