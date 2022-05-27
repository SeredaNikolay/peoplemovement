using System;
using System.Collections.Generic;
using System.Text;
using NetTopologySuite.Geometries;

namespace VisitCounter
{
    class Cell
    {
        Polygon _cell;
        bool _customTypeDetected = false;
        CustomizerType _cellCustomType = CustomizerType.None;

        static Dictionary<CustomizerType, double> CustomTypeToCostDic = new Dictionary<CustomizerType, double>()
        {
            {CustomizerType.TargetDestination, 2.0},
            {CustomizerType.Passage, 2.0},
            {CustomizerType.Building, Double.PositiveInfinity},
            {CustomizerType.Barrier, Double.PositiveInfinity},
            {CustomizerType.Water, Double.PositiveInfinity},
            {CustomizerType.StrongTransportWay, Double.PositiveInfinity},
            {CustomizerType.WeakTransportWay, 10.0},
            {CustomizerType.WalkWay, 1.0},
            {CustomizerType.Walkable, 6.0},
            {CustomizerType.None, 4.0},
        };
        public int VisitCount { get; set; }
        public double GetCellCost()
        {
            return Cell.CustomTypeToCostDic[this._cellCustomType];
        }
        public void SetCustomizerTypeCost(CustomizerType key, double value)
        {
            bool isKey = Cell.CustomTypeToCostDic.ContainsKey(key);
            if (isKey && Cell.CustomTypeToCostDic[key] != Double.PositiveInfinity)
            {
                Cell.CustomTypeToCostDic[key] = value;
            }
            else
            {
                Console.WriteLine("Wrong CustomizerType for cost");
            }
        }
        public CustomizerType CellCustomType
        {
            get
            {
                if(!this._customTypeDetected)
                {
                    this.DetectCustomType();
                }
                return this._cellCustomType;
            }
        }
        static int cnt = 0;
        void DetectCustomType()
        {
            if(!this._customTypeDetected)
            {
                /*foreach (KeyValuePair<String, List<InfrastructureObject>> pair in InfrastructureObject.InfrastructureDic)
                {
                    if (pair.Key != "point")
                    {
                        foreach (InfrastructureObject infrastObj in pair.Value)
                        {
                            if (infrastObj.CustomType < this._cellCustomType)
                            {
                                if (this._cell.Intersects(infrastObj.Geometry))
                                {
                                    this._cellCustomType = infrastObj.CustomType;
                                }
                            }
                        }
                    }
                }*/
                //if(this._cellCustomType != CustomizerType.None)
                /*if(cnt < 20000)
                {
                    Geometry geometry = Customizer.Instance.Customize(this._cell, this._cellCustomType);
                    TestModule.MapObjectsTmp.Add(geometry);
                    cnt++;
                }*/
                this._customTypeDetected = true;
            }
        }
        public Cell(Coordinate crd, double gridPitch)
        {
            this._cell = new Polygon(
                new LinearRing(new Coordinate[]{
                    new Coordinate(crd.X, crd.Y),
                    new Coordinate(crd.X+gridPitch, crd.Y),
                    new Coordinate(crd.X+gridPitch, crd.Y+gridPitch),
                    new Coordinate(crd.X, crd.Y+gridPitch),
                    new Coordinate(crd.X, crd.Y),
                }));
            this.VisitCount = 0;
            this.DetectCustomType();
        }
    }
}
