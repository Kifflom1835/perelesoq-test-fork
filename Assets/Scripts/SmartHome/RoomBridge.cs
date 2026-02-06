using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

namespace SmartHome {
    public class RoomBridge : ElectricObject
    {
        [SerializeField]
        Renderer indicator;

        [SerializeField]
        List<GameObject> connectors = new List<GameObject>();

        public override void ChangeConnection(bool value)
        {
            base.Toggle();
            ToggleBridge();
            SwithVisual();
        }

        [ContextMenu("Toggle Bridge")]
        void ToggleBridge()
        {
            coonectedToNetwork = !coonectedToNetwork;
            foreach (GameObject connector in connectors)
            {
                if (connector.TryGetComponent<IToggle>(out IToggle toggle))
                {
                    toggle.ChangeConnection(coonectedToNetwork);
                }
            }
        }

        void SwithVisual()
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
