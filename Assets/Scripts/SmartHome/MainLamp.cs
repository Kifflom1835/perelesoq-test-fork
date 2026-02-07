using UnityEngine;

namespace SmartHome
{
    public class MainLamp : ElectricObject
    {
        [SerializeField]
        int roomNumber = 1;

        [SerializeField]
        Renderer lightMesh;

        public override void Start()
        {
            base.Start();

            PowerSource.Instance.RegisterLamp(this);

            lightMesh = GetComponent<Renderer>();
        }

        void Update()
        {

        }
        public override void ChangeConnection(bool value)
        {
            base.ChangeConnection(value);

            /* Light l = LightsDataset.Instance.GetLightByNumber(roomNumber);
             if (l != null)
             {

                 l.intensity = value ? 1 : 0;
             }*/

            ToggleLight();

            //isOn = value;
        }

        public override void Toggle()
        {
            isOn = !isOn;

            //if (!coonectedToNetwork) return;

            ToggleLight();
        }

        void ToggleLight()
        {
            Light l = LightsDataset.Instance.GetLightByNumber(roomNumber);

            if (l != null)
            {

                l.gameObject.SetActive(isOn);
            }

            if (isOn)
                lightMesh.material = MaterialsDataset.Instance.CeilLampOn;
            else
                lightMesh.material = MaterialsDataset.Instance.CeilLampOff;
        }
    }
}
