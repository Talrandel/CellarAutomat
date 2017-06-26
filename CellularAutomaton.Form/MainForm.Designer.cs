namespace CellarAutomatForm
{
    partial class MainForm
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
            this.cellularAutomatonRecorder1 = new CellularAutomaton.Components.Recorder.CellularAutomatonRecorder();
            this.SuspendLayout();
            // 
            // cellularAutomatonRecorder1
            // 
            this.cellularAutomatonRecorder1.AutoSize = true;
            this.cellularAutomatonRecorder1.FileExtension = "bin";
            this.cellularAutomatonRecorder1.FileFilter = "Двоичные файлы (*.bin)|*.bin";
            this.cellularAutomatonRecorder1.FileName = "record";
            this.cellularAutomatonRecorder1.Location = new System.Drawing.Point(12, 12);
            this.cellularAutomatonRecorder1.Name = "cellularAutomatonRecorder1";
            this.cellularAutomatonRecorder1.Size = new System.Drawing.Size(256, 239);
            this.cellularAutomatonRecorder1.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(677, 290);
            this.Controls.Add(this.cellularAutomatonRecorder1);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CellularAutomaton.Components.Recorder.CellularAutomatonRecorder cellularAutomatonRecorder1;
    }
}