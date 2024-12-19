using UnityEngine;

public class EnermySkelenAnimationTrigger : MonoBehaviour
{
    public Enermy_Skeleton enermy => GetComponentInParent<Enermy_Skeleton>();

    private void AnimationTrigger()
    {
        enermy.AnimationTrigger();
    }
}
