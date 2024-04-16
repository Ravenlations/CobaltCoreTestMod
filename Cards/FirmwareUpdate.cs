using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace Ravenlations.Raven.Cards;

internal sealed class FirmwareUpdate : Card, RavenCard
{

    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("FirmwareUpdate", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {

                deck = ModEntry.Instance.RavenDeck.Deck,

                rarity = Rarity.rare,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "FirmwareUpdate", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        var data = base.GetData(state);

        data.cost = upgrade == Upgrade.A ? 2 : 3 ;
        data.exhaust = true;

        return data;

    }

    public override List<CardAction> GetActions(State s, Combat c)
        => upgrade switch
        {
            Upgrade.A => [

                new AStatus{
                    status = ModEntry.Instance.BackwardsMissilesPermanent.Status,
                    statusAmount = 1,
                    targetPlayer = true,
                }

            ],
            Upgrade.B => [

                new AStatus{
                    status = ModEntry.Instance.BackwardsMissilesPermanent.Status,
                    statusAmount = 2,
                    targetPlayer = true,
                }

            ],
            _ => [
                new AStatus{
                    status = ModEntry.Instance.BackwardsMissilesPermanent.Status,
                    statusAmount = 1,
                    targetPlayer = true,
                }
            ]

        };
}
