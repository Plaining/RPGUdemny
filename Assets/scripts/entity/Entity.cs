using System.Collections;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public int facingDir { get; private set; } = 1;
    public bool facingRight = true;

    public System.Action onFlipped;

    [Header("Knockback info")]
    [SerializeField] protected Vector2 knockbackDirection;
    [SerializeField] protected float knockbackDuration; //0.07是最好的数值，被击退的距离
    protected bool isKnocked;

    [Header("Collision info")]
    public Transform attackCheck;
    public float attackCheckRadius;
    [SerializeField] protected Transform groudCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask WhatIsGround;
    

    #region Components
    public CapsuleCollider2D cd { get; private set; }
    public CharacterStat stat {  get; private set; }
    public Animator anim { get; private set; }
    public SpriteRenderer sr { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public EntityFX fx { get; private set; }
    #endregion

    protected virtual void Awake()
    {
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        fx = GetComponent<EntityFX>();
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        stat = GetComponent<CharacterStat>();
        cd = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    public virtual void SlowEntityBy(float _slowPercentage, float _slowDuration)
    {
        Debug.Log("error");
    }

    protected virtual void ReturnDefaultSpeed()
    {
        anim.speed = 1;
    }


    public virtual void Damage()
    {
        fx.StartCoroutine("FlashFX");
        StartCoroutine("HitKnockback");
    }

    protected virtual IEnumerator HitKnockback()
    {
        isKnocked = true;
        rb.linearVelocity = new Vector2(knockbackDirection.x * -facingDir, knockbackDirection.y);
        yield return new WaitForSeconds(knockbackDuration);
        isKnocked = false;
    }

    #region Velocity
    public virtual void setZeroVelocity()
    {
        SetVelocity(0, 0);
    }

    public virtual void SetVelocity(float _xVelocity, float _yVelocity)
    {
        if (isKnocked)
        {
            //如果玩家被击退了，僵直
            return;
        }
        rb.linearVelocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(rb.linearVelocityX);
    }
    #endregion
    #region Collision
    public virtual bool isGroundDetected() => Physics2D.Raycast(groudCheck.position, Vector2.down, groundCheckDistance, WhatIsGround);
    public virtual bool isWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, WhatIsGround);

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groudCheck.position, new Vector3(groudCheck.position.x, groudCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
        Gizmos.DrawWireSphere(attackCheck.position,attackCheckRadius);
    }
    #endregion
    #region Flip
    public virtual void Flip()
    {
        facingDir *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
        if (onFlipped != null) {
            onFlipped();
        }
    }
    public virtual void FlipController(float _x)
    {
        if (_x > 0 && !facingRight)
        {//如果有向右的向量，且不是面向右边，需要flip
            Flip();
        }
        else if (_x < 0 && facingRight)//如果有向左的速度向量，且不是面向左边，需要Flip
        {
            Flip();
        }
    }
    #endregion

    public void MakeTransparent(bool _transparent)
    {
        if (_transparent)
        {
            sr.color = Color.clear;
        }
        else
        {
            sr.color = Color.white;
        }
    }

    public virtual void Die()
    {

    }
}
