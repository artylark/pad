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
            this.SetTitle();
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
            }
            this.textBox1.Text = "";
            this._isSaved = true;
            this._fileName = "";
            this.SetTitle();
            this.toolStripStatusLabelStatus.Text = "新規";
        }

        private string[] _filters = new string[]
        {
            "テキスト ドキュメント|*.txt",
            "テキスト形式ファイル|*.txt;*.md;*.py;*.pyw;*.json;*.kv;*.c;*.h;*.cpp;*.cs;*.html;*.htm;*.css;*.js",
            "すべてのファイル|*.*"
        };

        private void 開くOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var dialog = new OpenFileDialog();
            dialog.Filter = String.Join("|", this._filters);
            dialog.FilterIndex = 3;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this._fileName = dialog.FileName;
                var reader = new StreamReader(this._fileName);
                this.textBox1.Text = reader.ReadToEnd();
                reader.Close();
                this._isSaved = true;
                this.SetTitle();
                this.toolStripStatusLabelStatus.Text = "開きました: " + this._fileName;
            }
        }

        private void Save()
        {
            var writer = new StreamWriter(this._fileName);
            writer.WriteLine(this.textBox1.Text);
            writer.Close();
            this._isSaved = true;
            this.SetTitle();
            this.toolStripStatusLabelStatus.Text = "保存しました: " + this._fileName;
        }

        private void SaveAs()
        {
            using var dialog = new SaveFileDialog();
            dialog.Filter = String.Join("|", this._filters);
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

        private void SetTitle()
        {
            string title = "";
            if (this._isSaved == false)
            {
                title += "*";
            }

            if (this._fileName == "")
            {
                title += "無題";
            }
            else
            {
                title += Path.GetFileName(this._fileName);
            }

            title += " - Pad";
            this.Text = title;
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
            if (this._isSaved == true)
            {
                this._isSaved = false;
                this.SetTitle();
            }
            this.CountLength();
        }

        private void textBox1_MouseMove(object sender, MouseEventArgs e)
        {
            this.CountLength();
        }

        private void toolStripStatusLabelStatus_TextChanged(object sender, EventArgs e)
        {
            if (this.toolStripStatusLabelStatus.Text != "")
            {
                Task.Run(async () =>
                {
                    await Task.Delay(3000);
                    this.toolStripStatusLabelStatus.Text = "";
                });
            }
        }
    }
}

