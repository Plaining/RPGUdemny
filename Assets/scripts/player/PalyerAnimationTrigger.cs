using UnityEngine;

public class PalyerAnimationTrigger : MonoBehaviour
{
    public Player player => GetComponentInParent<Player>();

    private void AnimationTrigger()
    {
        player.AnimationTrigger();
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);
        foreach(var hit in colliders)
        {
            if(hit.GetComponent<Enemy>()!= null)
            {
                EnemyStat _target = hit.GetComponent<EnemyStat>();
                player.stat.doDamage(_target);
            }
        }
    }

    private void ThrowSword()
    {
        SkillManager.instance.sword.CreateSword();
    }
}
