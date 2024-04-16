using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace Ravenlations.Raven.Cards;

internal sealed class HackerBoost : Card, RavenCard

{
    private static ISpriteEntry TopArt = null!;
    private static ISpriteEntry BottomArt = null!;
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
       //TopArt = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Cards/HackerBoostTop.png"));
       BottomArt = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Cards/HackerBoostBottom.png"));

        helper.Content.Cards.RegisterCard("HackerBoost", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.RavenDeck.Deck,
                rarity = Rarity.common,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            //Art = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Cards/HackerBoost.png")).Sprite,
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "HackerBoost", "name"]).Localize,
            
        });
    }
    public override CardData GetData(State state) => new()
    {
        art = (flipped ? BottomArt : TopArt).Sprite,
        cost = upgrade == Upgrade.A ? 0 : 1,
        floppable = true,
    };

    public override List<CardAction> GetActions(State s, Combat c)
        => upgrade switch
        {
            Upgrade.A => [

                new AAttack{
                   damage = 1,
                   disabled = flipped
                },

                new ADummyAction(),

                new AStatus{
                    status = Status.backwardsMissiles,
                    statusAmount = 1,
                    targetPlayer = true,
                    disabled = !flipped
                },
                new ADrawCard{
                    count = 1,
                    disabled = !flipped
                }

            ],
            Upgrade.B => [

                new AAttack{
                   damage = 2,
                   disabled = flipped
                },
                new ADummyAction(),

                new AStatus{
                    status = Status.backwardsMissiles,
                    statusAmount = 2,
                    targetPlayer = true,
                    disabled = !flipped
                },
                new ADrawCard{
                    count = 1,
                    disabled = !flipped
                }

            ],
            _ => [

                new AAttack{
                   damage = 1,
                   disabled = flipped
                },

                new ADummyAction(),

                new AStatus{
                    status = Status.backwardsMissiles,
                    statusAmount = 1,
                    targetPlayer = true,
                    disabled = !flipped
                },
                new ADrawCard{
                    count = 1,
                    disabled = !flipped
                }

            ]
        };
}
