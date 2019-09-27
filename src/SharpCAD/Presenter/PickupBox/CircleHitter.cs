using System;
using System.Collections.Generic;

using SharpCAD.DatabaseServices;
using SharpCAD.UI;

namespace SharpCAD.UI
{
    internal class CircleHitter : EntityHitter
    {
        internal override bool Hit(PickupBox pkbox, Entity entity)
        {
            Circle circle = entity as Circle;
            if (circle == null)
                return false;

            return MathUtils.BoundingCross(pkbox.reservedBounding, circle);
        }
    }
}
