namespace Ravenlations.Raven;
internal sealed class BackwardsMissilesPermanentManager : IStatusLogicHook
{
    public static ModEntry Instance => ModEntry.Instance;
    public BackwardsMissilesPermanentManager()
    {
        /* We task Kokoro with the job to register our status into the game */
        Instance.KokoroApi.RegisterStatusLogicHook(this, 0);
    }
    public bool HandleStatusTurnAutoStep(State state, Combat combat, StatusTurnTriggerTiming timing, Ship ship, Status status, ref int amount, ref StatusTurnAutoStepSetStrategy setStrategy)
    {
        if (status != Instance.BackwardsMissilesPermanent.Status)
            return false;

        if (timing == StatusTurnTriggerTiming.TurnStart)
        {

            combat.QueueImmediate( new AStatus()
            {
                status = Status.backwardsMissiles,
                statusAmount = amount,
                targetPlayer = ship.isPlayerShip,
                timer = 0,
            });

            return false;

        }
        return false;
    }
}