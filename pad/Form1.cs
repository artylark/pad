using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pad
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this._isSaved = true;
            this._fileName = "";
        }

        private bool _isSaved;
        private string _fileName;

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this._isSaved = false;
            string text = string.Format("{0}文字", this.textBox1.Text.Length);
            this.toolStripStatusLabelCount.Text = text;
        }

        private void 新規NToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this._isSaved == false)
            {
                DialogResult result = MessageBox.Show("変更内容を保存しますか？", "Pad", MessageBoxButtons.YesNoCancel);

                if (result == DialogResult.Yes)
                {

                }
            }
        }

        private void 新しいウィンドウWToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 開くOToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void SaveAs(string filename)
        {

        }

        private void 上書き保存SToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 名前を付けて保存AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string filename = dialog.FileName;
                this.toolStripStatusLabelStatus.Text = filename;

            }
        }

    }
}
