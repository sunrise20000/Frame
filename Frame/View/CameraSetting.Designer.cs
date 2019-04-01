namespace Frame.View
{
    partial class CameraSetting
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CameraSetting));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.hDisplay1 = new HalWindow.HDisplay();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonZDown = new System.Windows.Forms.Button();
            this.buttonZUp = new System.Windows.Forms.Button();
            this.buttonRegion = new System.Windows.Forms.Button();
            this.textBoxStepDistance = new System.Windows.Forms.TextBox();
            this.buttonGetWordPoint = new System.Windows.Forms.Button();
            this.buttonCalibrate = new System.Windows.Forms.Button();
            this.buttonRight = new System.Windows.Forms.Button();
            this.buttonUp = new System.Windows.Forms.Button();
            this.buttonDown = new System.Windows.Forms.Button();
            this.buttonLeft = new System.Windows.Forms.Button();
            this.buttonGetImagePoint = new System.Windows.Forms.Button();
            this.dataGridViewCalibratePoint = new System.Windows.Forms.DataGridView();
            this.buttonContinues = new System.Windows.Forms.Button();
            this.buttonSnap = new System.Windows.Forms.Button();
            this.comboBoxCamList = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCalibratePoint)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 500F));
            this.tableLayoutPanel1.Controls.Add(this.hDisplay1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(804, 570);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // hDisplay1
            // 
            this.hDisplay1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hDisplay1.HImageX = null;
            this.hDisplay1.HRegionXList = null;
            this.hDisplay1.HStringXList = null;
            this.hDisplay1.IsCancelImageMove = false;
            this.hDisplay1.Location = new System.Drawing.Point(3, 3);
            this.hDisplay1.Name = "hDisplay1";
            this.hDisplay1.Size = new System.Drawing.Size(298, 564);
            this.hDisplay1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.dataGridViewCalibratePoint);
            this.panel1.Controls.Add(this.buttonContinues);
            this.panel1.Controls.Add(this.buttonSnap);
            this.panel1.Controls.Add(this.comboBoxCamList);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(307, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(494, 564);
            this.panel1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonZDown);
            this.groupBox1.Controls.Add(this.buttonZUp);
            this.groupBox1.Controls.Add(this.buttonRegion);
            this.groupBox1.Controls.Add(this.textBoxStepDistance);
            this.groupBox1.Controls.Add(this.buttonGetWordPoint);
            this.groupBox1.Controls.Add(this.buttonCalibrate);
            this.groupBox1.Controls.Add(this.buttonRight);
            this.groupBox1.Controls.Add(this.buttonUp);
            this.groupBox1.Controls.Add(this.buttonDown);
            this.groupBox1.Controls.Add(this.buttonLeft);
            this.groupBox1.Controls.Add(this.buttonGetImagePoint);
            this.groupBox1.Location = new System.Drawing.Point(3, 307);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(491, 190);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "标定";
            // 
            // buttonZDown
            // 
            this.buttonZDown.Image = ((System.Drawing.Image)(resources.GetObject("buttonZDown.Image")));
            this.buttonZDown.Location = new System.Drawing.Point(431, 129);
            this.buttonZDown.Name = "buttonZDown";
            this.buttonZDown.Size = new System.Drawing.Size(40, 52);
            this.buttonZDown.TabIndex = 13;
            this.buttonZDown.UseVisualStyleBackColor = true;
            this.buttonZDown.Click += new System.EventHandler(this.buttonZDown_Click);
            // 
            // buttonZUp
            // 
            this.buttonZUp.Image = ((System.Drawing.Image)(resources.GetObject("buttonZUp.Image")));
            this.buttonZUp.Location = new System.Drawing.Point(431, 19);
            this.buttonZUp.Name = "buttonZUp";
            this.buttonZUp.Size = new System.Drawing.Size(40, 52);
            this.buttonZUp.TabIndex = 12;
            this.buttonZUp.UseVisualStyleBackColor = true;
            this.buttonZUp.Click += new System.EventHandler(this.buttonZUp_Click);
            // 
            // buttonRegion
            // 
            this.buttonRegion.Location = new System.Drawing.Point(106, 59);
            this.buttonRegion.Name = "buttonRegion";
            this.buttonRegion.Size = new System.Drawing.Size(92, 23);
            this.buttonRegion.TabIndex = 11;
            this.buttonRegion.Text = "区域";
            this.buttonRegion.UseVisualStyleBackColor = true;
            this.buttonRegion.Click += new System.EventHandler(this.buttonRegion_Click);
            // 
            // textBoxStepDistance
            // 
            this.textBoxStepDistance.Location = new System.Drawing.Point(288, 91);
            this.textBoxStepDistance.Name = "textBoxStepDistance";
            this.textBoxStepDistance.Size = new System.Drawing.Size(54, 21);
            this.textBoxStepDistance.TabIndex = 10;
            // 
            // buttonGetWordPoint
            // 
            this.buttonGetWordPoint.Location = new System.Drawing.Point(106, 21);
            this.buttonGetWordPoint.Name = "buttonGetWordPoint";
            this.buttonGetWordPoint.Size = new System.Drawing.Size(92, 23);
            this.buttonGetWordPoint.TabIndex = 7;
            this.buttonGetWordPoint.Text = "获取机械坐标";
            this.buttonGetWordPoint.UseVisualStyleBackColor = true;
            this.buttonGetWordPoint.Click += new System.EventHandler(this.buttonGetWordPoint_Click);
            // 
            // buttonCalibrate
            // 
            this.buttonCalibrate.Location = new System.Drawing.Point(9, 59);
            this.buttonCalibrate.Name = "buttonCalibrate";
            this.buttonCalibrate.Size = new System.Drawing.Size(92, 23);
            this.buttonCalibrate.TabIndex = 8;
            this.buttonCalibrate.Text = "执行标定";
            this.buttonCalibrate.UseVisualStyleBackColor = true;
            this.buttonCalibrate.Click += new System.EventHandler(this.buttonCalibrate_Click);
            // 
            // buttonRight
            // 
            this.buttonRight.Image = ((System.Drawing.Image)(resources.GetObject("buttonRight.Image")));
            this.buttonRight.Location = new System.Drawing.Point(351, 84);
            this.buttonRight.Name = "buttonRight";
            this.buttonRight.Size = new System.Drawing.Size(64, 34);
            this.buttonRight.TabIndex = 8;
            this.buttonRight.UseVisualStyleBackColor = true;
            this.buttonRight.Click += new System.EventHandler(this.buttonRight_Click);
            // 
            // buttonUp
            // 
            this.buttonUp.Image = ((System.Drawing.Image)(resources.GetObject("buttonUp.Image")));
            this.buttonUp.Location = new System.Drawing.Point(295, 19);
            this.buttonUp.Name = "buttonUp";
            this.buttonUp.Size = new System.Drawing.Size(40, 52);
            this.buttonUp.TabIndex = 8;
            this.buttonUp.UseVisualStyleBackColor = true;
            this.buttonUp.Click += new System.EventHandler(this.buttonUp_Click);
            // 
            // buttonDown
            // 
            this.buttonDown.Image = ((System.Drawing.Image)(resources.GetObject("buttonDown.Image")));
            this.buttonDown.Location = new System.Drawing.Point(295, 129);
            this.buttonDown.Name = "buttonDown";
            this.buttonDown.Size = new System.Drawing.Size(40, 52);
            this.buttonDown.TabIndex = 8;
            this.buttonDown.UseVisualStyleBackColor = true;
            this.buttonDown.Click += new System.EventHandler(this.buttonDown_Click);
            // 
            // buttonLeft
            // 
            this.buttonLeft.Image = ((System.Drawing.Image)(resources.GetObject("buttonLeft.Image")));
            this.buttonLeft.Location = new System.Drawing.Point(210, 84);
            this.buttonLeft.Name = "buttonLeft";
            this.buttonLeft.Size = new System.Drawing.Size(64, 34);
            this.buttonLeft.TabIndex = 8;
            this.buttonLeft.UseVisualStyleBackColor = true;
            this.buttonLeft.Click += new System.EventHandler(this.buttonLeft_Click);
            // 
            // buttonGetImagePoint
            // 
            this.buttonGetImagePoint.Location = new System.Drawing.Point(9, 21);
            this.buttonGetImagePoint.Name = "buttonGetImagePoint";
            this.buttonGetImagePoint.Size = new System.Drawing.Size(92, 23);
            this.buttonGetImagePoint.TabIndex = 8;
            this.buttonGetImagePoint.Text = "获取图像坐标";
            this.buttonGetImagePoint.UseVisualStyleBackColor = true;
            this.buttonGetImagePoint.Click += new System.EventHandler(this.buttonGetImagePoint_Click);
            // 
            // dataGridViewCalibratePoint
            // 
            this.dataGridViewCalibratePoint.AllowUserToAddRows = false;
            this.dataGridViewCalibratePoint.AllowUserToDeleteRows = false;
            this.dataGridViewCalibratePoint.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCalibratePoint.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridViewCalibratePoint.Location = new System.Drawing.Point(3, 67);
            this.dataGridViewCalibratePoint.Name = "dataGridViewCalibratePoint";
            this.dataGridViewCalibratePoint.RowTemplate.Height = 23;
            this.dataGridViewCalibratePoint.Size = new System.Drawing.Size(488, 234);
            this.dataGridViewCalibratePoint.TabIndex = 5;
            // 
            // buttonContinues
            // 
            this.buttonContinues.Location = new System.Drawing.Point(316, 16);
            this.buttonContinues.Name = "buttonContinues";
            this.buttonContinues.Size = new System.Drawing.Size(75, 23);
            this.buttonContinues.TabIndex = 4;
            this.buttonContinues.Text = "连续";
            this.buttonContinues.UseVisualStyleBackColor = true;
            this.buttonContinues.Click += new System.EventHandler(this.buttonContinues_Click);
            // 
            // buttonSnap
            // 
            this.buttonSnap.Location = new System.Drawing.Point(215, 16);
            this.buttonSnap.Name = "buttonSnap";
            this.buttonSnap.Size = new System.Drawing.Size(83, 23);
            this.buttonSnap.TabIndex = 3;
            this.buttonSnap.Text = "单帧";
            this.buttonSnap.UseVisualStyleBackColor = true;
            this.buttonSnap.Click += new System.EventHandler(this.buttonSnap_Click);
            // 
            // comboBoxCamList
            // 
            this.comboBoxCamList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCamList.FormattingEnabled = true;
            this.comboBoxCamList.Location = new System.Drawing.Point(47, 17);
            this.comboBoxCamList.Name = "comboBoxCamList";
            this.comboBoxCamList.Size = new System.Drawing.Size(144, 20);
            this.comboBoxCamList.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "相机";
            // 
            // CameraSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "CameraSetting";
            this.Size = new System.Drawing.Size(804, 570);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCalibratePoint)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private HalWindow.HDisplay hDisplay1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxCamList;
        private System.Windows.Forms.Button buttonContinues;
        private System.Windows.Forms.Button buttonSnap;
        private System.Windows.Forms.DataGridView dataGridViewCalibratePoint;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonGetWordPoint;
        private System.Windows.Forms.Button buttonGetImagePoint;
        private System.Windows.Forms.Button buttonCalibrate;
        private System.Windows.Forms.Button buttonLeft;
        private System.Windows.Forms.Button buttonRight;
        private System.Windows.Forms.Button buttonUp;
        private System.Windows.Forms.Button buttonDown;
        private System.Windows.Forms.TextBox textBoxStepDistance;
        private System.Windows.Forms.Button buttonRegion;
        private System.Windows.Forms.Button buttonZDown;
        private System.Windows.Forms.Button buttonZUp;
    }
}
