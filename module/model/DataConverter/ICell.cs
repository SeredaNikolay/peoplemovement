using NetTopologySuite.Geometries;

namespace VisitCounter
{
    public interface ICell
    {
        public int VisitCount { get; set; }
        public void SetCustomizerTypeCost(CustomizerType key,
                                          double value);
        public bool GetCellCustomType(out CustomizerType customType);
        public void SetCellCustomType(CustomizerType customType);
        public Polygon CellPolygon { get; }

    }
}
