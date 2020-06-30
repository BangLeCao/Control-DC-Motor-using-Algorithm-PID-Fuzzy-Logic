using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Runtime.InteropServices;
using ZedGraph;
namespace fuzzy_control
{
    public partial class Form1 : Form
    {
        struct package
        {
            public float data;
            public float set;
            public float voltage;
            public float kp;
            public float ki;
            public float kd;
        }
        struct tune
        {
            public float kp;
            public float ki;
            public float kd;
            //public float th;
        }
        tune Tune = new tune();
        struct send
        {
            public byte header1;
            public byte header2;
            public byte header3;
            public byte header4;
            public float data;
        }
        send data_send = new send();
        package data = new package();       
        int number;
        float data_point, st_point, control_point;
        byte[] dynamic_buffer;
        byte[] backup_buffer;
        byte[] process_buffer;
        float t;
        byte[] fifo_buffer = new byte[2048];
        byte[] data_handler = new byte[36];
        byte[] tempp;
        byte receive_status = 0;
        int stx_check, etx_check;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            graph_initiate();
            sp.Checked = true;
            start_button.ForeColor = Color.Red;
            stop_button.ForeColor = Color.Green;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            com.DataSource = SerialPort.GetPortNames();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!serialPort1.IsOpen)
            {
                serialPort1.PortName = com.Text;
                serialPort1.Open();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            timer2.Enabled = false;
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
            }
        }
        
        private void graph_initiate()
        {
            GraphPane myPane = zedGraphControl1.GraphPane;
            myPane.Title.Text = "current value graph";
            myPane.XAxis.Title.Text = "Thời gian (0.2s per unit)";
            myPane.YAxis.Title.Text = "data";
            RollingPointPairList list = new RollingPointPairList(60000);
            RollingPointPairList list1 = new RollingPointPairList(60000);
            LineItem curve = myPane.AddCurve("data", list, Color.Red, SymbolType.None);
            LineItem curve1 = myPane.AddCurve("set_point", list1, Color.Blue, SymbolType.None);
            myPane.XAxis.Scale.Min = 0;
            myPane.XAxis.Scale.Max = 120;
            myPane.XAxis.Scale.MinorStep = 1;
            myPane.XAxis.Scale.MajorStep = 5;
            myPane.YAxis.Scale.Min = 0;
            myPane.YAxis.Scale.Max = 25;
            myPane.AxisChange();

            GraphPane myPane1 = zedGraphControl2.GraphPane;
            myPane1.Title.Text = "control value graph";
            myPane1.XAxis.Title.Text = "Thời gian (0.2s per unit)";
            myPane1.YAxis.Title.Text = "voltage";
            RollingPointPairList list2 = new RollingPointPairList(60000);
            LineItem curve2 = myPane1.AddCurve("voltage", list2, Color.Red, SymbolType.None);
            myPane1.XAxis.Scale.Min = 0;
            myPane1.XAxis.Scale.Max = 120;
            myPane1.XAxis.Scale.MinorStep = 1;
            myPane1.XAxis.Scale.MajorStep = 5;
            myPane1.YAxis.Scale.Min = 0;
            myPane1.YAxis.Scale.Max = 12;
            myPane1.AxisChange();
        }
        private void draw()
        {
            LineItem curve = zedGraphControl1.GraphPane.CurveList[0] as LineItem;
            LineItem curve1 = zedGraphControl1.GraphPane.CurveList[1] as LineItem;
            IPointListEdit list = curve.Points as IPointListEdit;
            IPointListEdit list1 = curve1.Points as IPointListEdit;
            list.Add(t, data_point);
            list1.Add(t, st_point);
            Scale xScale = zedGraphControl1.GraphPane.XAxis.Scale;
            Scale yScale = zedGraphControl1.GraphPane.YAxis.Scale;

            LineItem curve2 = zedGraphControl2.GraphPane.CurveList[0] as LineItem;
            IPointListEdit list2 = curve2.Points as IPointListEdit;
            list2.Add(t, control_point);
            Scale xScale1 = zedGraphControl2.GraphPane.XAxis.Scale;
            Scale yScale1 = zedGraphControl2.GraphPane.YAxis.Scale;
            t += 0.1f;
            
            if (t > xScale.Max - xScale.MajorStep)
            {
                xScale.Max = t + xScale.MajorStep;
                xScale.Min = xScale.Max - 30;
                xScale1.Max = t + xScale1.MajorStep;
                xScale1.Min = xScale1.Max - 30;
            }

            
            if (data_point > yScale.Max - yScale.MajorStep)
            {
                yScale.Max = data_point + yScale.MajorStep;
            }
            else if (data_point < yScale.Min + yScale.MajorStep)
            {
                yScale.Min = data_point - yScale.MajorStep;
            }
            if (control_point > yScale1.Max - yScale1.MajorStep)
            {
                yScale1.Max = control_point + yScale1.MajorStep;
            }
            else if (control_point < yScale1.Min + yScale1.MajorStep)
            {
                yScale1.Min = control_point - yScale1.MajorStep;
            }

            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
            zedGraphControl1.Refresh();

            zedGraphControl2.AxisChange();
            zedGraphControl2.Invalidate();
            zedGraphControl2.Refresh();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                status.Text = "connected";
                status.ForeColor = Color.Green;
                timer2.Enabled = true;
            }else if (!serialPort1.IsOpen)
            {
                status.Text = "disconnected";
                status.ForeColor = Color.Red;
                timer2.Enabled = false;
            }
            if (sp.Checked == true)
                unit.Text = "rad/s";
            else
                unit.Text = "rad";
        }

    
        public static byte[] Serialize<T>(T s)
          where T : struct
        {
            var size = Marshal.SizeOf(typeof(T));
            var array = new byte[size];
            var ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(s, ptr, true);
            Marshal.Copy(ptr, array, 0, size);
            Marshal.FreeHGlobal(ptr);
            return array;
        }

        public static T Deserialize<T>(byte[] array)
            where T : struct
        {
            var size = Marshal.SizeOf(typeof(T));
            var ptr = Marshal.AllocHGlobal(size);
            Marshal.Copy(array, 0, ptr, size);
            var s = (T)Marshal.PtrToStructure(ptr, typeof(T));
            Marshal.FreeHGlobal(ptr);
            return s;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                if (sp.Checked == true)
                {
                    if (start_button.ForeColor == Color.Red)
                    {
                        byte[] run = new byte[4] { 115, 112, 114, 117 };
                        serialPort1.Write(run, 0, 4);
                    }
                    else if (start_button.ForeColor == Color.Green && receive_status == 1)
                    {
                        data_send.header1 = 45;
                        data_send.header2 = 46;
                        data_send.header3 = 47;
                        data_send.header4 = 48;
                        data_send.data = Convert.ToSingle(set_speed.Text);
                        serialPort1.Write(Serialize<send>(data_send), 0, 8);
                    }
                }
                else if (po.Checked == true)
                {

                    if (start_button.ForeColor == Color.Red)
                    {
                        byte[] run = new byte[4] { 112, 111, 114, 117 };
                        serialPort1.Write(run, 0, 4);
                    }
                    else if (start_button.ForeColor == Color.Green && receive_status == 1)
                    {
                        data_send.header1 = 54;
                        data_send.header2 = 64;
                        data_send.header3 = 74;
                        data_send.header4 = 84;
                        data_send.data = Convert.ToSingle(set_pos.Text);
                        serialPort1.Write(Serialize<send>(data_send), 0, 8);
                    }
                }
            }
            catch
            {

            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            zedGraphControl1.GraphPane.CurveList.Clear();
            zedGraphControl1.GraphPane.GraphObjList.Clear(); 
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();

            zedGraphControl2.GraphPane.CurveList.Clear();
            zedGraphControl2.GraphPane.GraphObjList.Clear();
            zedGraphControl2.AxisChange();
            zedGraphControl2.Invalidate();
            t = 0;
            graph_initiate();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            number = serialPort1.BytesToRead;
            if(number != 0)
            {
                dynamic_buffer = new byte[number];
                serialPort1.Read(dynamic_buffer, 0, number);
                stx_check = IndexOf(dynamic_buffer, new byte[2] { 2, 22 }, 0);
                etx_check = IndexOf(dynamic_buffer, new byte[2] { 22, 3 }, 0);
                if(stx_check != -1 && etx_check != -1 && stx_check == 0 && etx_check == number-2)
                {
                    process_buffer = new byte[number];
                    Array.Copy(dynamic_buffer, process_buffer, number);
                    this.Invoke(new EventHandler(package_process));
                    return;
                }
                else if(stx_check == -1 && etx_check == -1)
                {
                    return;
                }
                else if(stx_check != -1 && etx_check == -1)
                {
                    backup_buffer = new byte[number];
                    Array.Copy(dynamic_buffer, backup_buffer, number);
                    return;
                }
                else if(stx_check == -1 && etx_check != -1)
                {
                    process_buffer = new byte[number + backup_buffer.Length];
                    Array.Copy(backup_buffer, 0, process_buffer, 0, backup_buffer.Length);
                    Array.Copy(dynamic_buffer, 0, process_buffer, backup_buffer.Length, number);
                    this.Invoke(new EventHandler(package_process));
                    return;
                }
                return;              
            }
            receive_status = 0;
        }

        private void package_process(object sender, EventArgs e)
        {
            if (process_buffer[2] == 115 && process_buffer[3] == 112)
            {
                tempp = new byte[process_buffer.Length - 6];
                Array.Copy(process_buffer, 4, tempp, 0, tempp.Length);
                data = Deserialize<package>(tempp);
                data_point = data.data;
                st_point = data.set;
                control_point = data.voltage;
                Kp.Text = data.kp.ToString("#,0.00");
                Ki.Text = data.ki.ToString("#,0.00");
                thres.Text = data.kd.ToString("#,0.00");
                voltage.Text = data.voltage.ToString("#,0.00");
                test.Text = data_point.ToString("#,0.00");
                draw();
                receive_status = 1;
                return;
            }
            else if(process_buffer[2] == 6 && process_buffer[3] == 6)
            {
                tempp = new byte[process_buffer.Length - 6];
                Array.Copy(process_buffer, 4, tempp, 0, tempp.Length);
                if (tempp[0] == 115 && tempp[1] == 112)
                {
                    start_button.ForeColor = Color.Green;
                    stop_button.ForeColor = Color.Red;
                    data_send.header1 = 45;
                    data_send.header2 = 46;
                    data_send.header3 = 47;
                    data_send.header4 = 48;
                    data_send.data = Convert.ToSingle(set_speed.Text);
                    serialPort1.Write(Serialize<send>(data_send), 0, 8);
                }
                else if (tempp[0] == 112 && tempp[1] == 111)
                {
                    start_button.ForeColor = Color.Green;
                    stop_button.ForeColor = Color.Red;
                    data_send.header1 = 54;
                    data_send.header2 = 64;
                    data_send.header3 = 74;
                    data_send.header4 = 84;
                    data_send.data = Convert.ToSingle(set_pos.Text);
                    serialPort1.Write(Serialize<send>(data_send), 0, 8);
                }
                else if(tempp[0] == 24 && tempp[1] == 27)
                {
                    start_button.ForeColor = Color.Red;
                    stop_button.ForeColor = Color.Green;
                }
                return;
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            GraphPane myPane = zedGraphControl1.GraphPane;
            myPane.XAxis.Scale.Min = 0;
            myPane.XAxis.Scale.Max = t;
            myPane.AxisChange();
            GraphPane myPane1 = zedGraphControl2.GraphPane;
            myPane1.XAxis.Scale.Min = 0;
            myPane1.XAxis.Scale.Max = t;
            myPane1.AxisChange();
            zedGraphControl1.Invalidate();
            zedGraphControl2.Invalidate();
        }

        public static int IndexOf(byte[] array, byte[] pattern, int offset)
        {
            int success = 0;
            for (int i = offset; i < array.Length; i++)
            {
                if (array[i] == pattern[success])
                {
                    success++;
                }
                else
                {
                    success = 0;
                }

                if (pattern.Length == success)
                {
                    return i - pattern.Length + 1;
                }
            }
            return -1;
        }
        private void button5_Click(object sender, EventArgs e)
        {
            
            byte[] dummy = new byte[13];
            Tune.kp = Convert.ToSingle(P.Text);
            Tune.ki = Convert.ToSingle(I.Text);
            Tune.kd = Convert.ToSingle(D.Text);
            byte[] convert = Serialize<tune>(Tune);
            dummy[0] = 69;
            Array.Copy(convert, 0, dummy, 1, 12);
            serialPort1.Write(dummy, 0, 13);
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            byte[] stop = new byte[4] { 115, 116, 111, 112 };
            serialPort1.Write(stop, 0, 4);
        }
    }
}
