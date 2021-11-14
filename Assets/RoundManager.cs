using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
    public Slider WaterSlider;

    private void Awake()
    {
        WaterSlider.maxValue = 60;
        WaterSlider.value = 60;
    }

    // Update is called once per frame
    void Update()
    {
        WaterSlider.value -= Time.deltaTime;

        if (WaterSlider.value == 0)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        Time.timeScale = 0;
        Debug.Log("Game Over");
    }
}
