using System;
using System.Collections;
using HalconDotNet;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


namespace ViewROI
{
	public delegate void IconicDelegate(int val);
	public delegate void FuncDelegate();
    
	/// <summary>
	/// This class works as a wrapper class for the HALCON window
	/// HWindow. HWndCtrl is in charge of the visualization.
	/// You can move and zoom the visible image part by using GUI component 
	/// inputs or with the mouse. The class HWndCtrl uses a graphics stack 
	/// to manage the iconic objects for the display. Each object is linked 
	/// to a graphical context, which determines how the object is to be drawn.
	/// The context can be changed by calling changeGraphicSettings().
	/// The graphical "modes" are defined by the class GraphicsContext and 
	/// map most of the dev_set_* operators provided in HDevelop.
	/// </summary>
	public partial class HWndCtrl
	{
		private bool   mousePressed = false;
        private double startX = 0, startY = 0;

		/// <summary>HALCON window</summary>
        private HWindowControl viewPort;

		/// <summary>
		/// Instance of ROIController, which manages ROI interaction
		/// </summary>
		private ROIController roiManager;


		/* Basic parameters, like dimension of window and displayed image part */
		private int   windowWidth;
		private int   windowHeight;
		private int   imageWidth;
		private int   imageHeight;

		private int[] CompRangeX;
		private int[] CompRangeY;

		private double stepSizeX, stepSizeY;

		/* Image coordinates, which describe the image part that is displayed  
		   in the HALCON window */
		private double ImgRow1, ImgCol1, ImgRow2, ImgCol2;

		/// <summary>Error message when an exception is thrown</summary>
		public string  exceptionText = "";

		/// <summary> 
		/// List of HALCON objects to be drawn into the HALCON window. 
		/// The list shouldn't contain more than MAXNUMOBJLIST objects, 
		/// otherwise the first entry is removed from the list.
		/// </summary>
		private ArrayList HObjList;

        private ArrayList StrList;
        private bool isCancelImageMove = false;
		/// <summary>
		/// Instance that describes the graphical context for the
		/// HALCON window. According on the graphical settings
		/// attached to each HALCON object, this graphical context list 
		/// is updated constantly.
		/// </summary>
		private GraphicsContext	mGC;

        public event EventHandler<MouseMove> MouseMoveEvent;
        public event EventHandler<MouseDownPointEvent> MouseDownPointEvent;
		/// <summary> 
		/// Initializes the image dimension, mouse delegation, and the 
		/// graphical context setup of the instance.
		/// </summary>
		/// <param name="view"> HALCON window </param>
		public HWndCtrl(HWindowControl view)
		{
			viewPort = view;
			windowWidth = viewPort.Size.Width;
			windowHeight = viewPort.Size.Height;

			/*default*/
			CompRangeX = new int[] { 0, 100 };
			CompRangeY = new int[] { 0, 100 };

			viewPort.HMouseUp += new HalconDotNet.HMouseEventHandler(this.mouseUp);
			viewPort.HMouseMove += new HalconDotNet.HMouseEventHandler(this.mouseMoved);
            viewPort.HMouseWheel += new HalconDotNet.HMouseEventHandler(this.mouseWheel);
            viewPort.HMouseDown += new HalconDotNet.HMouseEventHandler(this.MouseDown);
			// graphical stack 
			HObjList = new ArrayList();
            StrList = new ArrayList();
			mGC = new GraphicsContext();
            
		}


		/// <summary>
		/// Registers an instance of an ROIController with this window 
		/// controller (and vice versa).
		/// </summary>
		/// <param name="rC"> 
		/// Controller that manages interactive ROIs for the HALCON window 
		/// </param>
        public void useROIController(ROIController rC)
        {
            roiManager = rC;
            rC.setViewController(this);
        }

		/// <summary>
		/// Adjust window settings by the values supplied for the left 
		/// upper corner and the right lower corner
		/// </summary>
		/// <param name="r1">y coordinate of left upper corner</param>
		/// <param name="c1">x coordinate of left upper corner</param>
		/// <param name="r2">y coordinate of right lower corner</param>
		/// <param name="c2">x coordinate of right lower corner</param>
		private void setImagePart(int r1, int c1, int r2, int c2)
		{
            double ratio_win = (double)viewPort.WindowSize.Width / (double)viewPort.WindowSize.Height;
            double ratio_img = (double)r2 / (double)c2;

            if (ratio_win >= ratio_img)
            {
                ImgRow1 = 0;
                ImgRow2 = c2 - 1;
                ImgCol1 = (int)(-r2 * (ratio_win / ratio_img - 1d) / 2d);
                ImgCol2 = (int)(r2 - ImgCol1);
            }
            else
            {
                ImgRow1 = (int)(-c2 * (ratio_img / ratio_win - 1d) / 2d);
                ImgRow2 = (int)(c2 - ImgRow1);
                ImgCol1 = 0;
                ImgCol2 = r2 - 1;
            }

            //                  按照原始图像尺寸，X：Y为1显示图像

            System.Drawing.Rectangle rect = viewPort.ImagePart;
            rect.X = (int)ImgCol1;
            rect.Y = (int)ImgRow1;
            rect.Width = (int)(ImgCol2 - ImgCol1);
            rect.Height = (int)(ImgRow2 - ImgRow1);
            viewPort.ImagePart = rect;
            viewPort.HalconWindow.SetPart((int)ImgRow1, (int)ImgCol1, (int)ImgRow2, (int)ImgCol2);
		}

		/*******************************************************************/
        //private void exceptionGC(string message)
        //{
        //    exceptionText = message;
        //    NotifyIconObserver(ERR_DEFINING_GC);
        //}


		/****************************************************************************/
		/*                          graphical element                               */
		/****************************************************************************/
		private void ScaleImage(double x, double y, double scale)
		{
			double lengthC, lengthR;
			double percentC, percentR;
			int    lenC, lenR;

			percentC = (x - ImgCol1) / (ImgCol2 - ImgCol1);
			percentR = (y - ImgRow1) / (ImgRow2 - ImgRow1);

			lengthC = (ImgCol2 - ImgCol1) * scale;
			lengthR = (ImgRow2 - ImgRow1) * scale;

			ImgCol1 = x - lengthC * percentC;
			ImgCol2 = x + lengthC * (1 - percentC);

			ImgRow1 = y - lengthR * percentR;
			ImgRow2 = y + lengthR * (1 - percentR);

			lenC = (int)Math.Round(lengthC);
			lenR = (int)Math.Round(lengthR);

			System.Drawing.Rectangle rect = viewPort.ImagePart;
			rect.X = (int)Math.Round(ImgCol1);
			rect.Y = (int)Math.Round(ImgRow1);
			rect.Width = (lenC > 0) ? lenC : 1;
			rect.Height = (lenR > 0) ? lenR : 1;
			viewPort.ImagePart = rect;
			repaint();
		}

		/*******************************************************************/
		private void moveImage(double motionX, double motionY)
		{
			ImgRow1 += -motionY;
			ImgRow2 += -motionY;

			ImgCol1 += -motionX;
			ImgCol2 += -motionX;

			System.Drawing.Rectangle rect = viewPort.ImagePart;
			rect.X = (int)Math.Round(ImgCol1);
			rect.Y = (int)Math.Round(ImgRow1);
			viewPort.ImagePart = rect;
            viewPort.HalconWindow.SetPart((int)ImgRow1, (int)ImgCol1, (int)ImgRow2, (int)ImgCol2);
			repaint();
		}
        /// <summary>
        /// 自适应图片至窗口
        /// </summary>
		public void AutoZoom()
        {
            int count = HObjList.Count;
            if (count > 0)
            {
                setImagePart(0, 0, imageWidth, imageHeight);
                repaint();
            }
		}
        private void MouseDown(object sender, HalconDotNet.HMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                {
                    if (roiManager.ROIList.Count > 0)
                    {
                        var roi = Clone(roiManager.ROIList[roiManager.ROIList.Count - 1]);
                        roiManager.CopyROIShape(roi as ROI, (double)e.X, (double)e.Y);
                    }
                }
            }
        }
        public static T Clone<T>(T RealObject)
        {
            using (Stream objectStream = new MemoryStream())
            {
                //利用 System.Runtime.Serialization序列化与反序列化完成引用对象的复制  
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(objectStream, RealObject);
                objectStream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(objectStream);
            }
        } 
		/*******************************************************************/
		private void mouseUp(object sender, HalconDotNet.HMouseEventArgs e)
		{
			mousePressed = false;

			if (roiManager != null
				&& (roiManager.activeROIidx != -1))
			{
				roiManager.NotifyRCObserver(ROIController.EVENT_UPDATE_ROI);
			}
		}
        private void mouseWheel(object sender, HalconDotNet.HMouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                ScaleImage(e.X, e.Y, 0.9);
            }
            else
            {
                ScaleImage(e.X, e.Y, 1.1);
            }
        }
		/*******************************************************************/
		private void mouseMoved(object sender, HalconDotNet.HMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                mousePressed = true;
                int activeROIidx = -1;
                //double scale;
                startX = e.X;
                startY = e.Y;
                if (roiManager != null)
                {
                    activeROIidx = roiManager.mouseDownAction(e.X, e.Y);
                }

                MouseMove me = new MouseMove();
                try
                {
                    if (HObjList.Count > 0)
                    {
                        HTuple gray = 0;
                        HObjectEntry entry = (HObjectEntry)HObjList[0];
                        if ((e.Y >= 0) && (e.X >= 0))
                        {
                            HOperatorSet.GetGrayval(entry.HObj, e.Y, e.X, out gray);
                            me.Gray = (uint)gray.I;
                        }
                    }
                }
                catch { me.Gray = 0; }
                me.X = (int)e.X + 1;   //图像从0开始，显示数据从1开始
                me.Y = (int)e.Y + 1;
                MouseMoveEventOutput(me);
            }
            else
            {
                double motionX, motionY;

                if (!mousePressed)
                    return;

                if (roiManager != null && (roiManager.activeROIidx != -1))
                {
                    roiManager.mouseMoveAction(e.X, e.Y);
                }
                else
                {
                    if (!isCancelImageMove)
                    {
                        motionX = ((e.X - startX));
                        motionY = ((e.Y - startY));

                        if (((int)motionX != 0) || ((int)motionY != 0))
                        {
                            moveImage(motionX, motionY);
                            startX = e.X - motionX;
                            startY = e.Y - motionY;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 将数据以消息的形式传出
        /// </summary>
        /// <param name="e"></param>
        protected void MouseMoveEventOutput(MouseMove e)
        {
            EventHandler<MouseMove> tempEvent = this.MouseMoveEvent;
            if (tempEvent != null)
            {
                tempEvent(this, e);
            }
        }
       
        /// <summary>
		/// Triggers a repaint of the HALCON window
		/// </summary>
		public void repaint()
		{
            try
            {
                //Control c = new Control();
                //Control cOld = new Control();
                //Control obj = viewPort;
                //while (c != null)
                //{
                //    c = null;
                //    c = obj.Parent;
                //    if (c != null)
                //    {
                //        obj = c;
                //        cOld = c;
                //    }
                //}
                //Type t = cOld.GetType();
                //if (t.Name.Contains("Form"))
                //{
                    repaint(viewPort.HalconWindow);
                //}
                //c.Dispose();
                //cOld.Dispose();
            }
            catch { }
		}

		/// <summary>
		/// Repaints the HALCON window 'window'
		/// </summary>
		private void repaint(HalconDotNet.HWindow window)
		{
			int count = HObjList.Count;
            if (count > 0)
            {
                HObjectEntry entry;
                HSystem.SetSystem("flush_graphic", "false");
                window.ClearWindow();
                mGC.stateOfSettings.Clear();
                for (int i = 0; i < count; i++)
                {
                    entry = ((HObjectEntry)HObjList[i]);
                    if (entry.colorShow.Length > 0)
                    {
                        window.SetColor(entry.colorShow);
                    }
                    mGC.applyContext(window, entry.gContext);
                    window.DispObj(entry.HObj);
                }
                if (roiManager != null)
                    roiManager.paintData(window);
                HSystem.SetSystem("flush_graphic", "true");
                window.DispLine(-100.0, -100.0, -101.0, -101.0);
            }
            count=StrList.Count;
            if (count > 0)
            {
                foreach (StringX strX in StrList)
                {
                    window.SetFont("-Consolas-" + strX.size.ToString() + "-*-" + (strX.bold ? "1" : "0") + "-*-*-" + (strX.slant ? "1" : "0") + "-");
                    window.SetColor(strX.color.Name.ToLower());
                    window.SetTposition(strX.row, strX.column);
                    window.WriteString(strX.str);
                }
            }
		}



		/********************************************************************/
		/*                      GRAPHICSSTACK                               */
		/********************************************************************/

		/// <summary>
		/// Adds an iconic object to the graphics stack similar to the way
		/// it is defined for the HDevelop graphics stack.
		/// </summary>
		/// <param name="obj">Iconic object</param>
		public void AddHObject(HObject obj,string color,bool isImage)
		{
			HObjectEntry entry;
            HTuple hv_Width = null, hv_Height = null;

			if (obj == null)
				return;
            if (isImage)
            {
                if (obj.IsInitialized())
                {
                    HOperatorSet.GetImageSize(obj, out hv_Width, out hv_Height);
                    clearList();

                    if ((hv_Height != imageHeight) || (hv_Width != imageWidth))
                    {
                        viewPort.HalconWindow.SetDraw("margin");
                        imageHeight = hv_Height;
                        imageWidth = hv_Width;
                        setImagePart(0, 0, hv_Width, hv_Height);
                    }
                    HObjList.Clear();
                    StrList.Clear();
                }
                else
                {
                    viewPort.HalconWindow.ClearWindow();
                    HObjList.Clear();
                    StrList.Clear();
                }
            }
            if (obj.IsInitialized())
            {
                entry = new HObjectEntry(obj, color, mGC.copyContextList());
                HObjList.Add(entry);
            }

            //if (HObjList.Count > MAXNUMOBJLIST)
            //    HObjList.RemoveAt(1);
		}
        /// <summary>
        /// 添加字符串显示
        /// </summary>
        /// <param name="sx"></param>
        public void AddString(StringX sx)
        {
            StrList.Add(sx);
        }
		/// <summary>
		/// Clears all entries from the graphics stack 
		/// </summary>
		public void clearList()
		{
			
            //for (int i = 0; i < HObjList.Count; i++)
            //{
            //    if (i > 0)
            //    {
            //        HObjectEntry entry = ((HObjectEntry)HObjList[i]);
            //        entry.HObj.Dispose();
            //    }
            //}
            HObjList.Clear();
            StrList.Clear();
		}

		/// <summary>
		/// Returns the number of items on the graphics stack
		/// </summary>
		public int GetListCount()
		{
			return HObjList.Count;
		}

        public bool IsCancelImageMove
        {
            get { return isCancelImageMove; }
            set { isCancelImageMove = value; }
        }

	}//end of class

    public class MouseMove : EventArgs
    {
        public uint Gray = 0;
        public int X = 0;
        public int Y = 0;
    }

    public class MouseDownPointEvent : EventArgs
    {
        public double X = 0;
        public double Y = 0;
    }

    public class HWindowDisplay
    {
        public HWindowControl hWindowControl;
    }
}//end of namespace
