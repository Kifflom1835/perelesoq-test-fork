using System.Collections;
using TMPro;
using UnityEngine;


namespace SmartHome
{
    public class Door : ElectriicObject, IToggle
    {
        [Header("Door Settings")]
        [SerializeField]
        private GameObject pivot;

        [SerializeField]
        private float rotationTime = 5f;

        [SerializeField]
        private float targetAngle = -90f;

        [Header("Door Driver")]
        [SerializeField]
        private TMP_Text doorTextState;


        private bool isOpen = false;
        private bool isAnimating = false;

        public override int GetConsumption => consumption/5;

        public override void Start()
        {
            base.Start();
            //TODO: восстановить сотояние из префов
        }

        [ContextMenu("Toggle Door")]
        public void ToggleFromApp()
        {
            if (!coonectedToNetwork) return;
            ToggleDoor();
        }

        public override void Toggle()
        {
            base.Toggle();

            return;

            if (!coonectedToNetwork) return;
                ToggleDoor();
        }
        public override void ChangeConnection(bool value)
        {
            base.ChangeConnection(value);
            SwithDoorDriverVisual();

        }

        public void ToggleDoor()
        {
            if (isAnimating) return;

            StartCoroutine(AnimateDoor());

            //TODO: записать расход сети
            //TODO: сохранить состояние в префах 
        }
        IEnumerator AnimateDoor()
        {
            isAnimating = true;
            isOn = true;

            float startAngle = NormalizeAngle(pivot.transform.localEulerAngles.y);
            float endAngle;

            if (startAngle == 0)
                endAngle = targetAngle;
            else
                endAngle = 0;

            float elapsedTime = 0f;

            while (elapsedTime < rotationTime)
            {
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / rotationTime);
                float currentAngle = Mathf.Lerp(startAngle, endAngle, t);


                pivot.transform.localEulerAngles = new Vector3(
                    pivot.transform.localEulerAngles.x,
                    currentAngle,
                    pivot.transform.localEulerAngles.z
                );

                yield return null;
            }

            pivot.transform.localEulerAngles = new Vector3(
              pivot.transform.localEulerAngles.x,
              isOpen ? 0f : targetAngle,
              pivot.transform.localEulerAngles.z
            );

            isOpen = !isOpen;
            isAnimating = false;
            isOn = false;

            ChangeDoorDriverState();

            yield return null;
        }

        private float NormalizeAngle(float angle)
        {
            angle %= 360f;
            if (angle > 180f) angle -= 360f;
            if (angle < -180f) angle += 360f;
            return angle;
        }

        private void ChangeDoorDriverState()
        {

            if (doorTextState == null) return;

            if (isOpen)
            {
                doorTextState.text = "OPENED";
            }
            else
            {
                doorTextState.text = "CLOSED";
            }
        }

        void SwithDoorDriverVisual()
        {
            if (coonectedToNetwork)
            {
                doorTextState.color = Color.green;
            }
            else
            {
                doorTextState.color = Color.red;
            }
        }

    }
}
