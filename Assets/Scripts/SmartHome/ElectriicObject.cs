using UnityEngine;
namespace SmartHome
{
    public class ElectriicObject : MonoBehaviour, IToggle
    {
        [Header("Settings")]
        [SerializeField]
        protected int consumption;
        [SerializeField]
        protected bool coonectedToNetwork = false;
        protected string ObjName = string.Empty;

        [SerializeField]
        protected bool isOn;
        [SerializeField]
        protected bool toggleBySwither = true;

        public bool IsOn => isOn;
        public bool CoonectedToNetwork => coonectedToNetwork;

        public bool ToggleBySwither => toggleBySwither;
        public virtual int GetConsumption => consumption;

        public virtual void ChangeConnection(bool value)
        {
            coonectedToNetwork = value;
        }

        public virtual void Toggle()
        {
            if (!coonectedToNetwork) return;
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        public virtual void Start()
        {
            if (consumption > 0)
                PowerSource.Instance.RegisterDevice(this);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }


    interface IToggle
    {
        bool ToggleBySwither { get; }
        public void Toggle();
        public void ChangeConnection(bool value);
    }
}