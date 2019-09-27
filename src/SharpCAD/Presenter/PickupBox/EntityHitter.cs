using System;

using SharpCAD.DatabaseServices;

namespace SharpCAD.UI
{
    internal abstract class EntityHitter
    {
        internal abstract bool Hit(PickupBox pkbox, Entity entity);
    }
}
