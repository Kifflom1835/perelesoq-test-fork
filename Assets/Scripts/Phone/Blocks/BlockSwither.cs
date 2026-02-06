using UnityEngine;
using SmartHome;
using TMPro;
using UnityEngine.UI;
using System;

namespace Phone
{
    public class BlockSwither : Block
    {
        [SerializeField] Toggle toggle;

        public Swither swither;


        void Start()
        {

        }
        void Update()
        {

        }
        private void OnDestroy()
        {
            if (swither != null)
                swither.OnConnectionToNetworkChanged -= SetStatus;

        }
        public override void Init(ElectricObject obj)
        {   
            swither = obj as Swither;

            SetName();
            SetStatus();
            toggle.isOn = swither.IsOn;

            toggle.onValueChanged.AddListener(OnToggleChanged);
            swither.OnConnectionToNetworkChanged += SetStatus;
        }

        public void SetName()
        {
            deviceName.text = swither.GetObjName;
        }
        public void SetStatus()
        {
            if (swither.CoonectedToNetwork)
            {
                toggle.interactable = true;
                status.text = "on";
            }
            else
            {
                toggle.interactable = false;
                status.text = "off";
            }


        }

        public void OnToggleChanged(bool value) 
        {
            swither.Toggle();
        }
    }
}
