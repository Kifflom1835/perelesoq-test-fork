using System;
using System.Collections.Generic;
using UnityEngine;

namespace SmartHome
{
    public class Swither : ElectricObject, IToggle
    {
        [SerializeField]
        List<GameObject> connectors = new List<GameObject>();

        [SerializeField]
        GameObject pivot;
        [SerializeField]
        Renderer indicator;

        void Start()
        {
            PowerSource.Instance.RegisterSwither(this);
        }

        void Update()
        {

        }

        public override void Toggle()
        {
            base.Toggle();
            ToggleSwither();
        }
        public override void ChangeConnection(bool value)
        {
            base.ChangeConnection(value);
            SwithConnection();
            SwithConnectionVisual();
        }

        [ContextMenu("Toggle Swither")]
        void ToggleSwither()
        {
            IsOn = !IsOn;
            foreach(GameObject connector in connectors)
            {
                if(connector.TryGetComponent<IToggle>(out IToggle toggle))
                {
                    if (coonectedToNetwork)
                    {
                        toggle.ChangeConnection(IsOn);
                    }
                    else
                        toggle.ChangeConnection(coonectedToNetwork);

                    if (toggle.ToggleBySwither)
                        toggle.Toggle();
                }
            }

            SwithToggleVisual();
        }
        void SwithToggleVisual()
        {
            if (IsOn)
            {
                pivot.transform.localEulerAngles = new Vector3(0,0,-7);
            }
            else
            {
                pivot.transform.localEulerAngles = new Vector3(0, 0, 7);
            }
        }
        void SwithConnection()
        {
            foreach (GameObject connector in connectors)
            {
                if (connector.TryGetComponent<IToggle>(out IToggle toggle))
                {
                    toggle.ChangeConnection(coonectedToNetwork);
                }
            }
        }
        void SwithConnectionVisual()
        {

            if (coonectedToNetwork)
            {
                indicator.material = MaterialsDataset.Instance.isOnMaterial;
            }
            else
            {
                indicator.material = MaterialsDataset.Instance.isOffMaterial;
            }
        }
    }
}
