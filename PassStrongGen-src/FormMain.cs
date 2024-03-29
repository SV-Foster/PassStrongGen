/***

Copyright 2023, SV Foster. All rights reserved.

License:
    This program is free for personal, educational and/or non-profit usage    

Revision History:

***/

using System;
using System.Windows.Forms;
using System.Diagnostics;
using DataGeneratorRandom;


namespace PassStrongGen
{
    enum GenerationType
    {
        US_ASCII,
        HEX
    }

    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            InitializeModeComboBox();
        }

        private void InitializeModeComboBox()
        {
            cmbMode.DataSource = Enum.GetValues(typeof(GenerationType));
            cmbMode.FormattingEnabled = true;
            cmbMode.Format += delegate (object sender, ListControlConvertEventArgs e)
            {
                e.Value = GetGenerationTypeString((GenerationType)e.Value);
            };
            cmbMode.SelectedIndex = 0;
        }

        private string GetGenerationTypeString(GenerationType type)
        {
            switch (type)
            {
                case GenerationType.US_ASCII:
                    return Properties.Resources.UIMSG_100_GEN_USASCII;

                case GenerationType.HEX:
                    return Properties.Resources.UIMSG_101_GEN_HEX;

                default:
                    return type.ToString(); // Or handle other values as needed
            }
        }

        private void Main_Shown(object sender, EventArgs e)
        {
            OptionsLoad();
            GUIGeometryFix();

            // generate some pass on start
            buttonGenerate_Click(null, null);
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            OptionsSave();
            OptionsSaveMode(cmbMode.SelectedItem);
        }

        private void OptionsSave()
        {
            Properties.Settings.Default.GenerationMode = (int)cmbMode.SelectedItem;
            Properties.Settings.Default.AutoCopyToClipboard = this.checkBox5.Checked;
            Properties.Settings.Default.USASCIICharsUP = this.checkBox1.Checked;
            Properties.Settings.Default.USASCIICharsDown = this.checkBox2.Checked;
            Properties.Settings.Default.USASCIICharsDigits = this.checkBox3.Checked;
            Properties.Settings.Default.USASCIICharsSpecial = this.checkBox4.Checked;

            Properties.Settings.Default.Save();
        }

        private void OptionsSaveMode(object SValue)
        {
            Debug.Assert(SValue != null, "OptionsSaveMode call w/null!");

            switch ((GenerationType)SValue)
            {
                case GenerationType.US_ASCII:
                    Properties.Settings.Default.USASCIILength = (int)numLength.Value;
                    break;

                case GenerationType.HEX:
                    Properties.Settings.Default.HEXLength = (int)numLength.Value;
                    break;

                default:
                    throw new Exception(Properties.Resources.UIMSG_105_GEN_MODE_INVALID);
            }

            Properties.Settings.Default.Save();
        }

        private void OptionsLoad()
        {
            // check settings are correct
            if (!Enum.IsDefined(typeof(GenerationType), Properties.Settings.Default.GenerationMode))
                Properties.Settings.Default.GenerationMode = (int)GenerationType.US_ASCII;

            // load up
            cmbMode.SelectedItem = (GenerationType)Properties.Settings.Default.GenerationMode;
            OptionsLoadMode(Properties.Settings.Default.GenerationMode);

            this.checkBox5.Checked = Properties.Settings.Default.AutoCopyToClipboard;
            this.checkBox1.Checked = Properties.Settings.Default.USASCIICharsUP;
            this.checkBox2.Checked = Properties.Settings.Default.USASCIICharsDown;
            this.checkBox3.Checked = Properties.Settings.Default.USASCIICharsDigits;
            this.checkBox4.Checked = Properties.Settings.Default.USASCIICharsSpecial;
        }

        private void OptionsLoadMode(object SValue)
        {
            Debug.Assert(SValue != null, "OptionsLoadMode call w/null!");

            switch ((GenerationType)SValue)
            {
                case GenerationType.US_ASCII:
                    numLength.Value = Properties.Settings.Default.USASCIILength;
                    label2.Text = Properties.Resources.UIMSG_102_GEN_CNT_CHARS;
                    break;

                case GenerationType.HEX:
                    numLength.Value = Properties.Settings.Default.HEXLength;
                    label2.Text = Properties.Resources.UIMSG_103_GEN_CNT_BYTES;
                    break;

                default:
                    throw new Exception(Properties.Resources.UIMSG_105_GEN_MODE_INVALID);
            }
        }

        private void GUIGeometryFix()
        {
            // center some elements
            this.lbl1.Left = this.Width / 2 - this.lbl1.Width / 2;
            this.button1.Left = this.Width / 2 - this.button1.Width / 2;
        }

        private void GUIControlsLockUnlock(in GenerationType tp)
        {
            // for US_ASCII mode
            this.checkBox1.Enabled = (tp == GenerationType.US_ASCII);
            this.checkBox2.Enabled = (tp == GenerationType.US_ASCII);
            this.checkBox3.Enabled = (tp == GenerationType.US_ASCII);
            this.checkBox4.Enabled = (tp == GenerationType.US_ASCII);
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                // new password
                switch ((GenerationType)cmbMode.SelectedItem)
                {
                    case GenerationType.US_ASCII:
                        string gs = "";
                        if (this.checkBox1.Checked)
                            gs += TDataGeneratorRandom.USASCIILettersUp;

                        if (this.checkBox2.Checked)
                            gs += TDataGeneratorRandom.USASCIILettersDown;

                        if (this.checkBox3.Checked)
                            gs += TDataGeneratorRandom.USASCIIDigits;

                        if (this.checkBox4.Checked)
                            gs += TDataGeneratorRandom.USASCIISpecialChars;

                        if (String.IsNullOrEmpty(gs))
                            throw new Exception(Properties.Resources.UIMSG_104_NO_CHARS_SELECTED);

                        txtPassword.Text = TDataGeneratorRandom.StringByABCGen((int)numLength.Value, gs);
                        break;

                    case GenerationType.HEX:
                        txtPassword.Text = TDataGeneratorRandom.StringBytesHEXGet((int)numLength.Value);
                        break;

                    default:
                        throw new Exception(Properties.Resources.UIMSG_105_GEN_MODE_INVALID);
                }

                // save to clipboard
                if ((sender != null) && this.checkBox5.Checked)
                    buttonCopy_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBoxCenter.Show(this, ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void buttonCopy_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPassword.Text))
                Clipboard.SetText(txtPassword.Text);
        }

        private object ComboBoxSelectedPrev = null;
        private void comboBoxMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            // update GUI
            GUIControlsLockUnlock((GenerationType)this.cmbMode.SelectedItem);
            
            // if changed to new mode load mode details
            if (ComboBoxSelectedPrev != this.cmbMode.SelectedItem)
            {
                if (ComboBoxSelectedPrev != null)
                    OptionsSaveMode((GenerationType)ComboBoxSelectedPrev);
                OptionsLoadMode(this.cmbMode.SelectedItem);

                ComboBoxSelectedPrev = this.cmbMode.SelectedItem;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // exit app
            this.Close();
        }

        private void numLength_ValueChanged(object sender, EventArgs e)
        {
            // limit values user can set
            if ((int)numLength.Value <= 0)
            {
                numLength.Value = 1;
                return;
            }

            if (((GenerationType)this.cmbMode.SelectedItem == GenerationType.HEX) && ((int)numLength.Value > 16))
            {
                numLength.Value = 16;
                return;
            }

            if ((int)numLength.Value > 32)
            {
                numLength.Value = 32;
                return;
            }
        }
    }
}
