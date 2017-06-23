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
                _player?.Dispose();
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
            this.tLPMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBMain)).BeginInit();
            this.SuspendLayout();
            // 
            // tLPMain
            // 
            resources.ApplyResources(this.tLPMain, "tLPMain");
            this.tLPMain.Controls.Add(this.pBMain, 0, 0);
            this.tLPMain.Controls.Add(this.playerController, 0, 1);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tLPMain;
        private System.Windows.Forms.PictureBox pBMain;
        private PlayerController playerController;
    }
}
