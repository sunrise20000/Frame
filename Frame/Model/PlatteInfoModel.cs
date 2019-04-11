using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frame.Model
{
    
    public class PlatteInfoModel
    {
        private double[,] Points;
        private PlatteInfoModel() { }
        public PlatteInfoModel(string Name, int Row, int Col, PointModel StartPoint, PointModel EndPoint)
        {
            this.Row = Row;
            this.Col = Col;
            this.Name = Name;
            Points = GetAveragePoints(Row, Col, StartPoint.X, StartPoint.Y, EndPoint.X, EndPoint.Y, EndPoint.Z);
        }
        public string Name { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }

        public PointModel this[int index]
        {
            get {
                return new PointModel(Points[index,0], Points[index, 1], Points[index, 2]);
            }
        }
        private double[,] GetAveragePoints(int rowcount, int columncount, double x1, double y1, double x2, double y2, double z)
        {
            double[,] points = new double[rowcount * columncount, 3];
            double averageRow = (x2 - x1) / (rowcount - 1);
            double averageColumn = (y2 - y1) / (columncount - 1);
            for (int row = 0; row < rowcount; row++)
            {
                for (int column = 0; column < columncount; column++)
                {
                    points[row * columncount + column, 0] = x1 + averageRow * row;
                    points[row * columncount + column, 1] = y1 + averageColumn * column;
                    points[row * columncount + column, 2] = z;
                }
            }
            return points;
        }

    }
}
