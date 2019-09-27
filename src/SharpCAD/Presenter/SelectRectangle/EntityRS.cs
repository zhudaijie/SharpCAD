using System;

using SharpCAD.ApplicationServices;
using SharpCAD.DatabaseServices;

namespace SharpCAD
{
    internal abstract class EntityRS
    {
        internal abstract bool Cross(Bounding bounding, Entity entity);
        internal virtual bool Window(Bounding bounding, Entity entity)
        {
            return bounding.Contains(entity.bounding);
        }
    }
}
