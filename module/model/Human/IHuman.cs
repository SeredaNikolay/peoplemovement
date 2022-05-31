using System;
using System.Collections.Generic;
using System.Text;
using NetTopologySuite.Geometries;

namespace VisitCounter
{
    public interface IHuman : IID, IBaseObject, IHumanPriority
    {
        public void SetDestinationCoordinates(List<Coordinate> dstCrdList);
        public void Live(List<double> delegateArg);
    }
}
