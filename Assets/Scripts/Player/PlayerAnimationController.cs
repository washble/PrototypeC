using UnityEngine;


public class PlayerAnimationController : Singleton<PlayerAnimationController>
{
    private InputManager inputManager;
    private Animator animator;

    private readonly int Running = Animator.StringToHash("Running");
    private readonly int Attack = Animator.StringToHash("Attack");

    protected override void Awake()
    {
        base.Awake();
        
        animator = GetComponentInChildren<Animator>();
        if (!animator)
        {
            enabled = false;
            return;
        }
        inputManager = InputManager.Instance;
    }
    
    public void RunStart()
    {
        if (!IsRunning())
        {
            RunningAnimationOnOff(true);
        }
    }

    public void RunEnd()
    {
        if (IsRunning())
        {
            RunningAnimationOnOff(false);    
        }
    }

    private bool IsRunning()
    {
        return animator.GetBool(Running);
    }
    
    private void RunningAnimationOnOff(bool value)
    {
        animator.SetBool(Running, value);
    }

    public void AttackStart()
    {
        if (!IsRunning())
        {
            AttackAnimationOnOff(true);   
        }   
    }

    public void AttackEnd()
    {
        if (IsAttack())
        {
            AttackAnimationOnOff(false);
        }
    }

    private bool IsAttack()
    {
        return animator.GetBool(Attack);
    }
    
    private void AttackAnimationOnOff(bool value)
    {
        animator.SetBool(Attack, value);
    }
}
