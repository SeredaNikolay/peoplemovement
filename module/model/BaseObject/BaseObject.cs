using System;
using NetTopologySuite.Geometries;

namespace VisitCounter
{
    abstract class BaseObject: IBaseObject
    {
        private Geometry geometry = null;
        public static bool IsSuitableClass(Geometry geom)
        {
            return geom != null && !geom.IsEmpty
                &&(geom is Polygon|| geom is LineString 
                || geom is Point);
        }
        public Geometry Geometry
        {
            get
            {
                return this.geometry;
            }
            set
            {
                if (BaseObject.IsSuitableClass(value))
                {
                    this.geometry = value;
                }
                else
                {
                    Console.WriteLine("Wrong input class value!"
                        +"(" + value.GetType() + ")");
                }
            }
        }
        public BaseObject(Geometry geometry)
        {
            this.Geometry = geometry;
        }
    }
}
