using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCompanionMoveIdle : CompanionMove
{
    private SwordCompanion companion;
    
    public SwordCompanionMoveIdle(CompanionBase companionBase) : base(companionBase)
    {
        this.companion = companionBase as SwordCompanion;
    }

    public override void Move()
    {
        companion.CState = CompanionState.Idle;
        companion.ChangeCurMove(companion.moveRun);
    }
}
