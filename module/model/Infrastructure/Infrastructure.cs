using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace VisitCounter
{
    public class Infrastructure : BaseObject, IInfrastructure
    {
        static int s_nextID = 1;
        public int ID { get; private set; }
        public Dictionary<String, bool> HasTagDic { get; set; }
        public CustomizerType CustomType { get; set;}
        Polygon GetPolygonBuffer(Geometry geometry, double radius)
        {
            LinearRing oldExteriorRing, newExteriorRing;
            if (geometry is Polygon)
            {
                oldExteriorRing = (LinearRing)((Polygon)geometry)
                    .ExteriorRing;
            }
            else
            {
                oldExteriorRing = (LinearRing)geometry;
            }
            newExteriorRing = (LinearRing)((Polygon)oldExteriorRing
                .Buffer(radius)).ExteriorRing;
            return new Polygon(newExteriorRing);
        }
        Polygon GetLinearRingBuffer(Geometry geometry, double radius)
        {
            LinearRing newExteriorRing;
            if (geometry is Polygon)
            {
                newExteriorRing = (LinearRing)((Polygon)geometry)
                    .ExteriorRing;
            }
            else
            {
                newExteriorRing = (LinearRing)geometry;
            }
            return (Polygon)newExteriorRing.Buffer(radius);
        }
        public void SetBufferedGeometry(double additionalRadius,
                                        bool needsPolygon)
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
                    newPolygon = this.GetLinearRingBuffer(geometry, 
                                                          radius);
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
        public Infrastructure(Geometry geometry, 
                              Dictionary<String, bool> hasTagDic)
                              : base(geometry)
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
            this.ID = Infrastructure.s_nextID;
            Infrastructure.s_nextID++;
        }
    }
}
