using Nanoray.PluginManager;
using Nickel;

namespace Ravenlations.Raven;

/* Much like a namespace, these interfaces can be named whatever you'd like.
 * We recommend using descriptive names for what they're supposed to do.
 * In this case, we use the IDemoCard interface to call for a Card's 'Register' method */
internal interface RavenCard
{
    static abstract void Register(IPluginPackage<IModManifest> package, IModHelper helper);
}

internal interface IDemoArtifact
{
    static abstract void Register(IModHelper helper);
}
