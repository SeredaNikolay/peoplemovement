using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace VisitCounter
{
    public abstract class MovingObject: BaseObject, IMovingObject
    {
        double _step = 2.0;
        Coordinate _curCrd;
        Coordinate _nextCrd;
        double _curStep;
        double _distToNextCrd;
        public List<Coordinate> DstCoordinates { get; set; }
        public List<Coordinate> VisitedCrdList { get; private set; }
        public int PrevCellI { get; set; }
        public int PrevCellJ { get; set; }
        public MovingObject(Point point) : base(point)
        {
            this.PrevCellJ = -1;
            this.PrevCellI = -1;
        }
        void StepToNextPoint()
        {
            double xA = this._curCrd.X, xB = this._nextCrd.X;
            double yA = this._curCrd.Y, yB = this._nextCrd.Y;
            double ac = this._curStep;
            double cb = this._distToNextCrd - this._curStep;
            double k = ac / cb;
            double xC = (xA + k * xB) / (1 + k);
            double yC = (yA + k * yB) / (1 + k);
            ((Point)this.Geometry).X = xC;
            ((Point)this.Geometry).Y = yC;
            this._curStep = 0.0;
        }
        void StepOverOrOnNextPoint()
        {
            this._curStep -= this._distToNextCrd;
            this._curCrd = this._nextCrd;
            this.DstCoordinates.RemoveAt(0);
            ((Point)this.Geometry).X = this._curCrd.X;
            ((Point)this.Geometry).Y = this._curCrd.Y;
        }
        protected void Move()
        {
            this._curStep = this._step;
            this.VisitedCrdList = new List<Coordinate>();
            this.VisitedCrdList.Add(new Coordinate(
                    ((Point)this.Geometry).Coordinate));
            if (this.DstCoordinates != null
                && this.DstCoordinates.Count > 0
                && ((Point)this.Geometry).Coordinate 
                    != this.DstCoordinates[^1])
            {
                while (this.DstCoordinates.Count > 0 && 
                    this._curStep > Constant.epsilon)
                {
                    this._curCrd = ((Point)this.Geometry).Coordinate;
                    this._nextCrd = this.DstCoordinates[0];
                    this._distToNextCrd = this._curCrd.Distance(
                        this._nextCrd);
                    if (this._curStep >= this._distToNextCrd)
                    {
                        this.StepOverOrOnNextPoint();
                    }
                    else
                    {
                        this.StepToNextPoint();
                    }
                    this.VisitedCrdList.Add(
                        new Coordinate(
                            ((Point)this.Geometry).Coordinate));
                }
            }
        }
    }
}
