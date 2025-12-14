using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{
    Weapon,
    Armor,//»¤¼×
    Amulet,//»¤·û
    Flash//Ò©Æ¿
}
[CreateAssetMenu(fileName = "ItemData_Equipment", menuName = "Data/Equipment")]
public class ItemData_Equipment : ItemData
{
    public EquipmentType equipmentType;

    public ItemEffect[] itemEffects;

    [Header("Major stats")]
    public int strength;// ÉËº¦! 1 point increase damage by 1 and crit.power by 1%
    public int agility; // Ãô½Ý! 1 point increase evasion by 1% and crit.chance by 1%
    public int intelligence; // ÖÇÁ¦ Ä§·¨¹¥»÷! 1 point increase magic damage by 1 and magic resistance by 3%
    public int vitality; // »îÁ¦ »Ö¸´HP! 1 point increase health by 3 or 5 point

    [Header("Offensive stats")]
    public int critChange;//±©»÷ÂÊ
    public int critPower;//±©»÷ÉËº¦
    public int damage;

    [Header("Defensive stats")]
    public int maxHealth;
    public int evasion; //ÉÁ±Ü
    public int armor; //ÎäÆ÷¼õÉË
    public int magicRegistance;//Ä§·¨¿¹ÐÔ

    [Header("Magic stats")]
    public int fireDamage;
    public int iceDamage;
    public int lightningDamage;

    [Header("Craft requirements")]
    public List<InventoryItem> craftingMaterials;

    public void ExecuteItemEffect()
    {
        foreach (var item in itemEffects)
        {
            item.ExecuteEffect();
        }
    }
    public void AddModifiers()
    {
        PlayerStat playerStat = PlayerManager.instance.player.GetComponent<PlayerStat>();
        playerStat.strength.AddModifier(strength);
        playerStat.agility.AddModifier(agility);
        playerStat.intelligence.AddModifier(intelligence);
        playerStat.vitality.AddModifier(vitality);
        playerStat.critChange.AddModifier(critChange);
        playerStat.critPower.AddModifier(critPower);
        playerStat.damage.AddModifier(damage);
        playerStat.maxHealth.AddModifier(maxHealth);
        playerStat.evasion.AddModifier(evasion);
        playerStat.armor.AddModifier(armor);
        playerStat.magicRegistance.AddModifier(magicRegistance);
        playerStat.fireDamage.AddModifier(fireDamage);
        playerStat.iceDamage.AddModifier(iceDamage);
        playerStat.lightningDamage.AddModifier(lightningDamage);
    }
    public void RemoveModifiers() {
        PlayerStat playerStat = PlayerManager.instance.player.GetComponent<PlayerStat>();

        playerStat.strength.RemoveModifier(strength);
        playerStat.agility.RemoveModifier(agility);
        playerStat.intelligence.RemoveModifier(intelligence);
        playerStat.vitality.RemoveModifier(vitality);
        playerStat.critChange.RemoveModifier(critChange);
        playerStat.critPower.RemoveModifier(critPower);
        playerStat.damage.RemoveModifier(damage);
        playerStat.maxHealth.RemoveModifier(maxHealth);
        playerStat.evasion.RemoveModifier(evasion);
        playerStat.armor.RemoveModifier(armor);
        playerStat.magicRegistance.RemoveModifier(magicRegistance);
        playerStat.fireDamage.RemoveModifier(fireDamage);
        playerStat.iceDamage.RemoveModifier(iceDamage);
        playerStat.lightningDamage.RemoveModifier(lightningDamage);
    }
}
