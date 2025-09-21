using UnityEngine;

public class Clone_Skill_Controller : MonoBehaviour
{
    private SpriteRenderer sr;
    private Animator anim;

    [SerializeField] private float colorLoosingSpeed;
    private float cloneTimer;
    [SerializeField] private Transform attackCheck;
    [SerializeField] private float attackCheckRadius = .8f;

    private Transform closestEnermy;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        cloneTimer -= Time.deltaTime;
        if (cloneTimer < 0)
        {
            sr.color = new Color(1,1,1,sr.color.a-(Time.deltaTime*colorLoosingSpeed));
            if (sr.color.a <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
    public void SetupClone(Transform _newTransform, float _newCloneDuration, bool canAttack, Vector3 _offset)
    {
        if (canAttack) {
            anim.SetInteger("AttackNumber", Random.Range(1, 3));
        }
        transform.position = _newTransform.position + _offset;
        cloneTimer = _newCloneDuration;
        FaceClosesTarget();
    }
    private void AnimationTrigger()
    {
        cloneTimer = -.1f;
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCheck.position, attackCheckRadius);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enermy>() != null)
            {
                hit.GetComponent<Enermy>().Damage();
            }
        }
    }
    private void FaceClosesTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 25);
        float closesDistance = Mathf.Infinity;
        foreach (var hit in colliders)
        {
            if(hit.GetComponent<Enermy>() != null)
            {
                float distanceToEnermy = Vector2.Distance(transform.position,hit.transform.position);
                if(distanceToEnermy < closesDistance)
                {
                    closestEnermy = hit.transform;
                    closesDistance = distanceToEnermy;
                }
            }
        }
        if (closestEnermy != null) { 
            if(transform.position.x > closestEnermy.position.x)
            {
                transform.Rotate(0, 180, 0);
            }
        }
    }
}
