using UnityEngine;

namespace SmartHome
{
    public class MainLamp : ElectriicObject
    {
        [SerializeField]
        int roomNumber = 1;



        public override void Start()
        {
            base.Start();
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

            isOn = l.gameObject.activeInHierarchy;
        }
    }
}
