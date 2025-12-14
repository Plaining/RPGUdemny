using UnityEngine;

[CreateAssetMenu(fileName = "Thunder strike effect", menuName = "Data/Item effect/Thunder strike")]
public class ThunderStrike_effect : ItemEffect
{
    [SerializeField] private GameObject thunderStrikePrefab;
    public override void ExecuteEffect()
    {
        //base.ExecuteEffect();
        GameObject newThunderStrike = Instantiate(thunderStrikePrefab);

    }
}
