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

namespace Frame.View
{
    public enum EnumHandEyeCalibrateType
    {
        EyeInHand,
        EyeToHandCamUp,
        EyeToHandCamDown,
    }
    public partial class CameraSetting : MessageUserControl
    {
        DataTable CalibDt = new DataTable();
        List<CalibratePointModel> PointList = new List<CalibratePointModel>();
        VisionLib.HalconVision Vision = VisionLib.HalconVision.Instance;
        List<VisionPointData> ImagePoint = new List<VisionPointData>();
        List<VisionPointData> MechinePoint = new List<VisionPointData>();
        Instrument.InstrumentRobotABB Robot = null;
        public CameraSetting()
        {
            InitializeComponent();
            for (int i = 0; i < 9; i++)
                PointList.Add(new CalibratePointModel() { Name = (i + 1).ToString(), Index = i });
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

            foreach (var it in Config.ConfigManger.Instance.HardwarecfgEntry.Cameras)
            {
                comboBoxCamList.Items.Add(it.NameForVision);
            }
            comboBoxCamList.SelectedIndex = 0;
            comboBoxCalibrationMode.SelectedIndex = 0;
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
            var Cam = Camera.CameraManager.Instance.FindInstanseByName(comboBoxCamList.Text) as HaiKangCamera;
            Cam.OpenCamera();
            Cam.SnapShot();
            Cam.ImageAcquired -= Cam_ImageAcquired;
            Cam.ImageAcquired += Cam_ImageAcquired;
            Cam.CloseCamera();
        }

        private void Cam_ImageAcquired(object sender, CLCamera.ImageEventArgs<HalconDotNet.HObject> e)
        {
            hDisplay1.HImageX=e.image;

        }

        private void buttonCalibrate_Click(object sender, EventArgs e)
        {

            Vision.HandEyeCalibration(null,null,"");
        }

        private void buttonGetImagePoint_Click(object sender, EventArgs e)
        {
            
            
            if (Enum.TryParse(comboBoxCalibrationMode.Text, out EnumHandEyeCalibrateType type))
            {
                CalibratePointModel PtModel = null;
                var rows = dataGridViewCalibratePoint.SelectedRows;
                if (rows.Count > 0)
                {
                    PtModel = (from pts in PointList where pts.Name.Equals(rows[0].Cells[0].Value.ToString()) select pts).First();
                }
                else
                {
                    MessageBox.Show("请选择一行");
                    return;
                }
                switch (type)
                {
                    case EnumHandEyeCalibrateType.EyeInHand:
                        
                        break;
                    case EnumHandEyeCalibrateType.EyeToHandCamDown:
                        PtModel.CamX = 9;
                        PtModel.CamY = 10;
                        UpdateCalibratePoint();
                        break;
                    case EnumHandEyeCalibrateType.EyeToHandCamUp:

                        break;
                }
            }
        }

        private void buttonGetWordPoint_Click(object sender, EventArgs e)
        {
            Robot = Instrument.InstrumentMgr<InstrumentCfgBase, CommunicationCfgBase>.Instance.FindInstanseByName("RobotAbb") as Instrument.InstrumentRobotABB;
            if (Robot == null)
                MessageBox.Show("机械手获取失败");
            if (Enum.TryParse(comboBoxCalibrationMode.Text, out EnumHandEyeCalibrateType type))
            {
                switch (type)
                {
                    case EnumHandEyeCalibrateType.EyeInHand:

                        break;
                    case EnumHandEyeCalibrateType.EyeToHandCamDown:
                        var pt = Robot.GetCurrentPostion(ABBRobotLib.Definations.EnumRobotTool.Tool1);
                        
                        break;
                    case EnumHandEyeCalibrateType.EyeToHandCamUp:

                        break;
                }
            }
        }
    }
}
