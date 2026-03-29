using System;
using UnityEngine;

namespace Works.JES.Code.Farms
{
    public enum CropProcess
    {
        NoneProcess = 0,
        Wet,
        Shock,
        Fire,
        Dry,
        Ice
    }

    [CreateAssetMenu(fileName = "Crop Data", menuName = "SO/Crop/Data", order = 0)]
    public class CropData : ScriptableObject
    {
        [field:SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public string Name { get; private set; } = "New Crop";
        [field: SerializeField] public float GrowTime { get; private set; } = 30f;
        [field: SerializeField] public int BuyPrice { get; private set; } = 10;
        [field: SerializeField] public int SellPrice { get; private set; } = 10;
        [field:SerializeField] public Vector2 GrowTemper { get; private set; }
        [field: SerializeField] public GameObject CropPrefab { get; private set; }
        [field: SerializeField] public Mesh CropMesh { get; private set; }
    }
}