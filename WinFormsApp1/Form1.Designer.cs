namespace WinFormsApp1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.XOY = new System.Windows.Forms.Panel();
            this.Parametrs = new System.Windows.Forms.Panel();
            this.NumNodesText = new System.Windows.Forms.Label();
            this.Reset = new System.Windows.Forms.Button();
            this.NumNodes = new System.Windows.Forms.TextBox();
            this.EnableNodes = new System.Windows.Forms.Button();
            this.GetSolution = new System.Windows.Forms.Button();
            this.Change = new System.Windows.Forms.Button();
            this.Show = new System.Windows.Forms.Button();
            this.qtxt = new System.Windows.Forms.Label();
            this.qval = new System.Windows.Forms.TextBox();
            this.Tval = new System.Windows.Forms.TextBox();
            this.Ttxt = new System.Windows.Forms.Label();
            this.htxt = new System.Windows.Forms.Label();
            this.hval = new System.Windows.Forms.TextBox();
            this.Numtext = new System.Windows.Forms.Label();
            this.NumG = new System.Windows.Forms.TextBox();
            this.Grantext = new System.Windows.Forms.Label();
            this.Qv = new System.Windows.Forms.TextBox();
            this.Qt = new System.Windows.Forms.Label();
            this.DataText = new System.Windows.Forms.Label();
            this.Parametrs.SuspendLayout();
            this.SuspendLayout();
            // 
            // XOY
            // 
            this.XOY.BackColor = System.Drawing.SystemColors.Info;
            this.XOY.Dock = System.Windows.Forms.DockStyle.Left;
            this.XOY.Location = new System.Drawing.Point(0, 0);
            this.XOY.Name = "XOY";
            this.XOY.Size = new System.Drawing.Size(1032, 666);
            this.XOY.TabIndex = 0;
            this.XOY.MouseClick += new System.Windows.Forms.MouseEventHandler(this.XOY_MouseClick);
            // 
            // Parametrs
            // 
            this.Parametrs.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Parametrs.Controls.Add(this.NumNodesText);
            this.Parametrs.Controls.Add(this.Reset);
            this.Parametrs.Controls.Add(this.NumNodes);
            this.Parametrs.Controls.Add(this.EnableNodes);
            this.Parametrs.Controls.Add(this.GetSolution);
            this.Parametrs.Controls.Add(this.Change);
            this.Parametrs.Controls.Add(this.Show);
            this.Parametrs.Controls.Add(this.qtxt);
            this.Parametrs.Controls.Add(this.qval);
            this.Parametrs.Controls.Add(this.Tval);
            this.Parametrs.Controls.Add(this.Ttxt);
            this.Parametrs.Controls.Add(this.htxt);
            this.Parametrs.Controls.Add(this.hval);
            this.Parametrs.Controls.Add(this.Numtext);
            this.Parametrs.Controls.Add(this.NumG);
            this.Parametrs.Controls.Add(this.Grantext);
            this.Parametrs.Controls.Add(this.Qv);
            this.Parametrs.Controls.Add(this.Qt);
            this.Parametrs.Controls.Add(this.DataText);
            this.Parametrs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Parametrs.Location = new System.Drawing.Point(1032, 0);
            this.Parametrs.Name = "Parametrs";
            this.Parametrs.Size = new System.Drawing.Size(410, 666);
            this.Parametrs.TabIndex = 1;
            // 
            // NumNodesText
            // 
            this.NumNodesText.AutoSize = true;
            this.NumNodesText.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.NumNodesText.Location = new System.Drawing.Point(10, 526);
            this.NumNodesText.Name = "NumNodesText";
            this.NumNodesText.Size = new System.Drawing.Size(34, 28);
            this.NumNodesText.TabIndex = 18;
            this.NumNodesText.Text = "№";
            // 
            // Reset
            // 
            this.Reset.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Reset.Location = new System.Drawing.Point(218, 430);
            this.Reset.Name = "Reset";
            this.Reset.Size = new System.Drawing.Size(170, 68);
            this.Reset.TabIndex = 15;
            this.Reset.Text = "Сброс";
            this.Reset.UseVisualStyleBackColor = true;
            // 
            // NumNodes
            // 
            this.NumNodes.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.NumNodes.Location = new System.Drawing.Point(50, 523);
            this.NumNodes.Name = "NumNodes";
            this.NumNodes.Size = new System.Drawing.Size(100, 34);
            this.NumNodes.TabIndex = 17;
            // 
            // EnableNodes
            // 
            this.EnableNodes.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.EnableNodes.Location = new System.Drawing.Point(18, 572);
            this.EnableNodes.Name = "EnableNodes";
            this.EnableNodes.Size = new System.Drawing.Size(170, 68);
            this.EnableNodes.TabIndex = 16;
            this.EnableNodes.Text = "Показать выбранные узлы";
            this.EnableNodes.UseVisualStyleBackColor = true;
            this.EnableNodes.Click += new System.EventHandler(this.EnableNodes_Click);
            // 
            // GetSolution
            // 
            this.GetSolution.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.GetSolution.Location = new System.Drawing.Point(27, 430);
            this.GetSolution.Name = "GetSolution";
            this.GetSolution.Size = new System.Drawing.Size(170, 68);
            this.GetSolution.TabIndex = 14;
            this.GetSolution.Text = "Получить решение";
            this.GetSolution.UseVisualStyleBackColor = true;
            this.GetSolution.Click += new System.EventHandler(this.GetSolution_Click);
            // 
            // Change
            // 
            this.Change.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Change.Location = new System.Drawing.Point(116, 362);
            this.Change.Name = "Change";
            this.Change.Size = new System.Drawing.Size(170, 43);
            this.Change.TabIndex = 13;
            this.Change.Text = "Изменить";
            this.Change.UseVisualStyleBackColor = true;
            this.Change.Click += new System.EventHandler(this.Change_Click);
            // 
            // Show
            // 
            this.Show.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Show.Location = new System.Drawing.Point(116, 280);
            this.Show.Name = "Show";
            this.Show.Size = new System.Drawing.Size(170, 57);
            this.Show.TabIndex = 12;
            this.Show.Text = "Показать";
            this.Show.UseVisualStyleBackColor = true;
            this.Show.Click += new System.EventHandler(this.Show_Click);
            // 
            // qtxt
            // 
            this.qtxt.AutoSize = true;
            this.qtxt.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.qtxt.Location = new System.Drawing.Point(256, 221);
            this.qtxt.Name = "qtxt";
            this.qtxt.Size = new System.Drawing.Size(28, 28);
            this.qtxt.TabIndex = 11;
            this.qtxt.Text = "q:";
            // 
            // qval
            // 
            this.qval.Location = new System.Drawing.Point(293, 229);
            this.qval.Name = "qval";
            this.qval.Size = new System.Drawing.Size(50, 23);
            this.qval.TabIndex = 10;
            this.qval.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Tval
            // 
            this.Tval.Location = new System.Drawing.Point(185, 229);
            this.Tval.Name = "Tval";
            this.Tval.Size = new System.Drawing.Size(50, 23);
            this.Tval.TabIndex = 9;
            this.Tval.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Ttxt
            // 
            this.Ttxt.AutoSize = true;
            this.Ttxt.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Ttxt.Location = new System.Drawing.Point(153, 229);
            this.Ttxt.Name = "Ttxt";
            this.Ttxt.Size = new System.Drawing.Size(26, 28);
            this.Ttxt.TabIndex = 8;
            this.Ttxt.Text = "T:";
            // 
            // htxt
            // 
            this.htxt.AutoSize = true;
            this.htxt.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.htxt.Location = new System.Drawing.Point(44, 229);
            this.htxt.Name = "htxt";
            this.htxt.Size = new System.Drawing.Size(27, 28);
            this.htxt.TabIndex = 7;
            this.htxt.Text = "h:";
            // 
            // hval
            // 
            this.hval.Location = new System.Drawing.Point(77, 234);
            this.hval.Name = "hval";
            this.hval.Size = new System.Drawing.Size(50, 23);
            this.hval.TabIndex = 6;
            this.hval.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Numtext
            // 
            this.Numtext.AutoSize = true;
            this.Numtext.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Numtext.Location = new System.Drawing.Point(116, 161);
            this.Numtext.Name = "Numtext";
            this.Numtext.Size = new System.Drawing.Size(34, 28);
            this.Numtext.TabIndex = 5;
            this.Numtext.Text = "№";
            // 
            // NumG
            // 
            this.NumG.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.NumG.Location = new System.Drawing.Point(153, 161);
            this.NumG.Name = "NumG";
            this.NumG.Size = new System.Drawing.Size(100, 34);
            this.NumG.TabIndex = 4;
            this.NumG.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Grantext
            // 
            this.Grantext.AutoSize = true;
            this.Grantext.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Grantext.Location = new System.Drawing.Point(116, 115);
            this.Grantext.Name = "Grantext";
            this.Grantext.Size = new System.Drawing.Size(168, 28);
            this.Grantext.TabIndex = 3;
            this.Grantext.Text = "Граница области";
            // 
            // Qv
            // 
            this.Qv.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Qv.Location = new System.Drawing.Point(153, 61);
            this.Qv.Name = "Qv";
            this.Qv.Size = new System.Drawing.Size(100, 34);
            this.Qv.TabIndex = 2;
            this.Qv.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Qt
            // 
            this.Qt.AutoSize = true;
            this.Qt.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Qt.Location = new System.Drawing.Point(116, 61);
            this.Qt.Name = "Qt";
            this.Qt.Size = new System.Drawing.Size(31, 28);
            this.Qt.TabIndex = 1;
            this.Qt.Text = "Q:";
            // 
            // DataText
            // 
            this.DataText.AutoSize = true;
            this.DataText.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.DataText.Location = new System.Drawing.Point(93, 9);
            this.DataText.Name = "DataText";
            this.DataText.Size = new System.Drawing.Size(224, 28);
            this.DataText.TabIndex = 0;
            this.DataText.Text = "Введенные параметры:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1442, 666);
            this.Controls.Add(this.Parametrs);
            this.Controls.Add(this.XOY);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Parametrs.ResumeLayout(false);
            this.Parametrs.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel XOY;
        private Panel Parametrs;
        private Label Qt;
        private Label DataText;
        private Button Reset;
        private Button GetSolution;
        private Button Change;
        private Button Show;
        private Label qtxt;
        private TextBox qval;
        private TextBox Tval;
        private Label Ttxt;
        private Label htxt;
        private TextBox hval;
        private Label Numtext;
        private TextBox NumG;
        private Label Grantext;
        private TextBox Qv;
        private Button EnableNodes;
        private TextBox NumNodes;
        private Label NumNodesText;
    }
}