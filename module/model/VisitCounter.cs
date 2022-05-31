using System;
using OSMLSGlobalLibrary.Modules;
using System.Collections.Generic;
using CityDataExpansionModule;
using NetTopologySuite.Geometries;
using VC = VisitCounter;

namespace Test
{
    public class VisitCounter : OSMLSModule
    {
        long _oldMs = 0;
        bool _readLineIsActive = false;
        int _iterationCount = 0;
        bool _updated = true;
        String _command;
        VC.IGrid _grid;
        VC.ICommandLine _commandLine;
        protected override void Initialize()
        {
            Polygon mainRectangle = GetModule<CityDataExpansion>()
                .MapBorders;
            this._commandLine = new VC.CommandLine(MapObjects,
                                                        mainRectangle);
            this._grid = this._commandLine.GetGrid();
            VC.IHuman human;
            if (this._commandLine.GetHumanDic().TryGetValue(1, out human))
            {
                human.SetDestinationCoordinates(
                    new List<Coordinate>()
                    {
                        new Coordinate(4194240, 7516324)
                    });
            }
            if (this._commandLine.GetHumanDic().TryGetValue(2, out human))
            {
                human.SetDestinationCoordinates(
                    new List<Coordinate>()
                    {
                        new Coordinate(4194302, 7516220),
                        new Coordinate(4194296, 7516230)
                    });
            }
        }
        public override void Update(long elapsedMilliseconds)
        {
            if (this._updated && elapsedMilliseconds - this._oldMs >= 500)
            {
                int j, i;
                this._updated = false;
                foreach (KeyValuePair<int, VC.IHuman> pair 
                    in this._commandLine.GetHumanDic())
                {
                    pair.Value.Live(null);
                    j = pair.Value.PrevCellJ;
                    i = pair.Value.PrevCellI;
                    this._grid.AddVisitsToCells(pair.Value.VisitedCrdList,
                                                ref j, ref i);
                    pair.Value.PrevCellJ = j;
                    pair.Value.PrevCellI = i;
                }
                this._iterationCount++;
                this._updated = true;
                this._oldMs = elapsedMilliseconds;
                if (!this._readLineIsActive)
                {
                    this._readLineIsActive = true;
                    this._command = Console.ReadLine();
                    if (this._command == "csv")
                    {
                        this._commandLine.CreateCSV();
                    }
                    this._readLineIsActive = false;
                }
            }
        }
    }
}
