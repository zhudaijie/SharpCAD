using System;

using SharpCAD.DatabaseServices;

namespace SharpCAD.Commands.Modify.Offset
{
    internal abstract class _OffsetOperation
    {
        public abstract Entity result { get; }

        public abstract bool Do(double value, LitMath.Vector2 refPoint);
    }
}
