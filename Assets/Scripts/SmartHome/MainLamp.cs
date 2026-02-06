using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
namespace SmartHome
{
    public class MainLamp : Lamp
    {
        [SerializeField]
        bool isMainRoomLight = false;
        [SerializeField]
        int roomNumber = 1;

        public override void ChangeConnection(bool value)
        {
           // GetComponent<ElectriicObject>().ChangeConnection(value);

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

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
