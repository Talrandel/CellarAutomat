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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "_recorder")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "components")]
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _recorder?.Dispose();
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
            this.lMaxFrames = new System.Windows.Forms.Label();
            this.nUDMaxFrames = new System.Windows.Forms.NumericUpDown();
            this.gBProgress = new System.Windows.Forms.GroupBox();
            this.tLPRecordInfo = new System.Windows.Forms.TableLayoutPanel();
            this.lRecordedFrames = new System.Windows.Forms.Label();
            this.lRecordedFramesValue = new System.Windows.Forms.Label();
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
            ((System.ComponentModel.ISupportInitialize)(this.nUDMaxFrames)).BeginInit();
            this.gBProgress.SuspendLayout();
            this.tLPRecordInfo.SuspendLayout();
            this.gBControlButton.SuspendLayout();
            this.tLPControlButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // tLPMain
            // 
            resources.ApplyResources(this.tLPMain, "tLPMain");
            this.tLPMain.Controls.Add(this.gBSettings, 0, 0);
            this.tLPMain.Controls.Add(this.gBProgress, 0, 1);
            this.tLPMain.Controls.Add(this.gBControlButton, 0, 2);
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
            this.tLPSettings.Controls.Add(this.lMaxFrames, 0, 5);
            this.tLPSettings.Controls.Add(this.nUDMaxFrames, 1, 5);
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
            this.toolTip.SetToolTip(this.nUDWidth, resources.GetString("nUDWidth.ToolTip"));
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
            this.toolTip.SetToolTip(this.nUDHeight, resources.GetString("nUDHeight.ToolTip"));
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
            this.toolTip.SetToolTip(this.nUDDencity, resources.GetString("nUDDencity.ToolTip"));
            // 
            // nUDStatesCount
            // 
            resources.ApplyResources(this.nUDStatesCount, "nUDStatesCount");
            this.nUDStatesCount.Name = "nUDStatesCount";
            this.toolTip.SetToolTip(this.nUDStatesCount, resources.GetString("nUDStatesCount.ToolTip"));
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
            this.toolTip.SetToolTip(this.cBCellularAutomatonRules, resources.GetString("cBCellularAutomatonRules.ToolTip"));
            // 
            // lMaxFrames
            // 
            resources.ApplyResources(this.lMaxFrames, "lMaxFrames");
            this.lMaxFrames.Name = "lMaxFrames";
            // 
            // nUDMaxFrames
            // 
            resources.ApplyResources(this.nUDMaxFrames, "nUDMaxFrames");
            this.nUDMaxFrames.Name = "nUDMaxFrames";
            this.toolTip.SetToolTip(this.nUDMaxFrames, resources.GetString("nUDMaxFrames.ToolTip"));
            // 
            // gBProgress
            // 
            resources.ApplyResources(this.gBProgress, "gBProgress");
            this.gBProgress.Controls.Add(this.tLPRecordInfo);
            this.gBProgress.Name = "gBProgress";
            this.gBProgress.TabStop = false;
            // 
            // tLPRecordInfo
            // 
            resources.ApplyResources(this.tLPRecordInfo, "tLPRecordInfo");
            this.tLPRecordInfo.Controls.Add(this.lRecordedFrames, 0, 0);
            this.tLPRecordInfo.Controls.Add(this.lRecordedFramesValue, 1, 0);
            this.tLPRecordInfo.Name = "tLPRecordInfo";
            // 
            // lRecordedFrames
            // 
            resources.ApplyResources(this.lRecordedFrames, "lRecordedFrames");
            this.lRecordedFrames.Name = "lRecordedFrames";
            // 
            // lRecordedFramesValue
            // 
            resources.ApplyResources(this.lRecordedFramesValue, "lRecordedFramesValue");
            this.lRecordedFramesValue.Name = "lRecordedFramesValue";
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
            this.toolTip.SetToolTip(this.bRecord, resources.GetString("bRecord.ToolTip"));
            this.bRecord.UseVisualStyleBackColor = true;
            this.bRecord.Click += new System.EventHandler(this.bRecord_Click);
            // 
            // bStop
            // 
            resources.ApplyResources(this.bStop, "bStop");
            this.bStop.Name = "bStop";
            this.toolTip.SetToolTip(this.bStop, resources.GetString("bStop.ToolTip"));
            this.bStop.UseVisualStyleBackColor = true;
            this.bStop.Click += new System.EventHandler(this.bStop_Click);
            // 
            // bSave
            // 
            resources.ApplyResources(this.bSave, "bSave");
            this.tLPControlButton.SetColumnSpan(this.bSave, 2);
            this.bSave.Name = "bSave";
            this.toolTip.SetToolTip(this.bSave, resources.GetString("bSave.ToolTip"));
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
            ((System.ComponentModel.ISupportInitialize)(this.nUDMaxFrames)).EndInit();
            this.gBProgress.ResumeLayout(false);
            this.gBProgress.PerformLayout();
            this.tLPRecordInfo.ResumeLayout(false);
            this.tLPRecordInfo.PerformLayout();
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
        private System.Windows.Forms.Label lMaxFrames;
        private System.Windows.Forms.NumericUpDown nUDMaxFrames;
        private System.Windows.Forms.TableLayoutPanel tLPRecordInfo;
        private System.Windows.Forms.Label lRecordedFrames;
        private System.Windows.Forms.Label lRecordedFramesValue;
        private System.Windows.Forms.GroupBox gBProgress;
    }
}
