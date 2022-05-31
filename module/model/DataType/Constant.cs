using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace VisitCounter
{
    public static class Constant
    {
        public const double meter = 1;
        public const double epsilon = 0.0000000001;
        public const long epsPowMinus1 = (long)(1 / 0.00000000001);
        public const double humanRadius = 0.5 * meter;
        public const String tagOfAllTags = "__AllTags__";
    }
}
