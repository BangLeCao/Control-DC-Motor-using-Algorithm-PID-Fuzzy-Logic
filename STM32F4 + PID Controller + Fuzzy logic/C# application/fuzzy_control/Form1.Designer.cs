namespace fuzzy_control
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
            this.com = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.status = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.start_button = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.set_speed = new System.Windows.Forms.TextBox();
            this.stop_button = new System.Windows.Forms.Button();
            this.sp = new System.Windows.Forms.RadioButton();
            this.po = new System.Windows.Forms.RadioButton();
            this.set_pos = new System.Windows.Forms.TextBox();
            this.testing = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.I = new System.Windows.Forms.TextBox();
            this.P = new System.Windows.Forms.TextBox();
            this.D = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.th = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.Kp = new System.Windows.Forms.Label();
            this.Ki = new System.Windows.Forms.Label();
            this.thres = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.zedGraphControl2 = new ZedGraph.ZedGraphControl();
            this.test = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.unit = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.voltage = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // com
            // 
            this.com.FormattingEnabled = true;
            this.com.Location = new System.Drawing.Point(773, 12);
            this.com.Name = "com";
            this.com.Size = new System.Drawing.Size(121, 21);
            this.com.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(830, 41);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "refresh";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(911, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "connect";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(911, 41);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 5;
            this.button4.Text = "disconnect";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // status
            // 
            this.status.AutoSize = true;
            this.status.Location = new System.Drawing.Point(755, 46);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(35, 13);
            this.status.TabIndex = 6;
            this.status.Text = "label1";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 200;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // start_button
            // 
            this.start_button.Location = new System.Drawing.Point(922, 96);
            this.start_button.Name = "start_button";
            this.start_button.Size = new System.Drawing.Size(75, 23);
            this.start_button.TabIndex = 12;
            this.start_button.Text = "start";
            this.start_button.UseVisualStyleBackColor = true;
            this.start_button.Click += new System.EventHandler(this.button9_Click);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(758, 125);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(75, 23);
            this.button10.TabIndex = 13;
            this.button10.Text = "clear graph";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // timer2
            // 
            this.timer2.Interval = 10;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.Location = new System.Drawing.Point(6, 6);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0D;
            this.zedGraphControl1.ScrollMaxX = 0D;
            this.zedGraphControl1.ScrollMaxY = 0D;
            this.zedGraphControl1.ScrollMaxY2 = 0D;
            this.zedGraphControl1.ScrollMinX = 0D;
            this.zedGraphControl1.ScrollMinY = 0D;
            this.zedGraphControl1.ScrollMinY2 = 0D;
            this.zedGraphControl1.Size = new System.Drawing.Size(720, 344);
            this.zedGraphControl1.TabIndex = 14;
            this.zedGraphControl1.UseExtendedPrintDialog = true;
            // 
            // set_speed
            // 
            this.set_speed.Location = new System.Drawing.Point(758, 166);
            this.set_speed.Name = "set_speed";
            this.set_speed.Size = new System.Drawing.Size(100, 20);
            this.set_speed.TabIndex = 15;
            this.set_speed.Text = "3.3";
            // 
            // stop_button
            // 
            this.stop_button.Location = new System.Drawing.Point(922, 125);
            this.stop_button.Name = "stop_button";
            this.stop_button.Size = new System.Drawing.Size(75, 25);
            this.stop_button.TabIndex = 16;
            this.stop_button.Text = "stop";
            this.stop_button.UseVisualStyleBackColor = true;
            this.stop_button.Click += new System.EventHandler(this.button3_Click);
            // 
            // sp
            // 
            this.sp.AutoSize = true;
            this.sp.Location = new System.Drawing.Point(758, 96);
            this.sp.Name = "sp";
            this.sp.Size = new System.Drawing.Size(54, 17);
            this.sp.TabIndex = 17;
            this.sp.TabStop = true;
            this.sp.Text = "speed";
            this.sp.UseVisualStyleBackColor = true;
            // 
            // po
            // 
            this.po.AutoSize = true;
            this.po.Location = new System.Drawing.Point(835, 96);
            this.po.Name = "po";
            this.po.Size = new System.Drawing.Size(59, 17);
            this.po.TabIndex = 18;
            this.po.TabStop = true;
            this.po.Text = "positon";
            this.po.UseVisualStyleBackColor = true;
            // 
            // set_pos
            // 
            this.set_pos.Location = new System.Drawing.Point(758, 198);
            this.set_pos.Name = "set_pos";
            this.set_pos.Size = new System.Drawing.Size(100, 20);
            this.set_pos.TabIndex = 19;
            // 
            // testing
            // 
            this.testing.AutoSize = true;
            this.testing.Location = new System.Drawing.Point(891, 169);
            this.testing.Name = "testing";
            this.testing.Size = new System.Drawing.Size(56, 13);
            this.testing.TabIndex = 20;
            this.testing.Text = "set_speed";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(891, 201);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "set_position";
            // 
            // I
            // 
            this.I.Location = new System.Drawing.Point(814, 331);
            this.I.Name = "I";
            this.I.Size = new System.Drawing.Size(32, 20);
            this.I.TabIndex = 21;
            this.I.Text = "2.1";
            // 
            // P
            // 
            this.P.Location = new System.Drawing.Point(773, 331);
            this.P.Name = "P";
            this.P.Size = new System.Drawing.Size(32, 20);
            this.P.TabIndex = 22;
            this.P.Text = "2.3";
            // 
            // D
            // 
            this.D.Location = new System.Drawing.Point(862, 331);
            this.D.Name = "D";
            this.D.Size = new System.Drawing.Size(43, 20);
            this.D.TabIndex = 23;
            this.D.Text = "0.0005";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(827, 281);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(15, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "ki";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(875, 281);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(19, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "kd";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(771, 281);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "kp";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(960, 329);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(66, 23);
            this.button5.TabIndex = 24;
            this.button5.Text = "tunning";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // th
            // 
            this.th.Location = new System.Drawing.Point(922, 331);
            this.th.Name = "th";
            this.th.Size = new System.Drawing.Size(32, 20);
            this.th.TabIndex = 25;
            this.th.Text = "5.56";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(919, 281);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 13);
            this.label6.TabIndex = 20;
            this.label6.Text = "threshold";
            // 
            // Kp
            // 
            this.Kp.AutoSize = true;
            this.Kp.Location = new System.Drawing.Point(770, 306);
            this.Kp.Name = "Kp";
            this.Kp.Size = new System.Drawing.Size(35, 13);
            this.Kp.TabIndex = 26;
            this.Kp.Text = "label7";
            // 
            // Ki
            // 
            this.Ki.AutoSize = true;
            this.Ki.Location = new System.Drawing.Point(811, 306);
            this.Ki.Name = "Ki";
            this.Ki.Size = new System.Drawing.Size(35, 13);
            this.Ki.TabIndex = 27;
            this.Ki.Text = "label8";
            // 
            // thres
            // 
            this.thres.AutoSize = true;
            this.thres.Location = new System.Drawing.Point(919, 306);
            this.thres.Name = "thres";
            this.thres.Size = new System.Drawing.Size(35, 13);
            this.thres.TabIndex = 28;
            this.thres.Text = "label9";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(865, 306);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(40, 13);
            this.label10.TabIndex = 29;
            this.label10.Text = "0.0005";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(740, 382);
            this.tabControl1.TabIndex = 30;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.zedGraphControl1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(732, 356);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "speed_position";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.zedGraphControl2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(732, 356);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "voltage";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // zedGraphControl2
            // 
            this.zedGraphControl2.Location = new System.Drawing.Point(6, 5);
            this.zedGraphControl2.Name = "zedGraphControl2";
            this.zedGraphControl2.ScrollGrace = 0D;
            this.zedGraphControl2.ScrollMaxX = 0D;
            this.zedGraphControl2.ScrollMaxY = 0D;
            this.zedGraphControl2.ScrollMaxY2 = 0D;
            this.zedGraphControl2.ScrollMinX = 0D;
            this.zedGraphControl2.ScrollMinY = 0D;
            this.zedGraphControl2.ScrollMinY2 = 0D;
            this.zedGraphControl2.Size = new System.Drawing.Size(720, 345);
            this.zedGraphControl2.TabIndex = 0;
            this.zedGraphControl2.UseExtendedPrintDialog = true;
            // 
            // test
            // 
            this.test.AutoSize = true;
            this.test.Location = new System.Drawing.Point(864, 224);
            this.test.Name = "test";
            this.test.Size = new System.Drawing.Size(28, 13);
            this.test.TabIndex = 26;
            this.test.Text = "0.00";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(758, 224);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 31;
            this.label1.Text = "current value";
            // 
            // unit
            // 
            this.unit.AutoSize = true;
            this.unit.Location = new System.Drawing.Point(934, 224);
            this.unit.Name = "unit";
            this.unit.Size = new System.Drawing.Size(35, 13);
            this.unit.TabIndex = 31;
            this.unit.Text = "label1";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(758, 251);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 13);
            this.label7.TabIndex = 31;
            this.label7.Text = "control value";
            // 
            // voltage
            // 
            this.voltage.AutoSize = true;
            this.voltage.Location = new System.Drawing.Point(864, 251);
            this.voltage.Name = "voltage";
            this.voltage.Size = new System.Drawing.Size(28, 13);
            this.voltage.TabIndex = 26;
            this.voltage.Text = "0.00";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(934, 251);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(42, 13);
            this.label9.TabIndex = 31;
            this.label9.Text = "voltage";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(839, 125);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 32;
            this.button3.Text = "stretch";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1038, 404);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.unit);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.thres);
            this.Controls.Add(this.Ki);
            this.Controls.Add(this.voltage);
            this.Controls.Add(this.test);
            this.Controls.Add(this.Kp);
            this.Controls.Add(this.th);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.D);
            this.Controls.Add(this.P);
            this.Controls.Add(this.I);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.testing);
            this.Controls.Add(this.set_pos);
            this.Controls.Add(this.po);
            this.Controls.Add(this.sp);
            this.Controls.Add(this.stop_button);
            this.Controls.Add(this.set_speed);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.start_button);
            this.Controls.Add(this.status);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.com);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox com;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button4;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Label status;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button start_button;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Timer timer2;
        private ZedGraph.ZedGraphControl zedGraphControl1;
        private System.Windows.Forms.TextBox set_speed;
        private System.Windows.Forms.Button stop_button;
        private System.Windows.Forms.RadioButton sp;
        private System.Windows.Forms.RadioButton po;
        private System.Windows.Forms.TextBox set_pos;
        private System.Windows.Forms.Label testing;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox I;
        private System.Windows.Forms.TextBox P;
        private System.Windows.Forms.TextBox D;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox th;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label Kp;
        private System.Windows.Forms.Label Ki;
        private System.Windows.Forms.Label thres;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label test;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label unit;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label voltage;
        private System.Windows.Forms.Label label9;
        private ZedGraph.ZedGraphControl zedGraphControl2;
        private System.Windows.Forms.Button button3;
    }
}

