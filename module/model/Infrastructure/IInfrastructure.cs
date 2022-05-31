

namespace VisitCounter
{
    interface IInfrastructure : IHasTagObject, IID, IBaseObject
    {
        public CustomizerType CustomType { get; set; }
        public void SetBufferedGeometry(double additionalRadius,
                                        bool needsPolygon);
    }
}
