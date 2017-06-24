namespace CellularAutomaton.Components.Recorder
{
    partial class CellularAutomatonRecorder
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
            if (disposing)
            {
                _saveFileDialog?.Dispose();
                components?.Dispose();
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CellularAutomatonRecorder));
            this.tLPMain = new System.Windows.Forms.TableLayoutPanel();
            this.gBSettings = new System.Windows.Forms.GroupBox();
            this.tLPSettings = new System.Windows.Forms.TableLayoutPanel();
            this.lWidth = new System.Windows.Forms.Label();
            this.nUDWidth = new System.Windows.Forms.NumericUpDown();
            this.lHeight = new System.Windows.Forms.Label();
            this.nUDHeight = new System.Windows.Forms.NumericUpDown();
            this.lDencity = new System.Windows.Forms.Label();
            this.lStatesCount = new System.Windows.Forms.Label();
            this.nUDDencity = new System.Windows.Forms.NumericUpDown();
            this.nUDStatesCount = new System.Windows.Forms.NumericUpDown();
            this.lRule = new System.Windows.Forms.Label();
            this.cBCellularAutomatonRules = new System.Windows.Forms.ComboBox();
            this.gBControlButton = new System.Windows.Forms.GroupBox();
            this.tLPControlButton = new System.Windows.Forms.TableLayoutPanel();
            this.bRecord = new System.Windows.Forms.Button();
            this.bStop = new System.Windows.Forms.Button();
            this.bSave = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.tLPMain.SuspendLayout();
            this.gBSettings.SuspendLayout();
            this.tLPSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUDWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDDencity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDStatesCount)).BeginInit();
            this.gBControlButton.SuspendLayout();
            this.tLPControlButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // tLPMain
            // 
            resources.ApplyResources(this.tLPMain, "tLPMain");
            this.tLPMain.Controls.Add(this.gBSettings, 0, 0);
            this.tLPMain.Controls.Add(this.gBControlButton, 0, 1);
            this.tLPMain.Name = "tLPMain";
            // 
            // gBSettings
            // 
            resources.ApplyResources(this.gBSettings, "gBSettings");
            this.gBSettings.Controls.Add(this.tLPSettings);
            this.gBSettings.Name = "gBSettings";
            this.gBSettings.TabStop = false;
            // 
            // tLPSettings
            // 
            resources.ApplyResources(this.tLPSettings, "tLPSettings");
            this.tLPSettings.Controls.Add(this.lWidth, 0, 0);
            this.tLPSettings.Controls.Add(this.nUDWidth, 1, 0);
            this.tLPSettings.Controls.Add(this.lHeight, 0, 1);
            this.tLPSettings.Controls.Add(this.nUDHeight, 1, 1);
            this.tLPSettings.Controls.Add(this.lDencity, 0, 2);
            this.tLPSettings.Controls.Add(this.lStatesCount, 0, 3);
            this.tLPSettings.Controls.Add(this.nUDDencity, 1, 2);
            this.tLPSettings.Controls.Add(this.nUDStatesCount, 1, 3);
            this.tLPSettings.Controls.Add(this.lRule, 0, 4);
            this.tLPSettings.Controls.Add(this.cBCellularAutomatonRules, 1, 4);
            this.tLPSettings.Name = "tLPSettings";
            // 
            // lWidth
            // 
            resources.ApplyResources(this.lWidth, "lWidth");
            this.lWidth.Name = "lWidth";
            // 
            // nUDWidth
            // 
            resources.ApplyResources(this.nUDWidth, "nUDWidth");
            this.nUDWidth.Name = "nUDWidth";
            // 
            // lHeight
            // 
            resources.ApplyResources(this.lHeight, "lHeight");
            this.lHeight.Name = "lHeight";
            // 
            // nUDHeight
            // 
            resources.ApplyResources(this.nUDHeight, "nUDHeight");
            this.nUDHeight.Name = "nUDHeight";
            // 
            // lDencity
            // 
            resources.ApplyResources(this.lDencity, "lDencity");
            this.lDencity.Name = "lDencity";
            // 
            // lStatesCount
            // 
            resources.ApplyResources(this.lStatesCount, "lStatesCount");
            this.lStatesCount.Name = "lStatesCount";
            // 
            // nUDDencity
            // 
            resources.ApplyResources(this.nUDDencity, "nUDDencity");
            this.nUDDencity.Name = "nUDDencity";
            // 
            // nUDStatesCount
            // 
            resources.ApplyResources(this.nUDStatesCount, "nUDStatesCount");
            this.nUDStatesCount.Name = "nUDStatesCount";
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
            // gBControlButton
            // 
            resources.ApplyResources(this.gBControlButton, "gBControlButton");
            this.gBControlButton.Controls.Add(this.tLPControlButton);
            this.gBControlButton.Name = "gBControlButton";
            this.gBControlButton.TabStop = false;
            // 
            // tLPControlButton
            // 
            resources.ApplyResources(this.tLPControlButton, "tLPControlButton");
            this.tLPControlButton.Controls.Add(this.bRecord, 0, 0);
            this.tLPControlButton.Controls.Add(this.bStop, 1, 0);
            this.tLPControlButton.Controls.Add(this.bSave, 0, 1);
            this.tLPControlButton.Name = "tLPControlButton";
            // 
            // bRecord
            // 
            resources.ApplyResources(this.bRecord, "bRecord");
            this.bRecord.Name = "bRecord";
            this.bRecord.UseVisualStyleBackColor = true;
            this.bRecord.Click += new System.EventHandler(this.bRecord_Click);
            // 
            // bStop
            // 
            resources.ApplyResources(this.bStop, "bStop");
            this.bStop.Name = "bStop";
            this.bStop.UseVisualStyleBackColor = true;
            this.bStop.Click += new System.EventHandler(this.bStop_Click);
            // 
            // bSave
            // 
            resources.ApplyResources(this.bSave, "bSave");
            this.tLPControlButton.SetColumnSpan(this.bSave, 2);
            this.bSave.Name = "bSave";
            this.bSave.UseVisualStyleBackColor = true;
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            // 
            // CellularAutomatonRecorder
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tLPMain);
            this.Name = "CellularAutomatonRecorder";
            this.Load += new System.EventHandler(this.CellularAutomatonRecorder_Load);
            this.tLPMain.ResumeLayout(false);
            this.tLPMain.PerformLayout();
            this.gBSettings.ResumeLayout(false);
            this.gBSettings.PerformLayout();
            this.tLPSettings.ResumeLayout(false);
            this.tLPSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUDWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDDencity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDStatesCount)).EndInit();
            this.gBControlButton.ResumeLayout(false);
            this.gBControlButton.PerformLayout();
            this.tLPControlButton.ResumeLayout(false);
            this.tLPControlButton.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tLPMain;
        private System.Windows.Forms.GroupBox gBSettings;
        private System.Windows.Forms.TableLayoutPanel tLPSettings;
        private System.Windows.Forms.ComboBox cBCellularAutomatonRules;
        private System.Windows.Forms.Label lRule;
        private System.Windows.Forms.NumericUpDown nUDStatesCount;
        private System.Windows.Forms.Label lStatesCount;
        private System.Windows.Forms.NumericUpDown nUDDencity;
        private System.Windows.Forms.Label lDencity;
        private System.Windows.Forms.NumericUpDown nUDHeight;
        private System.Windows.Forms.Label lHeight;
        private System.Windows.Forms.NumericUpDown nUDWidth;
        private System.Windows.Forms.Label lWidth;
        private System.Windows.Forms.GroupBox gBControlButton;
        private System.Windows.Forms.TableLayoutPanel tLPControlButton;
        private System.Windows.Forms.Button bSave;
        private System.Windows.Forms.Button bStop;
        private System.Windows.Forms.Button bRecord;
        private System.Windows.Forms.ToolTip toolTip;
    }
}
