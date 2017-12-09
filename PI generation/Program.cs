using System;


namespace PI_generation
{
    class Program
    {
        static void Main(string[] args)
        { 
            Console.Write("What accuracy you want?(numbers after the decimal point)\n");
            int n = Convert.ToInt32(Console.ReadLine());
            Console.Write(FindPi(n+1));      
            Console.Write("\nPress enter to exit");
            Console.ReadLine();
         }

       static string FindPi(int n)
        {   // start time counter here 
            var watch = System.Diagnostics.Stopwatch.StartNew();
            // evrey digit of pi added to it string 
            string myPi= "";

            //more about this method here:https://habrahabr.ru/post/188700/
            int[] PiArray = new int[10*n/3];
            int[] Reminders = new int[(10*n/3)];
            Reminders[Reminders.Length - 1] = 0;

                for (int i = 0; i < PiArray.Length-1; i++)
            {
                PiArray[i] = 2;   
            }

                //this part taken from main j cycle only for put ',' after '3'
            for (int i = PiArray.Length - 1; i > 0; i--)
            {
                Reminders[i - 1] = (PiArray[i] * 10 + Reminders[i]) / (i * 2 + 1) * i;
                PiArray[i] = (PiArray[i] * 10 + Reminders[i]) % (i * 2 + 1);

            }
            myPi += ((PiArray[0] * 10 + Reminders[0]) / 10);
            PiArray[0] = (PiArray[0] * 10 + Reminders[0]) % 10;
            myPi += ',';


            for (int j= 2; j<=n; j++)
            {
                for (int i = PiArray.Length - 1; i > 0; i--)
                {
                    Reminders[i - 1] = (PiArray[i] * 10 + Reminders[i]) / (i * 2 + 1) * i;
                    PiArray[i] = (PiArray[i] * 10 + Reminders[i]) % (i * 2 + 1);

                }
                myPi += ((PiArray[0] * 10 + Reminders[0]) / 10);
                PiArray[0] = (PiArray[0] * 10 + Reminders[0]) % 10;
               
            }
    watch.Stop();
            Console.WriteLine(String.Format("Calculation time is:{0}ms ({1}s)", watch.ElapsedMilliseconds, watch.ElapsedMilliseconds/1000));
            return myPi;
        }

    }
}
