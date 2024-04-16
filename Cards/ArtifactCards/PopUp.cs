using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace Ravenlations.Raven.Cards;

internal sealed class PopUp : Card, RavenCard
{
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("PopUp", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                
                deck = Deck.colorless,

                rarity = Rarity.rare,
                
            },
            
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "PopUp", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        var data = base.GetData(state);

        data.cost = 0;
        data.temporary = true;
        data.exhaust = true;
        return data;

    }

    public override List<CardAction> GetActions(State s, Combat c)
        => upgrade switch
        {
            _ => [

                new ADiscard{
                    count = 1,
                    ignoreRetain = true,
                }
            ]
        };
}
