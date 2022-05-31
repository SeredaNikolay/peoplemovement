using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;
using System.IO;
using System.Text;

namespace VisitCounter
{
    class Outputter: IOutputter
    {
        OSMLSGlobalLibrary.IInheritanceTreeCollection<Geometry> 
            _mapObjects;
        String _path;
        String _dirSep;
        String _filename;
        public Outputter(
            OSMLSGlobalLibrary.IInheritanceTreeCollection<Geometry>
                mapObjects)
        {
            this._mapObjects = mapObjects;
            this._path = Directory.GetCurrentDirectory();
            this._dirSep = Path.DirectorySeparatorChar.ToString();
            this._filename = "visits.csv";
        }
        public void AddInfrastToMap(
            Dictionary<String,List<Geometry>> infrastDic)
        {
            foreach (KeyValuePair<String, List<Geometry>> pair
                in infrastDic)
            {
                foreach (Geometry geometry in pair.Value)
                {
                    if (geometry != null && !geometry.IsEmpty)
                    {
                        this._mapObjects.Add(geometry);
                    }
                    else
                    {
                        Console.WriteLine("Wrong geometry in infrast dic");
                    }
                }
            }
        }
        public void AddPeopleToMap(List<Point> humanList)
        {
            foreach (Point geometry in humanList)
            {
                if (geometry != null && !geometry.IsEmpty)
                {
                    this._mapObjects.Add(geometry);
                }
                else
                {
                    Console.WriteLine("Wrong geometry in human list");
                }
            }
        }
        public void AddInfrastObjToMap(Polygon polygon, Point point)
        {
            if (polygon != null && !polygon.IsEmpty)
            {
                this._mapObjects.Add(polygon);
            }
            else
            {
                Console.WriteLine("Wrong infrast geometry");
            }
            if(point != null && !point.IsEmpty)
            {
                this._mapObjects.Add(point);
            }
        }
        public void AddHumanObjToMap(Point point)
        {
            if (point != null && !point.IsEmpty)
            {
                this._mapObjects.Add(point);
            }
            else
            {
                Console.WriteLine("Wrong human geometry");
            }
        }
        public void CreateCSV(int[,] matrix)
        {
            int cellCountY = matrix.GetLength(0);
            int cellCountX = matrix.GetLength(1);
            String fullName = this._path + this._dirSep + this._filename;
            using (FileStream fs = File.Create(fullName))
            {
                for (int j = 0; j < cellCountY; j++)
                {
                    String csvSep = ",";
                    for (int i = 0; i < cellCountX; i++)
                    {
                        if (i == cellCountX - 1)
                        {
                            csvSep = "\n";
                        }
                        int count = matrix[j, i];
                        byte[] bytes = new UTF8Encoding().GetBytes(
                            count.ToString() + csvSep);
                        fs.Write(bytes, 0, bytes.Length);
                    }
                }
            }
        }
        public void OutputInfrastInfoToConsole(
            List<InfrastructureInfo> infrastInfoList)
        {
            if (infrastInfoList == null || infrastInfoList.Count == 0)
            {
                Console.WriteLine("There is no infrastructure object");
            }
            else
            {
                foreach (InfrastructureInfo infrastInf in infrastInfoList)
                {
                    Console.WriteLine("=================================");
                    Console.WriteLine("Type: " + infrastInf.Type);
                    Console.WriteLine("ID: " + infrastInf.ID);
                    Console.WriteLine("Tags: " 
                        + String.Join(", ", infrastInf.TagList));
                }
            }
        }
        public void OutputHumanInfoToConsole(List<HumanInfo> humanInfoList)
        {
            if (humanInfoList == null || humanInfoList.Count == 0)
            {
                Console.WriteLine("There are not people");
            }
            else
            {
                foreach(HumanInfo humInfo in humanInfoList)
                {
                    Console.WriteLine("=================================");
                    Console.WriteLine("ID: " + humInfo.ID);
                    foreach (DestinationType dstType 
                        in humInfo.DstTypeList)
                    {
                        Console.WriteLine("--------------------");
                        Console.WriteLine("Name: " + dstType.Name);
                        Console.WriteLine("Value: " + dstType.Value);
                        Console.WriteLine("TypePriority: " 
                            + dstType.TypePriority);
                    }
                }
            }
        }
    }
}
