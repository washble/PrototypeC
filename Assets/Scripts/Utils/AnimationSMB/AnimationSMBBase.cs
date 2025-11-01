using System;
using UnityEngine;
using UnityEngine.Animations;

public class AnimationSMBBase : StateMachineBehaviour
{
    private event Action OnStateMachineEnterAction;
    private event Action OnStateEnterAction;
    private event Action OnStateUpdateAction;
    private event Action OnStateExitAction;
    private event Action OnStateMachineExitAction;

    public void OnStateMachineEnterSubscribe(Action action)
    {
        OnStateMachineEnterAction += action;
    }
    
    public void OnStateMachineEnterCancel(Action action)
    {
        OnStateMachineEnterAction -= action;
    }
    
    public void OnStateEnterSubscribe(Action action)
    {
        OnStateEnterAction += action;
    }
    
    public void OnStateEnterCancel(Action action)
    {
        OnStateEnterAction -= action;
    }
    
    public void OnStateUpdateSubscribe(Action action)
    {
        OnStateUpdateAction += action;
    }
    
    public void OnStateUpdateCancel(Action action)
    {
        OnStateUpdateAction -= action;
    }
    
    public void OnStateExitSubscribe(Action action)
    {
        OnStateExitAction += action;
    }
    
    public void OnStateExitCancel(Action action)
    {
        OnStateExitAction -= action;
    }
    
    public void OnStateMachineExitSubscribe(Action action)
    {
        OnStateMachineExitAction += action;
    }
    
    public void OnStateMachineExitCancel(Action action)
    {
        OnStateMachineExitAction -= action;
    }
    
    public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash, AnimatorControllerPlayable controller)
    {
        OnStateMachineEnterAction?.Invoke();
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex,
        AnimatorControllerPlayable controller)
    {
        OnStateEnterAction?.Invoke();
    }
    
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex,
        AnimatorControllerPlayable controller)
    {
        OnStateUpdateAction?.Invoke();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
    {
        OnStateExitAction?.Invoke();
    }

    public override void OnStateMachineExit(Animator animator, int stateMachinePathHash, AnimatorControllerPlayable controller)
    {
        OnStateMachineExitAction?.Invoke();

        AllActionCancel();
    }

    private void AllActionCancel()
    {
        OnStateMachineEnterAction = null;
        OnStateEnterAction = null;
        OnStateUpdateAction = null;
        OnStateExitAction = null;
        OnStateMachineExitAction = null;
    }
}
