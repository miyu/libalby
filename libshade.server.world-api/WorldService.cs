using Shade.Server.Nierians;

namespace Shade.Server.World
{
    public interface WorldService
    {
       WorldLoginResult Enter(NierianKey nierianKey);
       void Leave(NierianKey key);
    }
}
