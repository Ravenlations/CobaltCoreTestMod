using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace Ravenlations.Raven.Cards;
internal sealed class RandomBullshit : Card, RavenCard
{
    
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("RandomBullshit", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {

                deck = ModEntry.Instance.RavenDeck.Deck,

                rarity = Rarity.uncommon,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "RandomBullshit", "name"]).Localize
            ,
        });
    }
    public override CardData GetData(State state)
    {
        var data = base.GetData(state);

        data.cost = upgrade == Upgrade.None ? 1 : 2;
        
        return data;
    }
    public override List<CardAction> GetActions(State s, Combat c)
        => upgrade switch
        {
            Upgrade.A => [

                new ASpawn{thing = new Missile{missileType = MissileType.heavy, targetPlayer=true},offset = 0},
                new ASpawn{thing = new Missile{missileType = MissileType.seeker, targetPlayer=true},offset = 1},
                new ASpawn{thing = new Missile{missileType = MissileType.breacher, targetPlayer=true},offset = -1}

            ],
            Upgrade.B => [

                new ASpawn{thing = new Geode{} ,offset = 0},
                new ASpawn{thing = new Asteroid{},offset = 1},
                new ASpawn{thing = new Asteroid{},offset = -1}

            ],
            _ => [

                new ASpawn{thing = new Missile{missileType = MissileType.normal, targetPlayer=true},offset = 0},
                new ASpawn{thing = new Missile{missileType = MissileType.normal, targetPlayer=true},offset = 1},
                new ASpawn{thing = new Missile{missileType = MissileType.normal, targetPlayer=true},offset = -1}
            ]
        };
}
