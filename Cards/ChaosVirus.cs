using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace Ravenlations.Raven.Cards;

internal sealed class Virus : Card, RavenCard
{

    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Virus", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {

                deck = ModEntry.Instance.RavenDeck.Deck,

                rarity = Rarity.uncommon,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Virus", "name"]).Localize,
            
        });
    }
    public override CardData GetData(State state)
    {
        var data = base.GetData(state);

        data.cost = 1;

        return data;

    }

    public override List<CardAction> GetActions(State s, Combat c)
        => upgrade switch
        {
            Upgrade.A => [

                new AAttack{
                   damage = 1,
                },
                new AStatus{
                    status = Status.backwardsMissiles,
                    statusAmount = 1,
                    targetPlayer = true,
                },
                new AStatus{
                    status = Status.backwardsMissiles,
                    statusAmount = 3,
                    targetPlayer = false,
                }

            ],
            Upgrade.B => [

                new AAttack{
                   damage = 1,
                },
                new AStatus{
                    status = Status.backwardsMissiles,
                    statusAmount = 2,
                    targetPlayer = true,
                },
                new AStatus{
                    status = Status.backwardsMissiles,
                    statusAmount = 2,
                    targetPlayer = false,
                }

            ],
            _ => [
                new AAttack{
                   damage = 1,
                },
                new AStatus{
                    status = Status.backwardsMissiles,
                    statusAmount = 1,
                    targetPlayer = true,
                },
                new AStatus{
                    status = Status.backwardsMissiles,
                    statusAmount = 1,
                    targetPlayer = false,
                }
            ]

        };
}
