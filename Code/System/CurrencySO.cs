using System;
using Code.Core;
using UnityEngine;

namespace JES.Code.System
{
    [CreateAssetMenu(fileName = "Currency SO", menuName = "SO/Currency", order = 0)]
    public class CurrencySO : ScriptableObject
    {
        public NotifyValue<int> Money = new NotifyValue<int>(1000);
        public float Temper;
    }
}