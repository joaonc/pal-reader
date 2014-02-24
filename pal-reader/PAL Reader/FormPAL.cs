using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PALReader
{
    public partial class FormPAL : Form
    {
        public FormPAL()
        {
            InitializeComponent();
        }

        private void FormPAL_Load(object sender, EventArgs e)
        {
            labelColors.Text = "";
        }
        
        private void textBoxFile_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void textBoxFile_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files != null && files.Length != 0)
            {
                textBoxFile.Text = files[0];
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonGo_Click(object sender, EventArgs e)
        {
            try
            {
                PalReader palReader = new PalReader(textBoxFile.Text);
                textBoxPalette.Text = " R    G    B    A" + Environment.NewLine;
                textBoxPalette.Text += palReader.GetPalette();
                labelColors.Text = string.Format("Number of different colors: {0}", palReader.colors);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
    }
}
