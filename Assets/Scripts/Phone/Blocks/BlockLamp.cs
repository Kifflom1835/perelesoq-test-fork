using TMPro;
using UnityEngine;
using SmartHome;

namespace Phone 
{
    public class BlockLamp : Block
    {
        public ElectricObject lamp;

        void Start()
        {

        }

        void Update()
        {

        }
        private void OnDestroy()
        {
            if (lamp != null)
            {
                lamp.OnConnectionToNetworkChanged -= SetStatus;
                lamp.OnIsOnChanged -= SetStatus;
            }

        }

        public override void Init(ElectricObject obj)
        {
            lamp = obj;
            deviceName.text = lamp.GetObjName;
            SetStatus();

            lamp.OnConnectionToNetworkChanged += SetStatus;
            lamp.OnIsOnChanged += SetStatus;
        }

        public void SetStatus()
        {
            if (lamp.CoonectedToNetwork && lamp.IsOn)
            {
                status.text = "on";
            }
            else
                status.text = "off";
        }

    }
}

