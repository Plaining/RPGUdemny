using UnityEngine;

public class PalyerAnimationTrigger : MonoBehaviour
{
    public Player player => GetComponentInParent<Player>();

    private void AnimationTrigger()
    {
        player.AnimationTrigger();
    }
}
