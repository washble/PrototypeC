using System.Collections;
using System.Collections.Generic;

public class CompanionManager : Singleton<CompanionManager>
{
    public Dictionary<GameObjectTag, float> companionDamage = new();
}
