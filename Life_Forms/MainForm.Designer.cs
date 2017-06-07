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
            this.pictureBoxField = new System.Windows.Forms.PictureBox();
            this.buttonBuild = new System.Windows.Forms.Button();
            this.buttonStopBuild = new System.Windows.Forms.Button();
            this.comboBoxRules = new System.Windows.Forms.ComboBox();
            this.textBoxWidth = new System.Windows.Forms.TextBox();
            this.textBoxHeight = new System.Windows.Forms.TextBox();
            this.labelWidth = new System.Windows.Forms.Label();
            this.labelHeight = new System.Windows.Forms.Label();
            this.labelMessage = new System.Windows.Forms.Label();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.buttonPlay = new System.Windows.Forms.Button();
            this.textBoxDencity = new System.Windows.Forms.TextBox();
            this.labelDencity = new System.Windows.Forms.Label();
            this.labelShowDelay = new System.Windows.Forms.Label();
            this.textBoxShowDelay = new System.Windows.Forms.TextBox();
            this.buttonStopPlay = new System.Windows.Forms.Button();
            this.textBoxSeparator = new System.Windows.Forms.TextBox();
            this.textBoxHint = new System.Windows.Forms.TextBox();
            this.labelCALoaded = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxField)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxField
            // 
            this.pictureBoxField.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxField.Location = new System.Drawing.Point(12, 12);
            this.pictureBoxField.Name = "pictureBoxField";
            this.pictureBoxField.Size = new System.Drawing.Size(500, 500);
            this.pictureBoxField.TabIndex = 0;
            this.pictureBoxField.TabStop = false;
            // 
            // buttonBuild
            // 
            this.buttonBuild.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBuild.Location = new System.Drawing.Point(616, 158);
            this.buttonBuild.Name = "buttonBuild";
            this.buttonBuild.Size = new System.Drawing.Size(75, 23);
            this.buttonBuild.TabIndex = 7;
            this.buttonBuild.Text = "Построить";
            this.buttonBuild.UseVisualStyleBackColor = true;
            this.buttonBuild.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonStopBuild
            // 
            this.buttonStopBuild.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStopBuild.Location = new System.Drawing.Point(717, 158);
            this.buttonStopBuild.Name = "buttonStopBuild";
            this.buttonStopBuild.Size = new System.Drawing.Size(153, 23);
            this.buttonStopBuild.TabIndex = 8;
            this.buttonStopBuild.Text = "Остановить построение";
            this.buttonStopBuild.UseVisualStyleBackColor = true;
            this.buttonStopBuild.Click += new System.EventHandler(this.buttonStopBuilding_Click);
            // 
            // comboBoxRules
            // 
            this.comboBoxRules.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxRules.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxRules.FormattingEnabled = true;
            this.comboBoxRules.Location = new System.Drawing.Point(693, 117);
            this.comboBoxRules.Name = "comboBoxRules";
            this.comboBoxRules.Size = new System.Drawing.Size(177, 21);
            this.comboBoxRules.TabIndex = 6;
            // 
            // textBoxWidth
            // 
            this.textBoxWidth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxWidth.Location = new System.Drawing.Point(770, 12);
            this.textBoxWidth.Name = "textBoxWidth";
            this.textBoxWidth.Size = new System.Drawing.Size(100, 20);
            this.textBoxWidth.TabIndex = 1;
            this.textBoxWidth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress_SkipLetters);
            // 
            // textBoxHeight
            // 
            this.textBoxHeight.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxHeight.Location = new System.Drawing.Point(770, 38);
            this.textBoxHeight.Name = "textBoxHeight";
            this.textBoxHeight.Size = new System.Drawing.Size(100, 20);
            this.textBoxHeight.TabIndex = 2;
            // 
            // labelWidth
            // 
            this.labelWidth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelWidth.AutoSize = true;
            this.labelWidth.Location = new System.Drawing.Point(694, 18);
            this.labelWidth.Name = "labelWidth";
            this.labelWidth.Size = new System.Drawing.Size(46, 13);
            this.labelWidth.TabIndex = 4;
            this.labelWidth.Text = "Ширина";
            // 
            // labelHeight
            // 
            this.labelHeight.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelHeight.AutoSize = true;
            this.labelHeight.Location = new System.Drawing.Point(694, 41);
            this.labelHeight.Name = "labelHeight";
            this.labelHeight.Size = new System.Drawing.Size(45, 13);
            this.labelHeight.TabIndex = 4;
            this.labelHeight.Text = "Высота";
            // 
            // labelMessage
            // 
            this.labelMessage.AutoSize = true;
            this.labelMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelMessage.Location = new System.Drawing.Point(538, 205);
            this.labelMessage.Name = "labelMessage";
            this.labelMessage.Size = new System.Drawing.Size(0, 20);
            this.labelMessage.TabIndex = 5;
            // 
            // buttonLoad
            // 
            this.buttonLoad.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonLoad.Location = new System.Drawing.Point(616, 284);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(75, 23);
            this.buttonLoad.TabIndex = 9;
            this.buttonLoad.Text = "Загрузить";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // buttonPlay
            // 
            this.buttonPlay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPlay.Location = new System.Drawing.Point(717, 284);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(153, 23);
            this.buttonPlay.TabIndex = 10;
            this.buttonPlay.Text = "Воспроизвести";
            this.buttonPlay.UseVisualStyleBackColor = true;
            this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
            // 
            // textBoxDencity
            // 
            this.textBoxDencity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDencity.Location = new System.Drawing.Point(770, 65);
            this.textBoxDencity.Name = "textBoxDencity";
            this.textBoxDencity.Size = new System.Drawing.Size(100, 20);
            this.textBoxDencity.TabIndex = 3;
            // 
            // labelDencity
            // 
            this.labelDencity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelDencity.AutoSize = true;
            this.labelDencity.Location = new System.Drawing.Point(694, 68);
            this.labelDencity.Name = "labelDencity";
            this.labelDencity.Size = new System.Drawing.Size(61, 13);
            this.labelDencity.TabIndex = 4;
            this.labelDencity.Text = "Плотность";
            // 
            // labelShowDelay
            // 
            this.labelShowDelay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelShowDelay.AutoSize = true;
            this.labelShowDelay.Location = new System.Drawing.Point(613, 94);
            this.labelShowDelay.Name = "labelShowDelay";
            this.labelShowDelay.Size = new System.Drawing.Size(151, 13);
            this.labelShowDelay.TabIndex = 4;
            this.labelShowDelay.Text = "Задержка воспроизведения";
            // 
            // textBoxShowDelay
            // 
            this.textBoxShowDelay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxShowDelay.Location = new System.Drawing.Point(770, 91);
            this.textBoxShowDelay.Name = "textBoxShowDelay";
            this.textBoxShowDelay.Size = new System.Drawing.Size(100, 20);
            this.textBoxShowDelay.TabIndex = 5;
            // 
            // buttonStopPlay
            // 
            this.buttonStopPlay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStopPlay.Location = new System.Drawing.Point(717, 335);
            this.buttonStopPlay.Name = "buttonStopPlay";
            this.buttonStopPlay.Size = new System.Drawing.Size(153, 23);
            this.buttonStopPlay.TabIndex = 11;
            this.buttonStopPlay.Text = "Остановить воспроизведение";
            this.buttonStopPlay.UseVisualStyleBackColor = true;
            this.buttonStopPlay.Click += new System.EventHandler(this.buttonStopPlaying_Click);
            // 
            // textBoxSeparator
            // 
            this.textBoxSeparator.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSeparator.BackColor = System.Drawing.Color.DarkOrange;
            this.textBoxSeparator.Enabled = false;
            this.textBoxSeparator.Location = new System.Drawing.Point(519, -2);
            this.textBoxSeparator.Multiline = true;
            this.textBoxSeparator.Name = "textBoxSeparator";
            this.textBoxSeparator.ReadOnly = true;
            this.textBoxSeparator.Size = new System.Drawing.Size(13, 533);
            this.textBoxSeparator.TabIndex = 12;
            // 
            // textBoxHint
            // 
            this.textBoxHint.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxHint.Enabled = false;
            this.textBoxHint.Location = new System.Drawing.Point(616, 368);
            this.textBoxHint.Multiline = true;
            this.textBoxHint.Name = "textBoxHint";
            this.textBoxHint.ReadOnly = true;
            this.textBoxHint.Size = new System.Drawing.Size(254, 146);
            this.textBoxHint.TabIndex = 13;
            // 
            // labelCALoaded
            // 
            this.labelCALoaded.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCALoaded.AutoSize = true;
            this.labelCALoaded.Location = new System.Drawing.Point(539, 310);
            this.labelCALoaded.Name = "labelCALoaded";
            this.labelCALoaded.Size = new System.Drawing.Size(0, 13);
            this.labelCALoaded.TabIndex = 4;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(895, 526);
            this.Controls.Add(this.textBoxHint);
            this.Controls.Add(this.textBoxSeparator);
            this.Controls.Add(this.buttonStopPlay);
            this.Controls.Add(this.textBoxShowDelay);
            this.Controls.Add(this.textBoxDencity);
            this.Controls.Add(this.buttonPlay);
            this.Controls.Add(this.buttonLoad);
            this.Controls.Add(this.labelCALoaded);
            this.Controls.Add(this.labelShowDelay);
            this.Controls.Add(this.labelMessage);
            this.Controls.Add(this.labelDencity);
            this.Controls.Add(this.labelHeight);
            this.Controls.Add(this.labelWidth);
            this.Controls.Add(this.textBoxHeight);
            this.Controls.Add(this.textBoxWidth);
            this.Controls.Add(this.comboBoxRules);
            this.Controls.Add(this.buttonStopBuild);
            this.Controls.Add(this.buttonBuild);
            this.Controls.Add(this.pictureBoxField);
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.Text = "Клеточные автоматы";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxField)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.PictureBox pictureBoxField;
        public System.Windows.Forms.Button buttonBuild;
        public System.Windows.Forms.Button buttonStopBuild;
        public System.Windows.Forms.ComboBox comboBoxRules;
        public System.Windows.Forms.TextBox textBoxWidth;
        public System.Windows.Forms.TextBox textBoxHeight;
        public System.Windows.Forms.Label labelWidth;
        public System.Windows.Forms.Label labelHeight;
        public System.Windows.Forms.Button buttonLoad;
        public System.Windows.Forms.Button buttonPlay;
        public System.Windows.Forms.TextBox textBoxDencity;
        public System.Windows.Forms.Label labelDencity;
        public System.Windows.Forms.Label labelShowDelay;
        public System.Windows.Forms.TextBox textBoxShowDelay;
        public System.Windows.Forms.Button buttonStopPlay;
        public System.Windows.Forms.TextBox textBoxSeparator;
        public System.Windows.Forms.TextBox textBoxHint;
        public System.Windows.Forms.Label labelMessage;
        public System.Windows.Forms.Label labelCALoaded;
    }
}