using System.Collections.Generic;
using UnityEngine;

public class Sword_Skill_Controller : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private CircleCollider2D cd;
    private Player player;

    private bool canRotate = true;
    private bool isReturning;

    private float freezeTimeDuration;
    private float returnSpeed;

    [Header("Pierce info")]
    private int pierceAmount;

    [Header("Bounce info")]
    private float bounceSpeed;
    private bool isBouncing;
    private int bounceAmount;
    private List<Transform> enermyTarget;
    private int targetIndex;

    [Header("Spin info")]
    private float maxTravelDistance;
    private float spinDuration;
    private float spinTimer;
    private bool wasStopped;
    private bool isSpining;

    private float hitTimer;
    private float hitCooldown;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<CircleCollider2D>();
    }

    private void DestoryMe()
    {
        Destroy(gameObject);
    }
    public void SetupSword(Vector2 _dir, float _gravityScale, Player _player, float _freezeTimeDuration, float _returnSpeed)
    {
        player = _player;
        returnSpeed = _returnSpeed;
        rb.linearVelocity = _dir;
        rb.gravityScale = _gravityScale;
        freezeTimeDuration = _freezeTimeDuration;
        if (pierceAmount <= 0)
        {
            anim.SetBool("Rotation", true);
        }
/*����ͨ������ɾ����Զ�Ľ���Ҳ����ͨ��ʱ��ɾ��*/
        /*if (Vector2.Distance(transform.position, player.transform.position) > 20)
        {
            DestoryMe();
        }*/ 
        Invoke("DestoryMe", 7);
    }

    public void SetUpBounce(bool _isBouncing, int _bouncesAmount, float _bounceSpeed)
    {
        this.isBouncing = _isBouncing;
        this.bounceAmount = _bouncesAmount;
        this.bounceSpeed = _bounceSpeed;
        enermyTarget = new List<Transform>();
    }

    public void SetupPierce(int _pierceAmount)
    {
        this.pierceAmount = _pierceAmount;
    }

    public void SetupSpin(bool _isSpining, float _maxTravelDistance, float _spinDuration, float _hitCooldown)
    {
        isSpining = _isSpining;
        this.spinDuration = _spinDuration;
        this.maxTravelDistance = _maxTravelDistance;
        this.hitCooldown = _hitCooldown;
    }
    public void ReturnSword()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        rb.bodyType = RigidbodyType2D.Kinematic;
        transform.parent = null;
        isReturning = true;
    }

    public void Update()
    {
        if (canRotate)
            transform.right = rb.linearVelocity;
        BounceLogic();

        if (isReturning)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, returnSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, player.transform.position) < 1)
            {

                player.catchTheSword();
            }
        }

        SpinLogic();
    }

    private void SpinLogic()
    {
        if (isSpining)
        {
            if (Vector2.Distance(player.transform.position, transform.position) > maxTravelDistance && !wasStopped)
            {
                stopWhenSpining();
            }
            if (wasStopped)
            {
                spinTimer -= Time.deltaTime;
                if (spinTimer < 0)
                {
                    isReturning = true;
                    isSpining = false;
                }

                hitTimer -= Time.deltaTime;
                if (hitTimer < 0)
                {
                    hitTimer = hitCooldown;
                    Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1);
                    foreach (var hit in colliders)
                    {
                        if(hit.GetComponent<Enemy>() != null)
                        {
                            SwordSkillDamage(hit.GetComponent<Enemy>());
                        }
                    }
                }
            }
        }
    }

    private void stopWhenSpining()
    {
        wasStopped = true;
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        spinTimer = spinDuration;
    }

    private void BounceLogic()
    {
        if (isBouncing && enermyTarget.Count > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, enermyTarget[targetIndex].position, bounceSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, enermyTarget[targetIndex].position) < .1f)
            {
                SwordSkillDamage(enermyTarget[targetIndex].GetComponent<Enemy>());
                targetIndex++;
                bounceAmount--;
                if (bounceAmount <= 0)
                {
                    isBouncing = false;
                    isReturning = true;
                }
                if (targetIndex >= enermyTarget.Count)
                {
                    targetIndex = 0;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isReturning)
        {
            return;
        }
        if(collision.GetComponent<Enemy>() != null)
        {
            Enemy enermy = collision.GetComponent<Enemy>();
            SwordSkillDamage(enermy);
        }
        //collision.GetComponent<Enemy>()?.DamageImpact();
        SetupTargetsForBounds(collision);
        StuckInto(collision);
    }
     
    private void SwordSkillDamage(Enemy enermy)
    {
        player.stat.doDamage(enermy.GetComponent<CharacterStat>());
        enermy.StartCoroutine("FreezeTimerFor", freezeTimeDuration);
    }

    private void SetupTargetsForBounds(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            if (isBouncing && enermyTarget.Count <= 0)
            {
                /*��һ��10��С����ײԲ����������ж���enermy*/
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10);
                foreach (var hit in colliders)
                {
                    if (hit.GetComponent<Enemy>() != null)
                    {
                        enermyTarget.Add(hit.transform);
                    }
                }
            }
        }
    }

    /*��������Ŀ������*/
    private void StuckInto(Collider2D collision)
    {
        if (pierceAmount > 0 && collision.GetComponent<Enemy>() != null)
        {
            pierceAmount--;
            return;
        }
        if (isSpining)
        {
            stopWhenSpining();
            return;
        }
        canRotate = false;
        cd.enabled = false;

        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        /*���������bouncing���ֹ���ģʽ���Ҽ��Բ����ĵ�����������0����ô���أ����Ա�����ײ��
         * ����Ϳ�ס��
         */
        if (isBouncing && enermyTarget.Count > 0)
        {
            return;
        }

        anim.SetBool("Rotation", false);
        transform.parent = collision.transform;
    }
}
