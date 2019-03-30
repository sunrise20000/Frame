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
            DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
            this.ribbonControl2 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barButtonItemStart = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemStop = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemSetting = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemHome = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemHistory = new DevExpress.XtraBars.BarButtonItem();
            this.skinRibbonGalleryBarItem1 = new DevExpress.XtraBars.SkinRibbonGalleryBarItem();
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.barCheckItem1 = new DevExpress.XtraBars.BarCheckItem();
            this.barButtonItemPLC = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonGalleryBarItem1 = new DevExpress.XtraBars.RibbonGalleryBarItem();
            this.StateScanner = new DevExpress.XtraBars.BarStaticItem();
            this.StatePLC = new DevExpress.XtraBars.BarStaticItem();
            this.StateRobot = new DevExpress.XtraBars.BarStaticItem();
            this.ribbonPage2 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup3 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.barButtonItemCamera = new DevExpress.XtraBars.BarButtonItem();
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
            this.barButtonItemHistory,
            this.skinRibbonGalleryBarItem1,
            this.barStaticItem1,
            this.barCheckItem1,
            this.barButtonItemPLC,
            this.ribbonGalleryBarItem1,
            this.StateScanner,
            this.StatePLC,
            this.StateRobot,
            this.barButtonItemCamera});
            this.ribbonControl2.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl2.MaxItemId = 15;
            this.ribbonControl2.Name = "ribbonControl2";
            this.ribbonControl2.PageHeaderItemLinks.Add(this.StateRobot);
            this.ribbonControl2.PageHeaderItemLinks.Add(this.StatePLC);
            this.ribbonControl2.PageHeaderItemLinks.Add(this.StateScanner);
            this.ribbonControl2.PageHeaderItemLinks.Add(this.skinRibbonGalleryBarItem1);
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
            this.barButtonItemStart.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemStart_ItemClick);
            // 
            // barButtonItemStop
            // 
            this.barButtonItemStop.Caption = "Stop";
            this.barButtonItemStop.Id = 2;
            this.barButtonItemStop.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemStop.ImageOptions.Image")));
            this.barButtonItemStop.Name = "barButtonItemStop";
            this.barButtonItemStop.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.barButtonItemStop.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemStop_ItemClick);
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
            // skinRibbonGalleryBarItem1
            // 
            this.skinRibbonGalleryBarItem1.Caption = "skinRibbonGalleryBarItem1";
            this.skinRibbonGalleryBarItem1.Id = 6;
            this.skinRibbonGalleryBarItem1.Name = "skinRibbonGalleryBarItem1";
            // 
            // barStaticItem1
            // 
            this.barStaticItem1.Caption = "barStaticItem1";
            this.barStaticItem1.Id = 7;
            this.barStaticItem1.Name = "barStaticItem1";
            // 
            // barCheckItem1
            // 
            this.barCheckItem1.Caption = "barCheckItem1";
            this.barCheckItem1.Id = 8;
            this.barCheckItem1.Name = "barCheckItem1";
            // 
            // barButtonItemPLC
            // 
            this.barButtonItemPLC.Caption = "PLC";
            this.barButtonItemPLC.Description = "PLC";
            this.barButtonItemPLC.Id = 9;
            this.barButtonItemPLC.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.barButtonItemPLC.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemPLC.ImageOptions.Image")));
            this.barButtonItemPLC.Name = "barButtonItemPLC";
            this.barButtonItemPLC.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText;
            toolTipTitleItem2.Text = "PLC";
            superToolTip2.Items.Add(toolTipTitleItem2);
            this.barButtonItemPLC.SuperTip = superToolTip2;
            // 
            // ribbonGalleryBarItem1
            // 
            this.ribbonGalleryBarItem1.Caption = "ribbonGalleryBarItem1";
            // 
            // 
            // 
            this.ribbonGalleryBarItem1.Gallery.ShowItemText = true;
            this.ribbonGalleryBarItem1.Id = 10;
            this.ribbonGalleryBarItem1.Name = "ribbonGalleryBarItem1";
            // 
            // StateScanner
            // 
            this.StateScanner.Caption = "扫描枪";
            this.StateScanner.Id = 11;
            this.StateScanner.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("StateScanner.ImageOptions.Image")));
            this.StateScanner.Name = "StateScanner";
            // 
            // StatePLC
            // 
            this.StatePLC.Caption = "PLC";
            this.StatePLC.Id = 12;
            this.StatePLC.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("StatePLC.ImageOptions.Image")));
            this.StatePLC.Name = "StatePLC";
            // 
            // StateRobot
            // 
            this.StateRobot.Caption = "Robot";
            this.StateRobot.Id = 13;
            this.StateRobot.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("StateRobot.ImageOptions.Image")));
            this.StateRobot.Name = "StateRobot";
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
            this.ribbonPageGroup3.ItemLinks.Add(this.barButtonItemCamera);
            this.ribbonPageGroup3.ItemLinks.Add(this.barButtonItemHistory);
            this.ribbonPageGroup3.Name = "ribbonPageGroup3";
            this.ribbonPageGroup3.Text = "Menu";
            // 
            // barButtonItemCamera
            // 
            this.barButtonItemCamera.Caption = "Camera";
            this.barButtonItemCamera.Id = 14;
            this.barButtonItemCamera.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem1.ImageOptions.Image")));
            this.barButtonItemCamera.Name = "barButtonItemCamera";
            this.barButtonItemCamera.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.barButtonItemCamera.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemCamera_ItemClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(907, 637);
            this.Controls.Add(this.ribbonControl2);
            this.Name = "Form1";
            this.Ribbon = this.ribbonControl2;
            this.Text = "Frame";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
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
        //private View.HomeView homeView1;
        private DevExpress.XtraBars.SkinRibbonGalleryBarItem skinRibbonGalleryBarItem1;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
        private DevExpress.XtraBars.BarCheckItem barCheckItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItemPLC;
        private DevExpress.XtraBars.RibbonGalleryBarItem ribbonGalleryBarItem1;
        private DevExpress.XtraBars.BarStaticItem StateScanner;
        private DevExpress.XtraBars.BarStaticItem StatePLC;
        private DevExpress.XtraBars.BarStaticItem StateRobot;
        private DevExpress.XtraBars.BarButtonItem barButtonItemCamera;
    }
}

