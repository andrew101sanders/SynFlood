namespace MyPacketCapturer
{
    partial class SAK
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.PacketCapturebtn = new System.Windows.Forms.Button();
            this.synfloodbtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // PacketCapturebtn
            // 
            this.PacketCapturebtn.Location = new System.Drawing.Point(81, 81);
            this.PacketCapturebtn.Name = "PacketCapturebtn";
            this.PacketCapturebtn.Size = new System.Drawing.Size(230, 230);
            this.PacketCapturebtn.TabIndex = 0;
            this.PacketCapturebtn.Text = "Packet Capturer";
            this.PacketCapturebtn.UseVisualStyleBackColor = true;
            this.PacketCapturebtn.Click += new System.EventHandler(this.PacketCapturebtn_Click);
            // 
            // synfloodbtn
            // 
            this.synfloodbtn.Location = new System.Drawing.Point(463, 81);
            this.synfloodbtn.Name = "synfloodbtn";
            this.synfloodbtn.Size = new System.Drawing.Size(230, 230);
            this.synfloodbtn.TabIndex = 1;
            this.synfloodbtn.Text = "SYN Flooder";
            this.synfloodbtn.UseVisualStyleBackColor = true;
            this.synfloodbtn.Click += new System.EventHandler(this.synfloodbtn_Click);
            // 
            // SAK
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.synfloodbtn);
            this.Controls.Add(this.PacketCapturebtn);
            this.Name = "SAK";
            this.Text = "SAK";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button PacketCapturebtn;
        private System.Windows.Forms.Button synfloodbtn;
    }
}