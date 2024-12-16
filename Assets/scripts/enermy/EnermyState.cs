using UnityEngine;

public class EnermyState 
{
    protected EnermyStateMachine stateMachine;
    protected Enermy enermy;
    protected Rigidbody2D rb;

    protected float xInput;
    protected float yInput;
    private string animBoolName;

    protected float stateTimer; //��ʱ����entryʱ����һ����ʼֵ��update�в�ͣ�ĸ��¡��жϷ����������е�if������С��0��ʾ����ʱ���ˣ���ֹͣĳ������ˡ�
    protected bool triggerCalled = false;
    public EnermyState(Enermy _enermy, EnermyStateMachine _stateMachine, string _animBoolName)
    {
        this.enermy = _enermy;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Enter()
    {
        enermy.anim.SetBool(animBoolName, true);
        rb = enermy.rb;
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
        enermy.anim.SetBool(animBoolName, false);
    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
}