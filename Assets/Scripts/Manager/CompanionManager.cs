using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class CompanionManager : Singleton<CompanionManager>
{
    public Dictionary<GameObjectTag, float> companionDamage = new();

    [Header("[Find Close Enemy]")] 
    [SerializeField] private int enemyCheckInterval = 200;
    [SerializeField] private float enemyCheckRadius = 20;
    [SerializeField] private LayerMask enemyCheckLayerMask;
    private RaycastHit[] raycastEnemyHits;
    private int enemyHitCount;
    public (int, RaycastHit[]) RaycastEnemyHits => (enemyHitCount, raycastEnemyHits);

    [Header("[Debug]")]
    [SerializeField] private bool isDebug = false;
    [SerializeField] private Color enemyCheckColor = new Color(1, 0, 0, 0.5f);
    
    private CancellationToken cancellationToken;

    private void Start()
    {
        cancellationToken = this.GetCancellationTokenOnDestroy();
        FindCloseEnemyAroundPlayer().Forget();
    }

    private async UniTaskVoid FindCloseEnemyAroundPlayer()
    {
        Transform originTransform = GameManager.Instance.Player.transform;
        raycastEnemyHits = new RaycastHit[5];
        
        while (cancellationToken.CanBeCanceled)
        {
            enemyHitCount = FindCloseTarget
            (
                ref raycastEnemyHits, originTransform.position, enemyCheckRadius,
                originTransform.forward, enemyCheckLayerMask
            );
            await UniTask.Delay(enemyCheckInterval, cancellationToken: cancellationToken);
        }
    }

    private int FindCloseTarget
        (ref RaycastHit[] raycastHit, Vector3 originPosition, float checkRadius, Vector3 direction, LayerMask layerMask)
    {
        int hitCount = Physics.SphereCastNonAlloc(
            originPosition, checkRadius, direction, 
            raycastHit, 0, layerMask, QueryTriggerInteraction.Collide);
        
        return hitCount;
    }
        
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if(!isDebug) { return; }
        if (GameManager.Instance)
        {
            Gizmos.color = enemyCheckColor;
            Vector3 originPosition = GameManager.Instance.Player.transform.position;
            Gizmos.DrawSphere(originPosition, enemyCheckRadius);
        }
    }
#endif
}
