using UnityEngine;


[CreateAssetMenu(fileName = "New item Data", menuName = "Data/Item Effect")]
public class ItemEffect : ScriptableObject
{
    public virtual void ExecuteEffect()
    {
        Debug.Log("Effect");
    }
}
