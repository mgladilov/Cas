﻿
namespace CAS.UI.UIControls.ForecastControls
{
    partial class ForecastListScreen
    {
        /// <summary> 
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Обязательный метод для поддержки конструктора - не изменяйте 
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this._buttonComposeWorkPackage = new AvControls.AvButtonT.AvButtonT();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.buttonApplyFilter = new AvControls.AvButtonT.AvButtonT();
            this.labelDateAsOf = new System.Windows.Forms.Label();
            this.labelTitle = new AvControls.StatusImageLink.StatusImageLinkLabel();
			this.buttonAddShowEquipmentAndMaterials = new CAS.UI.Management.Dispatchering.RichReferenceButton();
			this.headerControl.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // headerControl
            // 
            this.headerControl.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.headerControl.ShowForecastButton = true;
            this.headerControl.ShowNoForecastMenuItem = false;
            this.headerControl.ShowPrintButton = true;
            this.headerControl.Size = new System.Drawing.Size(773, 58);
            this.headerControl.ForecastContextMenuClick += new System.EventHandler(this.ForecastMenuClick);
            this.headerControl.ReloadButtonClick += new System.EventHandler(this.HeaderControlButtonReloadClick);
            this.headerControl.PrintButtonDisplayerRequested += new System.EventHandler<CAS.UI.Interfaces.ReferenceEventArgs>(this.HeaderControlButtonPrintDisplayerRequested);
            this.headerControl.Controls.SetChildIndex(this.aircraftHeaderControl1, 0);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 130);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Size = new System.Drawing.Size(777, 205);
            // 
            // aircraftHeaderControl1
            // 
            this.aircraftHeaderControl1.ChildClickable = true;
            this.aircraftHeaderControl1.OperatorClickable = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this._buttonComposeWorkPackage);
			this.flowLayoutPanel1.Controls.Add(this.pictureBox3);
			this.flowLayoutPanel1.Controls.Add(this.buttonAddShowEquipmentAndMaterials);
			this.flowLayoutPanel1.Controls.Add(this.pictureBox2);
            this.flowLayoutPanel1.Controls.Add(this.buttonApplyFilter);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(654, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(117, 62);
            this.flowLayoutPanel1.TabIndex = 3;
            this.flowLayoutPanel1.WrapContents = false;
            //
            // panelTopContainer
            //
            this.panelTopContainer.Controls.Add(this.labelTitle);
            this.panelTopContainer.Controls.Add(this.labelDateAsOf);
            this.panelTopContainer.Controls.Add(this.flowLayoutPanel1);
            // 
            // _buttonComposeWorkPackage
            // 
            this._buttonComposeWorkPackage.ActiveBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this._buttonComposeWorkPackage.ActiveBackgroundImage = null;
            this._buttonComposeWorkPackage.Cursor = System.Windows.Forms.Cursors.Hand;
            this._buttonComposeWorkPackage.FontMain = new System.Drawing.Font("Verdana", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this._buttonComposeWorkPackage.FontSecondary = new System.Drawing.Font("Verdana", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this._buttonComposeWorkPackage.ForeColorMain = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(82)))), ((int)(((byte)(128)))));
            this._buttonComposeWorkPackage.ForeColorSecondary = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(82)))), ((int)(((byte)(128)))));
            this._buttonComposeWorkPackage.Icon = global::CAS.UI.Properties.Resources.WPAddIcon;
            this._buttonComposeWorkPackage.IconLayout = System.Windows.Forms.ImageLayout.Center;
            this._buttonComposeWorkPackage.IconNotEnabled = global::CAS.UI.Properties.Resources.WPAddIconGray;
            this._buttonComposeWorkPackage.Location = new System.Drawing.Point(65, 0);
            this._buttonComposeWorkPackage.Margin = new System.Windows.Forms.Padding(0);
            this._buttonComposeWorkPackage.Name = "_buttonComposeWorkPackage";
            this._buttonComposeWorkPackage.NormalBackgroundImage = null;
            this._buttonComposeWorkPackage.PaddingMain = new System.Windows.Forms.Padding(0);
            this._buttonComposeWorkPackage.PaddingSecondary = new System.Windows.Forms.Padding(0);
            this._buttonComposeWorkPackage.ShowToolTip = true;
            this._buttonComposeWorkPackage.Size = new System.Drawing.Size(52, 57);
            this._buttonComposeWorkPackage.TabIndex = 20;
            this._buttonComposeWorkPackage.TextAlignMain = System.Drawing.ContentAlignment.BottomLeft;
            this._buttonComposeWorkPackage.TextAlignSecondary = System.Drawing.ContentAlignment.TopLeft;
            this._buttonComposeWorkPackage.TextMain = "";
            this._buttonComposeWorkPackage.TextSecondary = "";
            this._buttonComposeWorkPackage.ToolTipText = "Create a work package";
            this._buttonComposeWorkPackage.Click += new System.EventHandler(this.ButtonCreateWorkPakageClick);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = global::CAS.UI.Properties.Resources.SeparatorLine1;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox2.Location = new System.Drawing.Point(56, 4);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(5, 50);
            this.pictureBox2.TabIndex = 15;
            this.pictureBox2.TabStop = false;
			// 
			// pictureBox3
			// 
			this.pictureBox3.BackgroundImage = global::CAS.UI.Properties.Resources.SeparatorLine1;
			this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.pictureBox3.Location = new System.Drawing.Point(56, 4);
			this.pictureBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.pictureBox3.Name = "pictureBox3";
			this.pictureBox3.Size = new System.Drawing.Size(5, 50);
			this.pictureBox3.TabIndex = 15;
			this.pictureBox3.TabStop = false;
			// 
			// buttonApplyFilter
			// 
			this.buttonApplyFilter.ActiveBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.buttonApplyFilter.ActiveBackgroundImage = null;
            this.buttonApplyFilter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonApplyFilter.FontMain = new System.Drawing.Font("Verdana", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.buttonApplyFilter.FontSecondary = new System.Drawing.Font("Verdana", 9.75F);
            this.buttonApplyFilter.ForeColorMain = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(82)))), ((int)(((byte)(128)))));
            this.buttonApplyFilter.ForeColorSecondary = System.Drawing.SystemColors.ControlText;
            this.buttonApplyFilter.Icon = global::CAS.UI.Properties.Resources.ApplyFilterIcon;
            this.buttonApplyFilter.IconLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonApplyFilter.IconNotEnabled = null;
            this.buttonApplyFilter.Location = new System.Drawing.Point(0, 0);
            this.buttonApplyFilter.Margin = new System.Windows.Forms.Padding(0);
            this.buttonApplyFilter.Name = "buttonApplyFilter";
            this.buttonApplyFilter.NormalBackgroundImage = null;
            this.buttonApplyFilter.PaddingMain = new System.Windows.Forms.Padding(0);
            this.buttonApplyFilter.PaddingSecondary = new System.Windows.Forms.Padding(0);
            this.buttonApplyFilter.ShowToolTip = true;
            this.buttonApplyFilter.Size = new System.Drawing.Size(52, 57);
            this.buttonApplyFilter.TabIndex = 18;
            this.buttonApplyFilter.TextAlignMain = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonApplyFilter.TextAlignSecondary = System.Drawing.ContentAlignment.TopLeft;
            this.buttonApplyFilter.TextMain = "";
            this.buttonApplyFilter.TextSecondary = "";
            this.buttonApplyFilter.ToolTipText = "Apply filter";
            this.buttonApplyFilter.Click += new System.EventHandler(this.ButtonApplyFilterClick);
            // 
            // labelDateAsOf
            // 
            this.labelDateAsOf.AutoSize = true;
            this.labelDateAsOf.Font = new System.Drawing.Font("Verdana", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.labelDateAsOf.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
            this.labelDateAsOf.Location = new System.Drawing.Point(57, 30);
            this.labelDateAsOf.Name = "labelDateAsOf";
            this.labelDateAsOf.Size = new System.Drawing.Size(0, 17);
            this.labelDateAsOf.TabIndex = 21;
            // 
            // labelTitle
            // 
            this.labelTitle.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.labelTitle.Enabled = false;
            this.labelTitle.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelTitle.ForeColor = System.Drawing.Color.DimGray;
            this.labelTitle.HoveredLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(163)))), ((int)(((byte)(255)))));
            this.labelTitle.ImageBackColor = System.Drawing.Color.Transparent;
            this.labelTitle.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.labelTitle.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(155)))), ((int)(((byte)(246)))));
            this.labelTitle.LinkMouseCapturedColor = System.Drawing.Color.Empty;
            this.labelTitle.Location = new System.Drawing.Point(21, 2);
            this.labelTitle.Margin = new System.Windows.Forms.Padding(0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(600, 22);
            this.labelTitle.Status = AvControls.Statuses.NotActive;
            this.labelTitle.TabIndex = 16;
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelTitle.TextFont = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			// 
			// buttonAddShowEquipmentAndMaterials
			// 
			this.buttonAddShowEquipmentAndMaterials.ActiveBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
			this.buttonAddShowEquipmentAndMaterials.ActiveBackgroundImage = null;
			this.buttonAddShowEquipmentAndMaterials.Cursor = System.Windows.Forms.Cursors.Hand;
			this.buttonAddShowEquipmentAndMaterials.Displayer = null;
			this.buttonAddShowEquipmentAndMaterials.DisplayerText = "";
			this.buttonAddShowEquipmentAndMaterials.Entity = null;
			this.buttonAddShowEquipmentAndMaterials.FontMain = new System.Drawing.Font("Verdana", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.buttonAddShowEquipmentAndMaterials.FontSecondary = new System.Drawing.Font("Verdana", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.buttonAddShowEquipmentAndMaterials.ForeColorMain = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(82)))), ((int)(((byte)(128)))));
			this.buttonAddShowEquipmentAndMaterials.ForeColorSecondary = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(82)))), ((int)(((byte)(128)))));
			this.buttonAddShowEquipmentAndMaterials.Icon = global::CAS.UI.Properties.Resources.KitsIcon;
			this.buttonAddShowEquipmentAndMaterials.IconLayout = System.Windows.Forms.ImageLayout.Center;
			this.buttonAddShowEquipmentAndMaterials.IconNotEnabled = global::CAS.UI.Properties.Resources.KitsIcon_gray;
			this.buttonAddShowEquipmentAndMaterials.Location = new System.Drawing.Point(63, 0);
			this.buttonAddShowEquipmentAndMaterials.Margin = new System.Windows.Forms.Padding(0);
			this.buttonAddShowEquipmentAndMaterials.Name = "buttonAddShowEquipmentAndMaterials";
			this.buttonAddShowEquipmentAndMaterials.NormalBackgroundImage = null;
			this.buttonAddShowEquipmentAndMaterials.PaddingMain = new System.Windows.Forms.Padding(0);
			this.buttonAddShowEquipmentAndMaterials.PaddingSecondary = new System.Windows.Forms.Padding(0);
			this.buttonAddShowEquipmentAndMaterials.ReflectionType = CAS.UI.Management.Dispatchering.ReflectionTypes.DisplayInNew;
			this.buttonAddShowEquipmentAndMaterials.ShowToolTip = true;
			this.buttonAddShowEquipmentAndMaterials.Size = new System.Drawing.Size(52, 57);
			this.buttonAddShowEquipmentAndMaterials.TabIndex = 19;
			this.buttonAddShowEquipmentAndMaterials.TextAlignMain = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonAddShowEquipmentAndMaterials.TextAlignSecondary = System.Drawing.ContentAlignment.TopLeft;
			this.buttonAddShowEquipmentAndMaterials.TextMain = "";
			this.buttonAddShowEquipmentAndMaterials.TextSecondary = "";
			this.buttonAddShowEquipmentAndMaterials.ToolTipText = "Show Equipment And Materials";
			this.buttonAddShowEquipmentAndMaterials.DisplayerRequested += new System.EventHandler<CAS.UI.Interfaces.ReferenceEventArgs>(this.ButtonShowEquipmentAndMaterialsDisplayerRequested);
			// 
			// ForecastListScreen
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ChildClickable = true;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ForecastListScreen";
            this.OperatorClickable = true;
            this.ShowAircraftStatusPanel = false;
            this.Size = new System.Drawing.Size(777, 383);
            this.headerControl.ResumeLayout(false);
            this.headerControl.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AvControls.StatusImageLink.StatusImageLinkLabel labelTitle;
        private System.Windows.Forms.Label labelDateAsOf;
        private AvControls.AvButtonT.AvButtonT _buttonComposeWorkPackage;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private AvControls.AvButtonT.AvButtonT buttonApplyFilter;
		private CAS.UI.Management.Dispatchering.RichReferenceButton buttonAddShowEquipmentAndMaterials;
	}
}
