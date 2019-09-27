using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpCAD.DatabaseServices;
using System.Windows.Forms;
using System.Drawing;

namespace SharpCAD.Commands.Draw
{
    internal class EllipseCmd : DrawCmd
    {
        private Ellipse _ellipse = null;

        /// <summary>
        /// 新增的图元
        /// </summary>
        protected override IEnumerable<Entity> newEntities
        {
            get { return new Ellipse[1] { _ellipse }; }
        }

        /// <summary>
        /// 步骤
        /// </summary>
        private enum Step
        {
            Step1_SpecifyCenter = 1,
            Step2_SpecityRadiusX = 2,
            Step3_SpecityRadiusY = 3,
        }
        private Step _step = Step.Step1_SpecifyCenter;

        /// <summary>
        /// 初始化
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            //
            _step = Step.Step1_SpecifyCenter;
            this.pointer.mode = UI.Pointer.Mode.Locate;
        }

        public override EventResult OnMouseDown(MouseEventArgs e)
        {
            if (_step == Step.Step1_SpecifyCenter)
            {
                if (e.Button == MouseButtons.Left)
                {
                    _ellipse = new Ellipse();
                    _ellipse.center = this.pointer.currentSnapPoint;
                    _ellipse.radiusX = 0;
                    _ellipse.radiusY = 0;
                    _ellipse.layerId = this.document.currentLayerId;
                    _ellipse.color = this.document.currentColor;

                    _step = Step.Step2_SpecityRadiusX;
                }
            }
            else if (_step == Step.Step2_SpecityRadiusX)
            {
                if (e.Button == MouseButtons.Left)
                {
                    _ellipse.radiusX = Math.Abs(_ellipse.center.x - this.pointer.currentSnapPoint.x);
                    _ellipse.radiusY = _ellipse.radiusX / 2.0;
                    _ellipse.layerId = this.document.currentLayerId;
                    _ellipse.color = this.document.currentColor;

                    _step = Step.Step3_SpecityRadiusY;
                }
            }
            else if (_step == Step.Step3_SpecityRadiusY)
            {
                if (e.Button == MouseButtons.Left)
                {
                    _ellipse.radiusY = Math.Abs(_ellipse.center.y - this.pointer.currentSnapPoint.y);
                    _ellipse.layerId = this.document.currentLayerId;
                    _ellipse.color = this.document.currentColor;

                    _mgr.FinishCurrentCommand();
                }
            }

            return EventResult.Handled;
        }

        public override EventResult OnMouseUp(MouseEventArgs e)
        {
            return EventResult.Handled;
        }

        public override EventResult OnMouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                return EventResult.Handled;
            }

            if (_ellipse != null)
            {
                if (_step == Step.Step2_SpecityRadiusX)
                {
                    _ellipse.radiusX = Math.Abs(_ellipse.center.x - this.pointer.currentSnapPoint.x);
                    _ellipse.radiusY = _ellipse.radiusX / 2.0;
                }
                else if (_step == Step.Step3_SpecityRadiusY)
                {
                    _ellipse.radiusY = Math.Abs(_ellipse.center.y - this.pointer.currentSnapPoint.y);
                }
            }

            return EventResult.Handled;
        }

        public override void OnPaint(Graphics g)
        {
            if (_ellipse != null)
            {
                Presenter presenter = _mgr.presenter as Presenter;
                presenter.DrawEntity(g, _ellipse);
            }
        }
    }
}
