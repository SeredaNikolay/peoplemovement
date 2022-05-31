using System.Collections.Generic;
using NetTopologySuite.Geometries;


namespace VisitCounter
{
    public interface IGrid
    {
        public Polygon MainRectangle { get; set; }
        public double GridPitch { get; set; }
        public int GetCellCountX { get; }
        public int GetCellCountY { get; }
        public ICell GetCellByIndexes(int j, int i);
        public bool GetIndexesByCoordinate(Coordinate coordinate,
                                   out int j,
                                   out int i);
        public int[,] GetVisitMatrix();
        public void AddVisitsToCells(List<Coordinate> crdList,
                                     ref int prevCellJ,
                                     ref int prevCellI);

    }
}
