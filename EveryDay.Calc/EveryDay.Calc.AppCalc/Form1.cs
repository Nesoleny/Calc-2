using EveryDay.Calc.Calculation.Interfaces;
using EveryDay.Calc.Calculation.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EveryDay.Calc.AppCalc
{
    public partial class Form1 : Form
    {
        private IOperation lastOperation;

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonOper_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button == null)
                return;

            var buttons = boxOperations.Controls.OfType<Button>().Cast<Button>();
            foreach (var btn in buttons)
            {
                btn.BackColor = SystemColors.Control;
            }

            button.BackColor = SystemColors.ActiveCaption;

            var operation = button.Tag as IOperation;
            if (operation == null)
                return;

            lastOperation = operation;

            Calculate();
        }

        private void Calculate()
        {
            var inputs = tbInput.Text.Trim().Split(' ');
            var args = inputs.Select(str => Helper.Str2Double(str));

            lastOperation.Input = args.ToArray();

            try
            {
                var result = lastOperation.GetResult();

                lblResult.ForeColor = Color.Black;
                lblResult.Text = string.Format("{0}", result);

                var baseOper = lastOperation as Operation;
                if (baseOper != null && !string.IsNullOrWhiteSpace(baseOper.Error))
                {
                    lblResult.ForeColor = Color.DarkGoldenrod;
                    lblResult.Text = baseOper.Error;
                }

                rtbHistory.Text = string.Format("{0}({1}) = {2}{3}{4}", lastOperation.Name, tbInput.Text, result, Environment.NewLine, rtbHistory.Text);
            }
            catch (Exception ex)
            {
                lblResult.ForeColor = Color.Red;
                lblResult.Text = ex.Message;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            rtbHistory.Text = "";
            lblResult.Text = "";

            var operations = Helper.LoadOperations().ToList();
           
            this.boxOperations.SuspendLayout();
            var count = 0;
            var y = 0;
            for (int i = 0; i < 2; i++)
            {


                foreach (var operation in operations)
                {
                    var button = new System.Windows.Forms.Button();

                    var buttonX = 14 + count * 81;
                    var buttonY = 30 + y * 42;

                    if (buttonX + 75 > boxOperations.Right)
                    {
                        count = 0;
                        y++;
                        buttonX = 14;
                        buttonY += 42;
                    }

                    button.Location = new System.Drawing.Point(buttonX, buttonY);
                    button.Name = string.Format("button_oper_{0}", count);
                    button.Size = new System.Drawing.Size(75, 36);
                    button.TabIndex = 5 + count;
                    button.Text = operation.Name;
                    button.UseVisualStyleBackColor = true;

                    button.Tag = operation;

                    button.Click += new System.EventHandler(this.buttonOper_Click);

                    var baseOper = operation as Operation;
                    if (baseOper != null)
                    {
                        this.toolTip.SetToolTip(button, baseOper.GetDescription());
                    }

                    this.boxOperations.Controls.Add(button);

                    count++;
                }
            }
            this.boxOperations.ResumeLayout(false);
        }

        private void tbInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (lastOperation != null)
                {
                    Calculate();
                }
                else
                {
                    lblResult.Text = "Select operation please";
                }
                tbInput.SelectAll();
            }
        }

        private void boxOperations_Resize(object sender, EventArgs e)
        {
            var buttons = boxOperations.Controls.OfType<Button>().Cast<Button>();
            var count = 0;
            var y = 0;
            foreach (var btn in buttons)
            {
                var buttonX = 14 + count * 81;
                var buttonY = 30 + y * 42;

                if (buttonX + 75 > boxOperations.Right)
                {
                    count = 0;
                    y++;
                    buttonX = 14;
                    buttonY += 42;
                }

                btn.Location = new System.Drawing.Point(buttonX, buttonY);

                count++;
            }
        }
    }
}
