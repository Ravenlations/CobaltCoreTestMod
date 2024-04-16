using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace Ravenlations.Raven.Cards;

internal sealed class GodMode : Card, RavenCard
{ 

    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("GodMode", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {

                deck = ModEntry.Instance.RavenDeck.Deck,

                rarity = Rarity.rare,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "GodMode", "name"]).Localize,
            
        });
    }
    public override CardData GetData(State state)
    {
        var data = base.GetData(state);

        data.cost = upgrade == Upgrade.B ? 3 : 2;
        data.exhaust = upgrade != Upgrade.B;


        return data;

    }

    public override List<CardAction> GetActions(State s, Combat c)
        => upgrade switch
        {
            Upgrade.A => [
                new AStatus {

                    status = Status.perfectShield,
                    statusAmount = 1,
                    targetPlayer = true
                },

                new AHeal{ healAmount = 2, targetPlayer = true}
            ],
            Upgrade.B => [

                new AStatus {

                    status = Status.perfectShield,
                    statusAmount = 1,
                    targetPlayer = true
                },

                new AHeal{ healAmount = 1, targetPlayer = true}
            ],
            _ => [

                new AStatus {

                    status = Status.perfectShield,
                    statusAmount = 1,
                    targetPlayer = true
                },

                new AHeal{ healAmount = 1, targetPlayer = true}


            ]
        };
}
