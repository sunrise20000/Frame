namespace Frame.View
{
    partial class SettingView
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
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.shapeModle1 = new HalconModle.ShapeModle();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonGetTranslate = new System.Windows.Forms.Button();
            this.buttonGetRotate = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.shapeModle1);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(219, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(318, 537);
            this.groupControl2.TabIndex = 1;
            this.groupControl2.Text = "相机设置";
            // 
            // shapeModle1
            // 
            this.shapeModle1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.shapeModle1.Location = new System.Drawing.Point(2, 21);
            this.shapeModle1.Margin = new System.Windows.Forms.Padding(2);
            this.shapeModle1.Name = "shapeModle1";
            this.shapeModle1.Size = new System.Drawing.Size(314, 514);
            this.shapeModle1.TabIndex = 0;
            // 
            // groupControl1
            // 
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(219, 537);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "工艺参数";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(33, 80);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonGetTranslate
            // 
            this.buttonGetTranslate.Location = new System.Drawing.Point(94, 118);
            this.buttonGetTranslate.Name = "buttonGetTranslate";
            this.buttonGetTranslate.Size = new System.Drawing.Size(75, 23);
            this.buttonGetTranslate.TabIndex = 3;
            this.buttonGetTranslate.Text = "获取平移";
            this.buttonGetTranslate.UseVisualStyleBackColor = true;
            this.buttonGetTranslate.Click += new System.EventHandler(this.button2_Click);
            // 
            // buttonGetRotate
            // 
            this.buttonGetRotate.Location = new System.Drawing.Point(3, 118);
            this.buttonGetRotate.Name = "buttonGetRotate";
            this.buttonGetRotate.Size = new System.Drawing.Size(75, 23);
            this.buttonGetRotate.TabIndex = 4;
            this.buttonGetRotate.Text = "获取旋转";
            this.buttonGetRotate.UseVisualStyleBackColor = true;
            this.buttonGetRotate.Click += new System.EventHandler(this.buttonGetRotate_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(3, 158);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(94, 158);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 6;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(94, 203);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 7;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // SettingView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.buttonGetRotate);
            this.Controls.Add(this.buttonGetTranslate);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Name = "SettingView";
            this.Size = new System.Drawing.Size(537, 537);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SettingView_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private HalconModle.ShapeModle shapeModle1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonGetTranslate;
        private System.Windows.Forms.Button buttonGetRotate;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
    }
}
