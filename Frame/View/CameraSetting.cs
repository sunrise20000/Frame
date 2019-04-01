using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Frame.Model;
using Frame.Camera;
using VisionLib.DataModel;
using Frame.Config.HardwareCfg.InstrumentCfg;
using Frame.Config.CommunicationCfg;
using System.Threading;
using ViewROI;

namespace Frame.View
{
    public partial class CameraSetting : MessageUserControl
    {
        DataTable CalibDt = new DataTable();
        List<CalibratePointModel> PointList = new List<CalibratePointModel>();
        VisionLib.HalconVision Vision = VisionLib.HalconVision.Instance;
        List<VisionPointData> ImagePoint = new List<VisionPointData>();
        List<VisionPointData> MechinePoint = new List<VisionPointData>();
        Instrument.InstrumentRobotABB Robot = null;
        const string CAMUP_CALIBFILE = @"Vision/Calib/Up.tup";
        const string CAMDOWN_CALIBFILE = @"Vision/Calib/Down.tup";
        Task ContinueGrabTask = null;
        CancellationTokenSource cts = null;
        public CameraSetting()
        {
            InitializeComponent();
            for (int i = 0; i < 9; i++)
            {
                PointList.Add(new CalibratePointModel() { Name =$"点{i+1}" });
            }
            var t = typeof(CalibratePointModel);
            var ps = t.GetProperties();
            foreach (var it in ps)
            {
                CalibDt.Columns.Add(it.Name);
            }

            UpdateCalibratePoint();
            dataGridViewCalibratePoint.DataSource = CalibDt;
            dataGridViewCalibratePoint.MultiSelect = false;
            dataGridViewCalibratePoint.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewCalibratePoint.Columns[0].ReadOnly = true;


            foreach (var it in Config.ConfigManger.Instance.HardwarecfgEntry.Cameras)
            {
                comboBoxCamList.Items.Add(it.NameForVision);
            }
            comboBoxCamList.SelectedIndex = 0;


            Robot = Instrument.InstrumentMgr<InstrumentCfgBase, CommunicationCfgBase>.Instance.FindInstanseByName("RobotAbb") as Instrument.InstrumentRobotABB;
        }
        ~CameraSetting()
        {
            foreach (var it in Config.ConfigManger.Instance.HardwarecfgEntry.Cameras)
            {
                var Cam = Camera.CameraManager.Instance.FindInstanseByName(it.NameForVision) as HaiKangCamera;
                if (Cam != null && Cam.m_IsConnected)
                    Cam.CloseCamera();
                    
            }
        }
        
        private void UpdateCalibratePoint()
        {

            if (CalibDt.Rows.Count == 0)
            {
                foreach (var it in PointList)
                {
                    var dr = CalibDt.NewRow();
                    dr[0] = it.Name;
                    dr[1] = it.CamX;
                    dr[2] = it.CamY;
                    dr[3] = it.X;
                    dr[4] = it.Y;
                    CalibDt.Rows.Add(dr);
                }
            }
            else
            {
                for(int i=0;i<PointList.Count;i++)
                {
                    var dr = CalibDt.Rows[i];
                    dr[0] = PointList[i].Name;
                    dr[1] = PointList[i].CamX;
                    dr[2] = PointList[i].CamY;
                    dr[3] = PointList[i].X;
                    dr[4] = PointList[i].Y;
                }
            }
        }

        private void buttonSnap_Click(object sender, EventArgs e)
        {
            try
            {
                if (cts != null)
                    cts.Cancel();
                var Cam = Camera.CameraManager.Instance.FindInstanseByName(comboBoxCamList.Text) as HaiKangCamera;
                if (!Cam.m_IsConnected)
                {
                    Cam.OpenCamera();
                    Cam.SetTriggerMode("Off");
                    Cam.ImageAcquired -= Cam_ImageAcquired;
                    Cam.ImageAcquired += Cam_ImageAcquired;
                }
                Cam.SnapShot();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void Cam_ImageAcquired(object sender, CLCamera.ImageEventArgs<HalconDotNet.HObject> e)
        {
            hDisplay1.HImageX=e.image;
        }

        private void buttonCalibrate_Click(object sender, EventArgs e)
        {
            try
            {
                List<VisionPointData> CamPtList = new List<VisionPointData>();
                List<VisionPointData> WordPtList = new List<VisionPointData>();
                foreach (var it in PointList)
                {
                    CamPtList.Add(new VisionPointData() { X = it.CamX, Y = it.CamY });
                    WordPtList.Add(new VisionPointData() { X = it.X, Y = it.Y });
                }
                Vision.HandEyeCalibration(CamPtList, WordPtList, comboBoxCamList.Text.Equals("Cam_Up") ? CAMUP_CALIBFILE : CAMDOWN_CALIBFILE);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonGetImagePoint_Click(object sender, EventArgs e)
        {
            try
            {
                CalibratePointModel PtModel = null;
                var rows = dataGridViewCalibratePoint.SelectedRows;
                if (rows.Count > 0)
                {
                    if (comboBoxCamList.Text.Equals("Cam_Up"))
                    {
                        if (hDisplay1.GetSearchRegions().Count > 0)
                        {
                            var reg = hDisplay1.GetSearchRegions().ElementAt(0);
                            var reduceImage=Vision.ReduceDomain(hDisplay1.HImageX, reg);

                            Vision.Find9PointCalibTbPoint(reduceImage, out List<VisionLib.DataModel.VisionPointData> ptOut);
                            if (ptOut.Count == 10)
                            {
                                for (int i = 0; i < 9; i++)
                                {
                                    PointList[i].CamX = ptOut[i].X;
                                    PointList[i].CamY = ptOut[i].Y;
                                }
                            }
                        }
                    }
                    else if (comboBoxCamList.Text.Equals("Cam_Down"))
                    {
                        PtModel = (from pts in PointList where pts.Name.Equals(rows[0].Cells[0].Value.ToString()) select pts).First();
                        Vision.FindShapeModel(hDisplay1.HImageX, "ModelUp.shm", out HalconDotNet.HTuple Row, out HalconDotNet.HTuple Col,
                            out HalconDotNet.HTuple Angle, out HalconDotNet.HTuple Score);
                        if (Col != null && Col.Length > 0)
                        {
                            PtModel.CamX = Col.D;
                            PtModel.CamY = Row.D;
                        }
                        else
                        {
                            MessageBox.Show("模板未找到，获取点失败");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("请选择一行");
                    return;
                }
                UpdateCalibratePoint();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonGetWordPoint_Click(object sender, EventArgs e)
        {
            try
            {
                Robot = Instrument.InstrumentMgr<InstrumentCfgBase, CommunicationCfgBase>.Instance.FindInstanseByName("RobotAbb") as Instrument.InstrumentRobotABB;
                if (Robot == null)
                    MessageBox.Show("机械手获取失败");

                CalibratePointModel PtModel = null;
                var rows = dataGridViewCalibratePoint.SelectedRows;
                if (rows.Count > 0)
                {
                    PtModel = (from pts in PointList where pts.Name.Equals(rows[0].Cells[0].Value.ToString()) select pts).First();
                    var WordPt = Robot.GetCurrentPostion(ABBRobotLib.Definations.EnumRobotTool.Tool1);
                    PtModel.X = WordPt.X;
                    PtModel.Y = WordPt.Y;
                    UpdateCalibratePoint();
                }
                else
                {
                    MessageBox.Show("请选择一行");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        #region 手动操作机器人,每次最大一定10mm
        const int MAX_DISTANCE = 20;
        private void buttonUp_Click(object sender, EventArgs e)
        {
            try
            {
                if (int.TryParse(textBoxStepDistance.Text, out int step))
                {
                    int s = Math.Min(Math.Abs(step), MAX_DISTANCE);
                    if (!Robot.IsOpen)
                    {
                        Robot.Open();
                    }
                    Robot.MoveRel(0, s, 0, ABBRobotLib.Definations.EnumRobotSpeed.V10, ABBRobotLib.Definations.EnumRobotTool.Tool1, ABBRobotLib.Definations.EnumMoveType.MoveJ);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonLeft_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBoxStepDistance.Text, out int step))
            {
                int s = Math.Min(Math.Abs(step), MAX_DISTANCE);
                if (!Robot.IsOpen)
                {
                    Robot.Open();
                }
                Robot.MoveRel(-s, 0, 0, ABBRobotLib.Definations.EnumRobotSpeed.V10, ABBRobotLib.Definations.EnumRobotTool.Tool1, ABBRobotLib.Definations.EnumMoveType.MoveJ);
            }
        }

        private void buttonRight_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBoxStepDistance.Text, out int step))
            {
                int s = Math.Min(Math.Abs(step), MAX_DISTANCE);
                if (!Robot.IsOpen)
                {
                    Robot.Open();
                }
                Robot.MoveRel(s, 0, 0, ABBRobotLib.Definations.EnumRobotSpeed.V10, ABBRobotLib.Definations.EnumRobotTool.Tool1, ABBRobotLib.Definations.EnumMoveType.MoveJ);
            }
        }

        private void buttonDown_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBoxStepDistance.Text, out int step))
            {
                int s = Math.Min(Math.Abs(step), MAX_DISTANCE);
                if (!Robot.IsOpen)
                {
                    Robot.Open();
                }
                Robot.MoveRel(0, -s, 0, ABBRobotLib.Definations.EnumRobotSpeed.V10, ABBRobotLib.Definations.EnumRobotTool.Tool1, ABBRobotLib.Definations.EnumMoveType.MoveJ);
            }
        }

        private void buttonZUp_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBoxStepDistance.Text, out int step))
            {
                int s = Math.Min(Math.Abs(step), MAX_DISTANCE);
                if (!Robot.IsOpen)
                {
                    Robot.Open();
                }
                Robot.MoveRel(0, 0, s, ABBRobotLib.Definations.EnumRobotSpeed.V10, ABBRobotLib.Definations.EnumRobotTool.Tool1, ABBRobotLib.Definations.EnumMoveType.MoveJ);
            }
        }

        private void buttonZDown_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBoxStepDistance.Text, out int step))
            {
                int s = Math.Min(Math.Abs(step), MAX_DISTANCE);
                if (!Robot.IsOpen)
                {
                    Robot.Open();
                }
                Robot.MoveRel(0, 0, -s, ABBRobotLib.Definations.EnumRobotSpeed.V10, ABBRobotLib.Definations.EnumRobotTool.Tool1, ABBRobotLib.Definations.EnumMoveType.MoveJ);
            }
        }
        #endregion

        private void buttonContinues_Click(object sender, EventArgs e)
        {
            var Cam = Camera.CameraManager.Instance.FindInstanseByName(comboBoxCamList.Text) as HaiKangCamera;
            if (ContinueGrabTask==null || ContinueGrabTask.Status == TaskStatus.Canceled || ContinueGrabTask.Status == TaskStatus.RanToCompletion)
            {
                cts = new CancellationTokenSource();
                ContinueGrabTask = new Task(() => {
                    while (!cts.IsCancellationRequested)
                    {
                        try
                        {
                            if (!Cam.m_IsConnected)
                            {
                                Cam.OpenCamera();
                                Cam.SetTriggerMode("Off");
                            }
                            Cam.SnapShot();
                            Thread.Sleep(50);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            break;
                        }
                    }
                }, cts.Token);
                ContinueGrabTask.Start();
            }
        }

        private void buttonRegion_Click(object sender, EventArgs e)
        {
            hDisplay1.AddRegion("旋转矩形", true);
            
        }

      
    }
}
