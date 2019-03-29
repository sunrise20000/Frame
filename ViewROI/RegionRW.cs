using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using HalconDotNet;
using FileRW;

namespace ViewROI
{
    public class RegionRW
    {
        string path = string.Empty;
        List<HObject> region = new List<HObject>();
        List<List<double>> regionDatasDouble;
        FileRW.Xml xml;
        List<List<string>> elements;
        List<string> element;
            //= { 
                            //  { "Row1", "Column1", "Row2", "Column2" }
                            //, { "Row", "Column" ,"Phi","Length1","Length2"}
                            //, {"Row","Column","Radius"}
                            //    ,};

        public RegionRW(string pathXml)
        {
            path = pathXml;
            xml = new Xml(path);
            
        }

        public void WriteRegionData()
        {
            int rowCount = regionDatasDouble.Count;
            
            for (int row = 0; row < rowCount; row++)
            {
                xml.Insert("Config", "Region", "name", row.ToString());
                List<double> regionData = regionDatasDouble[row];
                int columnCount = regionData.Count;
                for (int column = 0; column < columnCount; column++)
                {
                    xml.Insert("Config/Region[@name='" + row.ToString() + "']"
                        , "Para"+column.ToString(), "", regionDatasDouble[row][column].ToString());
                }
            }
        }

        public List<HObject> ReadRegionData()
        {
            HObject regionResult=new HObject();
            region.Clear();
            List<List<string>> regionDatasStr = xml.ReadAllChildallValue("Config");
            regionDatasDouble=new List<List<double>>();
            foreach (List<string> regionData in regionDatasStr)
            {
                regionResult = GetRegion(regionData);
                region.Add(regionResult.CopyObj(1, -1));
                List<double> regionDataDouble=new List<double>();
                foreach(string s in regionData)
                {
                    regionDataDouble.Add(double.Parse(s));
                }
                regionDatasDouble.Add(regionDataDouble);
            }
            return region;
        }
        private HObject GetRegion(List<string> regionData)
        {
            HObject regionResult = new HObject();
            switch (regionData[0])
            { 
                case "1":   //矩形
                    HOperatorSet.GenRectangle1(out regionResult, double.Parse(regionData[1]), double.Parse(regionData[2])
                    , double.Parse(regionData[3]), double.Parse(regionData[4]));
                    break;
                case "2":   //旋转矩形
                    HOperatorSet.GenRectangle2(out regionResult, double.Parse(regionData[1]), double.Parse(regionData[2])
                    , double.Parse(regionData[3]), double.Parse(regionData[4]), double.Parse(regionData[5]));
                    break;
                case "3":   //圆
                    HOperatorSet.GenCircle(out regionResult, double.Parse(regionData[1]), double.Parse(regionData[2])
                    , double.Parse(regionData[3]));
                    break;
                case "4":   //椭圆
                    HOperatorSet.GenEllipse(out regionResult, double.Parse(regionData[1]), double.Parse(regionData[2])
                    , double.Parse(regionData[3]), double.Parse(regionData[4]), double.Parse(regionData[5]));
                    break;
                case "5":   //多边形
                    break;
                default: break;

            }
            return regionResult;
        }
        public string Path
        {
            set { path = value; }
        }
        public List<List<double>> RegionDatas
        {
            get { return regionDatasDouble; }
            set { regionDatasDouble = value; }
        }
    }
}
