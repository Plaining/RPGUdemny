using UnityEngine;

public class EnermySkelenAnimationTrigger : MonoBehaviour
{
    public Enermy_Skeleton enermy => GetComponentInParent<Enermy_Skeleton>();

    private void AnimationTrigger()
    {
        enermy.AnimationTrigger();
    }
    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enermy.attackCheck.position, enermy.attackCheckRadius);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Player>() != null)
            {
                hit.GetComponent<Player
                    >().Damage();
            }
        }
    }
}
