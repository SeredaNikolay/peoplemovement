using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace VisitCounter
{
    /// <summary>Customizer interface.</summary>
    public interface ICustomizer
    {
        /// <summary>
        /// Gets CustomizerType to create customaized geometry.
        /// </summary>
        /// <param name="hasTagDic">
        /// Tag dictionary with key string tag and bool value.
        /// </param>
        /// <returns>
        /// CustomizerType to create customaized geometry.
        /// </returns>
        public CustomizerType GetCustomizerTypeByTags(
            Dictionary<String, bool> hasTagDic);

        /// <summary>Gets customized geometry.</summary>
        /// <param name="geometry">Geometry for customization.</param>
        /// <param name="objType">CustomizerType for customization.</param>
        /// <returns> Customized geometry.</returns>
        public Geometry Customize(Geometry geometry,
                          CustomizerType objType);
    }
}
