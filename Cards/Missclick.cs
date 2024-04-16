using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace Ravenlations.Raven.Cards;

internal sealed class Missclick : Card, RavenCard
{
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Missclick", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {

                deck = ModEntry.Instance.RavenDeck.Deck,

                rarity = Rarity.common,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Missclick", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        var data = base.GetData(state);
        
        data.cost = upgrade == Upgrade.B ? 1 : 0;

        return data;
    }

    public override List<CardAction> GetActions(State s, Combat c)
      => upgrade switch
      {
          Upgrade.A => [
              new ASpawn
                {
                   thing = new Missile{
                       missileType = MissileType.heavy,
                       targetPlayer = true
                   }
                },
          ],
          Upgrade.B => [
              new AStatus {
                 status = Status.backwardsMissiles,
                 statusAmount = 2,    
                 targetPlayer = true,
              },
              new ASpawn
                {
                    thing = new Missile{
                        missileType = MissileType.normal,
                        targetPlayer = true
                   }
                },
          ],
          _ => [
              new ASpawn{
                   thing = new Missile{
                       missileType = MissileType.normal,
                       targetPlayer = true
                    }
              }]
               
      };
}
