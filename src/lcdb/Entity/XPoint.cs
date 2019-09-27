using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCAD.DatabaseServices
{
    /// <summary>
    /// 点
    /// </summary>
    public class XPoint : Entity
    {
        /// <summary>
        /// 类名
        /// </summary>
        public override string className
        {
            get { return "XPoint"; }
        }

        /// <summary>
        /// 终点
        /// </summary>
        private LitMath.Vector2 _endPoint = new LitMath.Vector2();
        public LitMath.Vector2 endPoint
        {
            get { return _endPoint; }
            set { _endPoint = value; }
        }

        /// <summary>
        /// 外围边框
        /// </summary>
        public override Bounding bounding
        {
            get
            {
                return new Bounding(_endPoint, 0, 0);
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public XPoint()
        {
        }

        public XPoint(LitMath.Vector2 endPnt)
        {
            _endPoint = endPnt;
        }

        /// <summary>
        /// 绘制函数
        /// </summary>
        public override void Draw(IGraphicsDraw gd)
        {
            gd.DrawPoint(_endPoint);
        }

        /// <summary>
        /// 克隆函数
        /// </summary>
        public override object Clone()
        {
            XPoint xPoint = base.Clone() as XPoint;
            xPoint._endPoint = _endPoint;
            return xPoint;
        }

        protected override DBObject CreateInstance()
        {
            return new XPoint();
        }

        public override void Translate(LitMath.Vector2 translation)
        {
            _endPoint += translation;
        }

        public override void TransformBy(LitMath.Matrix3 transform)
        {
            _endPoint = transform * _endPoint;
        }

        /// <summary>
        /// 对象捕捉点
        /// </summary>
        public override List<ObjectSnapPoint> GetSnapPoints()
        {
            List<ObjectSnapPoint> snapPnts = new List<ObjectSnapPoint>();
            snapPnts.Add(new ObjectSnapPoint(ObjectSnapMode.End, _endPoint));

            return snapPnts;
        }

        /// <summary>
        /// 获取夹点
        /// </summary>
        public override List<GripPoint> GetGripPoints()
        {
            List<GripPoint> gripPnts = new List<GripPoint>();
            gripPnts.Add(new GripPoint(GripPointType.End, _endPoint));

            return gripPnts;
        }

        /// <summary>
        /// 设置夹点
        /// </summary>
        public override void SetGripPointAt(int index, GripPoint gripPoint, LitMath.Vector2 newPosition)
        {
            if (index == 0)
            {
                _endPoint = newPosition;
            }
        }

        /// <summary>
        /// 写XML
        /// </summary>
        public override void XmlOut(Filer.XmlFiler filer)
        {
            base.XmlOut(filer);

            filer.Write("endPoint", _endPoint);
        }

        /// <summary>
        /// 读XML
        /// </summary>
        public override void XmlIn(Filer.XmlFiler filer)
        {
            base.XmlIn(filer);

            filer.Read("endPoint", out _endPoint);
        }
    }
}
