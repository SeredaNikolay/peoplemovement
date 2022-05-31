using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace VisitCounter
{
    public interface ICustomizer
    {
        public CustomizerType GetCustomizerTypeByTags(
            Dictionary<String, bool> hasTagDic);
        public Geometry Customize(Geometry geometry,
                          CustomizerType objType);
    }
}
