namespace DVLD
{
    partial class frmDamagedorLostLicense
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDamagedorLostLicense));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblCreatedBy = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblAppFees = new System.Windows.Forms.Label();
            this.lblAppDate = new System.Windows.Forms.Label();
            this.lblLRAppID = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblOldLID = new System.Windows.Forms.Label();
            this.lblReplacedLId = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.linkLincenseInfo = new System.Windows.Forms.LinkLabel();
            this.linkLHistory = new System.Windows.Forms.LinkLabel();
            this.btnIssue = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.gbReplacement = new System.Windows.Forms.GroupBox();
            this.rbLost = new System.Windows.Forms.RadioButton();
            this.rbDamaged = new System.Windows.Forms.RadioButton();
            this.cntlDriverLicenseWithFilter1 = new DVLD.Controls.cntlDriverLicenseWithFilter();
            this.groupBox1.SuspendLayout();
            this.gbReplacement.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblCreatedBy);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.lblAppFees);
            this.groupBox1.Controls.Add(this.lblAppDate);
            this.groupBox1.Controls.Add(this.lblLRAppID);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.lblOldLID);
            this.groupBox1.Controls.Add(this.lblReplacedLId);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 567);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1119, 146);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Application Info";
            // 
            // lblCreatedBy
            // 
            this.lblCreatedBy.AutoSize = true;
            this.lblCreatedBy.Location = new System.Drawing.Point(749, 105);
            this.lblCreatedBy.Name = "lblCreatedBy";
            this.lblCreatedBy.Size = new System.Drawing.Size(56, 25);
            this.lblCreatedBy.TabIndex = 50;
            this.lblCreatedBy.Text = "????";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Image = ((System.Drawing.Image)(resources.GetObject("label3.Image")));
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label3.Location = new System.Drawing.Point(444, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(271, 25);
            this.label3.TabIndex = 49;
            this.label3.Text = "Replaced License ID:         ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Image = ((System.Drawing.Image)(resources.GetObject("label7.Image")));
            this.label7.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label7.Location = new System.Drawing.Point(25, 105);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(246, 25);
            this.label7.TabIndex = 46;
            this.label7.Text = "Appliaction Fees:           ";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Image = ((System.Drawing.Image)(resources.GetObject("label9.Image")));
            this.label9.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label9.Location = new System.Drawing.Point(25, 35);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(251, 25);
            this.label9.TabIndex = 45;
            this.label9.Text = "L.R.Application ID:          ";
            // 
            // lblAppFees
            // 
            this.lblAppFees.AutoSize = true;
            this.lblAppFees.Location = new System.Drawing.Point(306, 105);
            this.lblAppFees.Name = "lblAppFees";
            this.lblAppFees.Size = new System.Drawing.Size(56, 25);
            this.lblAppFees.TabIndex = 44;
            this.lblAppFees.Text = "????";
            // 
            // lblAppDate
            // 
            this.lblAppDate.AutoSize = true;
            this.lblAppDate.Location = new System.Drawing.Point(307, 70);
            this.lblAppDate.Name = "lblAppDate";
            this.lblAppDate.Size = new System.Drawing.Size(56, 25);
            this.lblAppDate.TabIndex = 42;
            this.lblAppDate.Text = "????";
            // 
            // lblLRAppID
            // 
            this.lblLRAppID.AutoSize = true;
            this.lblLRAppID.Location = new System.Drawing.Point(307, 35);
            this.lblLRAppID.Name = "lblLRAppID";
            this.lblLRAppID.Size = new System.Drawing.Size(56, 25);
            this.lblLRAppID.TabIndex = 41;
            this.lblLRAppID.Text = "????";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Image = ((System.Drawing.Image)(resources.GetObject("label8.Image")));
            this.label8.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label8.Location = new System.Drawing.Point(25, 70);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(249, 25);
            this.label8.TabIndex = 38;
            this.label8.Text = "Application Date:            ";
            // 
            // lblOldLID
            // 
            this.lblOldLID.AutoSize = true;
            this.lblOldLID.Location = new System.Drawing.Point(749, 70);
            this.lblOldLID.Name = "lblOldLID";
            this.lblOldLID.Size = new System.Drawing.Size(56, 25);
            this.lblOldLID.TabIndex = 35;
            this.lblOldLID.Text = "????";
            // 
            // lblReplacedLId
            // 
            this.lblReplacedLId.AutoSize = true;
            this.lblReplacedLId.Location = new System.Drawing.Point(749, 35);
            this.lblReplacedLId.Name = "lblReplacedLId";
            this.lblReplacedLId.Size = new System.Drawing.Size(56, 25);
            this.lblReplacedLId.TabIndex = 34;
            this.lblReplacedLId.Text = "????";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Image = ((System.Drawing.Image)(resources.GetObject("label19.Image")));
            this.label19.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label19.Location = new System.Drawing.Point(444, 105);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(271, 25);
            this.label19.TabIndex = 30;
            this.label19.Text = "Created By:                        ";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Image = ((System.Drawing.Image)(resources.GetObject("label18.Image")));
            this.label18.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label18.Location = new System.Drawing.Point(444, 70);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(269, 25);
            this.label18.TabIndex = 29;
            this.label18.Text = "Old License ID:                  ";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Red;
            this.lblTitle.Location = new System.Drawing.Point(208, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(711, 51);
            this.lblTitle.TabIndex = 15;
            this.lblTitle.Text = "Replacement For Damaged License";
            // 
            // linkLincenseInfo
            // 
            this.linkLincenseInfo.AutoSize = true;
            this.linkLincenseInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLincenseInfo.Location = new System.Drawing.Point(302, 743);
            this.linkLincenseInfo.Name = "linkLincenseInfo";
            this.linkLincenseInfo.Size = new System.Drawing.Size(182, 25);
            this.linkLincenseInfo.TabIndex = 25;
            this.linkLincenseInfo.TabStop = true;
            this.linkLincenseInfo.Text = "Show Licenses Info";
            this.linkLincenseInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLincenseInfo_LinkClicked);
            // 
            // linkLHistory
            // 
            this.linkLHistory.AutoSize = true;
            this.linkLHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLHistory.Location = new System.Drawing.Point(38, 743);
            this.linkLHistory.Name = "linkLHistory";
            this.linkLHistory.Size = new System.Drawing.Size(210, 25);
            this.linkLHistory.TabIndex = 24;
            this.linkLHistory.TabStop = true;
            this.linkLHistory.Text = "Show Licenses History";
            this.linkLHistory.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLHistory_LinkClicked);
            // 
            // btnIssue
            // 
            this.btnIssue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIssue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIssue.Image = ((System.Drawing.Image)(resources.GetObject("btnIssue.Image")));
            this.btnIssue.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnIssue.Location = new System.Drawing.Point(827, 732);
            this.btnIssue.Name = "btnIssue";
            this.btnIssue.Size = new System.Drawing.Size(305, 49);
            this.btnIssue.TabIndex = 23;
            this.btnIssue.Text = "Issue Replacement";
            this.btnIssue.UseVisualStyleBackColor = true;
            this.btnIssue.Click += new System.EventHandler(this.btnIssue_Click);
            // 
            // btnClose
            // 
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(666, 732);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(151, 49);
            this.btnClose.TabIndex = 22;
            this.btnClose.Text = " Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // gbReplacement
            // 
            this.gbReplacement.Controls.Add(this.rbLost);
            this.gbReplacement.Controls.Add(this.rbDamaged);
            this.gbReplacement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gbReplacement.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbReplacement.Location = new System.Drawing.Point(827, 74);
            this.gbReplacement.Name = "gbReplacement";
            this.gbReplacement.Size = new System.Drawing.Size(306, 91);
            this.gbReplacement.TabIndex = 26;
            this.gbReplacement.TabStop = false;
            this.gbReplacement.Text = "Replacement For:";
            // 
            // rbLost
            // 
            this.rbLost.AutoSize = true;
            this.rbLost.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbLost.Location = new System.Drawing.Point(25, 59);
            this.rbLost.Name = "rbLost";
            this.rbLost.Size = new System.Drawing.Size(132, 26);
            this.rbLost.TabIndex = 1;
            this.rbLost.TabStop = true;
            this.rbLost.Text = "Lost License";
            this.rbLost.UseVisualStyleBackColor = true;
            this.rbLost.CheckedChanged += new System.EventHandler(this.rbLost_CheckedChanged);
            // 
            // rbDamaged
            // 
            this.rbDamaged.AutoSize = true;
            this.rbDamaged.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbDamaged.Location = new System.Drawing.Point(25, 29);
            this.rbDamaged.Name = "rbDamaged";
            this.rbDamaged.Size = new System.Drawing.Size(175, 26);
            this.rbDamaged.TabIndex = 0;
            this.rbDamaged.TabStop = true;
            this.rbDamaged.Text = "Damaged License";
            this.rbDamaged.UseVisualStyleBackColor = true;
            this.rbDamaged.CheckedChanged += new System.EventHandler(this.rbDamaged_CheckedChanged);
            // 
            // cntlDriverLicenseWithFilter1
            // 
            this.cntlDriverLicenseWithFilter1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.cntlDriverLicenseWithFilter1.Location = new System.Drawing.Point(12, 74);
            this.cntlDriverLicenseWithFilter1.Name = "cntlDriverLicenseWithFilter1";
            this.cntlDriverLicenseWithFilter1.Size = new System.Drawing.Size(1121, 487);
            this.cntlDriverLicenseWithFilter1.TabIndex = 16;
            this.cntlDriverLicenseWithFilter1.OnLicenseSelected += new System.Action<int>(this.cntlDriverLicenseWithFilter1_OnLicenseSelected);
            // 
            // frmDamagedorLostLicense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(1143, 796);
            this.Controls.Add(this.gbReplacement);
            this.Controls.Add(this.linkLincenseInfo);
            this.Controls.Add(this.linkLHistory);
            this.Controls.Add(this.btnIssue);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cntlDriverLicenseWithFilter1);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmDamagedorLostLicense";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Replacement For Damaged License";
            this.Load += new System.EventHandler(this.frmDamagedorLostLicense_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbReplacement.ResumeLayout(false);
            this.gbReplacement.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblCreatedBy;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblAppFees;
        private System.Windows.Forms.Label lblAppDate;
        private System.Windows.Forms.Label lblLRAppID;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblOldLID;
        private System.Windows.Forms.Label lblReplacedLId;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private Controls.cntlDriverLicenseWithFilter cntlDriverLicenseWithFilter1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.LinkLabel linkLincenseInfo;
        private System.Windows.Forms.LinkLabel linkLHistory;
        private System.Windows.Forms.Button btnIssue;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox gbReplacement;
        private System.Windows.Forms.RadioButton rbLost;
        private System.Windows.Forms.RadioButton rbDamaged;
    }
}