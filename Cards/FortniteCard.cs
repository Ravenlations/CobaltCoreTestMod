using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.Reflection;


namespace Ravenlations.Raven.Cards;

internal sealed class FortniteCard : Card, RavenCard
{
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("FortniteCard", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {

                deck = ModEntry.Instance.RavenDeck.Deck,

                rarity = Rarity.uncommon,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "FortniteCard", "name"]).Localize,
            Art = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Cards/FortniteCard.png")).Sprite,
        });
    }
    public override CardData GetData(State state)
    {
        var data = base.GetData(state);

        data.cost = 1;
        data.exhaust = true;

        data.description = ModEntry.Instance.Localizations.Localize(["card", "FortniteCard", "description", upgrade.ToString()]);
        
        return data;
        
    }

    public override List<CardAction> GetActions(State s, Combat c)
        => upgrade switch
        {
            Upgrade.A => [

                new AAddCard
                {
                    destination = CardDestination.Deck,
                    card = new FortniteShield()
                },
                new AAddCard
                {
                    destination = CardDestination.Deck,
                    card = new FortniteShield(),
                    omitFromTooltips = true
                }
            ],
            Upgrade.B => [

                new AAddCard
                {
                    destination = CardDestination.Hand,
                    card = new FortniteShield(),
                    
                },
                new AAddCard
                {
                    destination = CardDestination.Hand,
                    card = new FortniteDance(),
                    
                }

            ],
            _ => [

                new AAddCard
                {
                    destination = CardDestination.Hand,
                    card = new FortniteShield()
                
                }
            ]
        };
}
