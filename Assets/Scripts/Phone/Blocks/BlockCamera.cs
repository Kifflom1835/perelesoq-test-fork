using UnityEngine;
using UnityEngine.UI;
using SmartHome;

namespace Phone 
{
    public class BlockCamera : Block
    {
        [SerializeField] Button button;

        CameraSwitcher cameraSwitcher;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            cameraSwitcher = Camera.main.GetComponent<CameraSwitcher>();
            button.onClick.AddListener(SwitchCamera);
        }

        // Update is called once per frame
        void Update()
        {

        }

        void SwitchCamera()
        {
            cameraSwitcher.MoveToNextPoint();
            deviceName.text = "Camera" + cameraSwitcher.CurrentPointIndex;
        }
    }
}
