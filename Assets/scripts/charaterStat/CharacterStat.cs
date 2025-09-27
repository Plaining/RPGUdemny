using UnityEngine;
using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;

public class CharacterStat : MonoBehaviour
{
    [Header("Major stats")]
    public Stat strength;// power! 1 point increase damage by 1 and crit.power by 1%
    public Stat agility; // minjie! 1 point increase evasion by 1% and crit.chance by 1%
    public Stat intelligence; // naozi! 1 point increase magic damage by 1 and magic resistance by 3%
    public Stat vitality; // huoli! 1 point increase health by 3 or 5 point

    [Header("Offensive stats")]
    public Stat critChange;//baojilv
    public Stat critPower;//baojishanghai
    public Stat damage;

    [Header("Defensive stats")]
    public Stat maxHealth;
    public Stat evasion; //shanbi shanbi
    public Stat armor; // wuqi jianshang
    public Stat magicRegistance;

    [Header("Magic stats")]
    public Stat fireDamage;
    public Stat iceDamage;
    public Stat lightningDamage;

    public bool isIgnited; // does damage over time
    public bool isChilled; // reduce armor by 20%
    public bool isShocked; // reduct accuracy by 20%

    private float ignitedTimer;
    private float chilledTimer;
    private float shockedTimer;

    private float igniteDamageCooldown = .3f;
    private float igniteDamageTimer;
    private int igniteDamage;

    public int currentHealth;

    public System.Action onHealthChanged;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        critPower.SetDefaultValue(150);
        currentHealth = GetMaxHealthValue();
    }

    protected virtual void Update()
    {
        ignitedTimer -= Time.deltaTime;
        chilledTimer -= Time.deltaTime;
        shockedTimer -= Time.deltaTime;

        igniteDamageTimer -= Time.deltaTime;
        if(ignitedTimer < 0 )
        {
            isIgnited = false;
        }
        if (chilledTimer < 0) 
        {
            isChilled = false;
        }
        if(shockedTimer < 0)
        {
            isShocked = false;
        }
        if ( igniteDamageTimer < 0 && isIgnited)
        {
            Debug.Log("Take burn damage:" + igniteDamage);
            DecreaseHeathBy(igniteDamage);
            if (currentHealth < 0)
            {
                Die();
            }
            igniteDamageTimer = igniteDamageCooldown;
        }
    }

    public virtual void doDamage(CharacterStat _targetStat)
    {
        if (TargetCanAvoidAttack(_targetStat))
        {
            return;
        }
        int totalDamage = damage.getValue() + strength.getValue();
        if (CanCrit())
        {
            totalDamage = CalculateCriticalDamage(totalDamage);
        }
        totalDamage = CheckTargetArmor(_targetStat, totalDamage);
        //_targetStat.TakeDamage(totalDamage);
        DoDamicalDamage(_targetStat);
    }

    public virtual void DoDamicalDamage(CharacterStat _targetStat)
    {
        int _fireDamage = fireDamage.getValue();
        int _iceDamage = iceDamage.getValue();
        int _lightningDamage = lightningDamage.getValue();
        int totalMagicalDamage = _fireDamage + _iceDamage + _lightningDamage + intelligence.getValue();
        totalMagicalDamage = CheckTargetResistance(_targetStat, totalMagicalDamage);
        _targetStat.TakeDamage(totalMagicalDamage);

        if(Mathf.Max(_fireDamage,_iceDamage,_lightningDamage)<=0)
        {
            return;
        }

        bool canApplyIgnite = _fireDamage > _iceDamage && _fireDamage > _lightningDamage;
        bool canApplyChill = _iceDamage > _fireDamage && _iceDamage > _lightningDamage;
        bool canApplyShock = _lightningDamage > _fireDamage && _lightningDamage > _iceDamage;


        while(!canApplyChill && !canApplyShock && !canApplyIgnite)
        {
            if (Random.value < .5f && _fireDamage > 0)
            {
                canApplyIgnite = true;
                _targetStat.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);
                return;
            }
            if (Random.value < .5f && _iceDamage > 0)
            {
                canApplyChill = true;
                _targetStat.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);
                return;
            }
            if (Random.value < .5f && _lightningDamage > 0)
            {
                canApplyIgnite = true;
                _targetStat.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);
                return;
            }

        }
        
        if (canApplyIgnite)
        {
            _targetStat.SetupIgniteDamage(Mathf.RoundToInt(_fireDamage * .2f));
        }

        _targetStat.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock); 
    }

    private static int CheckTargetResistance(CharacterStat _targetStat, int totalMagicalDamage)
    {
        totalMagicalDamage -= _targetStat.magicRegistance.getValue() + (_targetStat.intelligence.getValue() * 3);
        totalMagicalDamage = Mathf.Clamp(totalMagicalDamage, 0, int.MaxValue);
        return totalMagicalDamage;
    }

    private static int CheckTargetArmor(CharacterStat _targetStat, int totalDamage)
    {
        if (_targetStat.isChilled)
        {
            totalDamage -= Mathf.RoundToInt(_targetStat.armor.getValue() * .8f);
        }
        else
        {
            totalDamage -= _targetStat.armor.getValue();
        }
        totalDamage = Mathf.Clamp(totalDamage, 0, int.MaxValue);
        return totalDamage;
    }

    private bool TargetCanAvoidAttack(CharacterStat _targetStat)
    {
        // emermy minjie
        int totalEvasion = _targetStat.evasion.getValue() + _targetStat.agility.getValue();

        if (isShocked)
        {
            totalEvasion += 20;
        }

        if (totalEvasion > Random.Range(0, 100))
        {
            Debug.Log("Active Avoided");
            return true;
        }

        return false;
    }

    public void ApplyAilments(bool _ignite, bool _chill, bool _shock)
    {
        if (isIgnited || isChilled || isShocked) {
            return;
        }

        if (_ignite)
        {
            isIgnited = _ignite;
            ignitedTimer = 2;
        }
        if (_chill)
        {
            isChilled = _chill;
            chilledTimer = 2;
        }
        if (_shock)
        {
            isShocked = _shock;
            shockedTimer = 2;
        }

    }

    public void SetupIgniteDamage(int _damage)=>igniteDamage = _damage;

    public virtual void TakeDamage(int _damage)
    {
        DecreaseHeathBy(_damage);
        currentHealth -= _damage;
        if (currentHealth <= 0)
        {
            Die();
        }

    }

    protected virtual void DecreaseHeathBy(int _damage)
    {
        currentHealth -= _damage;
        if(onHealthChanged != null)
        {
            onHealthChanged();
        }
    }

    protected virtual void Die()
    {
        //throw new NotImplementedException();
    }

    private bool CanCrit()
    {
        int totalCriticalChance = critChange.getValue() + agility.getValue();
        if (Random.Range(0,100)<= totalCriticalChance)
        {
            return true;
        }
        return false;
    }

    private int CalculateCriticalDamage(int _damage)
    {
        //Debug.Log("pre critical damage:" + critPower.getValue() + _damage);//baoshang
        float totalCritPower = (critPower.getValue() + strength.getValue()) * .01f;
        float critDamage = _damage * totalCritPower;
        //Debug.Log("critical damage:" + critDamage);
        return Mathf.RoundToInt(critDamage);

    }

    public int GetMaxHealthValue()
    {
        return maxHealth.getValue() + vitality.getValue() * 5;
    }
}
