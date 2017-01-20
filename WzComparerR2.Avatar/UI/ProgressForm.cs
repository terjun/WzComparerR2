using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WzComparerR2.Avatar.UI
{
    public partial class ProgressForm : Form
    {
        private int max;

        public ProgressForm()
        {
            InitializeComponent();
            max = 2;
        }
        public void setText(string text)
        {
            pText.Text = text;
        }
        public void setMax(int m)
        {
            max = m;
            pBar.Maximum = m;
            pBar.Step = 1;
        }
        public void setProgress(int p)
        {
            pBar.Value = p;
            StringBuilder sb = new StringBuilder();
            sb.Append(p).Append("/").Append(max).Append(" (");
            sb.Append(String.Format("{0:0.00}",(float)p*100 / (float)max));
            sb.Append("%)");
            pText.Text = sb.ToString();
        }
    }
}
