using System;

namespace VisitCounter
{
    public interface IDestinationType
    {
        public double Value { get; set; }
        public String Name { get; }
        public int TypePriority { get; }
    }
}
