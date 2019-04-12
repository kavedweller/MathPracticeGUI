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
            this.tbQuestions.KeyPress += new KeyPressEventHandler(TextQuestions_KeyPress);
            this.tbAnswerBox.KeyPress += new KeyPressEventHandler(AnswerBox_KeyPress);
        }

        LearnMath lm = new LearnMath(); // let's create an object!!

        public int qn;          // if you can figure out how to write a for loop,
        public int count = 0;   // this is not needed. but for now I'll do it my way.

        int rand;               // rand is exposed to check for division
        int score = 0;
        long startTime;
        long endTime;

        private void Form1_Load(object sender, System.EventArgs e)
        {
            // I'll hide some controls here.
            // could have been set in the properties initially!
            lbQuestion.Visible = false;
            btnNext.Visible = false;
            tbAnswerBox.Visible = false;
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
            qn = Convert.ToInt32(tbQuestions.Text);
            tbQuestions.Enabled = false;
            label.Visible = false;
            btnOK.Visible = false;
            btnNext.Visible = true;
            tbAnswerBox.Visible = true;
            tbAnswerBox.Focus();
            startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            Compute();
        }

        // let's do some maths...
        private void Compute()
        {
            lbQuestion.Visible = true;
            tbAnswerBox.Clear();
            btnNext.Enabled = false;
            tbQuestions.Text = (qn - count) + " remaining";
            if (count < qn)
            {
                rand = lm.rnd.Next(1, 5);
                switch (rand)
                {
                    case 1:
                        lm.LearnSum();
                        lbQuestion.Text = lm.num1 + " + " + lm.num2 + " = ?";
                        break;

                    case 2:
                        lm.LearnSub();
                        lbQuestion.Text = lm.num1 + " − " + lm.num2 + " = ?";
                        break;

                    case 3:
                        lm.LearnMulti();
                        lbQuestion.Text = lm.num1 + " × " + lm.num2 + " = ?";
                        break;

                    default:
                        lm.LearnDiv();
                        lbQuestion.Text = lm.num1 + " ÷ " + lm.num2 + " = ?";
                        lblDecimal.Visible = true;
                        break;

                }
            }
            else
            {
                endTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                DisplayResult();
            }
        }

        private void DisplayResult()		// This is shown in the end
        {
            long totalTime = (endTime - startTime) / 1000;
            lbScore.Visible = true;
            lbScoreNum.Visible = true;
            lbScoreNum.Text = score + " out of " + qn;
            btnReset.Visible = true;
            btnExit.Visible = true;
            btnNext.Visible = false;
            tbAnswerBox.Visible = false;
            lbQuestion.Visible = false;
            tbQuestions.Visible = false;
            lblTime.Text = "Test duration: " + totalTime + " seconds";
            lblTime.Visible = true;
            // Messages displayed in the end
            if (score == qn)
            {
                MessageBox.Show("You've got " + score + " out of " + qn + "\t\t \nA perfect score!",
                "Well done!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else if (score >= (80*qn/100))
            {
                MessageBox.Show("You've got " + score + " out of " + qn + "\t\t \nGood, but you should practice more.",
                "Good ...", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (score <= (40 * qn / 100))
            {
                MessageBox.Show("You've got " + score + " out of " + qn + "\t\t \nYou need a lot of math practice.",
                "It's not good ...", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                MessageBox.Show("You've got " + score + " out of " + qn + "\t\t \nYou should practice more.",
                "Not good!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // check for digits and backspace only, I'll add some more codes later
        private void TextQuestions_KeyPress(object sender, KeyPressEventArgs e)
        {
            //const char Backspace = (char)8;
            e.Handled = !Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8;
        }

        //enable OK button
        private void TextQuestions_TextChanged(object sender, EventArgs e)
        {
            //check for empty string
            if (!string.IsNullOrEmpty(this.tbQuestions.Text))
            {
                btnOK.Enabled = true;
            }
            else
            {
                btnOK.Enabled = false;
            }
            
        }

        // the answerBox is different than above example
        // it occationally requires just one decimal ".", not more.
        private void AnswerBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (rand == 4)
            {
                //allows digits, backspace and only one decimal point
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)8 && (e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
                {
                    e.Handled = true;
                }
            }
            else
            {
                //allows digits only and backspace
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
                {
                    e.Handled = true;
                }
            }

        }

        //Next button is enabled after text input
        private void AnswerBox_TextChanged(object sender, EventArgs e)
        {
            //check for empty string
            if (!string.IsNullOrEmpty(this.tbAnswerBox.Text))
            {
                btnNext.Enabled = true;
            }
            else
            {
                btnNext.Enabled = false;
            }
        }


        // event-driven part
        private void BtnNext_Click(object sender, EventArgs e)
        {
            string result;
            if (rand == 4)
            {
                double userAns = Convert.ToDouble(tbAnswerBox.Text);
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
                int userAns = Convert.ToInt32(tbAnswerBox.Text);
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
            Compute();
        }


        // I don't know how it is done properly, I found it on stackoverflow!
        private void BtnReset_Click(object sender, EventArgs e)
        {
            Form1 NewForm = new Form1();
            NewForm.Show();
            this.Dispose(false);
        }

        //process termination (along with all its threads)
        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //this is the only child form for demo only
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox cr = new AboutBox();
            cr.Show();
        }

    }

}
