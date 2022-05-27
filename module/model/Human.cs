using System.Collections.Generic;
using NetTopologySuite.Geometries;


namespace VisitCounter
{
    class Human : HumanPriority
    {
        static int s_nextID = 1;
        public int ID { get; private set; }

        static Dictionary<int, Human> s_humanDic = new Dictionary<int, Human>();
        public static Dictionary<int, Human> HumanDic
        {
            get
            {
                return Human.s_humanDic;
            }
            set
            {
                Human.s_humanDic = value;
            }
        }
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
