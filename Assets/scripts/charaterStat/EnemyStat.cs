using UnityEngine;

public class EnemyStat :CharacterStat
{
    private Enemy enemy;
    protected override void Start()
    {
        base.Start();
        enemy = GetComponent<Enemy>();
    }

    protected override void Die()
    {
        base.Die();
        enemy.Die();
    }
}
