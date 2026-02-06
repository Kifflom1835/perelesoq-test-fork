using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace SmartHome
{
    public class Gate : ElectriicObject
    {
        [SerializeField]
        protected List<ElectriicObject> inputs = new List<ElectriicObject>();
        [SerializeField]
        protected ElectriicObject output;


        [SerializeField]
        protected List<Renderer> inputRenderers = new List<Renderer>();
        [SerializeField]
        protected Renderer outputRenderer;

        [SerializeField]
        bool isOR = true;

        void Start()
        {

        }
        void Update()
        {

        }
        public override void ChangeConnection(bool value)
        {
            base.ChangeConnection(value);

            CheckState();
        }
        public override void Toggle()
        {
            base.Toggle();

           CheckState();
        }

        void CheckState()
        {
            bool state;
            if (isOR)
                state = inputs.Any(input => CheckInput(input));
            else
                state = inputs.All(input => CheckInput(input));

            for (int i = 0; i < inputs.Count; i++)
            {
                /* if (isOn)
                     state = state || CheckInput(inputs[i]);
                 else
                     state = CheckInput(inputs[i]);*/

                if (CheckInput(inputs[i]))
                {
                    inputRenderers[i].material = MaterialsDataset.Instance.isOnMaterial;
                }
                else
                {
                    inputRenderers[i].material = MaterialsDataset.Instance.isOffMaterial;
                }

                inputRenderers[i].material = CheckInput(inputs[i]) ?
                    MaterialsDataset.Instance.isOnMaterial :
                    MaterialsDataset.Instance.isOffMaterial;
            }

            if (isOn != state)
            {
                output.Toggle();
            }

            isOn = state;

            outputRenderer.material = isOn ?
                MaterialsDataset.Instance.isOnMaterial :
                 MaterialsDataset.Instance.isOffMaterial;
        }

        bool CheckInput(ElectriicObject input)
        {
            return input.IsOn && input.CoonectedToNetwork;
        }
    }
}
