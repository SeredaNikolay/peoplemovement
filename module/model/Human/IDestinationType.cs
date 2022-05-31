using System;

namespace VisitCounter
{
    public interface IDestinationType
    {
        double Value { get; set; }
        String Name { get; }
        int TypePriority { get; }
    }
}
