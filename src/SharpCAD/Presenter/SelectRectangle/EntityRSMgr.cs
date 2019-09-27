using System;
using System.Collections.Generic;

using SharpCAD.ApplicationServices;
using SharpCAD.DatabaseServices;

namespace SharpCAD
{
    internal class EntityRSMgr
    {
        /// <summary>
        /// Singleton
        /// </summary>
        private static EntityRSMgr _instance = null;
        internal static EntityRSMgr Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new EntityRSMgr();
                }

                return _instance;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        private Dictionary<Type, EntityRS> _type2EntityRS = new Dictionary<Type, EntityRS>();

        private EntityRSMgr()
        {
            Initialize();
        }

        private void Initialize()
        {
            RegisterEntityRS(typeof(XPoint), new XPointRS());
            RegisterEntityRS(typeof(Line), new LineRS());
            RegisterEntityRS(typeof(Xline), new XlineRS());
            RegisterEntityRS(typeof(Ray), new RayRS());
            RegisterEntityRS(typeof(Polyline), new PolylineRS());
            RegisterEntityRS(typeof(Circle), new CircleRS());
            RegisterEntityRS(typeof(Ellipse), new EllipseRS());
            RegisterEntityRS(typeof(Arc), new ArcRS());
            RegisterEntityRS(typeof(Text), new TextRS());
        }

        private void RegisterEntityRS(Type type, EntityRS entityRS)
        {
            _type2EntityRS[type] = entityRS;
        }

        /// <summary>
        /// Select
        /// </summary>
        internal bool Cross(Bounding selectBound, Entity entity)
        {
            if (_type2EntityRS.ContainsKey(entity.GetType()))
            {
                return _type2EntityRS[entity.GetType()].Cross(selectBound, entity);
            }

            return false;
        }

        internal bool Window(Bounding selectBound, Entity entity)
        {
            if (_type2EntityRS.ContainsKey(entity.GetType()))
            {
                return _type2EntityRS[entity.GetType()].Window(selectBound, entity);
            }

            return false;
        }
    }
}
