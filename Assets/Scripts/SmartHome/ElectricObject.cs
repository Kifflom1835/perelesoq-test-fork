using System;
using UnityEngine;
namespace SmartHome
{
    public class ElectricObject : MonoBehaviour, IToggle
    {
        [Header("Settings")]
        [SerializeField]
        protected int consumption;
        [SerializeField]
        protected bool coonectedToNetwork = false;
        [SerializeField]
        protected string ObjName = string.Empty;

        [SerializeField]
        protected bool isOn;
        [SerializeField]
        protected bool toggleBySwither = true;

        public bool CoonectedToNetwork => coonectedToNetwork;
        public bool IsOn { get { return isOn; } protected set { isOn = value; OnIsOnChanged?.Invoke(); } }

        public bool ToggleBySwither => toggleBySwither;
        public virtual int GetConsumption => consumption;
        public string GetObjName => ObjName;

        public Action OnConnectionToNetworkChanged;
        public Action OnIsOnChanged;

        public virtual void Start()
        {
            if (consumption > 0)
                PowerSource.Instance.RegisterDevice(this);
        }
        void Update()
        {

        }

        public virtual void ChangeConnection(bool value)
        {
            coonectedToNetwork = value;

            if (OnConnectionToNetworkChanged != null)
            {
                OnConnectionToNetworkChanged.Invoke();
            }
        }

        public virtual void Toggle()
        {
            if (!coonectedToNetwork) return;
        }

    }


    interface IToggle
    {
        bool ToggleBySwither { get; }
        public void Toggle();
        public void ChangeConnection(bool value);
    }
}