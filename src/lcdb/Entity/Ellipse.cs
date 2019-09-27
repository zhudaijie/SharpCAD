using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCAD.DatabaseServices
{
    public class Ellipse : Entity
    {
        /// <summary>
        /// 类名
        /// </summary>
        public override string className
        {
            get { return "Ellipse"; }
        }

        /// <summary>
        /// 椭圆心
        /// </summary>
        private LitMath.Vector2 _center = new LitMath.Vector2(0, 0);
        public LitMath.Vector2 center
        {
            get { return _center; }
            set { _center = value; }
        }

        /// <summary>
        /// 半径X
        /// </summary>
        private double _radiusX = 0.0;
        public double radiusX
        {
            get { return _radiusX; }
            set { _radiusX = value; }
        }

        /// <summary>
        /// 半径Y
        /// </summary>
        private double _radiusY = 0.0;
        public double radiusY
        {
            get { return _radiusY; }
            set { _radiusY = value; }
        }

        /// <summary>
        /// 直径X
        /// </summary>
        public double diameterX
        {
            get { return _radiusX * 2; }
        }

        /// <summary>
        /// 直径Y
        /// </summary>
        public double diameterY
        {
            get { return _radiusY * 2; }
        }

        /// <summary>
        /// 外围边框
        /// </summary>
        public override Bounding bounding
        {
            get
            {
                return new Bounding(_center, this.diameterX, this.diameterY);
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public Ellipse()
        {
        }

        public Ellipse(LitMath.Vector2 center, double radiusX, double radiusY)
        {
            _center = center;
            _radiusX = radiusX;
            _radiusY = radiusY;
        }

        /// <summary>
        /// 绘制函数
        /// </summary>
        public override void Draw(IGraphicsDraw gd)
        {
            gd.DrawEllipse(_center, _radiusX, _radiusY);
        }

        /// <summary>
        /// 克隆函数
        /// </summary>
        public override object Clone()
        {
            Ellipse ellipse = base.Clone() as Ellipse;
            ellipse._center = _center;
            ellipse._radiusX = _radiusX;
            ellipse._radiusY = _radiusY;
            return ellipse;
        }

        /// <summary>
        /// 创建椭圆实例
        /// </summary>
        protected override DBObject CreateInstance()
        {
            return new Ellipse();
        }

        /// <summary>
        /// 平移
        /// </summary>
        public override void Translate(LitMath.Vector2 translation)
        {
            _center += translation;
        }

        /// <summary>
        /// Transform
        /// </summary>
        public override void TransformBy(LitMath.Matrix3 transform)
        {
            LitMath.Vector2 pntX = _center + new LitMath.Vector2(_radiusX, 0);
            LitMath.Vector2 pntY = _center + new LitMath.Vector2(0, _radiusY);

            _center = transform * _center;
            _radiusX = (transform * pntX - _center).length;
            _radiusY = (transform * pntY - _center).length;
        }

        /// <summary>
        /// 对象捕捉点
        /// </summary>
        public override List<ObjectSnapPoint> GetSnapPoints()
        {
            List<ObjectSnapPoint> snapPnts = new List<ObjectSnapPoint>();
            snapPnts.Add(new ObjectSnapPoint(ObjectSnapMode.Center, _center));

            return snapPnts;
        }

        /// <summary>
        /// 获取夹点
        /// </summary>
        public override List<GripPoint> GetGripPoints()
        {
            List<GripPoint> gripPnts = new List<GripPoint>();
            gripPnts.Add(new GripPoint(GripPointType.Center, _center));
            gripPnts.Add(new GripPoint(GripPointType.Quad, _center + new LitMath.Vector2(_radiusX, 0)));
            gripPnts.Add(new GripPoint(GripPointType.Quad, _center + new LitMath.Vector2(0, _radiusY)));
            gripPnts.Add(new GripPoint(GripPointType.Quad, _center + new LitMath.Vector2(-_radiusX, 0)));
            gripPnts.Add(new GripPoint(GripPointType.Quad, _center + new LitMath.Vector2(0, -_radiusY)));

            return gripPnts;
        }

        /// <summary>
        /// 设置夹点
        /// </summary>
        public override void SetGripPointAt(int index, GripPoint gripPoint, LitMath.Vector2 newPosition)
        {
            if (index == 0)
            {
                _center = newPosition;
            }
            else if (index >= 1 && index <= 4)
            {
                _radiusX = (newPosition - _center).length;
            }
        }

        /// <summary>
        /// 写XML
        /// </summary>
        public override void XmlOut(Filer.XmlFiler filer)
        {
            base.XmlOut(filer);

            filer.Write("center", _center);
            filer.Write("radiusX", _radiusX);
            filer.Write("radiusY", _radiusY);
        }

        /// <summary>
        /// 读XML
        /// </summary>
        public override void XmlIn(Filer.XmlFiler filer)
        {
            base.XmlIn(filer);

            filer.Read("center", out _center);
            filer.Read("radiusX", out _radiusX);
            filer.Read("radiusY", out _radiusY);
        }
    }
}
