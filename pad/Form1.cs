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

        private void 新規NToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this._isSaved == false)
            {
                DialogResult result = MessageBox.Show("変更内容を保存しますか？", "Pad", MessageBoxButtons.YesNoCancel);

                if (result == DialogResult.Cancel)
                {
                    return;
                }
                else if (result == DialogResult.Yes)
                {
                    if (this._fileName == "")
                    {
                        this.SaveAs();
                    }
                    else
                    {
                        this.Save();
                    }
                }
                else if (result == DialogResult.No)
                {

                }
                this._isSaved = true;
                this._fileName = "";
                this.textBox1.Text = "";
            }
        }

        private void 開くOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this._fileName = dialog.FileName;
                var reader = new StreamReader(this._fileName);
                this.textBox1.Text = reader.ReadToEnd();
                reader.Close();
            }

        }

        private void Save()
        {
            var writer = new StreamWriter(this._fileName);
            writer.WriteLine(this.textBox1.Text);
            writer.Close();
            this._isSaved = true;
            this.toolStripStatusLabelStatus.Text = "保存しました: " + this._fileName;
        }

        private void SaveAs()
        {
            using var dialog = new SaveFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this._fileName = dialog.FileName;
                this.Save();
            }

        }

        private void 上書き保存SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(this._fileName == "")
            {
                this.SaveAs();
            } 
            else
            {
                this.Save();
            }
        }

        private void 名前を付けて保存AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SaveAs();
        }

        private void CountLength()
        {
            int linage = this.textBox1.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None).Length;
            int length = this.textBox1.Text.Length - (2 * (linage - 1));
            string text = string.Format("{0}文字", length);
            if (this.textBox1.SelectedText.Length > 0)
            {
                int linage_selected = this.textBox1.SelectedText.Split(new string[] { Environment.NewLine }, StringSplitOptions.None).Length;
                int length_selected = this.textBox1.SelectedText.Length - (2 * (linage_selected - 1));
                string text_selected = string.Format("{0} / ", length_selected);
                this.toolStripStatusLabelCount.Text = text_selected + text;
            }
            else
            {
                this.toolStripStatusLabelCount.Text = text;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this._isSaved = false;
            this.CountLength();
        }

        private void textBox1_MouseMove(object sender, MouseEventArgs e)
        {
            this.CountLength();
        }
    }
}

