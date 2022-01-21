namespace ImgComparer
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbRefFolder = new System.Windows.Forms.GroupBox();
            this.btnSelectRef = new System.Windows.Forms.Button();
            this.txtRefFolder = new System.Windows.Forms.TextBox();
            this.gbTargetFolder = new System.Windows.Forms.GroupBox();
            this.btnSelectTarget = new System.Windows.Forms.Button();
            this.txtTargetFolder = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gbMode = new System.Windows.Forms.GroupBox();
            this.radioClean = new System.Windows.Forms.RadioButton();
            this.radioComparison = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCompare = new System.Windows.Forms.Button();
            this.btnAbort = new System.Windows.Forms.Button();
            this.panelLog = new System.Windows.Forms.Panel();
            this.richTextLog = new System.Windows.Forms.RichTextBox();
            this.progressBarComparer = new System.Windows.Forms.ProgressBar();
            this.gbRefFolder.SuspendLayout();
            this.gbTargetFolder.SuspendLayout();
            this.panel1.SuspendLayout();
            this.gbMode.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbRefFolder
            // 
            this.gbRefFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbRefFolder.Controls.Add(this.btnSelectRef);
            this.gbRefFolder.Controls.Add(this.txtRefFolder);
            this.gbRefFolder.Location = new System.Drawing.Point(3, 94);
            this.gbRefFolder.Name = "gbRefFolder";
            this.gbRefFolder.Size = new System.Drawing.Size(1152, 53);
            this.gbRefFolder.TabIndex = 0;
            this.gbRefFolder.TabStop = false;
            this.gbRefFolder.Text = "Reference folder";
            // 
            // btnSelectRef
            // 
            this.btnSelectRef.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectRef.Location = new System.Drawing.Point(1033, 14);
            this.btnSelectRef.Name = "btnSelectRef";
            this.btnSelectRef.Size = new System.Drawing.Size(113, 32);
            this.btnSelectRef.TabIndex = 1;
            this.btnSelectRef.Text = "Browse";
            this.btnSelectRef.UseVisualStyleBackColor = true;
            this.btnSelectRef.Click += new System.EventHandler(this.btnSelectRef_Click);
            // 
            // txtRefFolder
            // 
            this.txtRefFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRefFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRefFolder.Location = new System.Drawing.Point(8, 19);
            this.txtRefFolder.Name = "txtRefFolder";
            this.txtRefFolder.Size = new System.Drawing.Size(1019, 23);
            this.txtRefFolder.TabIndex = 0;
            this.txtRefFolder.TextChanged += new System.EventHandler(this.TextBox_ChangedCommon);
            this.txtRefFolder.Enter += new System.EventHandler(this.txtBox_CommonFocusEnter);
            this.txtRefFolder.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_CommonKeyDown);
            this.txtRefFolder.Leave += new System.EventHandler(this.txtRefFolder_Leave);
            // 
            // gbTargetFolder
            // 
            this.gbTargetFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbTargetFolder.Controls.Add(this.btnSelectTarget);
            this.gbTargetFolder.Controls.Add(this.txtTargetFolder);
            this.gbTargetFolder.Location = new System.Drawing.Point(3, 153);
            this.gbTargetFolder.Name = "gbTargetFolder";
            this.gbTargetFolder.Size = new System.Drawing.Size(1152, 52);
            this.gbTargetFolder.TabIndex = 1;
            this.gbTargetFolder.TabStop = false;
            this.gbTargetFolder.Text = "Target folder";
            // 
            // btnSelectTarget
            // 
            this.btnSelectTarget.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectTarget.Location = new System.Drawing.Point(1033, 14);
            this.btnSelectTarget.Name = "btnSelectTarget";
            this.btnSelectTarget.Size = new System.Drawing.Size(113, 32);
            this.btnSelectTarget.TabIndex = 1;
            this.btnSelectTarget.Text = "Browse";
            this.btnSelectTarget.UseVisualStyleBackColor = true;
            this.btnSelectTarget.Click += new System.EventHandler(this.btnSelectTarget_Click);
            // 
            // txtTargetFolder
            // 
            this.txtTargetFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTargetFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTargetFolder.Location = new System.Drawing.Point(8, 19);
            this.txtTargetFolder.Name = "txtTargetFolder";
            this.txtTargetFolder.Size = new System.Drawing.Size(1019, 23);
            this.txtTargetFolder.TabIndex = 0;
            this.txtTargetFolder.TextChanged += new System.EventHandler(this.TextBox_ChangedCommon);
            this.txtTargetFolder.Enter += new System.EventHandler(this.txtBox_CommonFocusEnter);
            this.txtTargetFolder.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_CommonKeyDown);
            this.txtTargetFolder.Leave += new System.EventHandler(this.txtTargetFolder_Leave);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.gbMode);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.gbRefFolder);
            this.panel1.Controls.Add(this.gbTargetFolder);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1160, 268);
            this.panel1.TabIndex = 2;
            this.panel1.Click += new System.EventHandler(this.panel1_Click);
            // 
            // gbMode
            // 
            this.gbMode.Controls.Add(this.radioClean);
            this.gbMode.Controls.Add(this.radioComparison);
            this.gbMode.Location = new System.Drawing.Point(4, 3);
            this.gbMode.Name = "gbMode";
            this.gbMode.Size = new System.Drawing.Size(1151, 85);
            this.gbMode.TabIndex = 3;
            this.gbMode.TabStop = false;
            this.gbMode.Text = "Mode";
            // 
            // radioClean
            // 
            this.radioClean.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radioClean.AutoSize = true;
            this.radioClean.Location = new System.Drawing.Point(7, 53);
            this.radioClean.Name = "radioClean";
            this.radioClean.Size = new System.Drawing.Size(198, 17);
            this.radioClean.TabIndex = 1;
            this.radioClean.Text = "Clean one folder (remove duplicated)";
            this.radioClean.UseVisualStyleBackColor = true;
            // 
            // radioComparison
            // 
            this.radioComparison.AutoSize = true;
            this.radioComparison.Checked = true;
            this.radioComparison.Location = new System.Drawing.Point(7, 20);
            this.radioComparison.Name = "radioComparison";
            this.radioComparison.Size = new System.Drawing.Size(176, 17);
            this.radioComparison.TabIndex = 0;
            this.radioComparison.TabStop = true;
            this.radioComparison.Text = "Comparison (remove duplicated)";
            this.radioComparison.UseVisualStyleBackColor = true;
            this.radioComparison.CheckedChanged += new System.EventHandler(this.radioComparison_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btnCompare);
            this.panel2.Controls.Add(this.btnAbort);
            this.panel2.Location = new System.Drawing.Point(319, 215);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(527, 48);
            this.panel2.TabIndex = 2;
            // 
            // btnCompare
            // 
            this.btnCompare.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCompare.Location = new System.Drawing.Point(3, 3);
            this.btnCompare.Name = "btnCompare";
            this.btnCompare.Size = new System.Drawing.Size(119, 41);
            this.btnCompare.TabIndex = 2;
            this.btnCompare.Text = "Compare";
            this.btnCompare.UseVisualStyleBackColor = true;
            this.btnCompare.Click += new System.EventHandler(this.btnCompare_Click);
            // 
            // btnAbort
            // 
            this.btnAbort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAbort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAbort.Location = new System.Drawing.Point(403, 3);
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.Size = new System.Drawing.Size(119, 41);
            this.btnAbort.TabIndex = 3;
            this.btnAbort.Text = "Abort";
            this.btnAbort.UseVisualStyleBackColor = true;
            this.btnAbort.Click += new System.EventHandler(this.btnAbort_Click);
            // 
            // panelLog
            // 
            this.panelLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelLog.Controls.Add(this.richTextLog);
            this.panelLog.Location = new System.Drawing.Point(12, 315);
            this.panelLog.Name = "panelLog";
            this.panelLog.Size = new System.Drawing.Size(1160, 374);
            this.panelLog.TabIndex = 3;
            // 
            // richTextLog
            // 
            this.richTextLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextLog.BackColor = System.Drawing.SystemColors.Window;
            this.richTextLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextLog.Location = new System.Drawing.Point(3, 4);
            this.richTextLog.Name = "richTextLog";
            this.richTextLog.Size = new System.Drawing.Size(1152, 365);
            this.richTextLog.TabIndex = 0;
            this.richTextLog.Text = "";
            // 
            // progressBarComparer
            // 
            this.progressBarComparer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarComparer.Location = new System.Drawing.Point(12, 286);
            this.progressBarComparer.Name = "progressBarComparer";
            this.progressBarComparer.Size = new System.Drawing.Size(1160, 23);
            this.progressBarComparer.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 701);
            this.Controls.Add(this.progressBarComparer);
            this.Controls.Add(this.panelLog);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Picture folder comparer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Click += new System.EventHandler(this.Form1_Click);
            this.gbRefFolder.ResumeLayout(false);
            this.gbRefFolder.PerformLayout();
            this.gbTargetFolder.ResumeLayout(false);
            this.gbTargetFolder.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.gbMode.ResumeLayout(false);
            this.gbMode.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panelLog.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox gbRefFolder;
        private System.Windows.Forms.Button btnSelectRef;
        private System.Windows.Forms.TextBox txtRefFolder;
        private System.Windows.Forms.GroupBox gbTargetFolder;
        private System.Windows.Forms.Button btnSelectTarget;
        private System.Windows.Forms.TextBox txtTargetFolder;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCompare;
        private System.Windows.Forms.Panel panelLog;
        private System.Windows.Forms.ProgressBar progressBarComparer;
        private System.Windows.Forms.Button btnAbort;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RichTextBox richTextLog;
        private System.Windows.Forms.GroupBox gbMode;
        private System.Windows.Forms.RadioButton radioClean;
        private System.Windows.Forms.RadioButton radioComparison;
    }
}

