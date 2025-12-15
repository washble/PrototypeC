using UnityEngine;

public class GunslingerEnemy : EnemyBase
{
    [SerializeField] private Transform weaponGrabTransform;
    
    private IMove moveIdle;
    private IMove moveRun;
    private IMove moveDash;
    private IMove moveAttack;
    private IMove moveDamaged;
    private IMove moveDie;

    protected override void Start()
    {
        base.Start();
        
        EnemyMoveSettings();
    }
    
    private void EnemyMoveSettings()
    {
        moveIdle = new GunslingerEnemyMoveIdle(this);
        moveRun = new GunslingerEnemyMoveRun(this);
        moveDash = new GunslingerEnemyMoveDash(this);
        moveAttack = new GunslingerEnemyMoveAttack(this);
        moveDamaged = new GunslingerEnemyMoveDamaged(this);
        moveDie = new GunslingerEnemyMoveDie(this);

        ChangeState(moveIdle);
    }
    
    private void ChangeState(IMove newState)
    {
        if(curMove == newState) { return; }
        
        curMove?.OnExit();
        curMove = newState;
        curMove.OnEnter();
    }

    internal void ChangeRun()
    {
        ChangeState(moveRun);
    }
    
    internal void ChangeAttack()
    {
        ChangeState(moveAttack);
    }

    internal void DamagedEnd(float health)
    {
        ChangeState(health > 0 ? moveIdle : moveDie);
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject otherGameObject = other.gameObject;
        switch (otherGameObject.layer)
        {
            case (int)GameObjectLayer.Weapon:
                if (otherGameObject.CompareTag(GameObjectTag.Player.ToString()))
                {
                    ChangeState(moveDamaged);
                }
                break;
        }
    }
}