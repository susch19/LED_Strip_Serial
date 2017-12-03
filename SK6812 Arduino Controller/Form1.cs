using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SK6812_Arduino_Controller.UserControl1;

namespace SK6812_Arduino_Controller
{
    public partial class Form1 : Form
    {
        struct brgw
        {
            public byte pos;
            public byte blue;
            public byte red;
            public byte green;
            public byte white;

            public override string ToString()
            {
                return $"Pos:{pos} | R:{red} | G:{green} | B:{blue}";
            }
        }

        public static int NumLeds = 94;
        static int numOfBytesPerLED = 5;
        bool flowingRainbow = false;
        bool tripleColorMixed = false;
        bool moveCustomWeird = false;
        bool moveCustomCorrect = false;
        int maxBrightness;
        int iter = 3;
        int span = 94;
        int offset = 0;
        Stopwatch responsteTimeMeasure = new Stopwatch();
        brgw[] ledColors = new brgw[NumLeds];

        List<brgw> ColorsToChange = new List<brgw>();
        //Form CustForm = new Form();
        //UserControl1 CustLedControl = new UserControl1();

        public Form1()
        {
            InitializeComponent();
            InitializeMyOwnComponents();

            serialPort.Open();
        }
        private void InitializeMyOwnComponents()
        {
            brightnessControl.Maximum = 255 + brightnessControl.LargeChange - 1;
            maxBrightness = 255 - brightnessControl.Value;
            //initializeCustForm();
            initializeCustControl();
        }

        public enum LED_Send_Flags : byte
        {
            terminator = 1,
            clear = 2,
            sickerFlag = 4,
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var res = colorDialog1.ShowDialog();
            if (res == DialogResult.OK)
            {

                for (byte i = 0; i < NumLeds; i++)
                {
                    ColorsToChange.Add(new brgw
                    {
                        pos = i,
                        blue = colorDialog1.Color.B,
                        green = colorDialog1.Color.G,
                        red = colorDialog1.Color.R,
                        white = 0
                    });
                }
                sendColors();
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            tripleColorMixed = !tripleColorMixed;
            AdjustOffsetTrackbar();
            TripleColorMixed();
        }
        private async void button3_Click(object sender, EventArgs e)
        {
            flowingRainbow = !flowingRainbow;
            AdjustOffsetTrackbar();
            FlowingRainbow();
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            moveCustomWeird = !moveCustomWeird;
            await MoveCustomWeird();
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            moveCustomCorrect = !moveCustomCorrect;
            await MoveCustomCorrect();
        }
        private void initializeCustControl()
        {
            ledPreview.Height = 40 + 30 * (NumLeds / (byte)(ledPreview.Width / 30));
            for (byte i = 0; i < NumLeds; i++)
            {
                var p = new Panel
                {
                    BackColor = Color.Black,
                    Size = new Size(20, 20),
                    Location = new Point(10 + 30 * (i % (byte)(ledPreview.Width / 30)), 10 + 30 * (i / (byte)(ledPreview.Width / 30)))
                };
                p.Click += (s, e) =>
                {
                    var res = colorDialog1.ShowDialog();
                    if (res == DialogResult.OK)
                    {
                        var selected = ledPreview.GetSelectedControls();
                        if (selected.Contains(p))
                            for (int o = 0; o < selected.Count; o++)
                            {
                                ColorsToChange.Add(new brgw
                                {
                                    pos = ((Tag)selected[o].Tag).Position,
                                    blue = (byte)(colorDialog1.Color.B / (255 / maxBrightness)),
                                    green = (byte)(colorDialog1.Color.G / (255 / maxBrightness)),
                                    red = (byte)(colorDialog1.Color.R / (255 / maxBrightness)),
                                    white = 0
                                });
                                selected[o].BackColor = colorDialog1.Color;
                            }
                        else
                        {
                            ColorsToChange.Add(new brgw
                            {
                                pos = i,
                                blue = (byte)(colorDialog1.Color.B / (255 / maxBrightness)),
                                green = (byte)(colorDialog1.Color.G / (255 / maxBrightness)),
                                red = (byte)(colorDialog1.Color.R / (255 / maxBrightness)),
                                white = 0
                            });
                            p.BackColor = colorDialog1.Color;
                        }
                    }
                };
                ledPreview.Controls.Add(p);
            }
            return;
            var b = new Button
            {
                Text = "SAVE",
                Dock = DockStyle.Bottom,
                Height = 30
            };
            b.Click += async (s, e) => await sendColors();


            //var randomButton = new Button
            //{
            //    Text = "RAND",
            //    Size = new Size(50, 20),
            //    Location = new Point(10 + 30 * ((NumLeds + 2) % (byte)Math.Sqrt(NumLeds)), 10 + 30 * (NumLeds / (byte)Math.Sqrt(NumLeds)))
            //};
            //randomButton.Click += async (s, e) => CustLedControl.RandomColors(maxBrightness); ;
            //CustLedControl.Controls.Add(randomButton);
            //CustLedControl.Controls.Add(b);
        }
        /*initializeCustForm
        private void initializeCustForm()
        {
            CustLedControl.Dock = DockStyle.Fill;
            CustForm.Controls.Add(CustLedControl);

            for (byte i = 0; i < NumLeds; i++)
            {
                var p = new Panel
                {
                    BackColor = Color.Black,
                    Size = new Size(20, 20),
                    Location = new Point(10 + 30 * (i % (byte)Math.Sqrt(NumLeds)), 10 + 30 * (i / (byte)Math.Sqrt(NumLeds)))
                };
                p.Click += (s, e) =>
                {
                    var res = colorDialog1.ShowDialog();
                    if (res == DialogResult.OK)
                    {
                        var selected = CustLedControl.GetSelectedControls();
                        if (selected.Contains(p))
                            for (int o = 0; o < selected.Count; o++)
                            {
                                ColorsToChange.Add(new brgw
                                {
                                    pos = ((Tag)selected[o].Tag).Position,
                                    blue = (byte)(colorDialog1.Color.B / (255 / maxBrightness)),
                                    green = (byte)(colorDialog1.Color.G / (255 / maxBrightness)),
                                    red = (byte)(colorDialog1.Color.R / (255 / maxBrightness)),
                                    white = 0
                                });
                                selected[o].BackColor = colorDialog1.Color;
                            }
                        else
                        {
                            ColorsToChange.Add(new brgw
                            {
                                pos = i,
                                blue = (byte)(colorDialog1.Color.B / (255 / maxBrightness)),
                                green = (byte)(colorDialog1.Color.G / (255 / maxBrightness)),
                                red = (byte)(colorDialog1.Color.R / (255 / maxBrightness)),
                                white = 0
                            });
                            p.BackColor = colorDialog1.Color;
                        }
                    }
                };
                CustLedControl.Controls.Add(p);
            }
            var b = new Button
            {
                Text = "SAVE",
                Dock = DockStyle.Bottom,
                Height = 30
            };
            b.Click += async (s, e) => await sendColors();

            var moveButton = new Button
            {
                Text = "MOVE",
                Size = new Size(50, 20),
                Location = new Point(10 + 30 * (NumLeds % (byte)Math.Sqrt(NumLeds)), 10 + 30 * (NumLeds / (byte)Math.Sqrt(NumLeds)))
            };
            moveButton.Click += async (s, e) => { moveCustom = !moveCustom; await MoveCustom(); };
            CustLedControl.Controls.Add(moveButton);

            var randomButton = new Button
            {
                Text = "RAND",
                Size = new Size(50, 20),
                Location = new Point(10 + 30 * ((NumLeds+2) % (byte)Math.Sqrt(NumLeds)), 10 + 30 * (NumLeds / (byte)Math.Sqrt(NumLeds)))
            };
            randomButton.Click += async (s, e) => CustLedControl.RandomColors(maxBrightness); ;
            CustLedControl.Controls.Add(randomButton);
            CustLedControl.Controls.Add(b);
            CustForm.Size = new Size(35 * (byte)Math.Sqrt(NumLeds), 45 * (byte)Math.Sqrt(NumLeds));
        }
        */
        private async Task FlowingRainbow()
        {
            int spanSeg = span / 3;
            int bestRGB = 25;
            double d = bestRGB;
            while (flowingRainbow)
            {
                for (byte led = 0; led < NumLeds; led++)
                {
                    var ledInSpan = led % span;
                    ColorsToChange.Add(new brgw
                    {
                        pos = (byte)((led + offset) % NumLeds),
                        red = (byte)((bestRGB * 1 / (Math.Min(Math.Abs(ledInSpan), Math.Abs(ledInSpan - span)) + 1)) * (maxBrightness / d)),
                        green = (byte)((bestRGB * 1 / (Math.Min(Math.Abs(ledInSpan - spanSeg), Math.Abs(ledInSpan - spanSeg * 4)) + 1)) * (maxBrightness / d)),
                        blue = (byte)((bestRGB * 1 / (Math.Min(Math.Abs(ledInSpan - spanSeg * 2), Math.Abs(ledInSpan + spanSeg)) + 1)) * (maxBrightness / d)),
                        //white = 255

                    });
                }
                calcOffset();

                Thread.Sleep(trackBar2.Value);

                await sendColors();
            }
        }

        public async Task TripleColorMixed()
        {
            while (tripleColorMixed)
            {
                for (int o = 0; o < NumLeds; o++)
                {
                    ColorsToChange.Add(new brgw
                    {
                        pos = (byte)((o + offset) % NumLeds),
                        green = (byte)((o % iter) * (byte)(maxBrightness / 2)),
                        red = (byte)(((o + 1) % iter) * (byte)(maxBrightness / 2)),
                        blue = (byte)(((o + 2) % iter) * (byte)(maxBrightness / 2))
                    });

                }
                var u = await sendColors();
                if (!u.status)
                    MessageBox.Show(u.error);
                calcOffset();
                Thread.Sleep(trackBar2.Value);
            }
        }

        public async Task MoveCustomWeird()
        {
            var col = ledPreview.GetAllColorsAndPos();

            while (moveCustomWeird)
            {
                foreach (var item in col)
                {
                    if (maxBrightness > 0)
                        ColorsToChange.Add(new brgw
                        {
                            pos = (byte)((item.Pos + offset) % NumLeds),
                            blue = (byte)(item.Color.B / (255 / maxBrightness)),
                            green = (byte)(item.Color.G / (255 / maxBrightness)),
                            red = (byte)(item.Color.R / (255 / maxBrightness)),
                        });
                    else
                        ColorsToChange.Add(new brgw
                        {
                            pos = (byte)((item.Pos + offset) % NumLeds),
                            blue = 0,
                            green = 0,
                            red = 0
                        });
                }
                var u = await sendColors();
                if (!u.status)
                    MessageBox.Show(u.error);
                calcOffset();
                Thread.Sleep(trackBar2.Value);
                col = ledPreview.GetAllColorsAndPos();
            }
        }
        public async Task MoveCustomCorrect()
        {
            var col = ledPreview.GetAllColorsAndPos();

            while (moveCustomCorrect)
            {
                foreach (var item in col)
                {
                    if (maxBrightness > 0)
                        ColorsToChange.Add(new brgw
                        {
                            pos = (byte)((NumLeds + (item.Pos + (reverseColorMove.Checked ? -1 : 1))) % NumLeds),
                            blue = (byte)(item.Color.B / (255 / maxBrightness)),
                            green = (byte)(item.Color.G / (255 / maxBrightness)),
                            red = (byte)(item.Color.R / (255 / maxBrightness)),
                        });
                    else
                        ColorsToChange.Add(new brgw
                        {
                            pos = (byte)((item.Pos + offset) % NumLeds),
                            blue = 0,
                            green = 0,
                            red = 0
                        });
                }
                var u = await sendColors();
                if (!u.status)
                    MessageBox.Show(u.error);
                calcOffset();
                Thread.Sleep(trackBar2.Value);
                col = ledPreview.GetAllColorsAndPos();
            }
        }

        private void calcOffset()
        {
            if (!manualOffsetControl.Checked)
                if (reverseColorMove.Checked)
                    offset = offset == 0 ? span : --offset;
                else
                    offset = ++offset % span;
            else
                offset = offsetTrackBar.Value;
            AdjustOffsetTrackbar();
        }
        public T MinOf<T>(T T1, T T2) where T : IComparable
        {
            if (T1.CompareTo(T2) < 0)
                return T1;
            else
                return T2;
        }

        private void AdjustOffsetTrackbar()
        {
            offsetTrackBar.Maximum = span;
            offsetTrackBar.Value = offset;
        }

        private void brightnessControl_ValueChanged(object sender, EventArgs e)
            => maxBrightness = 255 - brightnessControl.Value;

        private void manualOffsetControl_CheckedChanged(object sender, EventArgs e)
            => offsetTrackBar.Enabled = manualOffsetControl.Checked;



        SemaphoreSlim ssStaffel = new SemaphoreSlim(1);

        private async Task<(bool status, string error)> sendColors()
        {
            return await Task.Run(() =>
            {
                ssStaffel.Wait();
                try
                {
                    responsteTimeMeasure.Restart();
                    byte[] arr = new byte[ColorsToChange.Count * numOfBytesPerLED];

                    for (byte i = 0; i < ColorsToChange.Count; i++)
                    {
                        arr[i * numOfBytesPerLED] = ColorsToChange[i].pos;
                        if (ColorsToChange[i].red == ColorsToChange[i].blue
                        && ColorsToChange[i].red == ColorsToChange[i].green
                        && ColorsToChange[i].blue == ColorsToChange[i].green)
                            arr[i * numOfBytesPerLED + 4] = ColorsToChange[i].red;
                        else
                        {
                            arr[i * numOfBytesPerLED + 1] = ColorsToChange[i].red;
                            arr[i * numOfBytesPerLED + 2] = ColorsToChange[i].green;
                            arr[i * numOfBytesPerLED + 3] = ColorsToChange[i].blue;
                        }
                        //arr[i * numOfBytesPerLED + 4] = ColorsToChange[i].white;
                        //arr[i * 6 + 5] = (byte)(ColorsToChange.Count == i + 1 ? 1 : 0);
                    }
                    var panels = ledPreview.Controls.OfType<Panel>().ToList();
                    if (maxBrightness >= 1)
                        foreach (var item in ColorsToChange)
                            panels.FirstOrDefault(x => ((Tag)x.Tag).Position == item.pos).BackColor = Color.FromArgb(MinOf(255, (item.red * (255 / maxBrightness))),
                                                                                                                    MinOf(255, (item.green * (255 / maxBrightness))),
                                                                                                                    MinOf(255, (item.blue * (255 / maxBrightness))));
                    else
                        foreach (var item in panels)
                            item.BackColor = Color.Black;
                    ColorsToChange.Clear();
                    serialPort.Write(new byte[] { (byte)(NumLeds /*arr.Length / numOfBytesPerLED*/) }, 0, 1);

                    string k = "";
                    k = serialPort.ReadLine();

                    if (k == "ACK\r")
                    {
                        serialPort.Write(arr, 0, arr.Length);

                        if (serialPort.ReadLine() == "ACK\r")
                        {
                            responsteTimeMeasure.Stop();
                            label1.BeginInvoke((MethodInvoker)delegate () { label1.Text = responsteTimeMeasure.ElapsedMilliseconds.ToString(); ; });
                            return (true, null);
                        }
                    }
                    return (false, k);
                }
                catch (Exception e)
                {
                    return (false, e.Message);

                }
                finally
                {
                    ssStaffel.Release();
                }
            });
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            ledPreview.Refresh();
        }

    }
}

