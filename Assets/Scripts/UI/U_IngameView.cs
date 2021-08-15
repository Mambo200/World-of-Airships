using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class U_IngameView : UI_AUnit
{
    [SerializeField]
    private Image HealthBar;
    [SerializeField]
    private Image EnergieBar;


    public override void SetHealthbar(float _current, float _max)
    {
        HealthBar.fillAmount = _current / _max;
    }

    public override void SetEnergiebar(float _current, float _max)
    {
        EnergieBar.fillAmount = _current / _max;
    }
}
