using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HalconDotNet;
using ViewROI;
using System.Collections;
using System.IO;
namespace HalWindow
{
    [ToolboxBitmap(typeof(HDisplay), "HDisplay.ico")]
    public partial class HDisplay : UserControl
    {
        private HObject m_hImg;
        private List<RegionX> m_regionList;
        private List<StringX> m_hStrList;        
        List<HObject> searchRegionList;
        List<HObject> trainRegionList;
        public ROIController roiController;
        public HWndCtrl viewController;
        private bool isCancelImageMove = false;
       
        private IntPtr m_Windowhandle;
        public HDisplay()
        {
            InitializeComponent();
            searchRegionList = new List<HObject>();
            trainRegionList = new List<HObject>();
            viewController = new HWndCtrl(hWindowControl);
            roiController = new ROIController();
            viewController.useROIController(roiController);
            //减少控件的闪烁 
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, false);
            hWindowControl.BackColor = Color.Gray;
            viewController.MouseMoveEvent += viewController_MouseMoveEvent;
            hWindowControl.SizeChanged += new System.EventHandler(this.hWindowControl_SizeChanged);
            m_Windowhandle = hWindowControl.HalconID;
               
        }
        public HObject  DumpWindowImage
        {
            get
            {
                HObject _dumpwindowimage = new HObject();
                HOperatorSet.DumpWindowImage(out _dumpwindowimage, m_Windowhandle);
                return _dumpwindowimage;
            }
        }
        public void AddRegion(string searchRegionShape,bool isSearchRegion)
        {
            if (searchRegionShape == "矩形")
            {
                roiController.setROIShape(new ROIRectangle1(isSearchRegion), 200, 200);
            }
            else if (searchRegionShape == "旋转矩形")
            {
                roiController.setROIShape(new ROIRectangle2(isSearchRegion), 200, 200);
            }
            else if (searchRegionShape == "圆")
            {
                roiController.setROIShape(new ROICircle(isSearchRegion), 200, 200);
            }
            else if (searchRegionShape == "线")
            {
                roiController.setROIShape(new ROILine(isSearchRegion), 200, 200);
            }
            else if (searchRegionShape == "圆环")
            {
                roiController.setROIShape(new ROICircularArc(isSearchRegion), 200, 200);
            }
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            roiController.removeActive();
        }

        private void 适应大小ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            viewController.AutoZoom();
        }

        private void hWindowControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right&&(!isCancelImageMove))
            {
                Point p = new Point();
                p.X = (int)e.X;
                p.Y = (int)e.Y;
                if (roiController.getActiveROIIdx() != -1)
                {
                    cms_ROI.Show(this, p);
                }
                else
                {
                    cms_Set.Show(this, p);
                }
            }
        }
        public List<HObject> GetSearchRegions()
        { 
            searchRegionList.Clear();
            foreach (ROI r in roiController.ROIList)
            {
                if (r.GetIsSearchRegion())
                {
                    searchRegionList.Add(r.getRegion());
                }
            }
            return searchRegionList;
        }
        public List<HObject> GetTrainRegions()
        {
            trainRegionList.Clear();
            foreach (ROI r in roiController.ROIList)
            {
                if (!r.GetIsSearchRegion())
                {
                    trainRegionList.Add(r.getRegion());
                }
            }
            return trainRegionList;
        }

        public ArrayList GetROIList()
        {
            return roiController.ROIList;
        }
        public List<HTuple> GetRegionDatas(bool isSearchRegion)
        {
            List<HTuple> datas = new List<HTuple>();
            foreach (ROI r in roiController.ROIList)
            {
                if (r.GetIsSearchRegion() == isSearchRegion)
                {
                    datas.Add(r.getModelData());
                }
            }
            return datas;
        }
        public void SetROIList(ArrayList ROIList)
        {
            if (ROIList.Count > 0)
            {
                roiController.ROIList = ROIList;
                viewController.repaint();
            }
        }

        public void CleraRegion()
        {
            roiController.reset();
        }
        public void SetShowROI(bool isShow)
        {
            roiController.SetShowROI(isShow);
        }
        public bool IsCancelImageMove
        {
            get 
            {
                isCancelImageMove = viewController.IsCancelImageMove;
                return isCancelImageMove; 
            }
            set 
            {
                isCancelImageMove = value;
                viewController.IsCancelImageMove = isCancelImageMove; 
            }
        }
        public HObject HImageX
        {
            get { return m_hImg; }
            set
            {
                m_hImg = value;
                if (m_hImg != null)
                {
                //    if (m_hImg.IsInitialized())
                //    {
                        viewController.clearList();
                        viewController.AddHObject(m_hImg, "", true);
                        viewController.repaint();
                        HTuple width = 0, height = 0;
                        try
                        {
                            HOperatorSet.GetImageSize(m_hImg, out width, out height);
                        }
                        catch { }
                        ts_ImageSize.Text = "Image:" + width + "×" + height + "byte";
                //    }
                }
            }
        }    
        public List<RegionX> HRegionXList
        {
            get { return m_regionList; }
            set 
            { 
                m_regionList = value;
                if (m_regionList != null)
                {
                    int n = 0;
                    foreach (RegionX rx in m_regionList)
                    {
                        try
                        {
                            viewController.AddHObject(rx.Region, rx.Color, false);
                        }
                        catch { }
                    }
                    viewController.repaint();
                    //HRegionXList.Clear();
                }
            }
        }
        public List<HObject> HSeaarchRegionXList
        {
           // get { return searchRegionList; }
            set
            {
                searchRegionList = value;
                if (searchRegionList != null)
                {

                    foreach (HObject rx in searchRegionList)
                    {
                        try
                        {
                            viewController.AddHObject(rx, "blue", false);
                        }
                        catch { }
                    }
                    viewController.repaint();
                    //HRegionXList.Clear();
                }
            }
        }
        public List<StringX> HStringXList
        {
            get { return m_hStrList; }
            set
            {
                m_hStrList = value;
                if (m_hStrList != null)
                {
                    foreach (StringX sx in m_hStrList)
                    {
                        viewController.AddString(sx);
                    }
                    viewController.repaint();
                }
            }
        }
      
        private void viewController_MouseMoveEvent(object sender, MouseMove e)
        {
            ts_GrayValue.Text = e.Gray.ToString();
            ts_Position.Text = e.Y.ToString() + "×" + e.X.ToString();
        }

        /// <summary>
        /// hWindowControl大小改变触发事件，用于最小化、最大化或拖动大小时持续显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hWindowControl_SizeChanged(object sender, EventArgs e)
        {
            viewController.AutoZoom();
        }

        public event EventHandler<MouseDownPointEvent> MouseRightDownPointEvent
        {
            add { viewController.MouseDownPointEvent += value; }
            remove { viewController.MouseDownPointEvent -= value; }

        }
    }
    [Serializable]
    public class RegionX
    {
        private HObject _region;
        private String _color;
        private bool isSearchRegion;
        private bool isShowSearchRegionNumber;

        public RegionX(HObject region, String color)
        {
            _region = region;
            _color = color;
            isSearchRegion = false;
        }

        public HObject Region
        {
            get { return _region; }
            set { _region = value; }
        }

        public String Color
        {
            get { return _color; }
            set { _color = value; }
        }

        public bool IsSearchRegion
        {
            get { return isSearchRegion; }
            set { isSearchRegion = value; }
        }
        public bool IsShowSearchRegionNumber
        {
            get { return isShowSearchRegionNumber; }
            set { isShowSearchRegionNumber = value; }
        }
    }
    [Serializable]
    //public class StringX
    //{
    //    public string str = string.Empty;
    //    public int row = 0;
    //    public int column = 0;
    //    public Color color;

    //    public StringX()
    //    { }

    //    public StringX(string _str, int _row, int _column, Color _color)
    //    {
    //        str = _str;
    //        row = _row;
    //        column = _column;
    //        color = _color;
    //    }

    //}

    public class HDisplayMouseDown : EventArgs
    {
        public double X = 0;
        public double Y = 0;
    }

}
