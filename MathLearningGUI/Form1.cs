/*
 * written on 31-03-2019, partial project.
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

namespace MathLearningGUI
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            Load += new EventHandler(Form1_Load);
        }

        LearnMath lm = new LearnMath(); // let's create an object!!

        public int count = 0;   // if you can figure out how to write a for loop,
        public int qn;      // this is not needed. but for now I'll do it my way.
        int rand;
        int countdn;
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


        //add a newline in Multiline textbox
        private void AppendTextBoxLine(string newStr)
        {
            if (resultBox.Text.Length > 0)
            {
                resultBox.AppendText(Environment.NewLine);
            }
            resultBox.AppendText(newStr);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            qn = Convert.ToInt32(textQns.Text);
            textQns.Enabled = false;
            label.Visible = false;
            btnOK.Visible = false;
            btnNext.Visible = true;
            answerBox.Visible = true;
            answerBox.Focus();
            startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            countdn = qn;
            compute();
        }

        // let's do some maths...
        public void compute()
        {
            labelQuestion.Visible = true;
            answerBox.Clear();
            btnNext.Enabled = false;
            textQns.Text = countdn+" remaining";
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
                        labelQuestion.Text = lm.num1 + " ÷ " + lm.num2 + " = ?" ;
                        lblDecimal.Visible = true;
                        break;

                }
            }
            else
            {
                endTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                long totalTime = (endTime - startTime)/1000;
                lblScore.Visible = true;
                lblScoreNum.Visible = true;
                lblScoreNum.Text = score+" out of "+qn;
                btnReset.Visible = true;
                btnExit.Visible = true;
                btnNext.Visible = false;
                answerBox.Visible = false;
                labelQuestion.Visible = false;
                textQns.Visible = false;
                lblTime.Text = "Test duration: " + totalTime+" seconds";
                lblTime.Visible = true;
            }
        }


        // event-driven part
        private void button1_Click(object sender, EventArgs e)
        {
            string result;
            if (rand == 4)
            {
                double userAns = Convert.ToDouble(answerBox.Text);
                //userAns = (Math.Round(userAns, 3));
                userAns =Math.Truncate(userAns * 1000) / 1000;
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
            countdn--;
            compute();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            btnNext.Enabled = true;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            btnOK.Enabled = true;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Form1 NewForm = new Form1();
            NewForm.Show();
            this.Dispose(false);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox cr = new AboutBox();
            cr.Show();
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }
    }

}
