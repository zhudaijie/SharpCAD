using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpCAD.DatabaseServices;

namespace SharpCAD.UI
{
    internal class XPointHitter : EntityHitter
    {
        internal override bool Hit(PickupBox pkbox, Entity entity)
        {
            XPoint xPoint = entity as XPoint;
            if (xPoint == null)
                return false;

            XPointRS xPointRS = new XPointRS();
            return xPointRS.Cross(pkbox.reservedBounding, xPoint);
        }
    }
}
