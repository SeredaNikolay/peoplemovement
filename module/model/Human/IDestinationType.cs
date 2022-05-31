using System;

namespace VisitCounter
{
    /// <summary>DestinationType interface.</summary>
    public interface IDestinationType
    {
        /// <value>
        /// Property <c>Value</c> 
        /// Value of attraction to this destination type.
        /// </value>
        public double Value { get; set; }

        /// <value>
        /// Property <c>Name</c> 
        /// Name of this destination type.
        /// </value>
        public String Name { get; }

        /// <value>
        /// Property <c>TypePriority</c> 
        /// Priority of this destination type.
        /// </value>
        public int TypePriority { get; }
    }
}
