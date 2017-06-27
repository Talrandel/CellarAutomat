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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CellularAutomatonPlayer));
            this.tLPMain = new System.Windows.Forms.TableLayoutPanel();
            this.pBMain = new System.Windows.Forms.PictureBox();
            this.playerController = new CellularAutomaton.Components.Player.PlayerController();
            this.tLPPlayerSettings = new System.Windows.Forms.TableLayoutPanel();
            this.bLoadRecord = new System.Windows.Forms.Button();
            this.lFPM = new System.Windows.Forms.Label();
            this.nUDFPM = new System.Windows.Forms.NumericUpDown();
            this.tLPMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBMain)).BeginInit();
            this.tLPPlayerSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUDFPM)).BeginInit();
            this.SuspendLayout();
            // 
            // tLPMain
            // 
            resources.ApplyResources(this.tLPMain, "tLPMain");
            this.tLPMain.Controls.Add(this.pBMain, 0, 0);
            this.tLPMain.Controls.Add(this.playerController, 0, 1);
            this.tLPMain.Controls.Add(this.tLPPlayerSettings, 0, 2);
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
            this.playerController.Name = "playerController";
            // 
            // tLPPlayerSettings
            // 
            resources.ApplyResources(this.tLPPlayerSettings, "tLPPlayerSettings");
            this.tLPPlayerSettings.Controls.Add(this.bLoadRecord, 0, 1);
            this.tLPPlayerSettings.Controls.Add(this.lFPM, 0, 0);
            this.tLPPlayerSettings.Controls.Add(this.nUDFPM, 1, 0);
            this.tLPPlayerSettings.Name = "tLPPlayerSettings";
            // 
            // bLoadRecord
            // 
            this.tLPPlayerSettings.SetColumnSpan(this.bLoadRecord, 2);
            resources.ApplyResources(this.bLoadRecord, "bLoadRecord");
            this.bLoadRecord.Name = "bLoadRecord";
            this.bLoadRecord.UseVisualStyleBackColor = true;
            this.bLoadRecord.Click += new System.EventHandler(this.bLoadRecord_Click);
            // 
            // lFPM
            // 
            resources.ApplyResources(this.lFPM, "lFPM");
            this.lFPM.Name = "lFPM";
            // 
            // nUDFPM
            // 
            resources.ApplyResources(this.nUDFPM, "nUDFPM");
            this.nUDFPM.Name = "nUDFPM";
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
            this.tLPPlayerSettings.ResumeLayout(false);
            this.tLPPlayerSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUDFPM)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tLPMain;
        private System.Windows.Forms.PictureBox pBMain;
        private PlayerController playerController;
        private System.Windows.Forms.TableLayoutPanel tLPPlayerSettings;
        private System.Windows.Forms.Button bLoadRecord;
        private System.Windows.Forms.Label lFPM;
        private System.Windows.Forms.NumericUpDown nUDFPM;
    }
}
