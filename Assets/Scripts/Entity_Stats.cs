using UnityEngine;

public class Entity_Stats : MonoBehaviour
{
    public Stat maxHealth;
    public Stat_MajorGroup major;
    public Stat_OffenseGroup offense;
    public Stat_DefenseGroup defense;


    public float GetPhysicalDamage()
    {
        float baseDamage = offense.damage.GetValue();
        float bonusDamage = major.strength.GetValue();
        float totalBaseDamage = baseDamage + bonusDamage;

        float baseCritChance = offense.critChance.GetValue();
        float bonusCritChance = major.agiliti.GetValue() * .3f; //Chance from agility: +0.3% per AGI
        float critChance = baseCritChance + bonusCritChance;

        float baseCritPower = offense.critPower.GetValue();
        float bonusCritPower = major.strength.GetValue(); //Chance from Strength: +0.3% per AGI
        float critPower = (baseCritPower + bonusCritPower) / 100; //Total crit power as multiplier 

        bool isCrit = Random.Range(0, 100) < critChance;
        float finalDamage = isCrit ? totalBaseDamage * critPower : totalBaseDamage;

        return finalDamage;

    }
    public float GetMaxHealth()
    {
        float baseHp = maxHealth.GetValue();
        float bonusHp = major.vitality.GetValue();

        return (baseHp + bonusHp);
    }
    public float GetEvasion()
    {
        float baseEvasion = defense.evasion.GetValue();
        float bonusEvasion = major.agiliti.GetValue() * .5f;

        float totalEvasion = baseEvasion + bonusEvasion;
        float evasionCap = 85f;

        float finalEvasion = Mathf.Clamp(totalEvasion, 0, evasionCap);

        return finalEvasion;
    }
}
