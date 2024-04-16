using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace Ravenlations.Raven.Cards;

internal sealed class Glitch : Card, RavenCard
{
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Glitch", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {

                deck = ModEntry.Instance.RavenDeck.Deck,

                rarity = Rarity.uncommon,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Glitch", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        var data = base.GetData(state);

        data.cost = upgrade == Upgrade.A ? 1 : 2;
        data.exhaust = upgrade == Upgrade.B;
        return data;

    }

    public override List<CardAction> GetActions(State s, Combat c)
        => upgrade switch
        {
            Upgrade.A => [

                new AStatus{
                   status = Status.energyNextTurn,
                   statusAmount = 2,
                   targetPlayer = true,
                },
                new AStatus{
                    status = Status.lockdown,
                    statusAmount = 1,
                    targetPlayer = true,
                },
                new AStatus{
                    status = Status.backwardsMissiles,
                    statusAmount = 2,
                    targetPlayer = true,
                }
            ],
            Upgrade.B => [

                new AStatus{
                   status = Status.energyNextTurn,
                   statusAmount = 4,
                   targetPlayer = true,
                },
                new AStatus{
                    status = Status.backwardsMissiles,
                    statusAmount = 1,
                    targetPlayer = true,
                }

            ],
            _ => [

                new AStatus{
                   status = Status.energyNextTurn,
                   statusAmount = 2,
                   targetPlayer = true,
                },
                new AStatus{
                    status = Status.lockdown,
                    statusAmount = 1,
                    targetPlayer = true,
                },
                new AStatus{
                    status = Status.backwardsMissiles,
                    statusAmount = 1,
                    targetPlayer = true,
                }
            ]
        };
}
