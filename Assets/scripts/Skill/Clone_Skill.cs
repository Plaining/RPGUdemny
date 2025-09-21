using UnityEngine;

public class Clone_Skill : Skill
{
    [Header("clone info")]
    [SerializeField] private float cloneDuration;
    [SerializeField] private GameObject clonePrefab;

    [Space]
    [SerializeField] private bool canAttack;
    public void CreateClone(Transform _clonePosition, Vector3 _offset)
    {
        GameObject _newClone = Instantiate(clonePrefab);
        _newClone.GetComponent<Clone_Skill_Controller>().SetupClone(_clonePosition, cloneDuration, canAttack, _offset);
    }
}
