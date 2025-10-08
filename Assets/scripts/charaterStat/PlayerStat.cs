
using UnityEngine;

public class PlayerStat : CharacterStat
{
    private Player player;
    protected override void Start()
    {
        base.Start();
        player = GetComponent<Player>();
    }
    protected override void Die()
    {
        base.Die();
        player.Die();
    }
}
