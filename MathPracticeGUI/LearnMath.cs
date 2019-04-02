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
using System.Windows.Forms;

namespace MathPracticeGUI
{
    class LearnMath
    {


        public Random rnd = new Random();
        public int answer;
        public double answerDiv;
        public int num1;
        public int num2;
        public string opsign;



        // Addition ----------------------------------------------				
        public void learnSum()
        {
            num1 = rnd.Next(1, 10);
            num2 = rnd.Next(1, 10);

            opsign = " + ";
            answer = num1 + num2;
        }

        // Multiplication ----------------------------------------------		
        public void learnMulti()
        {
            num1 = rnd.Next(1, 10);
            num2 = rnd.Next(1, 10);

            opsign = " × ";
            answer = num1 * num2;
        }

        // Subtration ----------------------------------------------		
        public void learnSub()
        {
            num1 = rnd.Next(1, 10);
            num2 = rnd.Next(1, 10);

            if (num2 > num1)
            {
                //(num2, num1) = (num1, num2);   
                // tried swap using tuples, couldn't make it work.
                // requires NuGet package 'System.ValueTuple'

                int temp = num2;
                num2 = num1;
                num1 = temp;
            }

            opsign = " − ";
            answer = num1 - num2;
        }

        // Division ----------------------------------------------				
        public void learnDiv()
        {
            num1 = rnd.Next(1, 10);
            num2 = rnd.Next(1, 10);

            if (num2 > num1)
            {
                //(num2, num1) = (num1, num2);    
                // tried swap using tuples, couldn't make it work.
                // requires NuGet package 'System.ValueTuple'

                int temp = num2;
                num2 = num1;
                num1 = temp;
            }

            answerDiv = (double)num1 / num2;
            //answerDiv = (Math.Round(answerDiv, 3));
            answerDiv = Math.Truncate(answerDiv * 1000) / 1000; // Tuncated for a reason, okey!
            opsign = " ÷ ";
        }

    }
}
