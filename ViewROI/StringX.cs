using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using HalconDotNet;

namespace ViewROI
{
    /// <summary>
    /// 显示字符串
    /// </summary>
    public class StringX
    {
        public string str = string.Empty;
        public int row = 0;
        public int column = 0;
        public Color color;
        public int size = 16;
        public string font = "mono";
        public bool bold = true;
        public bool slant = false;

        public StringX(int _size, bool _bold, bool _slant, string _font = "mono")
        {
            size = _size;
            font = _font;
            bold = _bold;
            slant = _slant;
        }

        public void SetString(string _str, int _row, int _column, Color _color)
        {
            str = _str;
            row = _row;
            column = _column;
            color = _color;
        }
        public int Size
        {
            set { size = value; }
        }
        public string Font
        {
            set { font = value; }
        }
        public bool Bold
        {
            set { bold = value; }
        }
        public bool Slant
        {
            set { slant = value; }
        }
    }
}
