using SmartHome;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Phone
{
    public class BlockDoor : Block
    {
        [SerializeField] Button button;
        [SerializeField] TMP_Text btn_Text;

        public Door door;

        void Start()
        {

        }

        void Update()
        {

        }

        private void OnDestroy()
        {
            if (door != null)
            {
                door.OnIsOpenChanged += OnDoorStatusChanged;
                door.OnConnectionToNetworkChanged += OnConnectionChanged;
            }
        }

        public override void Init(ElectricObject obj)
        {

            door = obj as Door;

            OnDoorStatusChanged();

            door.OnIsOpenChanged += OnDoorStatusChanged;
            door.OnConnectionToNetworkChanged += OnConnectionChanged;

            button.onClick.AddListener(OnButtonClick);

            SetButtonText();
        }

        void OnDoorStatusChanged()
        {
            if (door.IsOpen)
                status.text = "opened";
            else
                status.text = "closed";
        }
        void OnConnectionChanged()
        {
            button.interactable = door.CoonectedToNetwork;
        }

        void OnButtonClick()
        {
            door.Toggle();
            StartCoroutine(DoorAnim());
        }

        IEnumerator DoorAnim()
        {
            button.interactable = false;
            yield return new WaitForSeconds(door.RotationTime);
            button.interactable = true;

            SetButtonText();
        }

        void SetButtonText()
        {
            if (door.IsOpen)
                btn_Text.text = "CLOSE";
            else
                btn_Text.text = "OPEN";
        }
    }
}
