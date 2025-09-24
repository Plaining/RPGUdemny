using UnityEngine;
using NUnit.Framework;
using System.Collections.Generic;

public class CharacterStat : MonoBehaviour
{
    [Header("Major stats")]
    public Stat strength;// power! 1 point increase damage by 1 and crit.power by 1%
    public Stat agility; // movespeed! 1 point increase evasion by 1% and crit.chance by 1%
    public Stat intelligence; // naozi! 1 point increase magic damage by 1 and magic resistance by 3%
    public Stat vitality; // huoli! 1 point increase health by 3 or 5 point

    [Header("Defensive stats")]
    public Stat maxHealth;
    public Stat evasion; //shanbi shanbi
    public Stat armor; // wuqi jianshang

    public Stat damage;
    [SerializeField] private int currentHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Start()
    {
        currentHealth = maxHealth.getValue();
    }

    public virtual void doDamage(CharacterStat _targetStat)
    {
        if (TargetCanAvoidAttack(_targetStat))
        {
            return;
        }
        int totalDamage = damage.getValue() + strength.getValue();
        totalDamage = CheckTargetArmor(_targetStat, totalDamage);
        _targetStat.TakeDamage(totalDamage);
    }

    private static int CheckTargetArmor(CharacterStat _targetStat, int totalDamage)
    {
        totalDamage -= _targetStat.armor.getValue();
        totalDamage = Mathf.Clamp(totalDamage, 0, int.MaxValue);
        return totalDamage;
    }

    private static bool TargetCanAvoidAttack(CharacterStat _targetStat)
    {
        int totalEvasion = _targetStat.evasion.getValue() + _targetStat.agility.getValue();
        if (totalEvasion > Random.Range(0, 100))
        {
            Debug.Log("Active Avoided");
            return true;
        }

        return false;
    }

    public virtual void TakeDamage(int _damage)
    {
        currentHealth -= _damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        //throw new NotImplementedException();
    }
}
