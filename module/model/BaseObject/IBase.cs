using System;
using System.Collections.Generic;
using System.Text;

namespace VisitCounter
{

    /// <summary>
    /// Parent interface for ICommandLine and IDataConverter.
    /// </summary>
    public interface IBase
    {
        /// <summary>
        /// Gets main rectangle's infrastructure in dictionary.
        /// </summary>
        /// <returns>
        /// Dictionary of infrastructure with
        /// string key ("polygon", "way", "point", "pointbuffer") and 
        /// IInfrastructure list value.
        /// </returns>
        public Dictionary<String, List<IInfrastructure>> GetInfrastDic();

        /// <summary>
        /// Gets main rectangle's people in dictionary.
        /// </summary>
        /// <returns>
        /// Dictionary of people with int human ID key and IHuman value.
        /// </returns>
        public Dictionary<int, IHuman> GetHumanDic();

        /// <summary>
        /// Gets main rectangle's grid.
        /// </summary>
        /// <returns>Gets main rectangle's IGrid.</returns>
        public IGrid GetGrid();
    }
}
