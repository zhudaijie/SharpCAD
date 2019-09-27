using System;
using System.Collections.Generic;

using SharpCAD.ApplicationServices;
using SharpCAD.DatabaseServices;
using SharpCAD.UI;

namespace SharpCAD.Commands.Modify
{
    internal abstract class ModifyCmd : Command
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            this.pointer.isShowAnchor = false;
        }

        /// <summary>
        /// 结束
        /// </summary>
        public override void Terminate()
        {
            _mgr.presenter.selections.Clear();

            base.Terminate();
        }
    }
}
