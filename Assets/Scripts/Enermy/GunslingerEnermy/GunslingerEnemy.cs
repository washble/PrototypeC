using UnityEngine;

public class GunslingerEnemy : EnemyBase
{
    [SerializeField] private Transform weaponGrabTransform;
    
    internal EnemyMove moveIdle;
    internal EnemyMove moveRun;
    internal EnemyMove moveDash;
    internal EnemyMove moveAttack;
    internal EnemyMove moveDamaged;
    internal EnemyMove moveDie;

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

        ChangeCurMove(moveIdle);
    }

    internal void ChangeCurMove(EnemyMove move)
    {
        CurMove = move;
    }

    internal void DamagedEnd(float health)
    {
        CurMove = health > 0 ? moveIdle : moveDie;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject otherGameObject = other.gameObject;
        switch (otherGameObject.layer)
        {
            case (int)GameObjectLayer.Weapon:
                if (otherGameObject.CompareTag(GameObjectTag.Player.ToString()))
                {
                    CurMove = moveDamaged;
                }
                break;
        }
    }
}
