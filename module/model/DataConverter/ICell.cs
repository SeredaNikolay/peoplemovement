using NetTopologySuite.Geometries;

namespace VisitCounter
{
    /// <summary>Cell interface.</summary>
    public interface ICell
    {
        /// <value>
        /// Property <c>VisitCount</c> Number of cell visits.
        /// </value>
        public int VisitCount { get; set; }

        /// <summary>Set cost for cells with CustomizerType type.</summary>
        /// <param name="key">
        /// CustomizerType for which the price will be set.
        /// </param>
        /// <param name="value">Cost value to set.</param>
        public void SetCustomizerTypeCost(CustomizerType key,
                                          double value);

        /// <summary>Gets cell CustomizerType.</summary>
        /// <param name="customType">Output CustomizerType</param>
        /// <returns>
        /// Success rate 
        /// (when value is set - true;
        /// when value is not set - false).
        /// </returns>
        public bool GetCellCustomType(out CustomizerType customType);

        /// <summary>Set cell CustomizerType.</summary>
        /// <param name="customType">CustomizerType to set.</param>
        public void SetCellCustomType(CustomizerType customType);

        /// <value>
        /// Property <c>CellPolygon</c> Сell polygon.
        /// </value>
        public Polygon CellPolygon { get; }
    }
}
