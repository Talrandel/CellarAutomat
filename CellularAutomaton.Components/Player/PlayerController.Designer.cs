namespace CellularAutomaton.Components.Player
{
    partial class PlayerController
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayerController));
            this.tLPMain = new System.Windows.Forms.TableLayoutPanel();
            this.tBFinder = new System.Windows.Forms.TrackBar();
            this.bPlay = new System.Windows.Forms.Button();
            this.bPause = new System.Windows.Forms.Button();
            this.bStop = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.tLPMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tBFinder)).BeginInit();
            this.SuspendLayout();
            // 
            // tLPMain
            // 
            resources.ApplyResources(this.tLPMain, "tLPMain");
            this.tLPMain.Controls.Add(this.tBFinder, 0, 0);
            this.tLPMain.Controls.Add(this.bPlay, 0, 1);
            this.tLPMain.Controls.Add(this.bPause, 1, 1);
            this.tLPMain.Controls.Add(this.bStop, 2, 1);
            this.tLPMain.Name = "tLPMain";
            // 
            // tBFinder
            // 
            this.tLPMain.SetColumnSpan(this.tBFinder, 3);
            this.tBFinder.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.tBFinder, "tBFinder");
            this.tBFinder.Name = "tBFinder";
            this.tBFinder.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.tBFinder.Scroll += new System.EventHandler(this.tBFinder_Scroll);
            this.tBFinder.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tBFinder_MouseUp);
            // 
            // bPlay
            // 
            resources.ApplyResources(this.bPlay, "bPlay");
            this.bPlay.Name = "bPlay";
            this.bPlay.UseVisualStyleBackColor = true;
            this.bPlay.Click += new System.EventHandler(this.bPlay_Click);
            // 
            // bPause
            // 
            resources.ApplyResources(this.bPause, "bPause");
            this.bPause.Name = "bPause";
            this.bPause.UseVisualStyleBackColor = true;
            this.bPause.Click += new System.EventHandler(this.bPause_Click);
            // 
            // bStop
            // 
            resources.ApplyResources(this.bStop, "bStop");
            this.bStop.Name = "bStop";
            this.bStop.UseVisualStyleBackColor = true;
            this.bStop.Click += new System.EventHandler(this.bStop_Click);
            // 
            // PlayerController
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tLPMain);
            this.Name = "PlayerController";
            this.Load += new System.EventHandler(this.PlayerController_Load);
            this.tLPMain.ResumeLayout(false);
            this.tLPMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tBFinder)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tLPMain;
        private System.Windows.Forms.Button bPlay;
        private System.Windows.Forms.Button bPause;
        private System.Windows.Forms.Button bStop;
        private System.Windows.Forms.TrackBar tBFinder;
        private System.Windows.Forms.ToolTip toolTip;
    }
}
