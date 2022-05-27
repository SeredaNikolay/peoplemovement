using System;
using NetTopologySuite.Geometries;

namespace VisitCounter
{
    class Grid
    {
        public static Grid GridInstance { get; set; }
        double _minX, _minY, _maxX, _maxY;
        int _cellCountX, _cellCountY;
        Polygon _mainRectangle = null;
        double _gridPitch = 0.0;
        Cell[,] _cellArr;
        public Polygon MainRectangle
        {
            get
            {
                return this._mainRectangle;
            }
            set
            {
                if (value == null || value.IsEmpty)
                {
                    this._mainRectangle = new Polygon(
                        new LinearRing(
                            new Coordinate[]
                            {
                                new Coordinate(0, 0),
                                new Coordinate(100, 0),
                                new Coordinate(100, 100),
                                new Coordinate(0, 100),
                                new Coordinate(0, 0)
                            }
                        )
                    );
                }
                else
                {
                    this._mainRectangle = value;
                }
            }
        }
        public double GridPitch
        {
            get
            {
                return this._gridPitch;
            }
            set
            {
                if (value < 1.0)
                {
                    this._gridPitch = 2.0;
                }
                else
                {
                    this._gridPitch = value;
                }
            }
        }
        public int GetCellCountX{ get { return this._cellCountX; } }
        public int GetCellCountY { get { return this._cellCountY; } }
        public Cell GetCellByIndexes(int j, int i)
        {
            bool jIndIsCorrect = j >= 0 && j < this._cellCountY;
            bool iIndIsCorrect = i >= 0 && i < this._cellCountX;
            if (jIndIsCorrect && iIndIsCorrect)
            {
                return this._cellArr[j, i];
            }
            return null;
        }
        public bool GetIndexesByCoordinate(Coordinate coordinate, out int j, out int i)
        {
            bool xIsCorrect = coordinate.X >= this._minX && coordinate.X < this._maxX;
            bool yIsCorrect = coordinate.Y >= this._minY && coordinate.Y < this._maxY;
            if (xIsCorrect && yIsCorrect)
            {
                j = (int)Math.Truncate((coordinate.Y - this._minY) / this._gridPitch);
                i = (int)Math.Truncate((coordinate.X - this._minX) / this._gridPitch);
                return true;
            }
            j = -1;
            i = -1;
            return false;
        }
        public Grid(Polygon mainRectangle, double gridPitch)
        {
            this.MainRectangle = mainRectangle;
            this.GridPitch = gridPitch;
            this._minX = mainRectangle.EnvelopeInternal.MinX;
            this._minY = mainRectangle.EnvelopeInternal.MinY;
            this._maxX = mainRectangle.EnvelopeInternal.MaxX;
            this._maxY = mainRectangle.EnvelopeInternal.MaxY;
            this._cellCountX = (int)Math.Ceiling((this._maxX - this._minX) / this.GridPitch);
            this._cellCountY = (int)Math.Ceiling((this._maxY - this._minY) / this.GridPitch);
            this._cellArr = new Cell[this._cellCountY, this._cellCountX];
            for (int j = 0; j < this._cellCountY; j++)
            {
                for (int i = 0; i < this._cellCountX; i++)
                {
                    this._cellArr[j, i] = new Cell(
                        new Coordinate(
                            this._minX + i * this._gridPitch,
                            this._minY + j * this._gridPitch
                        ),
                        this._gridPitch
                    );
                }
            }
        }
    }
}
