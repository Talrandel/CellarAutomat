namespace CellarAutomatForm
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
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

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Label labelMessage;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.Label labelShowDelay;
            this.pictureBoxField = new System.Windows.Forms.PictureBox();
            this.buttonBuild = new System.Windows.Forms.Button();
            this.buttonStopBuild = new System.Windows.Forms.Button();
            this.textBoxHint = new System.Windows.Forms.TextBox();
            this.nUDShowDelay = new System.Windows.Forms.NumericUpDown();
            this.buttonStopPlay = new System.Windows.Forms.Button();
            this.buttonPlay = new System.Windows.Forms.Button();
            this.buttonLoad = new System.Windows.Forms.Button();
            labelMessage = new System.Windows.Forms.Label();
            labelShowDelay = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDShowDelay)).BeginInit();
            this.SuspendLayout();
            // 
            // labelMessage
            // 
            resources.ApplyResources(labelMessage, "labelMessage");
            labelMessage.Name = "labelMessage";
            // 
            // labelShowDelay
            // 
            resources.ApplyResources(labelShowDelay, "labelShowDelay");
            labelShowDelay.Name = "labelShowDelay";
            // 
            // pictureBoxField
            // 
            resources.ApplyResources(this.pictureBoxField, "pictureBoxField");
            this.pictureBoxField.Name = "pictureBoxField";
            this.pictureBoxField.TabStop = false;
            // 
            // buttonBuild
            // 
            resources.ApplyResources(this.buttonBuild, "buttonBuild");
            this.buttonBuild.Name = "buttonBuild";
            this.buttonBuild.UseVisualStyleBackColor = true;
            this.buttonBuild.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonStopBuild
            // 
            resources.ApplyResources(this.buttonStopBuild, "buttonStopBuild");
            this.buttonStopBuild.Name = "buttonStopBuild";
            this.buttonStopBuild.UseVisualStyleBackColor = true;
            this.buttonStopBuild.Click += new System.EventHandler(this.buttonStopBuilding_Click);
            // 
            // textBoxHint
            // 
            resources.ApplyResources(this.textBoxHint, "textBoxHint");
            this.textBoxHint.Name = "textBoxHint";
            this.textBoxHint.ReadOnly = true;
            // 
            // nUDShowDelay
            // 
            resources.ApplyResources(this.nUDShowDelay, "nUDShowDelay");
            this.nUDShowDelay.Name = "nUDShowDelay";
            // 
            // buttonStopPlay
            // 
            resources.ApplyResources(this.buttonStopPlay, "buttonStopPlay");
            this.buttonStopPlay.Name = "buttonStopPlay";
            this.buttonStopPlay.UseVisualStyleBackColor = true;
            this.buttonStopPlay.Click += new System.EventHandler(this.buttonStopPlaying_Click);
            // 
            // buttonPlay
            // 
            resources.ApplyResources(this.buttonPlay, "buttonPlay");
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.UseVisualStyleBackColor = true;
            this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
            // 
            // buttonLoad
            // 
            resources.ApplyResources(this.buttonLoad, "buttonLoad");
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.nUDShowDelay);
            this.Controls.Add(this.textBoxHint);
            this.Controls.Add(this.buttonStopPlay);
            this.Controls.Add(this.buttonPlay);
            this.Controls.Add(this.buttonLoad);
            this.Controls.Add(labelMessage);
            this.Controls.Add(this.buttonStopBuild);
            this.Controls.Add(this.buttonBuild);
            this.Controls.Add(this.pictureBoxField);
            this.Controls.Add(labelShowDelay);
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDShowDelay)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.NumericUpDown nUDShowDelay;
        private System.Windows.Forms.PictureBox pictureBoxField;
        private System.Windows.Forms.Button buttonBuild;
        private System.Windows.Forms.Button buttonStopBuild;
        private System.Windows.Forms.TextBox textBoxHint;
        private System.Windows.Forms.Button buttonStopPlay;
        private System.Windows.Forms.Button buttonPlay;
        private System.Windows.Forms.Button buttonLoad;
    }
}