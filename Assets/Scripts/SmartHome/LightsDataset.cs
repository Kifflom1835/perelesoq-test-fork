using UnityEngine;

public class LightsDataset : MonoSingleton<LightsDataset>
{
    public GameObject Room1MainLight;
    public GameObject Room2MainLight;

    public Light GetLightByNumber(int number)
    {
        if(number == 1)
            return Room1MainLight.GetComponent<Light>();
        else
            return Room2MainLight.GetComponent<Light>();
    }
}
