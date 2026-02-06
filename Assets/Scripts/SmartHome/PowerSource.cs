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

    [SerializeField] private List<ElectricObject> connectedDevices = new List<ElectricObject>();


    [Header("Sorted objects")]
    [SerializeField] private List<ElectricObject> swithers = new List<ElectricObject>();
    [SerializeField] private List<ElectricObject> lamps = new List<ElectricObject>();
    [SerializeField] private ElectricObject door;
    [SerializeField] private CameraSwitcher cameraSwitcher;
    [SerializeField] private List<ElectricObject> gates = new List<ElectricObject>();


    public List<ElectricObject> GetSwithers => swithers;
    public List<ElectricObject> GetLamps => lamps;
    public List<ElectricObject> GetGates => gates;
    public ElectricObject GetDoor => door;

    public float TotalEnergyConsumed => totalEnergyConsumed; 
    public float CurrentPowerDraw => currentPowerDraw;
    public DateTime StartTime => startTime;


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

    public void RegisterDevice(ElectricObject obj)
    {
        if(!connectedDevices.Contains(obj))
            connectedDevices.Add(obj);
    }

    public void RegisterSwither(ElectricObject obj)
    {
        if (!swithers.Contains(obj))
            swithers.Add(obj);
    }
    public void RegisterDoor(ElectricObject obj)
    {
        door = (Door)obj;
    }

    public void RegisterLamp(ElectricObject obj)
    {
        if (!lamps.Contains(obj))
            lamps.Add(obj);
    }

    public void RegisterGate(ElectricObject obj, bool isOR)
    {
        if (!gates.Contains(obj))
            gates.Add(obj);
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
            ElectricObject device = connectedDevices[i];

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
            ElectricObject device = connectedDevices[i];

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
