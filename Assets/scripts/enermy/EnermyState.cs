using UnityEngine;

public class EnermyState 
{
    protected EnermyStateMachine stateMachine;
    protected Enemy _enermyBase;
    protected Rigidbody2D rb;

    protected float xInput;
    protected float yInput;
    private string animBoolName;


    protected float stateTimer; //��ʱ����entryʱ����һ����ʼֵ��update�в�ͣ�ĸ��¡��жϷ����������е�if������С��0��ʾ����ʱ���ˣ���ֹͣĳ������ˡ�
    protected bool triggerCalled = false;
    public EnermyState(Enemy _enermy, EnermyStateMachine _stateMachine, string _animBoolName)
    {
        this._enermyBase = _enermy;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Enter()
    {
        _enermyBase.anim.SetBool(animBoolName, true);
        rb = _enermyBase.rb;
        triggerCalled = false;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
    }

    public virtual void Exit()
    {
        _enermyBase.anim.SetBool(animBoolName, false);
        _enermyBase.AssignLastAnimName(animBoolName);
    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
    
}
