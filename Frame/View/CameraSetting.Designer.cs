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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.hDisplay1 = new HalWindow.HDisplay();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBoxCalibrationMode = new System.Windows.Forms.ComboBox();
            this.buttonGetWordPoint = new System.Windows.Forms.Button();
            this.buttonGetImagePoint = new System.Windows.Forms.Button();
            this.dataGridViewCalibratePoint = new System.Windows.Forms.DataGridView();
            this.buttonContinues = new System.Windows.Forms.Button();
            this.buttonSnap = new System.Windows.Forms.Button();
            this.comboBoxCamList = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonCalibrate = new System.Windows.Forms.Button();
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
            this.groupBox1.Controls.Add(this.comboBoxCalibrationMode);
            this.groupBox1.Controls.Add(this.buttonGetWordPoint);
            this.groupBox1.Controls.Add(this.buttonCalibrate);
            this.groupBox1.Controls.Add(this.buttonGetImagePoint);
            this.groupBox1.Location = new System.Drawing.Point(11, 371);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(483, 190);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "标定";
            // 
            // comboBoxCalibrationMode
            // 
            this.comboBoxCalibrationMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCalibrationMode.FormattingEnabled = true;
            this.comboBoxCalibrationMode.Items.AddRange(new object[] {
            "EyeInHand",
            "EyeToHandCamUp",
            "EyeToHandCamDown"});
            this.comboBoxCalibrationMode.Location = new System.Drawing.Point(259, 32);
            this.comboBoxCalibrationMode.Name = "comboBoxCalibrationMode";
            this.comboBoxCalibrationMode.Size = new System.Drawing.Size(121, 20);
            this.comboBoxCalibrationMode.TabIndex = 9;
            // 
            // buttonGetWordPoint
            // 
            this.buttonGetWordPoint.Location = new System.Drawing.Point(138, 30);
            this.buttonGetWordPoint.Name = "buttonGetWordPoint";
            this.buttonGetWordPoint.Size = new System.Drawing.Size(92, 23);
            this.buttonGetWordPoint.TabIndex = 7;
            this.buttonGetWordPoint.Text = "获取机械坐标";
            this.buttonGetWordPoint.UseVisualStyleBackColor = true;
            this.buttonGetWordPoint.Click += new System.EventHandler(this.buttonGetWordPoint_Click);
            // 
            // buttonGetImagePoint
            // 
            this.buttonGetImagePoint.Location = new System.Drawing.Point(17, 30);
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
            this.dataGridViewCalibratePoint.Size = new System.Drawing.Size(488, 297);
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
            // buttonCalibrate
            // 
            this.buttonCalibrate.Location = new System.Drawing.Point(17, 68);
            this.buttonCalibrate.Name = "buttonCalibrate";
            this.buttonCalibrate.Size = new System.Drawing.Size(92, 23);
            this.buttonCalibrate.TabIndex = 8;
            this.buttonCalibrate.Text = "执行标定";
            this.buttonCalibrate.UseVisualStyleBackColor = true;
            this.buttonCalibrate.Click += new System.EventHandler(this.buttonCalibrate_Click);
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
        private System.Windows.Forms.ComboBox comboBoxCalibrationMode;
        private System.Windows.Forms.Button buttonGetWordPoint;
        private System.Windows.Forms.Button buttonGetImagePoint;
        private System.Windows.Forms.Button buttonCalibrate;
    }
}
