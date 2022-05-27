using System;


namespace VisitCounter
{
    public interface IDestinationType
    {
        double Value { get; set; }
        String Name { get; }
        int TypePriority { get; }
    }
    public class DestinationType : IDestinationType
    {
        public String Name { get; private set; }
        public double Value { get; set; }
        public int TypePriority { get; private set; }
        public DoubleDelegate IterationCost { get; private set; }
        public DestinationType(String name, double startValue, int priority)
        {
            this.Name = name;
            this.Value = startValue;
            this.TypePriority = priority;
        }
    }
}
