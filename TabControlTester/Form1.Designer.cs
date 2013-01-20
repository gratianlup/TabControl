namespace TabControlTester {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.tabHost1 = new TabControl.TabHost();
            this.tabItem1 = new TabControl.TabItem();
            this.tabItem2 = new TabControl.TabItem();
            this.tabItem3 = new TabControl.TabItem();
            this.tabHost2 = new TabControl.TabHost();
            this.tabItem4 = new TabControl.TabItem();
            this.tabItem5 = new TabControl.TabItem();
            this.tabItem6 = new TabControl.TabItem();
            this.tabHost3 = new TabControl.TabHost();
            this.tabHost4 = new TabControl.TabHost();
            this.tabItem10 = new TabControl.TabItem();
            this.tabItem12 = new TabControl.TabItem();
            this.tabItem13 = new TabControl.TabItem();
            this.tabItem14 = new TabControl.TabItem();
            this.tabItem11 = new TabControl.TabItem();
            this.tabItem7 = new TabControl.TabItem();
            this.tabItem8 = new TabControl.TabItem();
            this.tabItem9 = new TabControl.TabItem();
            this.tabHost1.SuspendLayout();
            this.tabHost2.SuspendLayout();
            this.tabHost3.SuspendLayout();
            this.tabHost4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabHost1
            // 
            this.tabHost1.AllowTabReordering = false;
            this.tabHost1.CloseButtonAlignment = TabControl.CloseButtonAlignment.Left;
            this.tabHost1.CloseButtonBorderOpacity = 50;
            this.tabHost1.CloseButtonBorderOpacitySelected = 255;
            this.tabHost1.CloseButtonColor = System.Drawing.Color.Black;
            this.tabHost1.CloseButtonColorSelected = System.Drawing.Color.Black;
            this.tabHost1.CloseButtonOverColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.tabHost1.CloseButtonOverColorSelected = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.tabHost1.CloseButtonPressedColor = System.Drawing.Color.Red;
            this.tabHost1.CloseButtonsOnlyForSelected = true;
            this.tabHost1.Controls.Add(this.tabItem1);
            this.tabHost1.Controls.Add(this.tabItem2);
            this.tabHost1.Controls.Add(this.tabItem3);
            this.tabHost1.Dock = System.Windows.Forms.DockStyle.Left;
            this.tabHost1.InsertionMarkerColor = System.Drawing.Color.Empty;
            this.tabHost1.Location = new System.Drawing.Point(0, 0);
            this.tabHost1.Name = "tabHost1";
            this.tabHost1.SelectedTabHeight = 30;
            this.tabHost1.ShowCloseButtons = true;
            this.tabHost1.Size = new System.Drawing.Size(50, 404);
            this.tabHost1.TabAlignment = System.Windows.Forms.TabAlignment.Left;
            this.tabHost1.TabDistance = 1;
            this.tabHost1.TabHeight = 26;
            this.tabHost1.TabIndex = 0;
            this.tabHost1.Tabs.Add(this.tabItem1);
            this.tabHost1.Tabs.Add(this.tabItem2);
            this.tabHost1.Tabs.Add(this.tabItem3);
            // 
            // tabItem1
            // 
            this.tabItem1.AutoEllipsis = false;
            this.tabItem1.BackColor = System.Drawing.Color.SlateGray;
            this.tabItem1.BorderColor = System.Drawing.Color.Empty;
            this.tabItem1.ForeColor = System.Drawing.Color.White;
            this.tabItem1.HighlightBackColor = System.Drawing.Color.Gold;
            this.tabItem1.HighlightForeColor = System.Drawing.Color.Black;
            this.tabItem1.Image = null;
            this.tabItem1.Location = new System.Drawing.Point(0, 0);
            this.tabItem1.Name = "tabItem1";
            this.tabItem1.Selected = false;
            this.tabItem1.SelectedBackColor = System.Drawing.Color.DeepPink;
            this.tabItem1.SelectedForeColor = System.Drawing.Color.White;
            this.tabItem1.SelectedTabHeigth = 0;
            this.tabItem1.Size = new System.Drawing.Size(26, 100);
            this.tabItem1.TabHeigth = 0;
            this.tabItem1.TabIndex = 0;
            this.tabItem1.TabText = "TabItem1";
            this.tabItem1.TabWidth = 100;
            this.tabItem1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.tabItem1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.tabItem1.CloseButtonPressed += new System.EventHandler(this.tabItem2_CloseButtonPressed);
            // 
            // tabItem2
            // 
            this.tabItem2.AutoEllipsis = false;
            this.tabItem2.BackColor = System.Drawing.Color.SlateGray;
            this.tabItem2.BorderColor = System.Drawing.Color.Empty;
            this.tabItem2.ForeColor = System.Drawing.Color.White;
            this.tabItem2.HighlightBackColor = System.Drawing.Color.Gold;
            this.tabItem2.HighlightForeColor = System.Drawing.Color.Black;
            this.tabItem2.Image = null;
            this.tabItem2.Location = new System.Drawing.Point(0, 101);
            this.tabItem2.Name = "tabItem2";
            this.tabItem2.Selected = true;
            this.tabItem2.SelectedBackColor = System.Drawing.Color.DeepPink;
            this.tabItem2.SelectedForeColor = System.Drawing.Color.White;
            this.tabItem2.SelectedTabHeigth = 0;
            this.tabItem2.Size = new System.Drawing.Size(30, 100);
            this.tabItem2.TabHeigth = 0;
            this.tabItem2.TabIndex = 1;
            this.tabItem2.TabText = "TabItem2";
            this.tabItem2.TabWidth = 100;
            this.tabItem2.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.tabItem2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.tabItem2.CloseButtonPressed += new System.EventHandler(this.tabItem2_CloseButtonPressed);
            // 
            // tabItem3
            // 
            this.tabItem3.AutoEllipsis = false;
            this.tabItem3.BackColor = System.Drawing.Color.SlateGray;
            this.tabItem3.BorderColor = System.Drawing.Color.Empty;
            this.tabItem3.ForeColor = System.Drawing.Color.White;
            this.tabItem3.HighlightBackColor = System.Drawing.Color.Gold;
            this.tabItem3.HighlightForeColor = System.Drawing.Color.Black;
            this.tabItem3.Image = null;
            this.tabItem3.Location = new System.Drawing.Point(0, 202);
            this.tabItem3.Name = "tabItem3";
            this.tabItem3.Selected = false;
            this.tabItem3.SelectedBackColor = System.Drawing.Color.DeepPink;
            this.tabItem3.SelectedForeColor = System.Drawing.Color.White;
            this.tabItem3.SelectedTabHeigth = 0;
            this.tabItem3.Size = new System.Drawing.Size(26, 100);
            this.tabItem3.TabHeigth = 0;
            this.tabItem3.TabIndex = 2;
            this.tabItem3.TabText = "TabItem3";
            this.tabItem3.TabWidth = 100;
            this.tabItem3.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.tabItem3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            // 
            // tabHost2
            // 
            this.tabHost2.AllowTabReordering = false;
            this.tabHost2.CloseButtonAlignment = TabControl.CloseButtonAlignment.Right;
            this.tabHost2.CloseButtonBorderOpacity = 0;
            this.tabHost2.CloseButtonBorderOpacitySelected = 0;
            this.tabHost2.CloseButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tabHost2.CloseButtonColorSelected = System.Drawing.Color.Empty;
            this.tabHost2.CloseButtonOverColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.tabHost2.CloseButtonOverColorSelected = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.tabHost2.CloseButtonPressedColor = System.Drawing.Color.Red;
            this.tabHost2.CloseButtonsOnlyForSelected = true;
            this.tabHost2.Controls.Add(this.tabItem4);
            this.tabHost2.Controls.Add(this.tabItem5);
            this.tabHost2.Controls.Add(this.tabItem6);
            this.tabHost2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabHost2.InsertionMarkerColor = System.Drawing.Color.Empty;
            this.tabHost2.Location = new System.Drawing.Point(50, 0);
            this.tabHost2.Name = "tabHost2";
            this.tabHost2.SelectedTabHeight = 30;
            this.tabHost2.ShowCloseButtons = false;
            this.tabHost2.Size = new System.Drawing.Size(628, 56);
            this.tabHost2.TabAlignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabHost2.TabDistance = 1;
            this.tabHost2.TabHeight = 26;
            this.tabHost2.TabIndex = 1;
            this.tabHost2.Tabs.Add(this.tabItem4);
            this.tabHost2.Tabs.Add(this.tabItem5);
            this.tabHost2.Tabs.Add(this.tabItem6);
            // 
            // tabItem4
            // 
            this.tabItem4.AutoEllipsis = false;
            this.tabItem4.BackColor = System.Drawing.Color.SkyBlue;
            this.tabItem4.BorderColor = System.Drawing.Color.Empty;
            this.tabItem4.ForeColor = System.Drawing.Color.Black;
            this.tabItem4.HighlightBackColor = System.Drawing.Color.Gold;
            this.tabItem4.HighlightForeColor = System.Drawing.Color.Black;
            this.tabItem4.Image = null;
            this.tabItem4.Location = new System.Drawing.Point(0, 0);
            this.tabItem4.Name = "tabItem4";
            this.tabItem4.Selected = false;
            this.tabItem4.SelectedBackColor = System.Drawing.Color.Orange;
            this.tabItem4.SelectedForeColor = System.Drawing.Color.White;
            this.tabItem4.SelectedTabHeigth = 0;
            this.tabItem4.Size = new System.Drawing.Size(150, 26);
            this.tabItem4.TabHeigth = 0;
            this.tabItem4.TabIndex = 0;
            this.tabItem4.TabText = "TabItem1";
            this.tabItem4.TabWidth = 150;
            this.tabItem4.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.tabItem4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            // 
            // tabItem5
            // 
            this.tabItem5.AutoEllipsis = false;
            this.tabItem5.BackColor = System.Drawing.Color.LightPink;
            this.tabItem5.BorderColor = System.Drawing.Color.Empty;
            this.tabItem5.ForeColor = System.Drawing.Color.Black;
            this.tabItem5.HighlightBackColor = System.Drawing.Color.Gold;
            this.tabItem5.HighlightForeColor = System.Drawing.Color.Black;
            this.tabItem5.Image = null;
            this.tabItem5.Location = new System.Drawing.Point(151, 0);
            this.tabItem5.Name = "tabItem5";
            this.tabItem5.Selected = true;
            this.tabItem5.SelectedBackColor = System.Drawing.Color.MediumPurple;
            this.tabItem5.SelectedForeColor = System.Drawing.Color.White;
            this.tabItem5.SelectedTabHeigth = 50;
            this.tabItem5.Size = new System.Drawing.Size(150, 50);
            this.tabItem5.TabHeigth = 0;
            this.tabItem5.TabIndex = 1;
            this.tabItem5.TabText = "TabItem2";
            this.tabItem5.TabWidth = 150;
            this.tabItem5.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.tabItem5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            // 
            // tabItem6
            // 
            this.tabItem6.AutoEllipsis = false;
            this.tabItem6.BackColor = System.Drawing.Color.Gold;
            this.tabItem6.BorderColor = System.Drawing.Color.Empty;
            this.tabItem6.ForeColor = System.Drawing.Color.Black;
            this.tabItem6.HighlightBackColor = System.Drawing.Color.Gold;
            this.tabItem6.HighlightForeColor = System.Drawing.Color.Black;
            this.tabItem6.Image = null;
            this.tabItem6.Location = new System.Drawing.Point(302, 0);
            this.tabItem6.Name = "tabItem6";
            this.tabItem6.Selected = false;
            this.tabItem6.SelectedBackColor = System.Drawing.Color.DeepPink;
            this.tabItem6.SelectedForeColor = System.Drawing.Color.White;
            this.tabItem6.SelectedTabHeigth = 0;
            this.tabItem6.Size = new System.Drawing.Size(150, 26);
            this.tabItem6.TabHeigth = 0;
            this.tabItem6.TabIndex = 2;
            this.tabItem6.TabText = "TabItem3";
            this.tabItem6.TabWidth = 150;
            this.tabItem6.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.tabItem6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            // 
            // tabHost3
            // 
            this.tabHost3.AllowTabReordering = true;
            this.tabHost3.CloseButtonAlignment = TabControl.CloseButtonAlignment.Right;
            this.tabHost3.CloseButtonBorderOpacity = 0;
            this.tabHost3.CloseButtonBorderOpacitySelected = 0;
            this.tabHost3.CloseButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tabHost3.CloseButtonColorSelected = System.Drawing.Color.Empty;
            this.tabHost3.CloseButtonOverColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.tabHost3.CloseButtonOverColorSelected = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.tabHost3.CloseButtonPressedColor = System.Drawing.Color.Red;
            this.tabHost3.CloseButtonsOnlyForSelected = true;
            this.tabHost3.Controls.Add(this.tabItem7);
            this.tabHost3.Controls.Add(this.tabItem8);
            this.tabHost3.Controls.Add(this.tabItem9);
            this.tabHost3.Dock = System.Windows.Forms.DockStyle.Right;
            this.tabHost3.InsertionMarkerColor = System.Drawing.Color.Empty;
            this.tabHost3.Location = new System.Drawing.Point(634, 56);
            this.tabHost3.Name = "tabHost3";
            this.tabHost3.SelectedTabHeight = 30;
            this.tabHost3.ShowCloseButtons = false;
            this.tabHost3.Size = new System.Drawing.Size(44, 348);
            this.tabHost3.TabAlignment = System.Windows.Forms.TabAlignment.Right;
            this.tabHost3.TabDistance = 1;
            this.tabHost3.TabHeight = 26;
            this.tabHost3.TabIndex = 2;
            this.tabHost3.Tabs.Add(this.tabItem7);
            this.tabHost3.Tabs.Add(this.tabItem8);
            this.tabHost3.Tabs.Add(this.tabItem9);
            // 
            // tabHost4
            // 
            this.tabHost4.AllowTabReordering = true;
            this.tabHost4.CloseButtonAlignment = TabControl.CloseButtonAlignment.Right;
            this.tabHost4.CloseButtonBorderOpacity = 128;
            this.tabHost4.CloseButtonBorderOpacitySelected = 255;
            this.tabHost4.CloseButtonColor = System.Drawing.Color.SlateGray;
            this.tabHost4.CloseButtonColorSelected = System.Drawing.Color.Red;
            this.tabHost4.CloseButtonOverColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.tabHost4.CloseButtonOverColorSelected = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.tabHost4.CloseButtonPressedColor = System.Drawing.Color.Red;
            this.tabHost4.CloseButtonsOnlyForSelected = false;
            this.tabHost4.Controls.Add(this.tabItem11);
            this.tabHost4.Controls.Add(this.tabItem10);
            this.tabHost4.Controls.Add(this.tabItem12);
            this.tabHost4.Controls.Add(this.tabItem13);
            this.tabHost4.Controls.Add(this.tabItem14);
            this.tabHost4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabHost4.InsertionMarkerColor = System.Drawing.Color.Empty;
            this.tabHost4.Location = new System.Drawing.Point(50, 359);
            this.tabHost4.Name = "tabHost4";
            this.tabHost4.SelectedTabHeight = 30;
            this.tabHost4.ShowCloseButtons = true;
            this.tabHost4.Size = new System.Drawing.Size(584, 45);
            this.tabHost4.TabAlignment = System.Windows.Forms.TabAlignment.Top;
            this.tabHost4.TabDistance = 1;
            this.tabHost4.TabHeight = 26;
            this.tabHost4.TabIndex = 3;
            this.tabHost4.Tabs.Add(this.tabItem10);
            this.tabHost4.Tabs.Add(this.tabItem11);
            this.tabHost4.Tabs.Add(this.tabItem12);
            this.tabHost4.Tabs.Add(this.tabItem13);
            this.tabHost4.Tabs.Add(this.tabItem14);
            // 
            // tabItem10
            // 
            this.tabItem10.AutoEllipsis = false;
            this.tabItem10.BackColor = System.Drawing.Color.SlateGray;
            this.tabItem10.BorderColor = System.Drawing.Color.Empty;
            this.tabItem10.ForeColor = System.Drawing.Color.White;
            this.tabItem10.HighlightBackColor = System.Drawing.Color.Gold;
            this.tabItem10.HighlightForeColor = System.Drawing.Color.Black;
            this.tabItem10.Image = null;
            this.tabItem10.Location = new System.Drawing.Point(0, 19);
            this.tabItem10.Name = "tabItem10";
            this.tabItem10.Selected = false;
            this.tabItem10.SelectedBackColor = System.Drawing.Color.DeepPink;
            this.tabItem10.SelectedForeColor = System.Drawing.Color.White;
            this.tabItem10.SelectedTabHeigth = 0;
            this.tabItem10.Size = new System.Drawing.Size(100, 26);
            this.tabItem10.TabHeigth = 0;
            this.tabItem10.TabIndex = 0;
            this.tabItem10.TabText = "TabItem1";
            this.tabItem10.TabWidth = 100;
            this.tabItem10.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.tabItem10.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            // 
            // tabItem12
            // 
            this.tabItem12.AutoEllipsis = false;
            this.tabItem12.BackColor = System.Drawing.Color.SlateGray;
            this.tabItem12.BorderColor = System.Drawing.Color.Empty;
            this.tabItem12.ForeColor = System.Drawing.Color.White;
            this.tabItem12.HighlightBackColor = System.Drawing.Color.Gold;
            this.tabItem12.HighlightForeColor = System.Drawing.Color.Black;
            this.tabItem12.Image = null;
            this.tabItem12.Location = new System.Drawing.Point(202, 19);
            this.tabItem12.Name = "tabItem12";
            this.tabItem12.Selected = false;
            this.tabItem12.SelectedBackColor = System.Drawing.Color.DeepPink;
            this.tabItem12.SelectedForeColor = System.Drawing.Color.White;
            this.tabItem12.SelectedTabHeigth = 0;
            this.tabItem12.Size = new System.Drawing.Size(100, 26);
            this.tabItem12.TabHeigth = 0;
            this.tabItem12.TabIndex = 2;
            this.tabItem12.TabText = "TabItem3";
            this.tabItem12.TabWidth = 100;
            this.tabItem12.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.tabItem12.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            // 
            // tabItem13
            // 
            this.tabItem13.AutoEllipsis = false;
            this.tabItem13.BackColor = System.Drawing.Color.SlateGray;
            this.tabItem13.BorderColor = System.Drawing.Color.Empty;
            this.tabItem13.ForeColor = System.Drawing.Color.White;
            this.tabItem13.HighlightBackColor = System.Drawing.Color.Gold;
            this.tabItem13.HighlightForeColor = System.Drawing.Color.Black;
            this.tabItem13.Image = null;
            this.tabItem13.Location = new System.Drawing.Point(303, 19);
            this.tabItem13.Name = "tabItem13";
            this.tabItem13.Selected = false;
            this.tabItem13.SelectedBackColor = System.Drawing.Color.DeepPink;
            this.tabItem13.SelectedForeColor = System.Drawing.Color.White;
            this.tabItem13.SelectedTabHeigth = 0;
            this.tabItem13.Size = new System.Drawing.Size(100, 26);
            this.tabItem13.TabHeigth = 0;
            this.tabItem13.TabIndex = 3;
            this.tabItem13.TabText = "TabItem4";
            this.tabItem13.TabWidth = 100;
            this.tabItem13.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.tabItem13.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            // 
            // tabItem14
            // 
            this.tabItem14.AutoEllipsis = false;
            this.tabItem14.BackColor = System.Drawing.Color.SlateGray;
            this.tabItem14.BorderColor = System.Drawing.Color.Empty;
            this.tabItem14.ForeColor = System.Drawing.Color.White;
            this.tabItem14.HighlightBackColor = System.Drawing.Color.Gold;
            this.tabItem14.HighlightForeColor = System.Drawing.Color.Black;
            this.tabItem14.Image = null;
            this.tabItem14.Location = new System.Drawing.Point(404, 19);
            this.tabItem14.Name = "tabItem14";
            this.tabItem14.Selected = false;
            this.tabItem14.SelectedBackColor = System.Drawing.Color.DeepPink;
            this.tabItem14.SelectedForeColor = System.Drawing.Color.White;
            this.tabItem14.SelectedTabHeigth = 0;
            this.tabItem14.Size = new System.Drawing.Size(100, 26);
            this.tabItem14.TabHeigth = 0;
            this.tabItem14.TabIndex = 4;
            this.tabItem14.TabText = "TabItem5";
            this.tabItem14.TabWidth = 100;
            this.tabItem14.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.tabItem14.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            // 
            // tabItem11
            // 
            this.tabItem11.AutoEllipsis = false;
            this.tabItem11.BackColor = System.Drawing.Color.SlateGray;
            this.tabItem11.BorderColor = System.Drawing.Color.Empty;
            this.tabItem11.ForeColor = System.Drawing.Color.White;
            this.tabItem11.HighlightBackColor = System.Drawing.Color.Gold;
            this.tabItem11.HighlightForeColor = System.Drawing.Color.Black;
            this.tabItem11.Image = global::TabControlTester.Properties.Resources.email;
            this.tabItem11.Location = new System.Drawing.Point(101, 15);
            this.tabItem11.Name = "tabItem11";
            this.tabItem11.Selected = true;
            this.tabItem11.SelectedBackColor = System.Drawing.Color.DeepPink;
            this.tabItem11.SelectedForeColor = System.Drawing.Color.White;
            this.tabItem11.SelectedTabHeigth = 0;
            this.tabItem11.Size = new System.Drawing.Size(100, 30);
            this.tabItem11.TabHeigth = 0;
            this.tabItem11.TabIndex = 1;
            this.tabItem11.TabText = "TabItem2";
            this.tabItem11.TabWidth = 100;
            this.tabItem11.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.tabItem11.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            // 
            // tabItem7
            // 
            this.tabItem7.AutoEllipsis = false;
            this.tabItem7.BackColor = System.Drawing.Color.SlateGray;
            this.tabItem7.BorderColor = System.Drawing.Color.Empty;
            this.tabItem7.ForeColor = System.Drawing.Color.White;
            this.tabItem7.HighlightBackColor = System.Drawing.Color.Gold;
            this.tabItem7.HighlightForeColor = System.Drawing.Color.Black;
            this.tabItem7.Image = global::TabControlTester.Properties.Resources.brick;
            this.tabItem7.Location = new System.Drawing.Point(18, 0);
            this.tabItem7.Name = "tabItem7";
            this.tabItem7.Selected = false;
            this.tabItem7.SelectedBackColor = System.Drawing.Color.DeepPink;
            this.tabItem7.SelectedForeColor = System.Drawing.Color.White;
            this.tabItem7.SelectedTabHeigth = 0;
            this.tabItem7.Size = new System.Drawing.Size(26, 100);
            this.tabItem7.TabHeigth = 0;
            this.tabItem7.TabIndex = 0;
            this.tabItem7.TabText = "TabItem1";
            this.tabItem7.TabWidth = 100;
            this.tabItem7.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.tabItem7.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            // 
            // tabItem8
            // 
            this.tabItem8.AutoEllipsis = false;
            this.tabItem8.BackColor = System.Drawing.Color.SlateGray;
            this.tabItem8.BorderColor = System.Drawing.Color.Empty;
            this.tabItem8.ForeColor = System.Drawing.Color.White;
            this.tabItem8.HighlightBackColor = System.Drawing.Color.Gold;
            this.tabItem8.HighlightForeColor = System.Drawing.Color.Black;
            this.tabItem8.Image = global::TabControlTester.Properties.Resources.key;
            this.tabItem8.Location = new System.Drawing.Point(18, 101);
            this.tabItem8.Name = "tabItem8";
            this.tabItem8.Selected = false;
            this.tabItem8.SelectedBackColor = System.Drawing.Color.DeepPink;
            this.tabItem8.SelectedForeColor = System.Drawing.Color.White;
            this.tabItem8.SelectedTabHeigth = 0;
            this.tabItem8.Size = new System.Drawing.Size(26, 100);
            this.tabItem8.TabHeigth = 0;
            this.tabItem8.TabIndex = 1;
            this.tabItem8.TabText = "TabItem2";
            this.tabItem8.TabWidth = 100;
            this.tabItem8.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.tabItem8.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            // 
            // tabItem9
            // 
            this.tabItem9.AutoEllipsis = false;
            this.tabItem9.BackColor = System.Drawing.Color.SlateGray;
            this.tabItem9.BorderColor = System.Drawing.Color.Empty;
            this.tabItem9.ForeColor = System.Drawing.Color.White;
            this.tabItem9.HighlightBackColor = System.Drawing.Color.Gold;
            this.tabItem9.HighlightForeColor = System.Drawing.Color.Black;
            this.tabItem9.Image = global::TabControlTester.Properties.Resources.world;
            this.tabItem9.Location = new System.Drawing.Point(14, 202);
            this.tabItem9.Margin = new System.Windows.Forms.Padding(0);
            this.tabItem9.Name = "tabItem9";
            this.tabItem9.Selected = true;
            this.tabItem9.SelectedBackColor = System.Drawing.Color.DeepPink;
            this.tabItem9.SelectedForeColor = System.Drawing.Color.White;
            this.tabItem9.SelectedTabHeigth = 0;
            this.tabItem9.Size = new System.Drawing.Size(30, 100);
            this.tabItem9.TabHeigth = 0;
            this.tabItem9.TabIndex = 2;
            this.tabItem9.TabText = "TabItem3";
            this.tabItem9.TabWidth = 100;
            this.tabItem9.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.tabItem9.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 404);
            this.Controls.Add(this.tabHost4);
            this.Controls.Add(this.tabHost3);
            this.Controls.Add(this.tabHost2);
            this.Controls.Add(this.tabHost1);
            this.Name = "Form1";
            this.Text = "TabControl Demonstration | Copyright (c) Gratian Lup";
            this.tabHost1.ResumeLayout(false);
            this.tabHost2.ResumeLayout(false);
            this.tabHost3.ResumeLayout(false);
            this.tabHost4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private TabControl.TabHost tabHost1;
        private TabControl.TabItem tabItem1;
        private TabControl.TabItem tabItem2;
        private TabControl.TabItem tabItem3;
        private TabControl.TabHost tabHost2;
        private TabControl.TabItem tabItem4;
        private TabControl.TabItem tabItem5;
        private TabControl.TabItem tabItem6;
        private TabControl.TabHost tabHost3;
        private TabControl.TabItem tabItem7;
        private TabControl.TabItem tabItem8;
        private TabControl.TabItem tabItem9;
        private TabControl.TabHost tabHost4;
        private TabControl.TabItem tabItem10;
        private TabControl.TabItem tabItem11;
        private TabControl.TabItem tabItem12;
        private TabControl.TabItem tabItem13;
        private TabControl.TabItem tabItem14;
    }
}

