using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Stat
{
    [SerializeField] protected int baseValue;

    public List<int> modifier = new List<int>();
    public int getValue()
    {
        int finalValue = baseValue;
        foreach (int modifier in modifier)
        {
            finalValue += modifier;
        }
        return finalValue;
    }

    public void SetDefaultValue(int _value)
    {
        baseValue = _value;
    }

    public void AddModifier(int _modifier)
    {
        modifier.Add(_modifier);
    }

    public void RemoveModifier(int _modifier)
    {
        modifier.RemoveAt(_modifier);
    }
}
