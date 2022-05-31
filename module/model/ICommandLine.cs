using System;
using System.Collections.Generic;
using System.Text;

namespace VisitCounter
{
    interface ICommandLine: IBase
    {
        public void AddOsmInfrastructure(bool add);
        public void AddInfrastructure(String geometryType);
        public void AddHuman();
        public void ShowInfrastInfo(String strRadius,
                                    String strX,
                                    String strY);
        public void ShowHumanInfo(String strRadius,
                                  String strX,
                                  String strY);
        public void CreateCSV();
    }
}
