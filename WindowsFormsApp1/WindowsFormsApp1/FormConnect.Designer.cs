namespace SocketCommunicationToPython
{
    partial class SocketPythonKommunikation
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Connect = new System.Windows.Forms.Button();
            this.Disconnect = new System.Windows.Forms.Button();
            this.Send = new System.Windows.Forms.Button();
            this.SendText = new System.Windows.Forms.TextBox();
            this.StatusGroup = new System.Windows.Forms.GroupBox();
            this.Status = new System.Windows.Forms.TextBox();
            this.Ip = new System.Windows.Forms.TextBox();
            this.Port = new System.Windows.Forms.NumericUpDown();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.IpLable = new System.Windows.Forms.Label();
            this.PortLable = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.StatusGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Port)).BeginInit();
            this.SuspendLayout();
            // 
            // Connect
            // 
            this.Connect.Location = new System.Drawing.Point(7, 75);
            this.Connect.Name = "Connect";
            this.Connect.Size = new System.Drawing.Size(75, 23);
            this.Connect.TabIndex = 0;
            this.Connect.Text = "Connect";
            this.Connect.UseVisualStyleBackColor = true;
            this.Connect.Click += new System.EventHandler(this.Connect_Click);
            // 
            // Disconnect
            // 
            this.Disconnect.Location = new System.Drawing.Point(89, 75);
            this.Disconnect.Name = "Disconnect";
            this.Disconnect.Size = new System.Drawing.Size(75, 23);
            this.Disconnect.TabIndex = 1;
            this.Disconnect.Text = "Disconnect";
            this.Disconnect.UseVisualStyleBackColor = true;
            this.Disconnect.Click += new System.EventHandler(this.Disconnect_Click);
            // 
            // Send
            // 
            this.Send.Location = new System.Drawing.Point(169, 75);
            this.Send.Name = "Send";
            this.Send.Size = new System.Drawing.Size(75, 23);
            this.Send.TabIndex = 2;
            this.Send.Text = "Send Text";
            this.Send.UseVisualStyleBackColor = true;
            this.Send.Click += new System.EventHandler(this.Send_Click);
            // 
            // SendText
            // 
            this.SendText.Location = new System.Drawing.Point(250, 77);
            this.SendText.Name = "SendText";
            this.SendText.Size = new System.Drawing.Size(250, 20);
            this.SendText.TabIndex = 3;
            // 
            // StatusGroup
            // 
            this.StatusGroup.Controls.Add(this.Status);
            this.StatusGroup.Location = new System.Drawing.Point(7, 104);
            this.StatusGroup.Name = "StatusGroup";
            this.StatusGroup.Size = new System.Drawing.Size(500, 139);
            this.StatusGroup.TabIndex = 5;
            this.StatusGroup.TabStop = false;
            this.StatusGroup.Text = "Status";
            // 
            // Status
            // 
            this.Status.Location = new System.Drawing.Point(6, 30);
            this.Status.Multiline = true;
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(487, 98);
            this.Status.TabIndex = 5;
            // 
            // Ip
            // 
            this.Ip.Location = new System.Drawing.Point(7, 39);
            this.Ip.Name = "Ip";
            this.Ip.Size = new System.Drawing.Size(157, 20);
            this.Ip.TabIndex = 6;
            this.Ip.Text = "141.18.38.55";
            // 
            // Port
            // 
            this.Port.Location = new System.Drawing.Point(169, 39);
            this.Port.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Port.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.Port.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Port.Name = "Port";
            this.Port.Size = new System.Drawing.Size(80, 20);
            this.Port.TabIndex = 7;
            this.Port.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Port.Value = new decimal(new int[] {
            22000,
            0,
            0,
            0});
            // 
            // IpLable
            // 
            this.IpLable.AutoSize = true;
            this.IpLable.Location = new System.Drawing.Point(7, 21);
            this.IpLable.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.IpLable.Name = "IpLable";
            this.IpLable.Size = new System.Drawing.Size(17, 13);
            this.IpLable.TabIndex = 8;
            this.IpLable.Text = "IP";
            // 
            // PortLable
            // 
            this.PortLable.AutoSize = true;
            this.PortLable.Location = new System.Drawing.Point(169, 21);
            this.PortLable.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.PortLable.Name = "PortLable";
            this.PortLable.Size = new System.Drawing.Size(26, 13);
            this.PortLable.TabIndex = 9;
            this.PortLable.Text = "Port";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // SocketPythonKommunikation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 250);
            this.Controls.Add(this.PortLable);
            this.Controls.Add(this.IpLable);
            this.Controls.Add(this.Port);
            this.Controls.Add(this.Ip);
            this.Controls.Add(this.StatusGroup);
            this.Controls.Add(this.SendText);
            this.Controls.Add(this.Send);
            this.Controls.Add(this.Disconnect);
            this.Controls.Add(this.Connect);
            this.Name = "SocketPythonKommunikation";
            this.Text = "Socket Communication to Python";
            this.StatusGroup.ResumeLayout(false);
            this.StatusGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Port)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Connect;
        private System.Windows.Forms.Button Disconnect;
        private System.Windows.Forms.Button Send;
        private System.Windows.Forms.TextBox SendText;
        private System.Windows.Forms.GroupBox StatusGroup;
        private System.Windows.Forms.TextBox Ip;
        private System.Windows.Forms.NumericUpDown Port;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label IpLable;
        private System.Windows.Forms.Label PortLable;
        private System.Windows.Forms.TextBox Status;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    }
}

