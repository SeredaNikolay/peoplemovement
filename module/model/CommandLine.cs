using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;
using System.Linq;

namespace VisitCounter
{
    public class CommandLine: ICommandLine
    {
        IDataConverter _dataConverter;
        bool _cancel;
        public CommandLine(
            OSMLSGlobalLibrary.IInheritanceTreeCollection<Geometry>
                mapObjects,
            Polygon mainRectangle)
        {
            String command;
            String[] commandPart;
            this._dataConverter = new DataConverter(
                new DataProvider(mapObjects), 
                mainRectangle);

            Console.WriteLine("Add osm objects: ");
            command = Console.ReadLine();
            this.AddOsmInfrastructure(command == "yes");
            do
            {
                command = Console.ReadLine();
                commandPart = command.Split(' ');
                if (command != "start")
                {
                    if (commandPart.Length == 2)
                    {
                        this.AddCommand(commandPart);
                    }
                    else if (commandPart.Length == 5)
                    {
                        this.ShowInfoCommand(commandPart);
                    }
                }
            } while (command != "start");
        }
        void AddCommand(String[] commandPart)
        {
            this._cancel = false;
            if (commandPart[0] == "add")
            {
                if (commandPart[1] == "human")
                {
                    this.AddHuman();
                }
                else
                {
                    this.AddInfrastructure(commandPart[1]);
                }
            }
        }
        void ShowInfoCommand(String[] commandPart)
        {
            if (commandPart[0] == "infobycircle")
            {
                if (commandPart[1] == "infrast")
                {
                    this.ShowInfrastInfo(commandPart[2],
                                         commandPart[3], 
                                         commandPart[4]);
                }
                else if (commandPart[1] == "human")
                {
                    this.ShowHumanInfo(commandPart[2],
                                       commandPart[3],
                                       commandPart[4]);
                }
                else
                {
                    Console.WriteLine("Wrong infobycircle argument");
                }
            }
        }
        Coordinate[] StringToCoordinateArr(String strCoordinates)
        {
            List<Coordinate> coordinates = new List<Coordinate>();
            String[] crdStrPair = null;
            String[] strCrd = null;
            double x, y;
            bool err = false;
            if (strCoordinates != null && strCoordinates.Length != 0)
            {
                crdStrPair = strCoordinates.Split(new string[] { ", " },
                                                  StringSplitOptions.None);
                foreach (String strPair in crdStrPair)
                {
                    strCrd = strPair.Split(' ');
                    if (strCrd.Length == 2)
                    {
                        try
                        {
                            x = Convert.ToDouble(strCrd[0]);
                            y = Convert.ToDouble(strCrd[1]);
                            coordinates.Add(new Coordinate(x, y));
                        }
                        catch (Exception e)
                        {
                            err = true;
                            Console.WriteLine("Wrong coordinate");
                        }
                    }
                    else
                    {
                        err = true;
                        Console.WriteLine("Wrong coordinates "
                            +"in coordinate substring");
                    }
                }
            }
            else
            {
                err = true;
                Console.WriteLine("Wrong coordinate string");
            }
            if (err)
                return null;
            return coordinates.ToArray();
        }
        List<String> StringToTagList(String tagsStr, String geomType)
        {
            bool noErr = true;
            List<String> tagList = new List<String>();
            if (tagsStr != null && tagsStr.Length != 0)
            {
                tagList = tagsStr.Split(' ').ToList<String>();
                List<String> infrastTagList = this._dataConverter
                    .GetInfrastTagListByGeomType(geomType);
                foreach (String tag in tagList)
                {
                    if (!infrastTagList.Contains(tag))
                    {
                        noErr = false;
                        Console.WriteLine("Wrong tag list");
                        break;
                    }
                }
            }
            else
            {
                noErr = false;
                Console.WriteLine("Wrong tag string");
            }
            if (noErr)
                return tagList;
            return null;
        }
        bool StringToDouble(String doub, out double val, double? lowLimit = null)
        {
            try
            {
                val = Convert.ToDouble(doub);
                if (lowLimit == null || val >= lowLimit)
                    return true;
                Console.WriteLine("Too small double");
            }
            catch (Exception e)
            {
                Console.WriteLine("Wrong double value");
            }
            val = -1.0;
            return false;
        }
        List<String> GetTagList(String geomType)
        {
            bool repeat = true;
            String tagString;
            List<String> tagList = null;
            this._cancel = false;
            do
            {
                Console.WriteLine("Input tags:");
                tagString = Console.ReadLine();
                if (tagString != "cancel")
                {
                    tagList = this.StringToTagList(tagString, geomType);
                    if(tagList != null)
                    {
                        repeat = false;
                    }
                }
                else
                {
                    this._cancel = true;
                    repeat = false;
                }
            } while (repeat);
            return tagList;
        }
        Coordinate[] GetCoordinateArr()
        {
            bool repeat = true;
            String coordinates;
            Coordinate[] coordinateArr = null;
            List<String> tagList = new List<String>();
            this._cancel = false;
            do
            {
                Console.WriteLine("Input coordinates:");
                coordinates = Console.ReadLine();
                if (coordinates != "cancel")
                {
                    coordinateArr = this.StringToCoordinateArr(
                        coordinates);
                    if (coordinateArr != null)
                    {
                        repeat = false;
                    }
                    else
                    {
                        Console.WriteLine("Coordinate array is null");
                    }
                }
                else
                {
                    this._cancel = true;
                    repeat = false;
                }
            } while (repeat);
            return coordinateArr;
        }
        double GetRadius()
        {
            bool repeat = true;
            String radiusStr;
            double doub = 0.0;
            bool isOk;
            this._cancel = false;
            do
            {
                Console.WriteLine("Input radius:");
                radiusStr = Console.ReadLine();
                if (radiusStr != "cancel")
                {
                    isOk = this.StringToDouble(radiusStr, out doub, 0.1);
                    if(isOk)
                    {
                        repeat = false;
                    }
                    else
                    {
                        Console.WriteLine("Wrong double string");
                    }
                }
                else
                {
                    this._cancel = true;
                    repeat = false;
                }
            } while (repeat);
            return doub;
        }
        public void AddOsmInfrastructure(bool add)
        {
            this._dataConverter.AddOsmInfrastructure(add);
        }
        public void AddInfrastructure(String geometryType)
        {
            Coordinate[] crdArr = this.GetCoordinateArr();
            if (!this._cancel)
            {
                List<String> tagList = this.GetTagList(geometryType);
                if (!this._cancel)
                {
                    this._dataConverter.AddInfrastructure(geometryType,
                                                          crdArr,
                                                          tagList);
                }
            }
        }
        public void AddHuman()
        {
            Coordinate[] crdArr = this.GetCoordinateArr();
            if (!this._cancel)
            {
                this._dataConverter.AddHuman(crdArr);
            }
        }
        public void ShowInfrastInfo(String strRadius,
                                    String strX,
                                    String strY)
        {
            double x, y, radius;
            bool noErr = true;
            noErr &= this.StringToDouble(strX, out x, null);
            noErr &= this.StringToDouble(strY, out y, null);
            noErr &= this.StringToDouble(strRadius, out radius, 0.1);
            if(noErr)
            {
                Coordinate crd = new Coordinate(x, y);
                List<InfrastructureInfo> infrastInfoList =
                    new List<InfrastructureInfo>();
                this._dataConverter.GetInfrastInfoInRadius(
                    infrastInfoList,
                    crd,
                    radius);
                this._dataConverter.OutputInfrastInfoToConsole(
                    infrastInfoList);
            }
            else
            {
                Console.WriteLine("Wrong infobycircle argument (infrast)");
            }
        }
        public void ShowHumanInfo(String strRadius,
                                  String strX, 
                                  String strY)
        {
            double x, y, radius;
            bool noErr = true;
            noErr &= this.StringToDouble(strX, out x, null);
            noErr &= this.StringToDouble(strY, out y, null);
            noErr &= this.StringToDouble(strRadius, out radius, 0.1);
            if (noErr)
            {
                Coordinate crd = new Coordinate(x, y);
                List<HumanInfo> humanInfoList =
                    new List<HumanInfo>();
                this._dataConverter.GetHumanInfoInRadius(
                    humanInfoList,
                    crd,
                    radius);
                this._dataConverter.OutputHumanInfoToConsole(
                    humanInfoList);
            }
            else
            {
                Console.WriteLine("Wrong infobycircle argument (human)");
            }
        }
        public void CreateCSV()
        {
            this._dataConverter.CreateCSV();
        }
        public Dictionary<String, List<IInfrastructure>> GetInfrastDic()
        {
            return this._dataConverter.GetInfrastDic();
        }
        public Dictionary<int, IHuman> GetHumanDic()
        {
            return this._dataConverter.GetHumanDic();
        }
        public IGrid GetGrid()
        {
            return this._dataConverter.GetGrid();
        }
    }
}
