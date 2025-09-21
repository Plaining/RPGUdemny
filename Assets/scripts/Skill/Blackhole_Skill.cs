using UnityEngine;

public class Blackhole_Skill : Skill
{
    [SerializeField] private int amountOfAttacks;
    [SerializeField] private float cloneAttackCooldown;
    [SerializeField] private float blackholeDuration;
    [Space]
    [SerializeField] private GameObject blackHolePrefab;
    [SerializeField] private float maxSize;
    [SerializeField] private float growSpeed;
    [SerializeField] private float shrinkSpeed;

    Blackhole_skill_controller currentBlackHole;
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override void UseSkill()
    {
        base.UseSkill();
        GameObject newBlackHole = Instantiate(blackHolePrefab, player.transform.position, Quaternion.identity);
        currentBlackHole = newBlackHole.GetComponent<Blackhole_skill_controller>();
        currentBlackHole.SetupBlackhole(maxSize,growSpeed,shrinkSpeed,amountOfAttacks, cloneAttackCooldown, blackholeDuration);
    }

    public bool SkillCompleted()
    {
        if (!currentBlackHole)
        {
            return false;
        }
        if (currentBlackHole.playerCanExitState)
        {
            currentBlackHole = null;
            return true;
        }
        return false;
    }
}
