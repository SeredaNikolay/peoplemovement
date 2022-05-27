using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace VisitCounter
{
    interface IHasTagObject
    {
        public Dictionary<String, bool> HasTagDic { get; set; }
    }
    interface IInfrastructureObject: IHasTagObject
    {
        public void SetBufferedGeometry(double additionalRadius, bool needsPolygon);
    }
    class InfrastructureObject : BaseObject, IInfrastructureObject
    {
        static int s_nextID = 1;
        public int ID { get; private set; }
        public Dictionary<String, bool> HasTagDic { get; set; }
        public CustomizerType CustomType { get; set;}

        static Dictionary<String, List<InfrastructureObject>> s_infrastructureDic = new Dictionary<String, List<InfrastructureObject>>()
        {
            {"polygon", new List<InfrastructureObject>() },
            {"way", new List<InfrastructureObject>() },
            {"point", new List<InfrastructureObject>() },
            {"pointbuffer", new List<InfrastructureObject>() }
        };
        public static Dictionary<String, List<InfrastructureObject>> InfrastructureDic
        {
            get
            {
                return InfrastructureObject.s_infrastructureDic;
            }
            set
            {
                InfrastructureObject.s_infrastructureDic = value;
            }
        }
        public static double GetAdditionalRadiusForInfrastructureWay(InfrastructureObject infrastObj)
        {
            bool has;
            if (infrastObj.HasTagDic.TryGetValue("passageWay", out has))
                return 3.0;
            if (infrastObj.HasTagDic.TryGetValue("walkWay", out has))
                return 0.75;
            if (infrastObj.HasTagDic.TryGetValue("weakWay", out has))
                return 2.0;
            if (infrastObj.HasTagDic.TryGetValue("strongWay", out has))
                return 5.0;
            return 0.0;
        }
        Polygon GetPolygonBuffer(Geometry geometry, double radius)
        {
            LinearRing oldExteriorRing, newExteriorRing;
            if (geometry is Polygon)
            {
                oldExteriorRing = (LinearRing)((Polygon)geometry).ExteriorRing;
            }
            else
            {
                oldExteriorRing = (LinearRing)geometry;
            }
            newExteriorRing = (LinearRing)((Polygon)oldExteriorRing.Buffer(radius)).ExteriorRing;
            return new Polygon(newExteriorRing);
        }
        Polygon GetLinearRingBuffer(Geometry geometry, double radius)
        {
            LinearRing newExteriorRing;
            if (geometry is Polygon)
            {
                newExteriorRing = (LinearRing)((Polygon)geometry).ExteriorRing;
            }
            else
            {
                newExteriorRing = (LinearRing)geometry;
            }
            return (Polygon)newExteriorRing.Buffer(radius);
        }
        public void SetBufferedGeometry(double additionalRadius, bool needsPolygon)
        {
            Geometry geometry = this.Geometry;
            Polygon newPolygon = null;
            double radius = additionalRadius;
            if(this.Geometry is Polygon || geometry is LinearRing)
            {
                if (needsPolygon)
                {
                    newPolygon = this.GetPolygonBuffer(geometry, radius);
                }
                else
                {
                    newPolygon = this.GetLinearRingBuffer(geometry, radius);
                }
            }
            else if(geometry is LineString || geometry is Point)
            {
                newPolygon = (Polygon)geometry.Buffer(radius);
            }
            if(newPolygon != null)
            {
                this.Geometry = newPolygon;
            }
        }
        public InfrastructureObject(Geometry geometry, Dictionary<String, bool> hasTagDic) : base(geometry)
        {
            this.Geometry = geometry;
            if (hasTagDic == null)
            {
                this.HasTagDic = new Dictionary<String, bool>();
            }
            else
            {
                this.HasTagDic = hasTagDic;
            }
            this.ID = InfrastructureObject.s_nextID;
            InfrastructureObject.s_nextID++;
        }
    }
}
