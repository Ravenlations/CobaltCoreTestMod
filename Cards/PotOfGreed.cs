using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace Ravenlations.Raven.Cards;

internal sealed class PotOfGreed : Card, RavenCard
{
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("ALotOfMissiles", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.RavenDeck.Deck,

                rarity = Rarity.rare,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "PotOfGreed", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        var data = base.GetData(state);
        
        data.cost = 0;
        data.exhaust = upgrade == Upgrade.B;

        data.description = ModEntry.Instance.Localizations.Localize(["card", "PotOfGreed", "description", upgrade.ToString()]);
        return data;
    }

    public override List<CardAction> GetActions(State s, Combat c)
      => upgrade switch
      {
          Upgrade.A => [

              new ADrawCard{
                   count = 4,
                }

          ],
          Upgrade.B => [
              
              new ADrawCard{
                  count = 6,
              }
            
          ],
          _ => [

            new ADrawCard{
                  count = 2,
              }

          ]
      };
}
