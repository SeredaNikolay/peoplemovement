using System;
using System.Linq;
using NetTopologySuite.Geometries;
using OSMLSGlobalLibrary.Map;
using System.Collections.Generic;


namespace VisitCounter
{
    [CustomStyle(@"new style.Style({
                fill: new style.Fill({
                    color: 'rgba(0, 128, 0, 0.4)'
                }),
                stroke: new style.Stroke({
                    color: 'rgba(0, 128, 0, 1)',
                    width: 1
                })
            });
    ")]
    class Walkable : Polygon
    {
        public Walkable(Polygon polygon) : base(
            new LinearRing(polygon.ExteriorRing.Coordinates),
            (from p in polygon.InteriorRings
             select new LinearRing(p.Coordinates))
            .Cast<LinearRing>().ToArray()
        )
        {
        }
    }

    [CustomStyle(@"new style.Style({
                fill: new style.Fill({
                    color: 'rgba(30, 144, 255, 0.4)'
                }),
                stroke: new style.Stroke({
                    color: 'rgba(30, 144, 255, 1)',
                    width: 1
                })
            });
    ")]
    class Water : Polygon
    {
        public Water(Polygon polygon) : base(
            new LinearRing(polygon.ExteriorRing.Coordinates),
            (from p in polygon.InteriorRings
             select new LinearRing(p.Coordinates))
            .Cast<LinearRing>().ToArray()
        )
        {
        }
    }

    [CustomStyle(@"new style.Style({
                fill: new style.Fill({
                    color: 'rgba(0, 0, 128, 0.4)'
                }),
                stroke: new style.Stroke({
                    color: 'rgba(0, 0, 128, 1)',
                    width: 1
                })
            });
    ")]
    class Building : Polygon
    {
        public Building(Polygon polygon) : base(
            new LinearRing(polygon.ExteriorRing.Coordinates),
            (from p in polygon.InteriorRings
             select new LinearRing(p.Coordinates))
            .Cast<LinearRing>().ToArray()
        )
        {
        }
    }

    [CustomStyle(@"new style.Style({
                fill: new style.Fill({
                    color: 'rgba(0, 0, 0, 0.4)'
                }),
                stroke: new style.Stroke({
                    color: 'rgba(0, 0, 0, 1)',
                    width: 1
                })
            });
    ")]
    class Barrier : Polygon
    {
        public Barrier(Polygon polygon) : base(
            new LinearRing(polygon.ExteriorRing.Coordinates),
            (from p in polygon.InteriorRings
             select new LinearRing(p.Coordinates))
            .Cast<LinearRing>().ToArray()
        )
        {
        }
    }

    [CustomStyle(@"new style.Style({
                fill: new style.Fill({
                    color: 'rgba(138, 43, 226, 0.4)'
                }),
                stroke: new style.Stroke({
                    color: 'rgba(138, 43, 226, 1)',
                    width: 1
                })
            });
    ")]
    class WalkWay : Polygon
    {
        public WalkWay(Polygon polygon) : base(
            new LinearRing(polygon.ExteriorRing.Coordinates),
            (from p in polygon.InteriorRings
             select new LinearRing(p.Coordinates))
            .Cast<LinearRing>().ToArray()
        )
        {
        }
    }

    [CustomStyle(@"new style.Style({
                fill: new style.Fill({
                    color: 'rgba(255, 0, 255, 0.4)'
                }),
                stroke: new style.Stroke({
                    color: 'rgba(255, 0, 255, 1)',
                    width: 1
                })
            });
    ")]
    class WeakTransportWay : Polygon
    {
        public WeakTransportWay(Polygon polygon) : base(
            new LinearRing(polygon.ExteriorRing.Coordinates),
            (from p in polygon.InteriorRings
             select new LinearRing(p.Coordinates))
            .Cast<LinearRing>().ToArray()
        )
        {
        }
    }

    [CustomStyle(@"new style.Style({
                fill: new style.Fill({
                    color: 'rgba(255, 0, 0, 0.4)'
                }),
                stroke: new style.Stroke({
                    color: 'rgba(255, 0, 0, 1)',
                    width: 1
                })
            });
    ")]
    class StrongTransportWay : Polygon
    {
        public StrongTransportWay(Polygon polygon) : base(
            new LinearRing(polygon.ExteriorRing.Coordinates),
            (from p in polygon.InteriorRings
             select new LinearRing(p.Coordinates))
            .Cast<LinearRing>().ToArray()
        )
        {
        }
    }

    [CustomStyle(@"new style.Style({
                fill: new style.Fill({
                    color: 'rgba(139, 69, 19, 0.4)'
                }),
                stroke: new style.Stroke({
                    color: 'rgba(139, 69, 19, 1)',
                    width: 1
                })
            });
    ")]
    class TargetDestination : Polygon
    {
        public TargetDestination(Polygon polygon) : base(
            new LinearRing(polygon.ExteriorRing.Coordinates),
            (from p in polygon.InteriorRings
             select new LinearRing(p.Coordinates))
            .Cast<LinearRing>().ToArray()
        )
        {
        }
    }

    [CustomStyle(@"new style.Style({
                fill: new style.Fill({
                    color: 'rgba(0, 0, 255, 0.4)'
                }),
                stroke: new style.Stroke({
                    color: 'rgba(0, 0, 255, 1)',
                    width: 1
                })
            });
    ")]
    class Passage : Polygon
    {
        public Passage(Polygon polygon) : base(
            new LinearRing(polygon.ExteriorRing.Coordinates),
            (from p in polygon.InteriorRings
             select new LinearRing(p.Coordinates))
            .Cast<LinearRing>().ToArray()
        )
        {
        }
    }

    [CustomStyle(@"new style.Style({
                fill: new style.Fill({
                    color: 'rgba(189, 183, 107, 0.4)'
                }),
                stroke: new style.Stroke({
                    color: 'rgba(189, 183, 107, 1)',
                    width: 1
                })
            });
    ")]
    class None : Polygon
    {
        public None(Polygon polygon) : base(
            new LinearRing(polygon.ExteriorRing.Coordinates),
            (from p in polygon.InteriorRings
             select new LinearRing(p.Coordinates))
            .Cast<LinearRing>().ToArray()
        )
        {
        }
    }
    //--------------------------------------------------------
    [CustomStyle(
    @"new style.Style({
            image: new style.Circle({
                opacity: 1.0,
                scale: 1.0,
                radius: 5,
                fill: new style.Fill({
                    color: 'rgba(0, 128, 0, 1)'
                }),
                stroke: new style.Stroke({
                    color: 'rgba(0, 128, 0, 1)',
                    width: 1
                })
            })
        });"
    )]
    class LeisurePoint : Point
    {
        public LeisurePoint(Point point) : base(point.Coordinate)
        {

        }
    }

    [CustomStyle(
    @"new style.Style({
            image: new style.Circle({
                opacity: 1.0,
                scale: 1.0,
                radius: 5,
                fill: new style.Fill({
                    color: 'rgba(255, 0, 0, 1)'
                }),
                stroke: new style.Stroke({
                    color: 'rgba(255, 0, 0, 1)',
                    width: 1
                })
            })
        });"
    )]
    class WorkPoint : Point
    {
        public WorkPoint(Point point) : base(point.Coordinate)
        {

        }
    }

    [CustomStyle(
    @"new style.Style({
            image: new style.Circle({
                opacity: 1.0,
                scale: 1.0,
                radius: 5,
                fill: new style.Fill({
                    color: 'rgba(0, 0, 255, 1)'
                }),
                stroke: new style.Stroke({
                    color: 'rgba(0, 0, 255, 1)',
                    width: 1
                })
            })
        });"
    )]
    class HomePoint : Point
    {
        public HomePoint(Point point) : base(point.Coordinate)
        {

        }
    }

    [CustomStyle(
    @"new style.Style({
            image: new style.Circle({
                opacity: 1.0,
                scale: 1.0,
                radius: 5,
                fill: new style.Fill({
                    color: 'rgba(255, 140, 0, 1)'
                }),
                stroke: new style.Stroke({
                    color: 'rgba(255, 140, 0, 1)',
                    width: 1
                })
            })
        });"
    )]
    class FoodPoint : Point
    {
        public FoodPoint(Point point) : base(point.Coordinate)
        {

        }
    }

    [CustomStyle(
    @"new style.Style({
            image: new style.Circle({
                opacity: 1.0,
                scale: 1.0,
                radius: 5,
                fill: new style.Fill({
                    color: 'rgba(138, 43, 226, 1)'
                }),
                stroke: new style.Stroke({
                    color: 'rgba(138, 43, 226, 1)',
                    width: 1
                })
            })
        });"
    )]
    class HumanPoint : Point
    {
        public HumanPoint(Point point) : base(point.Coordinate)
        {

        }
    }
    public sealed class Customizer
    {
        private Customizer()
        {
        }

        private static Customizer customizer = null;
        private static readonly object threadlock = new object();
        public static Customizer Instance
        {
            get
            {
                lock (threadlock)
                {
                    if (customizer == null)
                        customizer = new Customizer();
                    return customizer;
                }
            }
        }
        public static CustomizerType GetCustomizerType(Dictionary<String, bool> hasTagDic)
        {
            bool has;
            if (hasTagDic.TryGetValue("walkable", out has))
            {
                if (hasTagDic.TryGetValue("leisure", out has))
                    return CustomizerType.Walkable;
                if (hasTagDic.TryGetValue("passageWay", out has))
                    return CustomizerType.Passage;
                if (hasTagDic.TryGetValue("walkWay", out has))
                    return CustomizerType.WalkWay;
                if (hasTagDic.TryGetValue("weakWay", out has))
                    return CustomizerType.WeakTransportWay;
            }
            if (hasTagDic.TryGetValue("!walkable", out has))
            {
                if (hasTagDic.TryGetValue("building", out has))
                    return CustomizerType.Building;
                if (hasTagDic.TryGetValue("residentionalBuilding", out has))
                    return CustomizerType.Building;
                if (hasTagDic.TryGetValue("!residentionalBuilding", out has))
                    return CustomizerType.Building;
                if (hasTagDic.TryGetValue("leisure", out has))
                    return CustomizerType.Barrier;
                if (hasTagDic.TryGetValue("food", out has))
                    return CustomizerType.Barrier;
                if (hasTagDic.TryGetValue("water", out has))
                    return CustomizerType.Water;
                if (hasTagDic.TryGetValue("strongWay", out has))
                    return CustomizerType.StrongTransportWay;
                if (hasTagDic.TryGetValue("barrier", out has))
                    return CustomizerType.Barrier;
            }
            if (hasTagDic.TryGetValue("leisure", out has))
                return CustomizerType.LeisurePoint;
            if (hasTagDic.TryGetValue("food", out has))
                return CustomizerType.FoodPoint;
            if (hasTagDic.TryGetValue("work", out has))
                return CustomizerType.WorkPoint;
            if (hasTagDic.TryGetValue("home", out has))
                return CustomizerType.HomePoint;
            if (hasTagDic.TryGetValue("destination", out has))
                return CustomizerType.TargetDestination;
            return CustomizerType.None;
        }
        public Geometry Customize(Geometry geometry, CustomizerType objType)
        {
            if (geometry is Polygon)
            {
                if (objType == CustomizerType.Walkable)
                    return new Walkable((Polygon)geometry);
                if (objType == CustomizerType.Water)
                    return new Water((Polygon)geometry);
                if (objType == CustomizerType.Building)
                    return new Building((Polygon)geometry);
                if (objType == CustomizerType.WalkWay)
                    return new WalkWay((Polygon)geometry);
                if (objType == CustomizerType.WeakTransportWay)
                    return new WeakTransportWay((Polygon)geometry);
                if (objType == CustomizerType.StrongTransportWay)
                    return new StrongTransportWay((Polygon)geometry);
                if (objType == CustomizerType.Barrier)
                    return new Barrier((Polygon)geometry);
                if (objType == CustomizerType.TargetDestination)
                    return new TargetDestination((Polygon)geometry);
                if (objType == CustomizerType.Passage)
                    return new Passage((Polygon)geometry);
                if (objType == CustomizerType.None)
                    return new None((Polygon)geometry);
            }
            else if(geometry is Point)
            {
                if (objType == CustomizerType.LeisurePoint)
                    return new LeisurePoint((Point)geometry);
                if (objType == CustomizerType.WorkPoint)
                    return new WorkPoint((Point)geometry);
                if (objType == CustomizerType.HomePoint)
                    return new HomePoint((Point)geometry);
                if (objType == CustomizerType.FoodPoint)
                    return new FoodPoint((Point)geometry);
                if (objType == CustomizerType.HumanPoint)
                    return new HumanPoint((Point)geometry);
            }
            Console.WriteLine("====================" + objType.ToString() + " " + geometry.GeometryType + "==============================");
            return null;
        }
    }
}
