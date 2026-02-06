using UnityEngine;

namespace SmartHome
{
    public class MainLamp : ElectricObject
    {
        [SerializeField]
        int roomNumber = 1;



        public override void Start()
        {
            base.Start();

            PowerSource.Instance.RegisterLamp(this);
        }

        void Update()
        {

        }
        public override void ChangeConnection(bool value)
        {
            base.ChangeConnection(value);

            Light l = LightsDataset.Instance.GetLightByNumber(roomNumber);
            if (l != null)
            {

                l.intensity = value ? 1 : 0;
            }
        }

        public override void Toggle()
        {
            if (!coonectedToNetwork) return;

            Light l = LightsDataset.Instance.GetLightByNumber(roomNumber);

            if (l != null)
            {

                l.gameObject.SetActive(!l.gameObject.activeInHierarchy);
            }

            IsOn = l.gameObject.activeInHierarchy;
        }
    }
}
