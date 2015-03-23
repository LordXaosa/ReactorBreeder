using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Net;
using System.Collections.Specialized;
using System.IO;

namespace ReactorBreeder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int webMax = 0, max = 0;
        public static int current = 0;
        public static int maximum = 0;
        string result, resultPic;

        int blocks, groups;

        int x, y, z, cycles;

        bool online = false;

        bool finished = true;

        bool arrayGenerated = true;

        decimal cyclesPerSecond;
        Stopwatch sw = new Stopwatch();

        bool[][][] resultArray;

        TimeSpan sendTimeLimit = new TimeSpan();
        TimeSpan queryTimeLimit = new TimeSpan();
        private void startBtn_Click(object sender, EventArgs e)
        {
            x = int.Parse(xTb.Text);
            y = int.Parse(yTb.Text);
            z = int.Parse(zTb.Text);
            cycles = int.Parse(cyclesTb.Text);

            if (x > 100 || y > 100 || z > 100)
            {
                MessageBox.Show("The array size is too large for sending to server. Local mode on.");
                sendCb.Checked = false;
            }
            if (x <= 0 || y <= 0 || z <= 0 || cycles <= 0)
            {
                MessageBox.Show("Values should me more then zero!");
                return;
            }
            LoadFile(x, y, z);
            Start();
        }

        private void Start()
        {
            sw.Reset();
            sw.Start();
            startBtn.Enabled = false;
            startBtn.Text = "Getting info...";
            finished = false;
            arrayGenerated = false;
            online = sendCb.Checked;

            xTb.Enabled = false;
            yTb.Enabled = false;
            zTb.Enabled = false;
            cyclesTb.Enabled = false;

            Application.DoEvents();

            if (online)
            {
                if (queryTimeLimit.TotalSeconds > 60)//request best results once in a minute
                {
                    queryTimeLimit = new TimeSpan();
                    try
                    {
                        using (var client = new WebClient())
                        {
                            var values = new NameValueCollection();
                            values["x"] = x.ToString();
                            values["y"] = y.ToString();
                            values["z"] = z.ToString();

                            var response = client.UploadValues("http://lordxaosa.ru/test/getMax.php", values);

                            var responseString = Encoding.Default.GetString(response);
                            webMax = int.Parse(responseString.Split('|')[0]);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            int cycles = int.Parse(cyclesTb.Text);
            maximum = Environment.ProcessorCount * cycles;
            current = 0;
            progressPb.Value = 0;
            progressPb.Maximum = maximum;
            startBtn.Text = "Processing...";

            Application.DoEvents();

            Thread tr = new Thread(() =>
            {
                List<Calculator> calcs = new List<Calculator>(x * y * z);

                Parallel.For(0, Environment.ProcessorCount, (i) =>
                {
                    Calculator calc = new Calculator(x, y, z, max, cycles);
                    calcs.Add(calc);
                });

                for (int i = 0; i < calcs.Count; i++)
                {
                    if (calcs[i] == null)
                    {
                        continue;
                    }
                    if (max < calcs[i].maxEnergy)
                    {
                        max = (int)calcs[i].maxEnergy;
                        result = calcs[i].result;
                        resultPic = calcs[i].resultPic;

                        blocks = calcs[i].Blocks;
                        groups = calcs[i].Groups;

                        resultArray = new bool[x][][];
                        for (int ix = 0; ix < x; ix++)
                        {
                            resultArray[ix] = new bool[y][];
                            for (int iy = 0; iy < y; iy++)
                            {
                                resultArray[ix][iy] = new bool[z];
                                for (int iz = 0; iz < z; iz++)
                                {
                                    resultArray[ix][iy][iz] = calcs[i].reactorsMax[ix, iy, iz];
                                }
                            }
                        }
                    }
                }
                finished = true;
            });
            tr.Start();
        }

        private void LoadFile(int x, int y, int z)
        {
            if (File.Exists(string.Format("{0}-{1}-{2}.txt", x, y, z)))
            {
                string file = File.ReadAllText(string.Format("{0}-{1}-{2}.txt", x, y, z));
                int.TryParse(file.Split('|')[0], out max);
                result = file.Split('|')[1];
                resultPic = file.Split('|')[2];
                if (!online)
                {
                    webMax = max;
                }
                int.TryParse(file.Split('|')[3], out blocks);
                int.TryParse(file.Split('|')[4], out groups);
                resultLbl.Text = result;
                resultTb.Text = resultPic;
            }
        }

        private void SaveFile(int x, int y, int z)
        {
            File.WriteAllText(string.Format("{0}-{1}-{2}.txt", x, y, z), string.Format("{0}|{1}|{2}|{3}|{4}", max, result, resultPic, blocks, groups));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            sendTimeLimit = sendTimeLimit.Add(new TimeSpan(0, 0, 0, 0, 100));
            queryTimeLimit = queryTimeLimit.Add(new TimeSpan(0, 0, 0, 0, 100));
            if (current <= maximum)
            {
                progressPb.Value = current;
            }
            if (finished)
            {
                if (!arrayGenerated)
                {
                    arrayGenerated = true;

                    if (max > webMax)
                    {
                        resultLbl.Text = result;
                        resultTb.Text = resultPic;
                        webMax = max;
                        startBtn.Text = "Saving file...";
                        Application.DoEvents();
                        SaveFile(x, y, z);
                        if (online)
                        {
                            if (sendTimeLimit.TotalSeconds > 300)//send data once in 5 minutes
                            {
                                sendTimeLimit = new TimeSpan();
                                try
                                {
                                    startBtn.Text = "Sending data... (this may take a while)";
                                    Application.DoEvents();
                                    System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                                    jss.MaxJsonLength = 1024 * 1024 * 50;//max 50 MB
                                    string data = jss.Serialize(resultArray);
                                    using (var client = new WebClient())
                                    {
                                        var values = new NameValueCollection();
                                        values["x"] = x.ToString();
                                        values["y"] = y.ToString();
                                        values["z"] = z.ToString();
                                        values["data"] = data;
                                        values["max"] = max.ToString();

                                        var response = client.UploadValues("http://lordxaosa.ru/test/reactorBreederTester.php", values);

                                        var responseString = Encoding.Default.GetString(response);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.ToString());
                                }
                            }
                        }
                    }
                    sw.Stop();
                    if (sw.ElapsedMilliseconds > 0)
                    {
                        cyclesPerSecond = ((decimal)(cycles * Environment.ProcessorCount) / (decimal)sw.ElapsedMilliseconds) * 1000.0m;
                    }
                    timeLb.Text = string.Format("Current speed: {0:N0} cycles per second", cyclesPerSecond);
                    if (continousCb.Checked)
                    {
                        Start();
                    }
                }
                if (!continousCb.Checked)
                {
                    startBtn.Enabled = true;
                    startBtn.Text = "Start";
                    xTb.Enabled = true;
                    yTb.Enabled = true;
                    zTb.Enabled = true;
                    cyclesTb.Enabled = true;
                }
            }
        }

        private void xTb_TextChanged(object sender, EventArgs e)
        {
            max = 0;
            webMax = 0;
            current = 0;
            maximum = 0;
            result = "";
            resultPic = "";
            resultArray = null;
        }
    }
    public class Group
    {
        public int MaxX = 0;
        public int MaxY = 0;
        public int MaxZ = 0;
        public int MinX = int.MaxValue;
        public int MinY = int.MaxValue;
        public int MinZ = int.MaxValue;
        public int Blocks = 0;
    }
}
