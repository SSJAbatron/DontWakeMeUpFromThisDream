using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WakeUpMeter : MonoBehaviour
{
    public Slider wakeUpMeter;

    private int maxValue = 100;
    private int currentValue;
    public float decreaseInterval = 2f;
    // Start is called before the first frame update

    public static WakeUpMeter instance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentValue = maxValue;
        wakeUpMeter.value = maxValue;
        wakeUpMeter.maxValue= maxValue;
        StartCoroutine(ReduceWakeUpValue());
    }

    void Update()
    {
        Debug.Log(currentValue);
    }

    private IEnumerator ReduceWakeUpValue()
    {
        while (true)
        {
            yield return new WaitForSeconds(decreaseInterval);
            currentValue -= 1;
            wakeUpMeter.value = currentValue;
        }
    }

    public void UpdateDontWakeUpMeter(int value)
    {
        if (currentValue + value > maxValue)
            currentValue = maxValue;
        else if(currentValue < 0)
            currentValue = 0;
        else
            currentValue += value;

        wakeUpMeter.value = currentValue;
    }

    public float getDontWakeUpMeterValue()
    {
        return currentValue;
    }

}
