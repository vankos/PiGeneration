using System;
using System.Text;

namespace PI_generation
{
    class Program
    {
        private static void Main(string[] args)
        {
            Console.Write("What accuracy you want?(numbers after the decimal point): ");
            int NumberOfNumbers = new int();
            string UserInputString = Console.ReadLine();
            NumberOfNumbers = ValidateInput(ref UserInputString);
            Console.Write(FindPi(NumberOfNumbers + 1));
            Console.Write("\nPress enter to exit");
            Console.ReadLine();
        }

        static string FindPi(int n)
        {   // start time counter here 
            var watch = System.Diagnostics.Stopwatch.StartNew();
            // evrey digit of pi added to this string 
            string myPi = null;
            //more about this method here:https://habrahabr.ru/post/188700/
            int[] PiArray = new int[10 * n / 3];
            int[] Reminders = new int[(10 * n / 3)];
            Reminders[Reminders.Length - 1] = 0;
            for (int i = 0; i < PiArray.Length - 1; i++)
            {
                PiArray[i] = 2;
            }
            //this part taken from main j cycle only for put ',' after '3'
            FoundOneDigit(ref PiArray,ref Reminders);
            myPi += ((PiArray[0] * 10 + Reminders[0]) / 10);
            PiArray[0] = (PiArray[0] * 10 + Reminders[0]) % 10;
            myPi += ',';

            int PotentiallyWrongNumbersCounter = 0;
            int ProgressPercent = 1;
            for (int j = 2; j <= n; j++)
            {
                ProgressPercent = CountPercentage(n, ProgressPercent, j);
                FoundOneDigit(ref PiArray, ref Reminders);
                /*sometimes when we divide on 10 we can get digit >9 , then we have to change previous digits +1, 
                and if prev digit is 9 we have to change digit before that and etc, thats why we must have counter of nines(PotentiallyWrongNumbersCounter)*/
                if (((PiArray[0] * 10 + Reminders[0]) / 10) == 9)
                    PotentiallyWrongNumbersCounter++;
                else PotentiallyWrongNumbersCounter = 0;

                myPi = CorrectWrongNumbers(myPi, PiArray, Reminders, PotentiallyWrongNumbersCounter);
                PiArray[0] = (PiArray[0] * 10 + Reminders[0]) % 10;
            }
            watch.Stop();
            Console.WriteLine("Calculation time is:{0}ms ({1}s)", watch.ElapsedMilliseconds, watch.ElapsedMilliseconds / 1000);
            return myPi;
        }


        private static int CountPercentage(int n, int ProgressPercent, int j)
        {
            if ((Convert.ToDouble(j) / n * 100) > ProgressPercent)
            {
                Console.Clear();
                Console.WriteLine("Progress:{0}%", ProgressPercent);
                while ((Convert.ToDouble(j)) / n * 100 > ProgressPercent)
                {
                    ProgressPercent++;
                }
            }

            return ProgressPercent;
        }

        //if we have situation with digit > 9 we must add to all previous potentially wrong nubers(that was = 9 and one before) +1
        private static string CorrectWrongNumbers(string myPi, int[] PiArray, int[] Reminders, int PotentiallyWrongNumbersCounter)
        {
            if (((PiArray[0] * 10 + Reminders[0]) / 10) > 9)
            {
                for (int i = myPi.Length - 1; i >= myPi.Length - PotentiallyWrongNumbersCounter - 1; i--)
                {
                    StringBuilder strB = new StringBuilder(myPi);
                    if (i == myPi.Length - PotentiallyWrongNumbersCounter - 1)
                        strB[i] = Convert.ToChar(Convert.ToInt32(strB[i]) + 1);
                    else
                        strB[i] = '0';
                }
                myPi += ((PiArray[0] * 10 + Reminders[0]) / 100);
            }
            else
                myPi += ((PiArray[0] * 10 + Reminders[0]) / 10);
            return myPi;
        }

        //manipulation needed to fine one digit
        private static void FoundOneDigit(ref int[] PiArray,ref int[] Reminders)
        {
            for (int i = PiArray.Length - 1; i > 0; i--)
            {
                Reminders[i - 1] = (PiArray[i] * 10 + Reminders[i]) / (i * 2 + 1) * i;
                PiArray[i] = (PiArray[i] * 10 + Reminders[i]) % (i * 2 + 1);

            }
        }

        private static int ValidateInput(ref string UserInputString)
        {
            int NumberOfNumbers;
            while ((!int.TryParse(UserInputString, out NumberOfNumbers)) || (NumberOfNumbers < 0))
            {
                Console.Write("Write correct value:");
                UserInputString = Console.ReadLine();
            }
            return NumberOfNumbers;
        }

    }
}
