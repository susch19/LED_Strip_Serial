namespace SK6812_Arduino_Controller
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.serialPort = new System.IO.Ports.SerialPort(this.components);
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.reverseColorMove = new System.Windows.Forms.CheckBox();
            this.offsetTrackBar = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.brightnessControl = new System.Windows.Forms.VScrollBar();
            this.manualOffsetControl = new System.Windows.Forms.CheckBox();
            this.button4 = new System.Windows.Forms.Button();
            this.ledPreview = new SK6812_Arduino_Controller.UserControl1();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button5 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.offsetTrackBar)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // serialPort
            // 
            this.serialPort.BaudRate = 921600;
            this.serialPort.PortName = "COM4";
            this.serialPort.ReadTimeout = 10000;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 17);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 45);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 71);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // trackBar2
            // 
            this.trackBar2.Location = new System.Drawing.Point(12, 249);
            this.trackBar2.Maximum = 1000;
            this.trackBar2.Minimum = 10;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(305, 45);
            this.trackBar2.TabIndex = 5;
            this.trackBar2.Value = 33;
            // 
            // reverseRainbow
            // 
            this.reverseColorMove.AutoSize = true;
            this.reverseColorMove.Location = new System.Drawing.Point(12, 175);
            this.reverseColorMove.Name = "reverseRainbow";
            this.reverseColorMove.Size = new System.Drawing.Size(80, 17);
            this.reverseColorMove.TabIndex = 6;
            this.reverseColorMove.Text = "checkBox1";
            this.reverseColorMove.UseVisualStyleBackColor = true;
            // 
            // offsetTrackBar
            // 
            this.offsetTrackBar.Enabled = false;
            this.offsetTrackBar.Location = new System.Drawing.Point(31, 198);
            this.offsetTrackBar.Maximum = 255;
            this.offsetTrackBar.Name = "offsetTrackBar";
            this.offsetTrackBar.Size = new System.Drawing.Size(286, 45);
            this.offsetTrackBar.TabIndex = 8;
            this.offsetTrackBar.Value = 25;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(375, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "label1";
            // 
            // brightnessControl
            // 
            this.brightnessControl.Dock = System.Windows.Forms.DockStyle.Right;
            this.brightnessControl.Location = new System.Drawing.Point(553, 16);
            this.brightnessControl.Maximum = 255;
            this.brightnessControl.Name = "brightnessControl";
            this.brightnessControl.Size = new System.Drawing.Size(22, 336);
            this.brightnessControl.TabIndex = 10;
            this.brightnessControl.Value = 230;
            this.brightnessControl.ValueChanged += new System.EventHandler(this.brightnessControl_ValueChanged);
            // 
            // manualOffsetControl
            // 
            this.manualOffsetControl.AutoSize = true;
            this.manualOffsetControl.Location = new System.Drawing.Point(10, 207);
            this.manualOffsetControl.Name = "manualOffsetControl";
            this.manualOffsetControl.Size = new System.Drawing.Size(15, 14);
            this.manualOffsetControl.TabIndex = 11;
            this.manualOffsetControl.UseVisualStyleBackColor = true;
            this.manualOffsetControl.CheckedChanged += new System.EventHandler(this.manualOffsetControl_CheckedChanged);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(12, 100);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 12;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // ledPreview
            // 
            this.ledPreview.Dock = System.Windows.Forms.DockStyle.Top;
            this.ledPreview.Location = new System.Drawing.Point(0, 0);
            this.ledPreview.Name = "ledPreview";
            this.ledPreview.Size = new System.Drawing.Size(578, 97);
            this.ledPreview.TabIndex = 13;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button5);
            this.groupBox1.Controls.Add(this.brightnessControl);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.manualOffsetControl);
            this.groupBox1.Controls.Add(this.reverseColorMove);
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.offsetTrackBar);
            this.groupBox1.Controls.Add(this.trackBar2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 97);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(578, 355);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(13, 130);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 13;
            this.button5.Text = "button5";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 452);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ledPreview);
            this.MinimumSize = new System.Drawing.Size(400, 400);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.offsetTrackBar)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

#endregion

        private System.IO.Ports.SerialPort serialPort;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.CheckBox reverseColorMove;
        private System.Windows.Forms.TrackBar offsetTrackBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.VScrollBar brightnessControl;
        private System.Windows.Forms.CheckBox manualOffsetControl;
        private System.Windows.Forms.Button button4;
        private UserControl1 ledPreview;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button5;
    }
}

