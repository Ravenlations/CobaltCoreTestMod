﻿using Nickel;
using System.Reflection;

namespace Ravenlations.Raven.Artifacts;

internal sealed class DemoArtifactCounting : Artifact, IDemoArtifact
{
    public int counter = 0;
    public static void Register(IModHelper helper)
    {
        helper.Content.Artifacts.RegisterArtifact("Counting", new()
        {
            ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                owner = Deck.colorless,
                pools = [ArtifactPool.Common]
            },
            Sprite = helper.Content.Sprites.RegisterSprite(ModEntry.Instance.Package.PackageRoot.GetRelativeFile("assets/artifacts/counting.png")).Sprite,
            Name = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "Counting", "name"]).Localize,
            Description = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "Counting", "description"]).Localize
        });
    }

    public override void OnTurnStart(State s, Combat c)
    {
        if (!c.isPlayerTurn)
            return;
        this.counter += 1;
        if (this.counter == 3)
        {
            c.QueueImmediate(new AStatus()
            {
                status = Status.backwardsMissiles,
                statusAmount = 2,
                targetPlayer = true,

            });
            this.counter = 0;
            
        }
    }

    public override void OnReceiveArtifact(State state)
    {
        this.counter = 0;
    }

    public override int? GetDisplayNumber(State s)
    {
        if (this.counter != 0)
            return this.counter;
        return null;
    }
}
