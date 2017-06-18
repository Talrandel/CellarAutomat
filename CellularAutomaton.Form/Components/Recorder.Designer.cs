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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Recorder));
            this.gBSettings = new System.Windows.Forms.GroupBox();
            this.tLPSettings = new System.Windows.Forms.TableLayoutPanel();
            this.gBSettingsField = new System.Windows.Forms.GroupBox();
            this.tLPSettingsField = new System.Windows.Forms.TableLayoutPanel();
            this.lHeight = new System.Windows.Forms.Label();
            this.lWidth = new System.Windows.Forms.Label();
            this.nUDHeight = new System.Windows.Forms.NumericUpDown();
            this.nUDWidth = new System.Windows.Forms.NumericUpDown();
            this.gBSettingsCellularAutomaton = new System.Windows.Forms.GroupBox();
            this.tLPSettingsCellularAutomaton = new System.Windows.Forms.TableLayoutPanel();
            this.lRule = new System.Windows.Forms.Label();
            this.cBCellularAutomatonRules = new System.Windows.Forms.ComboBox();
            this.nUDStatesCount = new System.Windows.Forms.NumericUpDown();
            this.lDencity = new System.Windows.Forms.Label();
            this.nUDDencity = new System.Windows.Forms.NumericUpDown();
            this.lStatesCount = new System.Windows.Forms.Label();
            this.tLPMain = new System.Windows.Forms.TableLayoutPanel();
            this.bStop = new System.Windows.Forms.Button();
            this.bRecord = new System.Windows.Forms.Button();
            this.bSave = new System.Windows.Forms.Button();
            this.gBSettings.SuspendLayout();
            this.tLPSettings.SuspendLayout();
            this.gBSettingsField.SuspendLayout();
            this.tLPSettingsField.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUDHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDWidth)).BeginInit();
            this.gBSettingsCellularAutomaton.SuspendLayout();
            this.tLPSettingsCellularAutomaton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUDStatesCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDDencity)).BeginInit();
            this.tLPMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // gBSettings
            // 
            resources.ApplyResources(this.gBSettings, "gBSettings");
            this.tLPMain.SetColumnSpan(this.gBSettings, 2);
            this.gBSettings.Controls.Add(this.tLPSettings);
            this.gBSettings.Name = "gBSettings";
            this.gBSettings.TabStop = false;
            // 
            // tLPSettings
            // 
            resources.ApplyResources(this.tLPSettings, "tLPSettings");
            this.tLPSettings.Controls.Add(this.gBSettingsField, 0, 0);
            this.tLPSettings.Controls.Add(this.gBSettingsCellularAutomaton, 0, 1);
            this.tLPSettings.Name = "tLPSettings";
            // 
            // gBSettingsField
            // 
            resources.ApplyResources(this.gBSettingsField, "gBSettingsField");
            this.gBSettingsField.Controls.Add(this.tLPSettingsField);
            this.gBSettingsField.Name = "gBSettingsField";
            this.gBSettingsField.TabStop = false;
            // 
            // tLPSettingsField
            // 
            resources.ApplyResources(this.tLPSettingsField, "tLPSettingsField");
            this.tLPSettingsField.Controls.Add(this.lHeight, 0, 1);
            this.tLPSettingsField.Controls.Add(this.lWidth, 0, 0);
            this.tLPSettingsField.Controls.Add(this.nUDHeight, 1, 1);
            this.tLPSettingsField.Controls.Add(this.nUDWidth, 1, 0);
            this.tLPSettingsField.Name = "tLPSettingsField";
            // 
            // lHeight
            // 
            resources.ApplyResources(this.lHeight, "lHeight");
            this.lHeight.Name = "lHeight";
            // 
            // lWidth
            // 
            resources.ApplyResources(this.lWidth, "lWidth");
            this.lWidth.Name = "lWidth";
            // 
            // nUDHeight
            // 
            resources.ApplyResources(this.nUDHeight, "nUDHeight");
            this.nUDHeight.Name = "nUDHeight";
            // 
            // nUDWidth
            // 
            resources.ApplyResources(this.nUDWidth, "nUDWidth");
            this.nUDWidth.Name = "nUDWidth";
            // 
            // gBSettingsCellularAutomaton
            // 
            resources.ApplyResources(this.gBSettingsCellularAutomaton, "gBSettingsCellularAutomaton");
            this.gBSettingsCellularAutomaton.Controls.Add(this.tLPSettingsCellularAutomaton);
            this.gBSettingsCellularAutomaton.Name = "gBSettingsCellularAutomaton";
            this.gBSettingsCellularAutomaton.TabStop = false;
            // 
            // tLPSettingsCellularAutomaton
            // 
            resources.ApplyResources(this.tLPSettingsCellularAutomaton, "tLPSettingsCellularAutomaton");
            this.tLPSettingsCellularAutomaton.Controls.Add(this.lRule, 0, 2);
            this.tLPSettingsCellularAutomaton.Controls.Add(this.cBCellularAutomatonRules, 1, 2);
            this.tLPSettingsCellularAutomaton.Controls.Add(this.nUDStatesCount, 1, 1);
            this.tLPSettingsCellularAutomaton.Controls.Add(this.lDencity, 0, 0);
            this.tLPSettingsCellularAutomaton.Controls.Add(this.nUDDencity, 1, 0);
            this.tLPSettingsCellularAutomaton.Controls.Add(this.lStatesCount, 0, 1);
            this.tLPSettingsCellularAutomaton.Name = "tLPSettingsCellularAutomaton";
            // 
            // lRule
            // 
            resources.ApplyResources(this.lRule, "lRule");
            this.lRule.Name = "lRule";
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
            resources.ApplyResources(this.lDencity, "lDencity");
            this.lDencity.Name = "lDencity";
            // 
            // nUDDencity
            // 
            resources.ApplyResources(this.nUDDencity, "nUDDencity");
            this.nUDDencity.Name = "nUDDencity";
            // 
            // lStatesCount
            // 
            resources.ApplyResources(this.lStatesCount, "lStatesCount");
            this.lStatesCount.Name = "lStatesCount";
            // 
            // tLPMain
            // 
            resources.ApplyResources(this.tLPMain, "tLPMain");
            this.tLPMain.Controls.Add(this.gBSettings, 0, 0);
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
            this.gBSettings.ResumeLayout(false);
            this.gBSettings.PerformLayout();
            this.tLPSettings.ResumeLayout(false);
            this.tLPSettings.PerformLayout();
            this.gBSettingsField.ResumeLayout(false);
            this.gBSettingsField.PerformLayout();
            this.tLPSettingsField.ResumeLayout(false);
            this.tLPSettingsField.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUDHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDWidth)).EndInit();
            this.gBSettingsCellularAutomaton.ResumeLayout(false);
            this.gBSettingsCellularAutomaton.PerformLayout();
            this.tLPSettingsCellularAutomaton.ResumeLayout(false);
            this.tLPSettingsCellularAutomaton.PerformLayout();
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
        private System.Windows.Forms.GroupBox gBSettings;
        private System.Windows.Forms.TableLayoutPanel tLPSettings;
        private System.Windows.Forms.GroupBox gBSettingsField;
        private System.Windows.Forms.TableLayoutPanel tLPSettingsField;
        private System.Windows.Forms.Label lHeight;
        private System.Windows.Forms.Label lWidth;
        private System.Windows.Forms.GroupBox gBSettingsCellularAutomaton;
        private System.Windows.Forms.TableLayoutPanel tLPSettingsCellularAutomaton;
        private System.Windows.Forms.Label lRule;
        private System.Windows.Forms.Label lDencity;
        private System.Windows.Forms.Label lStatesCount;
    }
}
