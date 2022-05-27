using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace RegexTester
{
    public partial class frmMain : Form
    {
        private MatchCollection matches;
        private bool help_switch = false;
        private string text;
        private string help;
        private const int regex_history_count = 50;

        public frmMain()
        {
            InitializeComponent();
            this.txtText.AllowDrop = true;
            this.txtText.DragEnter += new DragEventHandler(txtText_DragEnter);
            this.txtText.DragDrop += new DragEventHandler(txtText_DragDrop);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.help = string.Join(Environment.NewLine, Help.text);
            txtText.Text = Settings.Default.TEXT;
            LoadRegexList();
        }
        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Settings.Default.TEXT = help_switch ? text : txtText.Text;
            SaveRegexList();
            Settings.Default.Save();
        }
        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5) FindAllMatches();
            else if (e.KeyCode == Keys.Escape) ClearSelection();
        }
        private void btnFind_Click(object sender, EventArgs e)
        {
            FindAllMatches();
        }
        private void btnCopy_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            if (matches != null && matches.Count > 0)
            {
                foreach (Match match in matches)
                {
                    sb.AppendLine(match.Value);
                }
                Clipboard.SetText(sb.ToString());
            }
        }
        private void btnCut_Click(object sender, EventArgs e)
        {
            ReplaceAllMatches();
        }
        private void btnReplace_Click(object sender, EventArgs e)
        {
            ReplaceAllMatches(true);
        }
        private void btnCopyText_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtText.Text);
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearSelection();
        }
        private void btnHelp_Click(object sender, EventArgs e)
        {
            if (!help_switch) text = txtText.Text;
            txtText.Text = (help_switch = !help_switch) ? help : text;
            if (!help_switch) HighlightMatches();
            txtText.ReadOnly = help_switch;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void txtText_Enter(object sender, EventArgs e)
        {
            ClearSelection();
        }
        private void txtText_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }
        private void txtText_DragDrop(object sender, DragEventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files != null && files.Length > 0)
                {
                    foreach (string file in files)
                    {
                        sb.AppendLine(File.ReadAllText(file));
                    }
                    txtText.Text = sb.ToString();
                }
            }
        }
        private void FindAllMatches()
        {
            try
            {
                if (string.IsNullOrEmpty(txtText.Text) || string.IsNullOrEmpty(txtRegex.Text))
                {
                    SorryInfo();
                }
                else
                {
                    Cursor = Cursors.WaitCursor;
                    ClearSelection();
                    matches = Regex.Matches(txtText.Text, txtRegex.Text, RegexOptions.Multiline);
                    if (matches != null && matches.Count > 0)
                    {
                        AddRegexToList();
                        HighlightMatches();
                        lblMatchesCount.Text = string.Format("Matches count: {0}.", matches.Count);
                    }
                    else
                    {
                        lblMatchesCount.Text = "No matches found.";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        private void ReplaceAllMatches()
        {
            ReplaceAllMatches(false);
        }
        private void ReplaceAllMatches(bool replace_from_input)
        {
            string replace_exp = null;

            try
            {
                if (!help_switch)
                {
                    if (string.IsNullOrEmpty(txtText.Text) || string.IsNullOrEmpty(txtRegex.Text))
                    {
                        SorryInfo();
                    }
                    else
                    {
                        Cursor = Cursors.WaitCursor;

                        if (replace_from_input)
                        {
                            replace_exp = Microsoft.VisualBasic.Interaction.InputBox("Type replace string", "Replace matches", string.Empty, 500, 400);
                            replace_exp = UnescapeSpecialCharaters(replace_exp);
                            txtText.Text = Regex.Replace(txtText.Text, txtRegex.Text, replace_exp, RegexOptions.Multiline);
                        }
                        else
                        {
                            txtText.Text = Regex.Replace(txtText.Text, txtRegex.Text, string.Empty, RegexOptions.Multiline);
                        }

                        ClearSelection();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        private void HighlightMatches()
        {
            if (matches != null && matches.Count > 0)
            {
                foreach (Match match in matches)
                {
                    txtText.Select(match.Index, match.Length);
                    txtText.SelectionBackColor = SystemColors.ActiveCaption;
                    txtText.SelectionColor = Color.White;
                }
            }
        }
        private void ClearSelection()
        {
            matches = null;
            txtText.SelectionStart = 0;
            txtText.SelectionLength = txtText.Text.Length;
            txtText.SelectionBackColor = Color.White;
            txtText.SelectionColor = SystemColors.ControlText;
            txtText.SelectionLength = 0;
            lblMatchesCount.Text = null;
        }
        private void AddRegexToList()
        {
            if (txtRegex.AutoCompleteCustomSource == null)
            {
                txtRegex.AutoCompleteCustomSource = new AutoCompleteStringCollection();
            }

            if (!txtRegex.AutoCompleteCustomSource.Contains(txtRegex.Text))
            {
                if (txtRegex.AutoCompleteCustomSource.Count == regex_history_count)
                {
                    txtRegex.AutoCompleteCustomSource.RemoveAt(0);
                }
                txtRegex.AutoCompleteCustomSource.Add(txtRegex.Text);
            }
        }
        private void LoadRegexList()
        {
            if (Settings.Default.REGEX_LIST != null && Settings.Default.REGEX_LIST.Count > 0)
            {
                txtRegex.AutoCompleteCustomSource = new AutoCompleteStringCollection();
                foreach (string item in Settings.Default.REGEX_LIST)
                {
                    txtRegex.AutoCompleteCustomSource.Add(item);
                }
            }
        }
        private void SaveRegexList()
        {
            if (txtRegex.AutoCompleteCustomSource != null && txtRegex.AutoCompleteCustomSource.Count > 0)
            {
                Settings.Default.REGEX_LIST = new StringCollection();
                foreach (string item in txtRegex.AutoCompleteCustomSource)
                {
                    Settings.Default.REGEX_LIST.Add(item);
                }
            }
        }
        private void SorryInfo()
        {
            MessageBox.Show("No text or no regex.\n\nSorry my friend...", "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private string UnescapeSpecialCharaters(string input)
        {
            string ret = input.Replace("\\r\\n", Environment.NewLine);
            ret = ret.Replace("\\n", Environment.NewLine);
            ret = ret.Replace("\\t", "\t");
            return ret;
        }
    }
}
