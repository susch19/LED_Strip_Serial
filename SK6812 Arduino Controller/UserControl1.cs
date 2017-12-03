using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SK6812_Arduino_Controller
{
    public partial class UserControl1 : UserControl
    {
        public class Tag
        {
            public bool Selected { get; set; }
            public byte Position { get; set; }
        }
        byte pos = 0;

        public UserControl1()
        {
            InitializeComponent();
        }
        int xStart = 0;
        int yStart = 0;
        int xTo = 0;
        int yTo = 0;
        bool mouseDown = false;
        Random random = new Random();

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (var pen = new Pen(Color.HotPink))
                foreach (var item in Controls.OfType<Panel>())
                {
                    if (((Tag)item.Tag).Selected)
                    {
                        e.Graphics.DrawRectangle(pen, new Rectangle(new Point(item.Location.X - 1, item.Location.Y - 1),
                            new Size(item.Width + 1, item.Height + 1)));
                    }
                }

            using (System.Drawing.Pen myPen = new System.Drawing.Pen(Color.Black))
                e.Graphics.DrawRectangle(myPen, new Rectangle(new Point(MinOf(xStart, xTo), MinOf(yStart, yTo)),
                   new Size(MaxOf(xStart, xTo) - MinOf(xStart, xTo), MaxOf(yStart, yTo) - MinOf(yStart, yTo))));
        }

        private void UserControl1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            xStart = e.X;
            yStart = e.Y;
            foreach (var item in Controls.OfType<Panel>())
            {
                ((Tag)item.Tag).Selected = false;
            }
        }

        private void UserControl1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!mouseDown)
                return;
            xTo = e.X;
            yTo = e.Y;
            Refresh();
            foreach (var item in Controls.OfType<Panel>())
            {
                if ((item.Location.X < MaxOf(xStart, xTo)
                        && (item.Location.X + item.Size.Width) > MinOf(xStart, xTo)
                        && item.Location.Y < MaxOf(yStart, yTo)
                        && (item.Location.Y + item.Size.Height) > MinOf(yStart, yTo)))
                    ((Tag)item.Tag).Selected = true;
                else
                    ((Tag)item.Tag).Selected = false;
            }
        }

        private void UserControl1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
            xStart = 0;
            yStart = 0;
            xTo = 0;
            yTo = 0;
            Refresh();
        }

        public List<Panel> GetSelectedControls()
        {
            var ret = new List<Panel>();
            foreach (var item in Controls.OfType<Panel>())
            {
                if (((Tag)item.Tag).Selected)
                    ret.Add(item);
            }
            return ret;
        }
        public List<(int Pos, Color Color)> GetAllColorsAndPos()
        {
            var ret = new List<(int, Color)>();
            foreach (Control item in Controls.OfType<Panel>())
            {
                if (typeof(Panel) == item.GetType())
                    ret.Add((((Tag)item.Tag).Position, item.BackColor));
            }
            return ret;
        }

        public T MinOf<T>(T T1, T T2) where T : IComparable
        {
            if (T1.CompareTo(T2) < 0)
                return T1;
            else
                return T2;
        }
        public T MaxOf<T>(T T1, T T2) where T : IComparable
        {
            if (T1.CompareTo(T2) < 0)
                return T2;
            else
                return T1;
        }

        private void UserControl1_ControlAdded(object sender, ControlEventArgs e)
        {
            if (typeof(Panel) == e.Control.GetType())
                e.Control.Tag = new Tag { Selected = false, Position = pos++ };
        }

        internal void RandomColors(int maxBrightness)
        {
            int a;
            foreach (var item in Controls.OfType<Panel>())
            {
                a = item.BackColor.A;
                item.BackColor = Color.FromArgb(a, random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
            }
        }
        public override void Refresh()
        {
            Height = 40 + 30 * (Form1.NumLeds / (byte)(Width / 30));
            var panels = Controls.OfType<Panel>().ToList();
            for (int i = 0; i < panels.Count; i++)
            {
                panels[i].Location = new Point(10 + 30 * (i % (byte)(Width / 30)), 10 + 30 * (i / (byte)(Width / 30)));
            }
            base.Refresh();
        }
    }
}
