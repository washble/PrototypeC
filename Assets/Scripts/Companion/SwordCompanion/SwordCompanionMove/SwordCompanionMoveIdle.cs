using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCompanionMoveIdle : CompanionMove
{
    private SwordCompanion companion;
    
    public SwordCompanionMoveIdle(Companion companion) : base(companion)
    {
        this.companion = companion as SwordCompanion;
    }

    public override void Move()
    {
        companion.CState = CompanionState.Idle;
        companion.ChangeCurMove(companion.moveRun);
    }
}
