using System;
using Code.Core.EventSystems;
using Settings.Input;
using UnityEngine;

namespace JES.Code.System
{
    public class TPCamManager : MonoBehaviour
    {
        public enum ECamState
        {
            Farm,
            Factory
        }
        [SerializeField] private Transform farmTrm, factoryTrm,camTrm;
        [SerializeField] private InputSO inputSo;

        private ECamState _ecamState = ECamState.Farm;
        
        private void Awake()
        {
            inputSo.OnTabKeyPressedEvent+= OnTabKeyPressedEvent;
        }
        
        private void OnDestroy()
        {
            inputSo.OnTabKeyPressedEvent-= OnTabKeyPressedEvent;
        }

        private void Start()
        {
            GameEventBus.RaiseEvent(UIEvents.SetFactoryUIEvent.Initializer(false));
        }

        private void OnTabKeyPressedEvent()
        {
            if(_ecamState==ECamState.Farm)
            {
                _ecamState = ECamState.Factory;
                camTrm.position = factoryTrm.position;
            }
            else
            {
                _ecamState = ECamState.Farm;
                camTrm.position = farmTrm.position;
            }
            
            GameEventBus.RaiseEvent(UIEvents.SetFactoryUIEvent.Initializer(_ecamState == ECamState.Factory));
        }
    }
}