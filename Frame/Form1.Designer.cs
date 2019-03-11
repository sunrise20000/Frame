namespace Frame
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.ribbonControl2 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barButtonItemStart = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemStop = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemSetting = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemHome = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemHistory = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage2 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup3 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.homeView1 = new Frame.View.HomeView();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl2)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl2
            // 
            this.ribbonControl2.ExpandCollapseItem.Id = 0;
            this.ribbonControl2.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl2.ExpandCollapseItem,
            this.barButtonItemStart,
            this.barButtonItemStop,
            this.barButtonItemSetting,
            this.barButtonItemHome,
            this.barButtonItemHistory});
            this.ribbonControl2.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl2.MaxItemId = 6;
            this.ribbonControl2.Name = "ribbonControl2";
            this.ribbonControl2.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage2});
            this.ribbonControl2.Size = new System.Drawing.Size(907, 147);
            // 
            // barButtonItemStart
            // 
            this.barButtonItemStart.Caption = "Start";
            this.barButtonItemStart.Id = 1;
            this.barButtonItemStart.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemStart.ImageOptions.Image")));
            this.barButtonItemStart.Name = "barButtonItemStart";
            this.barButtonItemStart.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            // 
            // barButtonItemStop
            // 
            this.barButtonItemStop.Caption = "Stop";
            this.barButtonItemStop.Id = 2;
            this.barButtonItemStop.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemStop.ImageOptions.Image")));
            this.barButtonItemStop.Name = "barButtonItemStop";
            this.barButtonItemStop.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            // 
            // barButtonItemSetting
            // 
            this.barButtonItemSetting.Caption = "Setting";
            this.barButtonItemSetting.Id = 3;
            this.barButtonItemSetting.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemSetting.ImageOptions.Image")));
            this.barButtonItemSetting.Name = "barButtonItemSetting";
            this.barButtonItemSetting.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.barButtonItemSetting.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemSetting_ItemClick);
            // 
            // barButtonItemHome
            // 
            this.barButtonItemHome.Caption = "Home";
            this.barButtonItemHome.Id = 4;
            this.barButtonItemHome.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemHome.ImageOptions.Image")));
            this.barButtonItemHome.Name = "barButtonItemHome";
            this.barButtonItemHome.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.barButtonItemHome.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemHome_ItemClick);
            // 
            // barButtonItemHistory
            // 
            this.barButtonItemHistory.Caption = "History";
            this.barButtonItemHistory.Id = 5;
            this.barButtonItemHistory.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemHistory.ImageOptions.Image")));
            this.barButtonItemHistory.Name = "barButtonItemHistory";
            this.barButtonItemHistory.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.barButtonItemHistory.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemHistory_ItemClick);
            // 
            // ribbonPage2
            // 
            this.ribbonPage2.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup2,
            this.ribbonPageGroup3});
            this.ribbonPage2.Name = "ribbonPage2";
            this.ribbonPage2.Text = "Menu";
            // 
            // ribbonPageGroup2
            // 
            this.ribbonPageGroup2.ItemLinks.Add(this.barButtonItemStart);
            this.ribbonPageGroup2.ItemLinks.Add(this.barButtonItemStop);
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            this.ribbonPageGroup2.Text = "Run";
            // 
            // ribbonPageGroup3
            // 
            this.ribbonPageGroup3.ItemLinks.Add(this.barButtonItemHome);
            this.ribbonPageGroup3.ItemLinks.Add(this.barButtonItemSetting);
            this.ribbonPageGroup3.ItemLinks.Add(this.barButtonItemHistory);
            this.ribbonPageGroup3.Name = "ribbonPageGroup3";
            this.ribbonPageGroup3.Text = "Menu";
            // 
            // homeView1
            // 
            this.homeView1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.homeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.homeView1.Location = new System.Drawing.Point(0, 147);
            this.homeView1.Name = "homeView1";
            this.homeView1.Size = new System.Drawing.Size(907, 490);
            this.homeView1.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(907, 637);
            this.Controls.Add(this.homeView1);
            this.Controls.Add(this.ribbonControl2);
            this.Name = "Form1";
            this.Ribbon = this.ribbonControl2;
            this.Text = "Frame";
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl2;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage2;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraBars.BarButtonItem barButtonItemStart;
        private DevExpress.XtraBars.BarButtonItem barButtonItemStop;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup3;
        private DevExpress.XtraBars.BarButtonItem barButtonItemSetting;
        private DevExpress.XtraBars.BarButtonItem barButtonItemHome;
        private DevExpress.XtraBars.BarButtonItem barButtonItemHistory;
        private View.HomeView homeView1;
    }
}

