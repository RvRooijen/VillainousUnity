using System;

public class Maleficent : Villain
{   
    public Maleficent(GameSettings gameSettings, VillainData newVillainData) : base(gameSettings, newVillainData)
    {
        
    }

    public override void StartTurn()
    {
        base.StartTurn();
    }

    public override void EnterCheckWinState(object sender, EventArgs e)
    {
        base.EnterCheckWinState(sender, e);
    }

    public override void EnterMoveState(object sender, EventArgs e)
    {
        base.EnterMoveState(sender, e);
    }

    public override void EnterPerformActionsState(object sender, EventArgs e)
    {
        base.EnterPerformActionsState(sender, e);
    }

    public override void EnterWonState(object sender, EventArgs e)
    {
        base.EnterWonState(sender, e);
    }

    public override void EnterLostState(object sender, EventArgs e)
    {
        base.EnterLostState(sender, e);
    }
}