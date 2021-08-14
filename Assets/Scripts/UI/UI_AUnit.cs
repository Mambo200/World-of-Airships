using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UI_AUnit : MonoBehaviour
{
    public abstract void SetHealthbar(float _current, float _max);
    public abstract void SetEnergiebar(float _current, float _max);
}
