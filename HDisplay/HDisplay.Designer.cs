namespace HalWindow
{
    partial class HDisplay
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HDisplay));
            this.hWindowControl = new HalconDotNet.HWindowControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.ts_ImageSize = new System.Windows.Forms.ToolStripStatusLabel();
            this.ts_GrayValue = new System.Windows.Forms.ToolStripStatusLabel();
            this.ts_Position = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cms_ROI = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cms_Set = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.适应大小ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.cms_ROI.SuspendLayout();
            this.cms_Set.SuspendLayout();
            this.SuspendLayout();
            // 
            // hWindowControl
            // 
            this.hWindowControl.BackColor = System.Drawing.Color.Gray;
            this.hWindowControl.BorderColor = System.Drawing.Color.Gray;
            this.hWindowControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hWindowControl.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl.Location = new System.Drawing.Point(0, 0);
            this.hWindowControl.Margin = new System.Windows.Forms.Padding(0);
            this.hWindowControl.Name = "hWindowControl";
            this.hWindowControl.Size = new System.Drawing.Size(393, 294);
            this.hWindowControl.TabIndex = 0;
            this.hWindowControl.WindowSize = new System.Drawing.Size(393, 294);
            this.hWindowControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.hWindowControl_MouseDown);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.hWindowControl);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(393, 294);
            this.panel1.TabIndex = 1;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_ImageSize,
            this.ts_GrayValue,
            this.ts_Position});
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.ShowItemToolTips = true;
            this.statusStrip1.Size = new System.Drawing.Size(391, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // ts_ImageSize
            // 
            this.ts_ImageSize.Name = "ts_ImageSize";
            this.ts_ImageSize.Size = new System.Drawing.Size(216, 17);
            this.ts_ImageSize.Spring = true;
            this.ts_ImageSize.Text = "Image:0×0byte";
            // 
            // ts_GrayValue
            // 
            this.ts_GrayValue.AutoSize = false;
            this.ts_GrayValue.Image = ((System.Drawing.Image)(resources.GetObject("ts_GrayValue.Image")));
            this.ts_GrayValue.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ts_GrayValue.Name = "ts_GrayValue";
            this.ts_GrayValue.Size = new System.Drawing.Size(60, 17);
            this.ts_GrayValue.Text = "0";
            // 
            // ts_Position
            // 
            this.ts_Position.AutoSize = false;
            this.ts_Position.Image = ((System.Drawing.Image)(resources.GetObject("ts_Position.Image")));
            this.ts_Position.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ts_Position.Name = "ts_Position";
            this.ts_Position.Size = new System.Drawing.Size(100, 17);
            this.ts_Position.Text = "0,0";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.statusStrip1);
            this.panel2.Location = new System.Drawing.Point(0, 292);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(391, 22);
            this.panel2.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(393, 316);
            this.panel3.TabIndex = 3;
            // 
            // cms_ROI
            // 
            this.cms_ROI.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除ToolStripMenuItem});
            this.cms_ROI.Name = "cms_ROI";
            this.cms_ROI.Size = new System.Drawing.Size(101, 26);
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.删除ToolStripMenuItem.Text = "删除";
            this.删除ToolStripMenuItem.Click += new System.EventHandler(this.删除ToolStripMenuItem_Click);
            // 
            // cms_Set
            // 
            this.cms_Set.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.适应大小ToolStripMenuItem});
            this.cms_Set.Name = "cms_Set";
            this.cms_Set.Size = new System.Drawing.Size(125, 26);
            // 
            // 适应大小ToolStripMenuItem
            // 
            this.适应大小ToolStripMenuItem.Name = "适应大小ToolStripMenuItem";
            this.适应大小ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.适应大小ToolStripMenuItem.Text = "适应大小";
            this.适应大小ToolStripMenuItem.Click += new System.EventHandler(this.适应大小ToolStripMenuItem_Click);
            // 
            // HDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Name = "HDisplay";
            this.Size = new System.Drawing.Size(393, 316);
            this.panel1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.cms_ROI.ResumeLayout(false);
            this.cms_Set.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public HalconDotNet.HWindowControl hWindowControl;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel ts_ImageSize;
        private System.Windows.Forms.ToolStripStatusLabel ts_GrayValue;
        private System.Windows.Forms.ToolStripStatusLabel ts_Position;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ContextMenuStrip cms_ROI;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip cms_Set;
        private System.Windows.Forms.ToolStripMenuItem 适应大小ToolStripMenuItem;
    }
}
