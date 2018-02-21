namespace ERPFramework.Controls
{
    partial class Multiselect
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lsbLeft = new System.Windows.Forms.ListBox();
            this.lsbRight = new System.Windows.Forms.ListBox();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblRight = new System.Windows.Forms.Label();
            this.lblLeft = new System.Windows.Forms.Label();
            this.btnNone = new System.Windows.Forms.Button();
            this.btnMinus = new System.Windows.Forms.Button();
            this.btnAll = new System.Windows.Forms.Button();
            this.btnPlus = new System.Windows.Forms.Button();
            this.pnlTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // lsbLeft
            // 
            this.lsbLeft.AllowDrop = true;
            this.lsbLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.lsbLeft.Location = new System.Drawing.Point(0, 31);
            this.lsbLeft.Name = "lsbLeft";
            this.lsbLeft.Size = new System.Drawing.Size(139, 261);
            this.lsbLeft.TabIndex = 0;
            this.lsbLeft.DragDrop += new System.Windows.Forms.DragEventHandler(this.lsbLeft_DragDrop);
            this.lsbLeft.DragEnter += new System.Windows.Forms.DragEventHandler(this.lsbLeft_DragEnter);
            this.lsbLeft.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lsbLeft_MouseDown);
            // 
            // lsbRight
            // 
            this.lsbRight.AllowDrop = true;
            this.lsbRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.lsbRight.Location = new System.Drawing.Point(204, 31);
            this.lsbRight.Name = "lsbRight";
            this.lsbRight.Size = new System.Drawing.Size(139, 261);
            this.lsbRight.TabIndex = 1;
            this.lsbRight.DragDrop += new System.Windows.Forms.DragEventHandler(this.lsbRight_DragDrop);
            this.lsbRight.DragEnter += new System.Windows.Forms.DragEventHandler(this.lsbRight_DragEnter);
            this.lsbRight.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lsbRight_MouseDown);
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.lblRight);
            this.pnlTop.Controls.Add(this.lblLeft);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(343, 31);
            this.pnlTop.TabIndex = 2;
            // 
            // lblRight
            // 
            this.lblRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblRight.Location = new System.Drawing.Point(302, 0);
            this.lblRight.Name = "lblRight";
            this.lblRight.Size = new System.Drawing.Size(41, 31);
            this.lblRight.TabIndex = 1;
            this.lblRight.Text = "label2";
            this.lblRight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLeft
            // 
            this.lblLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblLeft.Location = new System.Drawing.Point(0, 0);
            this.lblLeft.Name = "lblLeft";
            this.lblLeft.Size = new System.Drawing.Size(41, 31);
            this.lblLeft.TabIndex = 0;
            this.lblLeft.Text = "label1";
            this.lblLeft.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnNone
            // 
            this.btnNone.Location = new System.Drawing.Point(147, 70);
            this.btnNone.Name = "btnNone";
            this.btnNone.Size = new System.Drawing.Size(47, 23);
            this.btnNone.TabIndex = 3;
            this.btnNone.Text = "<<";
            this.btnNone.UseVisualStyleBackColor = true;
            this.btnNone.Click += new System.EventHandler(this.btnNone_Click);
            // 
            // btnMinus
            // 
            this.btnMinus.Location = new System.Drawing.Point(147, 99);
            this.btnMinus.Name = "btnMinus";
            this.btnMinus.Size = new System.Drawing.Size(47, 23);
            this.btnMinus.TabIndex = 4;
            this.btnMinus.Text = "<";
            this.btnMinus.UseVisualStyleBackColor = true;
            this.btnMinus.Click += new System.EventHandler(this.btnMinus_Click);
            // 
            // btnAll
            // 
            this.btnAll.Location = new System.Drawing.Point(147, 157);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(47, 23);
            this.btnAll.TabIndex = 6;
            this.btnAll.Text = ">>";
            this.btnAll.UseVisualStyleBackColor = true;
            this.btnAll.Click += new System.EventHandler(this.btnAll_Click);
            // 
            // btnPlus
            // 
            this.btnPlus.Location = new System.Drawing.Point(147, 128);
            this.btnPlus.Name = "btnPlus";
            this.btnPlus.Size = new System.Drawing.Size(47, 23);
            this.btnPlus.TabIndex = 5;
            this.btnPlus.Text = ">";
            this.btnPlus.UseVisualStyleBackColor = true;
            this.btnPlus.Click += new System.EventHandler(this.btnPlus_Click);
            // 
            // Multiselect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnAll);
            this.Controls.Add(this.btnPlus);
            this.Controls.Add(this.btnMinus);
            this.Controls.Add(this.btnNone);
            this.Controls.Add(this.lsbRight);
            this.Controls.Add(this.lsbLeft);
            this.Controls.Add(this.pnlTop);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Multiselect";
            this.Size = new System.Drawing.Size(343, 292);
            this.pnlTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lsbLeft;
        private System.Windows.Forms.ListBox lsbRight;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblRight;
        private System.Windows.Forms.Label lblLeft;
        private System.Windows.Forms.Button btnNone;
        private System.Windows.Forms.Button btnMinus;
        private System.Windows.Forms.Button btnAll;
        private System.Windows.Forms.Button btnPlus;
    }
}
