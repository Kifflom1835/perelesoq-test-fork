using SmartHome;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSource : MonoSingleton<PowerSource>
{
    [Header("Statistic")]
    [SerializeField] private float totalEnergyConsumed = 0f; // кВт·ч
    [SerializeField] private float currentPowerDraw = 0f; // Вт
    [SerializeField] private DateTime startTime;

    [SerializeField] private TMPro.TMP_Text tmpDisplayText;

    [SerializeField] private List<ElectriicObject> connectedDevices = new List<ElectriicObject>();

    [SerializeField]
    private bool isEnable = true;

    WaitForSeconds  wairForOneSecond= new WaitForSeconds(1);

    void Start()
    {
        startTime = DateTime.Now;

        StartCoroutine(UpdateData());
    }

    void Update()
    {
        
    }

    public void RegisterDevice(ElectriicObject obj)
    {
        if(!connectedDevices.Contains(obj))
            connectedDevices.Add(obj);
    }

    IEnumerator UpdateData()
    {
        while (isEnable)
        {
            UpdatePowerConsumption();
            UpdateEnergyConsumption();
            UpdateDisplay();
            yield return wairForOneSecond;
        }
    }

    private void UpdatePowerConsumption()
    {
        float previousDraw = currentPowerDraw;
        currentPowerDraw = 0f;

        for (int i = 0; i < connectedDevices.Count; i++)
        {
            ElectriicObject device = connectedDevices[i];

            if (device.IsOn && device.CoonectedToNetwork)
            {
                float devicePower = device.GetConsumption;
                currentPowerDraw += devicePower;
            }
        }
    }

    private void UpdateEnergyConsumption()
    {
        float deltaTime = Time.deltaTime / 3600f; // Конвертируем секунды в часы

        for (int i = 0; i < connectedDevices.Count; i++)
        {
            ElectriicObject device = connectedDevices[i];

            if (device.IsOn && device.CoonectedToNetwork)
            {
                // Энергия = Мощность (Вт) × Время (часы)
                float energy = (currentPowerDraw) * deltaTime;
                totalEnergyConsumed += energy;
            }
        }
    }


    private void UpdateDisplay()
    {
        string displayString = $"CURRENT: {currentPowerDraw:F1} W\n" +
                              $"total: {totalEnergyConsumed:F3} W·h\n" +
                              $"TIME: {(DateTime.Now - startTime):hh\\:mm\\:ss}\n";

        if (tmpDisplayText != null)
            tmpDisplayText.text = displayString;
    }
}
