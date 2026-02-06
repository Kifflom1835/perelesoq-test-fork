using System.Collections.Generic;
using UnityEngine;

namespace SmartHome
{
    public class Swither : ElectriicObject, IToggle
    {
        [SerializeField]
        List<GameObject> connectors = new List<GameObject>();

        [SerializeField]
        GameObject pivot;
        [SerializeField]
        Renderer indicator;

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
            isOn = !isOn;
            foreach(GameObject connector in connectors)
            {
                if(connector.TryGetComponent<IToggle>(out IToggle toggle))
                {
                    if (coonectedToNetwork)
                    {
                        toggle.ChangeConnection(isOn);

                        if(toggle.ToggleBySwither)
                                toggle.Toggle();
                    }
                    else
                        toggle.ChangeConnection(coonectedToNetwork);
                }
            }

            SwithToggleVisual();
        }
        void SwithToggleVisual()
        {
            if (isOn)
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
