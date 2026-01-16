namespace Lab01
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
            this.UI_Label = new System.Windows.Forms.Label();
            this.UI_Solvecol_btn = new System.Windows.Forms.Button();
            this.UI_deadcol_btn = new System.Windows.Forms.Button();
            this.UI_sleeptime_lbl = new System.Windows.Forms.Label();
            this.UI_listbox = new System.Windows.Forms.ListBox();
            this.UI_solve_btn = new System.Windows.Forms.Button();
            this.UI_solvecolor_dialog = new System.Windows.Forms.ColorDialog();
            this.UI_deadcolor_dialog = new System.Windows.Forms.ColorDialog();
            this.SuspendLayout();
            // 
            // UI_Label
            // 
            this.UI_Label.AllowDrop = true;
            this.UI_Label.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.UI_Label.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.UI_Label.Location = new System.Drawing.Point(12, 9);
            this.UI_Label.Name = "UI_Label";
            this.UI_Label.Size = new System.Drawing.Size(776, 92);
            this.UI_Label.TabIndex = 0;
            this.UI_Label.Text = "Drop Maps Here!";
            this.UI_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.UI_Label.DragDrop += new System.Windows.Forms.DragEventHandler(this.UI_Label_DragDrop);
            this.UI_Label.DragEnter += new System.Windows.Forms.DragEventHandler(this.UI_Label_DragEnter);
            // 
            // UI_Solvecol_btn
            // 
            this.UI_Solvecol_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.UI_Solvecol_btn.Location = new System.Drawing.Point(15, 118);
            this.UI_Solvecol_btn.Name = "UI_Solvecol_btn";
            this.UI_Solvecol_btn.Size = new System.Drawing.Size(168, 54);
            this.UI_Solvecol_btn.TabIndex = 1;
            this.UI_Solvecol_btn.Text = "Solve Color";
            this.UI_Solvecol_btn.UseVisualStyleBackColor = false;
            this.UI_Solvecol_btn.Click += new System.EventHandler(this.UI_Solvecol_btn_Click);
            // 
            // UI_deadcol_btn
            // 
            this.UI_deadcol_btn.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.UI_deadcol_btn.Location = new System.Drawing.Point(15, 188);
            this.UI_deadcol_btn.Name = "UI_deadcol_btn";
            this.UI_deadcol_btn.Size = new System.Drawing.Size(168, 54);
            this.UI_deadcol_btn.TabIndex = 2;
            this.UI_deadcol_btn.Text = "Dead Color";
            this.UI_deadcol_btn.UseVisualStyleBackColor = false;
            this.UI_deadcol_btn.Click += new System.EventHandler(this.UI_deadcol_btn_Click);
            // 
            // UI_sleeptime_lbl
            // 
            this.UI_sleeptime_lbl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.UI_sleeptime_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UI_sleeptime_lbl.Location = new System.Drawing.Point(190, 155);
            this.UI_sleeptime_lbl.Name = "UI_sleeptime_lbl";
            this.UI_sleeptime_lbl.Size = new System.Drawing.Size(120, 55);
            this.UI_sleeptime_lbl.TabIndex = 3;
            this.UI_sleeptime_lbl.Text = "15ms";
            this.UI_sleeptime_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UI_listbox
            // 
            this.UI_listbox.FormattingEnabled = true;
            this.UI_listbox.ItemHeight = 16;
            this.UI_listbox.Location = new System.Drawing.Point(15, 259);
            this.UI_listbox.Name = "UI_listbox";
            this.UI_listbox.Size = new System.Drawing.Size(773, 180);
            this.UI_listbox.TabIndex = 5;
            // 
            // UI_solve_btn
            // 
            this.UI_solve_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UI_solve_btn.Location = new System.Drawing.Point(336, 118);
            this.UI_solve_btn.Name = "UI_solve_btn";
            this.UI_solve_btn.Size = new System.Drawing.Size(452, 124);
            this.UI_solve_btn.TabIndex = 6;
            this.UI_solve_btn.Text = "Solve";
            this.UI_solve_btn.UseVisualStyleBackColor = true;
            this.UI_solve_btn.Click += new System.EventHandler(this.UI_solve_btn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.UI_solve_btn);
            this.Controls.Add(this.UI_listbox);
            this.Controls.Add(this.UI_sleeptime_lbl);
            this.Controls.Add(this.UI_deadcol_btn);
            this.Controls.Add(this.UI_Solvecol_btn);
            this.Controls.Add(this.UI_Label);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label UI_Label;
        private System.Windows.Forms.Button UI_Solvecol_btn;
        private System.Windows.Forms.Button UI_deadcol_btn;
        private System.Windows.Forms.Label UI_sleeptime_lbl;
        private System.Windows.Forms.ListBox UI_listbox;
        private System.Windows.Forms.Button UI_solve_btn;
        private System.Windows.Forms.ColorDialog UI_solvecolor_dialog;
        private System.Windows.Forms.ColorDialog UI_deadcolor_dialog;
    }
}

