namespace CellarAutomatForm.Components
{
    partial class Recorder
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

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.GroupBox gBSettings;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Recorder));
            System.Windows.Forms.TableLayoutPanel tLPSettings;
            System.Windows.Forms.GroupBox gBSettingsField;
            System.Windows.Forms.TableLayoutPanel tLPSettingsField;
            System.Windows.Forms.Label lHeight;
            System.Windows.Forms.Label lWidth;
            System.Windows.Forms.GroupBox gBSettingsCellularAutomaton;
            System.Windows.Forms.TableLayoutPanel tLPSettingsCellularAutomaton;
            System.Windows.Forms.Label lRule;
            System.Windows.Forms.Label lDencity;
            System.Windows.Forms.Label lStatesCount;
            this.nUDHeight = new System.Windows.Forms.NumericUpDown();
            this.nUDWidth = new System.Windows.Forms.NumericUpDown();
            this.cBCellularAutomatonRules = new System.Windows.Forms.ComboBox();
            this.nUDStatesCount = new System.Windows.Forms.NumericUpDown();
            this.nUDDencity = new System.Windows.Forms.NumericUpDown();
            this.tLPMain = new System.Windows.Forms.TableLayoutPanel();
            this.bStop = new System.Windows.Forms.Button();
            this.bRecord = new System.Windows.Forms.Button();
            this.bSave = new System.Windows.Forms.Button();
            gBSettings = new System.Windows.Forms.GroupBox();
            tLPSettings = new System.Windows.Forms.TableLayoutPanel();
            gBSettingsField = new System.Windows.Forms.GroupBox();
            tLPSettingsField = new System.Windows.Forms.TableLayoutPanel();
            lHeight = new System.Windows.Forms.Label();
            lWidth = new System.Windows.Forms.Label();
            gBSettingsCellularAutomaton = new System.Windows.Forms.GroupBox();
            tLPSettingsCellularAutomaton = new System.Windows.Forms.TableLayoutPanel();
            lRule = new System.Windows.Forms.Label();
            lDencity = new System.Windows.Forms.Label();
            lStatesCount = new System.Windows.Forms.Label();
            gBSettings.SuspendLayout();
            tLPSettings.SuspendLayout();
            gBSettingsField.SuspendLayout();
            tLPSettingsField.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUDHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDWidth)).BeginInit();
            gBSettingsCellularAutomaton.SuspendLayout();
            tLPSettingsCellularAutomaton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUDStatesCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDDencity)).BeginInit();
            this.tLPMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // gBSettings
            // 
            resources.ApplyResources(gBSettings, "gBSettings");
            this.tLPMain.SetColumnSpan(gBSettings, 2);
            gBSettings.Controls.Add(tLPSettings);
            gBSettings.Name = "gBSettings";
            gBSettings.TabStop = false;
            // 
            // tLPSettings
            // 
            resources.ApplyResources(tLPSettings, "tLPSettings");
            tLPSettings.Controls.Add(gBSettingsField, 0, 0);
            tLPSettings.Controls.Add(gBSettingsCellularAutomaton, 0, 1);
            tLPSettings.Name = "tLPSettings";
            // 
            // gBSettingsField
            // 
            resources.ApplyResources(gBSettingsField, "gBSettingsField");
            gBSettingsField.Controls.Add(tLPSettingsField);
            gBSettingsField.Name = "gBSettingsField";
            gBSettingsField.TabStop = false;
            // 
            // tLPSettingsField
            // 
            resources.ApplyResources(tLPSettingsField, "tLPSettingsField");
            tLPSettingsField.Controls.Add(lHeight, 0, 1);
            tLPSettingsField.Controls.Add(lWidth, 0, 0);
            tLPSettingsField.Controls.Add(this.nUDHeight, 1, 1);
            tLPSettingsField.Controls.Add(this.nUDWidth, 1, 0);
            tLPSettingsField.Name = "tLPSettingsField";
            // 
            // lHeight
            // 
            resources.ApplyResources(lHeight, "lHeight");
            lHeight.Name = "lHeight";
            // 
            // lWidth
            // 
            resources.ApplyResources(lWidth, "lWidth");
            lWidth.Name = "lWidth";
            // 
            // nUDHeight
            // 
            resources.ApplyResources(this.nUDHeight, "nUDHeight");
            this.nUDHeight.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nUDHeight.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nUDHeight.Name = "nUDHeight";
            this.nUDHeight.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // nUDWidth
            // 
            resources.ApplyResources(this.nUDWidth, "nUDWidth");
            this.nUDWidth.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nUDWidth.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nUDWidth.Name = "nUDWidth";
            this.nUDWidth.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // gBSettingsCellularAutomaton
            // 
            resources.ApplyResources(gBSettingsCellularAutomaton, "gBSettingsCellularAutomaton");
            gBSettingsCellularAutomaton.Controls.Add(tLPSettingsCellularAutomaton);
            gBSettingsCellularAutomaton.Name = "gBSettingsCellularAutomaton";
            gBSettingsCellularAutomaton.TabStop = false;
            // 
            // tLPSettingsCellularAutomaton
            // 
            resources.ApplyResources(tLPSettingsCellularAutomaton, "tLPSettingsCellularAutomaton");
            tLPSettingsCellularAutomaton.Controls.Add(lRule, 0, 2);
            tLPSettingsCellularAutomaton.Controls.Add(this.cBCellularAutomatonRules, 1, 2);
            tLPSettingsCellularAutomaton.Controls.Add(this.nUDStatesCount, 1, 1);
            tLPSettingsCellularAutomaton.Controls.Add(lDencity, 0, 0);
            tLPSettingsCellularAutomaton.Controls.Add(this.nUDDencity, 1, 0);
            tLPSettingsCellularAutomaton.Controls.Add(lStatesCount, 0, 1);
            tLPSettingsCellularAutomaton.Name = "tLPSettingsCellularAutomaton";
            // 
            // lRule
            // 
            resources.ApplyResources(lRule, "lRule");
            lRule.Name = "lRule";
            // 
            // cBCellularAutomatonRules
            // 
            resources.ApplyResources(this.cBCellularAutomatonRules, "cBCellularAutomatonRules");
            this.cBCellularAutomatonRules.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBCellularAutomatonRules.FormattingEnabled = true;
            this.cBCellularAutomatonRules.Name = "cBCellularAutomatonRules";
            // 
            // nUDStatesCount
            // 
            resources.ApplyResources(this.nUDStatesCount, "nUDStatesCount");
            this.nUDStatesCount.Name = "nUDStatesCount";
            // 
            // lDencity
            // 
            resources.ApplyResources(lDencity, "lDencity");
            lDencity.Name = "lDencity";
            // 
            // nUDDencity
            // 
            resources.ApplyResources(this.nUDDencity, "nUDDencity");
            this.nUDDencity.Name = "nUDDencity";
            // 
            // lStatesCount
            // 
            resources.ApplyResources(lStatesCount, "lStatesCount");
            lStatesCount.Name = "lStatesCount";
            // 
            // tLPMain
            // 
            resources.ApplyResources(this.tLPMain, "tLPMain");
            this.tLPMain.Controls.Add(gBSettings, 0, 0);
            this.tLPMain.Controls.Add(this.bStop, 1, 1);
            this.tLPMain.Controls.Add(this.bRecord, 0, 1);
            this.tLPMain.Controls.Add(this.bSave, 0, 2);
            this.tLPMain.Name = "tLPMain";
            // 
            // bStop
            // 
            resources.ApplyResources(this.bStop, "bStop");
            this.bStop.Name = "bStop";
            this.bStop.UseVisualStyleBackColor = true;
            this.bStop.Click += new System.EventHandler(this.bStop_Click);
            // 
            // bRecord
            // 
            resources.ApplyResources(this.bRecord, "bRecord");
            this.bRecord.Name = "bRecord";
            this.bRecord.UseVisualStyleBackColor = true;
            this.bRecord.Click += new System.EventHandler(this.bRecord_Click);
            // 
            // bSave
            // 
            resources.ApplyResources(this.bSave, "bSave");
            this.tLPMain.SetColumnSpan(this.bSave, 2);
            this.bSave.Name = "bSave";
            this.bSave.UseVisualStyleBackColor = true;
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            // 
            // Recorder
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tLPMain);
            this.Name = "Recorder";
            gBSettings.ResumeLayout(false);
            gBSettings.PerformLayout();
            tLPSettings.ResumeLayout(false);
            tLPSettings.PerformLayout();
            gBSettingsField.ResumeLayout(false);
            gBSettingsField.PerformLayout();
            tLPSettingsField.ResumeLayout(false);
            tLPSettingsField.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUDHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDWidth)).EndInit();
            gBSettingsCellularAutomaton.ResumeLayout(false);
            gBSettingsCellularAutomaton.PerformLayout();
            tLPSettingsCellularAutomaton.ResumeLayout(false);
            tLPSettingsCellularAutomaton.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUDStatesCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDDencity)).EndInit();
            this.tLPMain.ResumeLayout(false);
            this.tLPMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nUDHeight;
        private System.Windows.Forms.NumericUpDown nUDWidth;
        private System.Windows.Forms.ComboBox cBCellularAutomatonRules;
        private System.Windows.Forms.NumericUpDown nUDStatesCount;
        private System.Windows.Forms.NumericUpDown nUDDencity;
        private System.Windows.Forms.TableLayoutPanel tLPMain;
        private System.Windows.Forms.Button bStop;
        private System.Windows.Forms.Button bRecord;
        private System.Windows.Forms.Button bSave;
    }
}
