using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

[DisallowMultipleComponent]
public abstract class WeaponBase : MonoBehaviour
{
    [Header("[Attack]")]
    [SerializeField] protected WeaponType weaponType;
    [SerializeField] protected WeaponAttackTypeSO[] attackTypes;
    
    [Header("[Grab]")]
    [SerializeField] private Transform weaponGrabTransform;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 rotation;
    
    protected WeaponAttackTypeSO CurWeaponAttackType;

    // ================ [Attack State] ================ // 
    protected AttackState CurAttackState;

    private CancellationTokenSource cts = new CancellationTokenSource();

    protected virtual void Start()
    {
        if(!weaponGrabTransform) { return; }

        SetWeaponGrabPosition(weaponGrabTransform, offset, Quaternion.Euler(rotation));
    }

    protected virtual void OnDestroy()
    {
        if (!cts.IsCancellationRequested)
        {
            cts.Cancel();
            cts.Dispose();
        }
    }

    public void SetWeaponGrabPosition(Transform targetTransforms, Vector3 offset, Quaternion rotation)
    {
        Transform thisTransform = transform;
        thisTransform.SetParent(targetTransforms, true);
        thisTransform.SetLocalPositionAndRotation
        (
            thisTransform.localPosition + offset,
            rotation
        );
    }

    public virtual void AttackStart(int startDelayMilliSecond)
    {
        if (CurAttackState == AttackState.Attack) return;
        CurAttackState = AttackState.Attack;
        AttackChecking(startDelayMilliSecond).Forget();
    }
    
    private async UniTaskVoid AttackChecking(int startDelayMilliSecond)
    {
        if (cts.IsCancellationRequested) { return; }
        CancellationToken token = cts!.Token;
        
        await UniTask.Delay(startDelayMilliSecond, cancellationToken: token);
        
        WeaponAttackTypeSO.IAttackDetail[] attackDetails = CurWeaponAttackType.attackDetails;
        while (CurAttackState == AttackState.Attack)
        {
            for (int i = 0; i < attackDetails.Length; i++)
            {
                switch (attackDetails[i].AttackDetailType)
                {
                    case AttackDetailType.Delay:
                        WeaponAttackTypeSO.DelayTime delayTime
                            = (WeaponAttackTypeSO.DelayTime)attackDetails[i];
                        await UniTask.Delay(delayTime.delayMilliSecond, cancellationToken: token);
                        break;
                    case AttackDetailType.AttackCount:
                        WeaponAttackTypeSO.AttackCount attackCount
                            = (WeaponAttackTypeSO.AttackCount)attackDetails[i];
                        Attack(attackCount.count);
                        break;
                }
            }
            CurAttackState = AttackState.Idle;
        }
    }

    protected abstract void Attack(int attackCount);

    public virtual void AttackStop()
    {
        CurAttackState = AttackState.Idle;
    }

    public void ChangeCurWeaponAttackType(int num)
    {
        if (num < attackTypes.Length)
        {
            CurWeaponAttackType = attackTypes[num];
        }
    }
}

public enum AttackState
{
    Idle,
    Attack,
}

public enum AttackDetailType
{
    Delay,
    AttackCount,
}
