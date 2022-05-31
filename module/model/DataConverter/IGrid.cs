using System.Collections.Generic;
using NetTopologySuite.Geometries;


namespace VisitCounter
{
    /// <summary>Grid interface.</summary>
    public interface IGrid
    {
        /// <value>
        /// Property <c>MainRectangle</c> Envelope polygon.
        /// </value>
        public Polygon MainRectangle { get; set; }

        /// <value>
        /// Property <c>GridPitch</c> Grid pitch (min value is 1).
        /// </value>
        public double GridPitch { get; set; }

        /// <value>
        /// Property <c>GetCellCountX</c> Cell count on the x-axis.
        /// </value>
        public int GetCellCountX { get; }

        /// <value>
        /// Property <c>GetCellCountX</c> Cell count on the y-axis.
        /// </value>
        public int GetCellCountY { get; }

        /// <summary>Gets cell by indexes.</summary>
        /// <param name="j">j index (y-axis index).</param>
        /// <param name="j">i index (x-axis index).</param>
        /// <returns>ICell cell by indexes j and i</returns>
        public ICell GetCellByIndexes(int j, int i);

        /// <summary>Gets indexes by coordinate.</summary>
        /// <param name="coordinate">Coordinate to get indexes.</param>
        /// <param name="j">Output i index (x-axis index).</param>
        /// <param name="j">Output i index (x-axis index).</param>
        /// <returns>
        /// Success rate 
        /// (when values are set - true;
        /// when values are not set - false).
        /// </returns>
        public bool GetIndexesByCoordinate(Coordinate coordinate,
                                   out int j,
                                   out int i);

        /// <summary>Gets matrix of cell visits.</summary>
        /// <returns>Matrix of cell visits.</returns>
        public int[,] GetVisitMatrix();

        /// <summary>Add visits to cells.</summary>
        /// <param name="crdList">List of visited coordinates.</param>
        /// <param name="prevCellJ">
        /// ref j index of the last visited cell.
        /// </param>
        /// <param name="prevCellI">
        /// ref i index of the last visited cell.
        /// </param>
        public void AddVisitsToCells(List<Coordinate> crdList,
                                     ref int prevCellJ,
                                     ref int prevCellI);
    }
}
