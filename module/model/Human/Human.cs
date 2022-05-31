using System.Collections.Generic;
using NetTopologySuite.Geometries;


namespace VisitCounter
{
    class Human : HumanPriority, IHuman
    {
        static int s_nextID = 1;
        public int ID { get; private set; }
        public Human(Point point) : base(point)
        {
            this.ID = Human.s_nextID;
            Human.s_nextID++;
        }
        public void SetDestinationCoordinates(List<Coordinate> dstCrdList)
        {
            this.DstCoordinates = dstCrdList;
        }
        public void Live(List<double> delegateArg)
        {
            this.Move();
        }
    }
}
