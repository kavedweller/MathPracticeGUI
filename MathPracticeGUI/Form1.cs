/*
 * written on 31-03-2019, student project.
 * If all else fails, think what would Kabir do.
 * 
 * kabir@post.com
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MathPracticeGUI
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            Load += new EventHandler(Form1_Load);

            // I think these lines could also be added in the Form1.Designer.cs
            // I'll do some experiments later! 
            this.textQns.KeyPress += new KeyPressEventHandler(textQns_KeyPress);
            this.answerBox.KeyPress += new KeyPressEventHandler(answerBox_KeyPress);
        }

        LearnMath lm = new LearnMath(); // let's create an object!!

        public int count = 0;   // if you can figure out how to write a for loop,
        public int qn;          // this is not needed. but for now I'll do it my way.

        int rand;               // rand is exposed to check for division
        int score = 0;
        long startTime;
        long endTime;

        private void Form1_Load(object sender, System.EventArgs e)
        {
            // I'll hide some controls here.
            // could have been set in the properties initially!
            labelQuestion.Visible = false;
            btnNext.Visible = false;
            answerBox.Visible = false;
        }


        //add a newline in multiline resultBox
        private void AppendTextBoxLine(string newStr)
        {
            if (resultBox.Text.Length > 0)
            {
                resultBox.AppendText(Environment.NewLine);
            }
            resultBox.AppendText(newStr);
        }

        // first encounter!!
        private void btnOK_Click(object sender, EventArgs e)
        {
            qn = Convert.ToInt32(textQns.Text);
            textQns.Enabled = false;
            label.Visible = false;
            btnOK.Visible = false;
            btnNext.Visible = true;
            answerBox.Visible = true;
            answerBox.Focus();
            startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            compute();
        }

        // let's do some maths...
        public void compute()
        {
            labelQuestion.Visible = true;
            answerBox.Clear();
            btnNext.Enabled = false;
            textQns.Text = (qn - count) + " remaining";
            if (count < qn)
            {
                rand = lm.rnd.Next(1, 5);
                switch (rand)
                {
                    case 1:
                        lm.learnSum();
                        labelQuestion.Text = lm.num1 + " + " + lm.num2 + " = ?";
                        break;

                    case 2:
                        lm.learnSub();
                        labelQuestion.Text = lm.num1 + " − " + lm.num2 + " = ?";
                        break;

                    case 3:
                        lm.learnMulti();
                        labelQuestion.Text = lm.num1 + " × " + lm.num2 + " = ?";
                        break;

                    default:
                        lm.learnDiv();
                        labelQuestion.Text = lm.num1 + " ÷ " + lm.num2 + " = ?";
                        lblDecimal.Visible = true;
                        break;

                }
            }
            else		//what happens in the end
            {
                endTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                long totalTime = (endTime - startTime) / 1000;
                lblScore.Visible = true;
                lblScoreNum.Visible = true;
                lblScoreNum.Text = score + " out of " + qn;
                btnReset.Visible = true;
                btnExit.Visible = true;
                btnNext.Visible = false;
                answerBox.Visible = false;
                labelQuestion.Visible = false;
                textQns.Visible = false;
                lblTime.Text = "Test duration: " + totalTime + " seconds";
                lblTime.Visible = true;
            }
        }


        // check for digits only, I'll add some more codes later
        private void textQns_KeyPress(object sender, KeyPressEventArgs e)
        {
            //const char Backspace = (char)8;
            e.Handled = !Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8;
        }

        //enable OK button
        private void textQns_TextChanged(object sender, EventArgs e)
        {
            btnOK.Enabled = true;
        }

        // the answerBox is different than above example
        // it occationally requires just one decimal ".", not more.
        private void answerBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            //allows only digits, control characters and decimal points
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // allows only one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        //Next button is enabled after text input
        private void answerBox_TextChanged(object sender, EventArgs e)
        {
            btnNext.Enabled = true;
        }


        // event-driven part
        private void btnNext_Click(object sender, EventArgs e)
        {
            string result;
            if (rand == 4)
            {

                double userAns = Convert.ToDouble(answerBox.Text);
                //userAns = (Math.Round(userAns, 3));
                userAns = Math.Truncate(userAns * 1000) / 1000;   // Rounding off could be difficult for the user, hence truncated!
                if (userAns == lm.answerDiv)
                {
                    result = "correct.";
                    score++;
                }
                else
                {
                    result = "wrong!!";
                }
                AppendTextBoxLine(lm.num1 + lm.opsign + lm.num2 + " = " + userAns + " is " + result);
            }
            else
            {
                int userAns = Convert.ToInt32(answerBox.Text);
                if (userAns == lm.answer)
                {
                    result = "correct.";
                    score++;
                }
                else
                {
                    result = "wrong!!";
                }
                AppendTextBoxLine(lm.num1 + lm.opsign + lm.num2 + " = " + userAns + " is " + result);
            }
            lblDecimal.Visible = false;

            count++;
            compute();
        }


        // I don't know how it is done properly, I found it on stackoverflow!
        private void btnReset_Click(object sender, EventArgs e)
        {
            Form1 NewForm = new Form1();
            NewForm.Show();
            this.Dispose(false);
        }

        //process termination (along with all its threads)
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //this is the only child form for demo only
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox cr = new AboutBox();
            cr.Show();
        }

    }

}
