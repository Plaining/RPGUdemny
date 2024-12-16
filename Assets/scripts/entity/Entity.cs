using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Entity : MonoBehaviour
{

    [Header("Move info")]
    public float moveSpeed = 3f;
    public float jumpForce = 10f;

    public int facingDir { get; private set; } = 1;
    public bool facingRight = true;

    [Header("Collision info")]
    [SerializeField] protected Transform groudCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask WhatIsGround;
    

    #region Components
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    #endregion

    protected virtual void Awake()
    {
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    #region Velocity
    public virtual void zeroVelocity()
    {
        rb.linearVelocity = new Vector2(0, 0);
    }

    public virtual void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.linearVelocity = new Vector2(_xVelocity * moveSpeed, _yVelocity);
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
    }
    #endregion
    #region Flip
    public virtual void Flip()
    {
        facingDir *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
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

}
