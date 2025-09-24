using UnityEngine;

public class EnemyStat :CharacterStat
{
    private Enemy enemy;
    public override void Start()
    {
        base.Start();
        enemy = GetComponent<Enemy>();
    }
    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);
        enemy.Damage();
    }

    protected override void Die()
    {
        base.Die();
        enemy.Die();
    }
}
