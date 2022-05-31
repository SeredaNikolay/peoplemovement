using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace VisitCounter
{
    public class Cell: ICell
    {
        bool _customTypeDetected = false;
        CustomizerType _cellCustomType = CustomizerType.None;

        static Dictionary<CustomizerType, double> CustomTypeToCostDic = 
            new Dictionary<CustomizerType, double>()
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
        public Polygon CellPolygon { get; private set; }
        public int VisitCount { get; set; }
        public double GetCellCost()
        {
            return Cell.CustomTypeToCostDic[this._cellCustomType];
        }
        public void SetCustomizerTypeCost(CustomizerType key, double value)
        {
            bool isKey = Cell.CustomTypeToCostDic.ContainsKey(key);
            if (isKey 
                && Cell.CustomTypeToCostDic[key] 
                    != Double.PositiveInfinity)
            {
                Cell.CustomTypeToCostDic[key] = value;
            }
            else
            {
                Console.WriteLine("Wrong CustomizerType for cost");
            }
        }
        public bool GetCellCustomType(out CustomizerType customType)
        {
            customType = this._cellCustomType;
            return this._customTypeDetected;
        }
        public void SetCellCustomType(CustomizerType customType)
        {
            this._cellCustomType = customType;
            if (!this._customTypeDetected)
                this._customTypeDetected = true;
        }
        public Cell(Coordinate crd, double gridPitch, 
            Dictionary<String,List<IInfrastructure>> infrastDic)
        {
            this.CellPolygon = new Polygon(
                new LinearRing(new Coordinate[]{
                    new Coordinate(crd.X, crd.Y),
                    new Coordinate(crd.X+gridPitch, crd.Y),
                    new Coordinate(crd.X+gridPitch, crd.Y+gridPitch),
                    new Coordinate(crd.X, crd.Y+gridPitch),
                    new Coordinate(crd.X, crd.Y),
                }));
            this.VisitCount = 0;
        }
    }
}
