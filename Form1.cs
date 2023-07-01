using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ScannerCalibrator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
           
            InitializeComponent();
            label1.Text = "0";
            label4.Text = "0";
           
        }

        private readonly Stopwatch _watch = new Stopwatch();
        protected bool KeyboardScannerInput = false;
        private long ScanDuration = 50;
        private List<long> ScanResults = new List<long>();
        public long maxTime = 0;
        public double avgTime= 0;
        
        
       
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
          /*  if (_watch.IsRunning)
            {
                _watch.Reset();
                _watch.Start();
                return;
            }*/

            if (!_watch.IsRunning)
            {
                _watch.Start();
                return;
            }
           

         long elapsedMilliseconds = _watch.ElapsedMilliseconds;
         ScanResults.Add(elapsedMilliseconds);
         
      //   KeyboardScannerInput = elapsedMilliseconds <= ScanDuration;
         maxTime = ScanResults.Max();
         avgTime = Math.Round(ScanResults.Average(),0);

         if (_watch.IsRunning) { _watch.Reset(); _watch.Stop(); return; }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                _watch.Reset();
                _watch.Stop();
                button1_Click(this, new EventArgs());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Length == 0)
            {
                MessageBox.Show("Сканировать надо!");
                return;
            }
            this.label1.Text = avgTime.ToString();
            this.label4.Text = maxTime.ToString();
            this.label5.Text = (avgTime > ScanDuration) ? "Медленно" : "Норма";
            this.ScanResults.Clear();
            this.textBox1.Clear();
            
            if (_watch.IsRunning) { _watch.Reset(); _watch.Stop(); return; }
        }
    }
}
