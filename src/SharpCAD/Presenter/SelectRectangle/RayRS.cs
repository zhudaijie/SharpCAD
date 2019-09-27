using System;

using SharpCAD.ApplicationServices;
using SharpCAD.DatabaseServices;

namespace SharpCAD
{
    internal class RayRS : EntityRS
    {
        internal override bool Cross(Bounding selectBound, Entity entity)
        {
            Ray ray = entity as Ray;
            if (ray == null)
            {
                return false;
            }

            return SharpCAD.UI.RayHitter.BoundingIntersectWithRay(selectBound, ray);
        }
    }
}
