using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace Ravenlations.Raven.Cards;

internal sealed class FortniteShield : Card, RavenCard
{
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("FortniteShield", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {

                deck = ModEntry.Instance.RavenDeck.Deck,

                rarity = Rarity.rare,
                
            },
            
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "FortniteShield", "name"]).Localize
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
                    status = Status.tempShield,
                    statusAmount = 2,
                    targetPlayer = true
                }
            ]
        };
}
