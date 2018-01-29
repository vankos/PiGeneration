using System;
using System.Text;

namespace PI_generation
{
    class Program
    {
        private static void Main(string[] args)
        {
            Console.Write("What accuracy you want?(numbers after the decimal point): ");
            int numberOfNumbers = new int();
            string userInputString = Console.ReadLine();
            numberOfNumbers = ValidateInput(userInputString);
            Console.WriteLine("pi={0}",FindPi(numberOfNumbers + 1));
            Console.Write("Press enter to exit");
            Console.ReadLine();
        }

        static string FindPi(int n)
        {   // start time counter here 
            var watch = System.Diagnostics.Stopwatch.StartNew();
            // evrey digit of pi added to this string 
            string myPi = null;
            //more about this method here:https://habrahabr.ru/post/188700/
            int[] piArray = new int[10 * n / 3];
            int[] reminders = new int[(10 * n / 3)];
            reminders[reminders.Length - 1] = 0;
            for (int i = 0; i < piArray.Length - 1; i++)
            {
                piArray[i] = 2;
            }
            //this part taken from main j cycle only for put ',' after '3'
            FoundOneDigit(ref piArray,ref reminders);
            myPi += ((piArray[0] * 10 + reminders[0]) / 10);
            piArray[0] = (piArray[0] * 10 + reminders[0]) % 10;
            myPi += ',';

            int potentiallyWrongNumbersCounter = 0;
            int progressPercent = 1;
            for (int j = 2; j <= n; j++)
            {
                progressPercent = CountPercentage(n, progressPercent, j,watch);
                FoundOneDigit(ref piArray, ref reminders);
                /*sometimes when we divide on 10 we can get digit >9 , then we have to change previous digits +1, 
                and if prev digit is 9 we have to change digit before that and etc, thats why we must have counter of nines(PotentiallyWrongNumbersCounter)*/
                if (((piArray[0] * 10 + reminders[0]) / 10) == 9)
                    potentiallyWrongNumbersCounter++;
                else potentiallyWrongNumbersCounter = 0;

                myPi = CorrectWrongNumbers(myPi, piArray, reminders, potentiallyWrongNumbersCounter);
                piArray[0] = (piArray[0] * 10 + reminders[0]) % 10;
            }
            watch.Stop();
            Console.WriteLine("Calculation time is:{0}ms ({1}s))", watch.ElapsedMilliseconds, watch.ElapsedMilliseconds / 1000);
            return myPi;
        }


        private static int CountPercentage(int n, int progressPercent, int j, System.Diagnostics.Stopwatch watch)
        {
            if ((Convert.ToDouble(j) / n * 100) > progressPercent)
            {
                Console.WriteLine("Progress:{0}% (Time:{1}ms ({2}s)", progressPercent, watch.ElapsedMilliseconds, watch.ElapsedMilliseconds / 1000);
                while ((Convert.ToDouble(j)) / n * 100 > progressPercent)
                {
                    progressPercent++;
                }
            }

            return progressPercent;
        }

        //if we have situation with digit > 9 we must add to all previous potentially wrong nubers(that was = 9 and one before) +1
        private static string CorrectWrongNumbers(string myPi, int[] piArray, int[] reminders, int potentiallyWrongNumbersCounter)
        {
            if (((piArray[0] * 10 + reminders[0]) / 10) > 9)
            {
                for (int i = myPi.Length - 1; i >= myPi.Length - potentiallyWrongNumbersCounter - 1; i--)
                {
                    StringBuilder strB = new StringBuilder(myPi);
                    if (i == myPi.Length - potentiallyWrongNumbersCounter - 1)
                        strB[i] = Convert.ToChar(Convert.ToInt32(strB[i]) + 1);
                    else
                        strB[i] = '0';
                }
                myPi += ((piArray[0] * 10 + reminders[0]) / 100);
            }
            else
                myPi += ((piArray[0] * 10 + reminders[0]) / 10);
            return myPi;
        }

        //manipulation needed to fine one digit
        private static void FoundOneDigit(ref int[] piArray,ref int[] reminders)
        {
            for (int i = piArray.Length - 1; i > 0; i--)
            {
                reminders[i - 1] = (piArray[i] * 10 + reminders[i]) / (i * 2 + 1) * i;
                piArray[i] = (piArray[i] * 10 + reminders[i]) % (i * 2 + 1);

            }
        }

        private static int ValidateInput(string userInputString)
        {
            int numberOfNumbers;
            while ((!int.TryParse(userInputString, out numberOfNumbers)) || (numberOfNumbers < 0))
            {
                Console.Write("Write correct value:");
                userInputString = Console.ReadLine();
            }
            return numberOfNumbers;
        }

    }
}
