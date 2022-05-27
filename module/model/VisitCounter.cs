using System;
using OSMLSGlobalLibrary.Modules;
using System.Collections.Generic;
using CityDataExpansionModule;
using CityDataExpansionModule.OsmGeometries;
using NetTopologySuite.Geometries;
using System.IO;
using System.Text;

namespace VisitCounter
{
    public class VisitCounter : OSMLSModule
    {
        City _city = null;
        long _oldMs = 0;
        bool _readLineIsActive = false;
        int _iterationCount = 0;
        bool _updated = true;
        String _command;
        String _path = Directory.GetCurrentDirectory();
        String _dirSep = Path.DirectorySeparatorChar.ToString();
        String _filename = "visits.csv";
        protected override void Initialize()
        {
            this._city = new City(MapObjects, 
                GetModule<CityDataExpansion>().MapBorders);
        }
        
        public override void Update(long elapsedMilliseconds)
        {
            if (this._updated && elapsedMilliseconds - this._oldMs >= 500)
            {
                this._updated = false;
                foreach (KeyValuePair<int, Human> pair in Human.HumanDic)
                {
                    pair.Value.Live(null);
                    //this._mapObjects.Add(new Point(((Point)pair.Value.Geometry).Coordinate.X, ((Point)pair.Value.Geometry).Coordinate.Y));
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
                        using (FileStream fs = File.Create(this._path + this._dirSep + this._filename))
                        {
                            Grid grid = Grid.GridInstance;
                            for (int j = 0; j < grid.GetCellCountY; j++)
                            {
                                String csvSep = ",";
                                for (int i = 0; i < grid.GetCellCountX; i++)
                                {
                                    if (i == grid.GetCellCountX - 1)
                                    {
                                        csvSep = "\n";
                                    }
                                    int count = grid.GetCellByIndexes(j, i).VisitCount;
                                    byte[] bytes = new UTF8Encoding().GetBytes(count.ToString() + csvSep);
                                    fs.Write(bytes, 0, bytes.Length);
                                }
                            }
                        }
                        Console.WriteLine("CSV created");
                    }
                    this._readLineIsActive = false;
                }
            }
        }
    }
}
