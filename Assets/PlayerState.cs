using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected Player player;
    protected Rigidbody2D rb;

    protected float xInput;
    protected float yInput;
    private string animBoolName;

    protected float stateTimer; //��ʱ����entryʱ����һ����ʼֵ��update�в�ͣ�ĸ��¡��жϷ����������е�if������С��0��ʾ����ʱ���ˣ���ֹͣĳ������ˡ�
    protected bool triggerCalled = false;

    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        player.anim.SetFloat("yVelocity", rb.linearVelocityY);

    }
    public virtual void Enter()
    {
        player.anim.SetBool(animBoolName, true);
        rb = player.rb;
        triggerCalled = false;
    }
    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false);
    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
}
