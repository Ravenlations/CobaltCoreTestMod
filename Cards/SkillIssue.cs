using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace Ravenlations.Raven.Cards;

internal sealed class SkillIssue : Card, RavenCard
{

    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("SkillIssue", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {

                deck = ModEntry.Instance.RavenDeck.Deck,

                rarity = Rarity.rare,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "SkillIssue", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        var data = base.GetData(state);

        data.cost = 0;
        data.unplayable = upgrade == Upgrade.None;
        data.infinite = upgrade != Upgrade.None;

        return data;

    }

    public override List<CardAction> GetActions(State s, Combat c)
        => upgrade switch
        {
            Upgrade.A => [
                new AAttack{
                   damage = 0,
                   piercing = true,
                }
            ],
            Upgrade.B => [
                new AAttack{
                   damage = 0,
                   
                },
                new AAttack{
                   damage = 0,
                   
                }
            ],
            _ => [
                new AAttack{
                   damage = 99,
                   piercing = true,
                   brittle = true,
                   stunEnemy = true,
                },

            ]
        };
}
