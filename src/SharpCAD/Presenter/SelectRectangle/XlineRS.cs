using System;

using SharpCAD.ApplicationServices;
using SharpCAD.DatabaseServices;

namespace SharpCAD
{
    internal class XlineRS : EntityRS
    {
        internal override bool Cross(Bounding selectBound, Entity entity)
        {
            Xline xline = entity as Xline;
            if (xline == null)
            {
                return false;
            }

            return SharpCAD.UI.XlineHitter.BoundingIntersectWithXline(selectBound, xline);
        }
    }
}
