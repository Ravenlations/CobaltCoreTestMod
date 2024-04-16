using Ravenlations.Raven.Cards;
using Nickel;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Ravenlations.Raven.Artifacts.Ship;

internal sealed class DemoArtifactBookOfTails : Artifact, IDemoArtifact
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Artifacts.RegisterArtifact("BookOfTails", new()
        {
            ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                owner = Deck.colorless,
                pools = [ArtifactPool.Common]
            },
            Sprite = helper.Content.Sprites.RegisterSprite(ModEntry.Instance.Package.PackageRoot.GetRelativeFile("assets/artifacts/bookoftails.png")).Sprite,
            Name = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "BookOfTails", "name"]).Localize,
            Description = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "BookOfTails", "description"]).Localize
        });
    }

    public override List<Tooltip>? GetExtraTooltips()
        => new List<Tooltip>
        {
            new TTCard
            {
                card = new PopUp
                {
                    temporaryOverride = true
                }
            }
        }
        .ToList();

    public override void OnTurnStart(State s, Combat c)
    {
        if (!c.isPlayerTurn)
            return;
        c.QueueImmediate([

        new AEnergy()
            {
                changeAmount = 1
            },
        new AAddCard
            {
                card = new PopUp
                {
                    temporaryOverride = true
                },
                destination = CardDestination.Hand
            }
        ]);
    }
}
