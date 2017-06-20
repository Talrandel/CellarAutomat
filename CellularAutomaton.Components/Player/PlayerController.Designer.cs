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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayerController));
            this.tLPControlButton = new System.Windows.Forms.TableLayoutPanel();
            this.bPlay = new System.Windows.Forms.Button();
            this.bPause = new System.Windows.Forms.Button();
            this.bStop = new System.Windows.Forms.Button();
            this.tBFinder = new System.Windows.Forms.TrackBar();
            this.tLPControlButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tBFinder)).BeginInit();
            this.SuspendLayout();
            // 
            // tLPControlButton
            // 
            resources.ApplyResources(this.tLPControlButton, "tLPControlButton");
            this.tLPControlButton.Controls.Add(this.tBFinder, 0, 0);
            this.tLPControlButton.Controls.Add(this.bPlay, 0, 1);
            this.tLPControlButton.Controls.Add(this.bPause, 1, 1);
            this.tLPControlButton.Controls.Add(this.bStop, 2, 1);
            this.tLPControlButton.Name = "tLPControlButton";
            // 
            // bPlay
            // 
            resources.ApplyResources(this.bPlay, "bPlay");
            this.bPlay.Name = "bPlay";
            this.bPlay.UseVisualStyleBackColor = true;
            // 
            // bPause
            // 
            resources.ApplyResources(this.bPause, "bPause");
            this.bPause.Name = "bPause";
            this.bPause.UseVisualStyleBackColor = true;
            // 
            // bStop
            // 
            resources.ApplyResources(this.bStop, "bStop");
            this.bStop.Name = "bStop";
            this.bStop.UseVisualStyleBackColor = true;
            // 
            // tBFinder
            // 
            this.tLPControlButton.SetColumnSpan(this.tBFinder, 3);
            this.tBFinder.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.tBFinder, "tBFinder");
            this.tBFinder.Name = "tBFinder";
            this.tBFinder.TickStyle = System.Windows.Forms.TickStyle.Both;
            // 
            // PlayerController
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tLPControlButton);
            this.Name = "PlayerController";
            this.tLPControlButton.ResumeLayout(false);
            this.tLPControlButton.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tBFinder)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tLPControlButton;
        private System.Windows.Forms.Button bPlay;
        private System.Windows.Forms.Button bPause;
        private System.Windows.Forms.Button bStop;
        private System.Windows.Forms.TrackBar tBFinder;
    }
}
