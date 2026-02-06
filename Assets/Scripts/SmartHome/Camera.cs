using UnityEngine;

namespace SmartHome
{
    public class CameraSwitcher : MonoBehaviour
    {

        [SerializeField] private Transform[] cameraPoints = new Transform[3];
        [SerializeField] private int currentPointIndex = 0;

        public int CurrentPointIndex => currentPointIndex;

        private Camera mainCamera;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            mainCamera = Camera.main;
            if (mainCamera == null)
            {
                mainCamera = GetComponent<Camera>();
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void GoToPoint1() => MoveToPoint(0);
        public void GoToPoint2() => MoveToPoint(1);
        public void GoToPoint3() => MoveToPoint(2);


        [ContextMenu("MoveToNextPoint")]
        public void MoveToNextPoint()
        {
            int nextIndex = (currentPointIndex + 1) % cameraPoints.Length;
            MoveToPoint(nextIndex);
        }

        public void MoveToPoint(int index)
        {
            if (index < 0 || index >= cameraPoints.Length) return;

            Transform point = cameraPoints[index];
            if (point == null)
            {
                Debug.LogWarning($"Camera point {index} is not assigned!");
                return;
            }

                transform.position = point.position;
                transform.rotation = point.rotation;

            currentPointIndex = index;
        }
    }
}
