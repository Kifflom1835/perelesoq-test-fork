using SmartHome;
using TMPro;
using UnityEngine;

namespace Phone
{
    public class BlockGate : Block
    {

        public ElectricObject gate;

        void Start()
        {

        }

        void Update()
        {

        }

        private void OnDestroy()
        {
            if (gate != null)
            {
                gate.OnIsOnChanged -= SetStatus;
            }

        }

        public override void Init(ElectricObject obj)
        {
            gate = obj;
            deviceName.text = gate.GetObjName;
            SetStatus();

            gate.OnIsOnChanged += SetStatus;
        }

        public void SetStatus()
        {
            if (gate.IsOn)
            {
                status.text = "opened";
            }
            else
                status.text = "closed";
        }
    }
}
