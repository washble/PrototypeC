using UnityEngine.AI;

public class PlayerBase : Singleton<PlayerBase>
{
    protected InputManager inputManager;
    protected UIManager uiManager;
    
    internal NavMeshAgent navMeshAgent;
    
    internal PlayerWeaponController playerWeaponController;
    
    protected override void Awake()
    {
        base.Awake();
        
        inputManager = InputManager.Instance;
        uiManager = UIManager.Instance;
        
        navMeshAgent = GetComponent<NavMeshAgent>();
        
        playerWeaponController = GetComponent<PlayerWeaponController>();
    }
}
