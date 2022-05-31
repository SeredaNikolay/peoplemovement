using System;
using System.Collections.Generic;
using System.Text;
using NetTopologySuite.Geometries;

namespace VisitCounter
{
    /// <summary>Human interface.</summary>
    public interface IHuman : IID, IBaseObject, IHumanPriority
    {
        /// <summary>Sets list of destination coordinates.</summary>
        /// <param name="dstCrdList">
        /// List of destination coordinates.
        /// </param>
        public void SetDestinationCoordinates(List<Coordinate> dstCrdList);

        /// <summary>Performs an iteration for human.</summary>
        /// <param name="delegateArg">
        /// List of double arguments to perform an iteration.
        /// </param>
        public void Live(List<double> delegateArg);
    }
}
