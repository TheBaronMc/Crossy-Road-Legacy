using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    public Slider lifeSlider;
    public Slider ennemiesSlider;
    public Slider timeSlider;

    public TextMeshProUGUI lifeCount;
    public TextMeshProUGUI ennmiesCount;
    public TextMeshProUGUI timeCount;

    public void Start()
    {
        UpdateLife();
        UpdateEnnemies();
        UpdateTime();
    }

    private void UpdateCounter(Slider s, TextMeshProUGUI c)
    {
        c.text = s.value.ToString();
    }

    private void UpdateValue(string key, Slider s, TextMeshProUGUI c)
    {
        PlayerPrefs.SetInt(key, (int)s.value);
        UpdateCounter(s, c);
    }

    public void UpdateLife()
    {
        UpdateValue("LIFE", lifeSlider, lifeCount);
    }

    public void UpdateEnnemies()
    {
        UpdateValue("ENNEMIES", ennemiesSlider, ennmiesCount);
    }

    public void UpdateTime()
    {
        UpdateValue("TIME", timeSlider, timeCount);
    }
}
