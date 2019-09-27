using System;
using System.Collections.Generic;

using SharpCAD.DatabaseServices;
using SharpCAD.UI;

namespace SharpCAD.UI
{
    internal class TextHitter : EntityHitter
    {
        internal override bool Hit(PickupBox pkbox, Entity entity)
        {
            Text text = entity as Text;
            if (text == null)
                return false;

            TextRS textRS = new TextRS();
            return textRS.Cross(pkbox.reservedBounding, text);
        }
    }
}
