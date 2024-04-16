using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace Ravenlations.Raven.Cards;

internal sealed class FortniteDance : Card, RavenCard
{
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("FortniteDance", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.RavenDeck.Deck,
                rarity = Rarity.rare,
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "FortniteDance", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        var data = base.GetData(state);
        data.cost = 0;
        data.temporary = true;
        return data;
    }

    public override List<CardAction> GetActions(State s, Combat c)
        => upgrade switch
        {
            _ => [

                new AStatus{
                    status = Status.evade,
                    statusAmount = 1,
                    targetPlayer = true
                },

                new AMove{

                    dir = 2,
                    isRandom = true,
                    targetPlayer = true,
                  
                }

                   
            ]
        };
}
