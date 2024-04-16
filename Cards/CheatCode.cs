using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace Ravenlations.Raven.Cards;


internal sealed class CheatCode : Card, RavenCard
{
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("CheatCode", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {

                deck = ModEntry.Instance.RavenDeck.Deck,

                rarity = Rarity.uncommon,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
         
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "CheatCode", "name"]).Localize,
            
        });
    }
    public override CardData GetData(State state)
    {
        var data = base.GetData(state);

        data.cost = upgrade == Upgrade.B ? 1: 2;
        data.floppable = true;
        return data;
    }

    public override List<CardAction> GetActions(State s, Combat c)
        => upgrade switch
        {
            Upgrade.A => [

                new AStatus{
                    status = Status.backwardsMissiles,
                    statusAmount = 3,
                    targetPlayer = true
                },
                new AVariableHint
                {
                    status = Status.backwardsMissiles,
                },

                new AAttack{

                   damage = GetDmg(s, 3 + s.ship.Get(Status.backwardsMissiles)),
                   disabled = flipped

                },
                new AStatus{
                    status = Status.evade,
                    statusAmount = 3 + s.ship.Get(Status.backwardsMissiles),
                    targetPlayer = true,
                    disabled = !flipped
                }

            ],
            Upgrade.B => [

                new AStatus{
                    status = Status.backwardsMissiles,
                    statusAmount = 2,
                    targetPlayer = true
                },
                new AVariableHint
                {
                    status = Status.backwardsMissiles,

                },
                new AAttack{

                   damage = GetDmg(s, 2 + s.ship.Get(Status.backwardsMissiles)),
                   disabled = flipped

                },
                new AStatus{
                    status = Status.evade,
                    statusAmount = 2 + s.ship.Get(Status.backwardsMissiles),
                    targetPlayer = true,
                    disabled = !flipped
                }
            ],
            _ => [

                new AStatus{
                    status = Status.backwardsMissiles,
                    statusAmount = 2,
                    targetPlayer = true
                },
                new AVariableHint
                {
                    status = Status.backwardsMissiles,
                    
                },
                new AAttack{

                   damage = GetDmg(s, 2 + s.ship.Get(Status.backwardsMissiles)),
                   disabled = flipped
                },
                new AStatus{
                    status = Status.evade,
                    statusAmount = 2 + s.ship.Get(Status.backwardsMissiles),
                    targetPlayer = true,
                    disabled = !flipped
                }
            ]
        };
}
