using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

namespace SmartHome {
    public class Lamp : ElectricObject, IToggle
    {
        [SerializeField]
        private List<GameObject> lights = new List<GameObject>();

        [SerializeField]
        Renderer lightMesh;



        public override void Start()
        {
            base.Start();

            PowerSource.Instance.RegisterLamp(this);

        }
        void Update()
        {

        }

        public override void Toggle()
        {
            base.Toggle();
            ToggleLight();
        }
        public override void ChangeConnection(bool value)
        {
            base.ChangeConnection(value);

          /*  if (lights.Count == 0) return;

            foreach (GameObject l in lights)
            {
                if (l.TryGetComponent<Light>(out Light light))
                {
                    light.intensity = value ? 1 : 0;
                }
            }*/

        }

        [ContextMenu("Toggle Light")]
        public void ToggleLight()
        {

            if (lights.Count == 0) return;

            foreach (GameObject l in lights) 
            {
                l.SetActive(!l.activeInHierarchy);
            }

            IsOn = lights[0].activeInHierarchy;

            if(isOn)
                lightMesh.material = MaterialsDataset.Instance.LampOn;
            else
                lightMesh.material = MaterialsDataset.Instance.LampOff;

        }
    }
}
