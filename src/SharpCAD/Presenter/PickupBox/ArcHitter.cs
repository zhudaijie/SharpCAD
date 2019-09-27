using System;
using System.Collections.Generic;

using SharpCAD.DatabaseServices;
using SharpCAD.UI;

namespace SharpCAD.UI
{
    internal class ArcHitter : EntityHitter
    {
        internal override bool Hit(PickupBox pkbox, Entity entity)
        {
            Arc arc = entity as Arc;
            if (arc == null)
                return false;

            ArcRS arcRS = new ArcRS();
            return arcRS.Cross(pkbox.reservedBounding, arc);
        }
    }
}
