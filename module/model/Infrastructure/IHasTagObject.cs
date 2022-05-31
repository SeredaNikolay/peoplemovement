using System;
using System.Collections.Generic;
using System.Text;

namespace VisitCounter
{
    /// <summary>
    /// Interface for classes with Dictionary<String, bool> field.
    /// </summary>
    public interface IHasTagObject
    {
        /// <value>
        /// Property <c>HasTagDic</c> 
        /// Tag dictionary with key string tag and bool value.
        /// </value>
        public Dictionary<String, bool> HasTagDic { get; set; }
    }
}
