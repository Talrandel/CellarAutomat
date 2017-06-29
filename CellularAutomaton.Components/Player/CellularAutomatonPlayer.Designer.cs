namespace CellularAutomaton.Components.Player
{
    partial class CellularAutomatonPlayer
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "_player")]
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CellularAutomatonPlayer));
            this.tLPMain = new System.Windows.Forms.TableLayoutPanel();
            this.pBMain = new System.Windows.Forms.PictureBox();
            this.playerController = new CellularAutomaton.Components.Player.PlayerController();
            this.tLPFramesPerMinute = new System.Windows.Forms.TableLayoutPanel();
            this.lFramesPerMinute = new System.Windows.Forms.Label();
            this.nUDFramesPerMinute = new System.Windows.Forms.NumericUpDown();
            this.bLoadRecord = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.tLPMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBMain)).BeginInit();
            this.tLPFramesPerMinute.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUDFramesPerMinute)).BeginInit();
            this.SuspendLayout();
            // 
            // tLPMain
            // 
            resources.ApplyResources(this.tLPMain, "tLPMain");
            this.tLPMain.Controls.Add(this.pBMain, 0, 0);
            this.tLPMain.Controls.Add(this.playerController, 0, 1);
            this.tLPMain.Controls.Add(this.tLPFramesPerMinute, 0, 2);
            this.tLPMain.Controls.Add(this.bLoadRecord, 0, 3);
            this.tLPMain.Name = "tLPMain";
            // 
            // pBMain
            // 
            resources.ApplyResources(this.pBMain, "pBMain");
            this.pBMain.Name = "pBMain";
            this.pBMain.TabStop = false;
            // 
            // playerController
            // 
            resources.ApplyResources(this.playerController, "playerController");
            this.playerController.FileName = "FileName";
            this.playerController.Name = "playerController";
            // 
            // tLPFramesPerMinute
            // 
            resources.ApplyResources(this.tLPFramesPerMinute, "tLPFramesPerMinute");
            this.tLPFramesPerMinute.Controls.Add(this.lFramesPerMinute, 0, 0);
            this.tLPFramesPerMinute.Controls.Add(this.nUDFramesPerMinute, 1, 0);
            this.tLPFramesPerMinute.Name = "tLPFramesPerMinute";
            // 
            // lFramesPerMinute
            // 
            resources.ApplyResources(this.lFramesPerMinute, "lFramesPerMinute");
            this.lFramesPerMinute.Name = "lFramesPerMinute";
            // 
            // nUDFramesPerMinute
            // 
            resources.ApplyResources(this.nUDFramesPerMinute, "nUDFramesPerMinute");
            this.nUDFramesPerMinute.Name = "nUDFramesPerMinute";
            this.nUDFramesPerMinute.ValueChanged += new System.EventHandler(this.nUDFramesPerMinute_ValueChanged);
            // 
            // bLoadRecord
            // 
            resources.ApplyResources(this.bLoadRecord, "bLoadRecord");
            this.bLoadRecord.Name = "bLoadRecord";
            this.bLoadRecord.UseVisualStyleBackColor = true;
            this.bLoadRecord.Click += new System.EventHandler(this.bLoadRecord_Click);
            // 
            // CellularAutomatonPlayer
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tLPMain);
            this.DoubleBuffered = true;
            this.Name = "CellularAutomatonPlayer";
            this.tLPMain.ResumeLayout(false);
            this.tLPMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBMain)).EndInit();
            this.tLPFramesPerMinute.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nUDFramesPerMinute)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tLPMain;
        private System.Windows.Forms.PictureBox pBMain;
        private PlayerController playerController;
        private System.Windows.Forms.TableLayoutPanel tLPFramesPerMinute;
        private System.Windows.Forms.Button bLoadRecord;
        private System.Windows.Forms.Label lFramesPerMinute;
        private System.Windows.Forms.NumericUpDown nUDFramesPerMinute;
        private System.Windows.Forms.ToolTip toolTip;
    }
}
