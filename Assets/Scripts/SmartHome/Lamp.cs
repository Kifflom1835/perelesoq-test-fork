using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

namespace SmartHome {
    public class Lamp : ElectriicObject, IToggle
    {
        [SerializeField]
        private List<GameObject> lights = new List<GameObject>();

        public override void Toggle()
        {
            base.Toggle();
            ToggleLight();
        }
        public override void ChangeConnection(bool value)
        {
            base.ChangeConnection(value);

            if (lights.Count == 0) return;

            foreach (GameObject l in lights)
            {
                if (l.TryGetComponent<Light>(out Light light))
                {
                    light.intensity = value ? 1 : 0;
                }
            }

        }

        [ContextMenu("Toggle Light")]
        public void ToggleLight()
        {

            if (lights.Count == 0) return;

            foreach (GameObject l in lights) 
            {
                l.SetActive(!l.activeInHierarchy);
            }

            isOn = lights[0].activeInHierarchy;

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
