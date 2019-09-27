using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpCAD.DatabaseServices;
using System.Windows.Forms;
using System.Drawing;

namespace SharpCAD.Commands.Draw
{
    /// <summary>
    /// 绘制点命令
    /// </summary>
    internal class PointCmd : DrawCmd
    {
        private XPoint _point = null;

        /// <summary>
        /// 新增的图元
        /// </summary>
        protected override IEnumerable<Entity> newEntities
        {
            get { return new XPoint[1] { _point }; }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            this.pointer.mode = UI.Pointer.Mode.Locate;
        }

        public override EventResult OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _point = new XPoint(this.pointer.currentSnapPoint);
                _point.color = this.document.currentColor;
                _mgr.FinishCurrentCommand();
            }
            
            return EventResult.Handled;
        }

        public override EventResult OnMouseUp(MouseEventArgs e)
        {
            return EventResult.Handled;
        }

        public override EventResult OnMouseMove(MouseEventArgs e)
        {
            return EventResult.Handled;
        }

        public override void OnPaint(Graphics g)
        {
            if (_point != null)
            {
                this.presenter.DrawEntity(g, _point);
            }
        }
    }
}
