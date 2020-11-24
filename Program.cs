using System;
using System.Threading;



namespace TestThreads
{
    class Program
    {
        public const int MAX = 999;
        static SemaphoreSlim emptyMass = new SemaphoreSlim(MAX, MAX);
        static SemaphoreSlim emptyMeat = new SemaphoreSlim(MAX, MAX);
        static SemaphoreSlim emptyOil = new SemaphoreSlim(MAX, MAX);
        static SemaphoreSlim fullMass = new SemaphoreSlim(0, MAX);
        static SemaphoreSlim fullMeat = new SemaphoreSlim(0, MAX);
        static SemaphoreSlim fullOil = new SemaphoreSlim(0, MAX);
        static Semaphore mutex = new Semaphore(1,   1);


        public static int QNT_Massa = 1;
        public static int QNT_Oleo = 1;
        public static int QNT_Carne = 1;

        public static int Massa_ = 0;
        public static int Oleo_ = 0;
        public static int Carne_ = 0;
        public static int Dinero = 0;

        public static int Pastel_Carne = 0;

        static void Main(string[] args)
        {
            Thread p0 = new Thread(Produtor0);
            Thread p1 = new Thread(Produtor1);
            Thread p2 = new Thread(Produtor2);
            Thread c0 = new Thread(Consumidor_Pastel_Carne);

            p0.Start(); p1.Start(); p2.Start();  c0.Start();
        }
        static void Produtor0()
        {
            int item_massa = QNT_Massa;
            while (true)
            {
                Console.WriteLine("Producing item...");
                Console.WriteLine(emptyMass.CurrentCount + " empty MASS items on buffer");
                emptyMass.Wait(/*QNT_Massa*/);
                mutex.WaitOne();
                Massa_ += QNT_Massa;
                mutex.Release();
                fullMass.Release();
            }
        }
        static void Produtor1()
        {
            //int count = 1;
            while (true)
            {
                Console.WriteLine("Producing item...");
                Console.WriteLine(emptyOil.CurrentCount + " empty OIL items on buffer");
                emptyOil.Wait(/*QNT_Massa*/);
                mutex.WaitOne();
                Oleo_ += QNT_Carne;
                mutex.Release();
                fullOil.Release();
            }
        }
        static void Produtor2()
        {
            //int count = 1;
            while (true)
            {
                Console.WriteLine("Producing item...");
                Console.WriteLine(emptyMass.CurrentCount + " empty MEAT items on buffer");
                emptyMeat.Wait(/*QNT_Massa*/);
                mutex.WaitOne();
                Carne_ += QNT_Massa;
                mutex.Release();
                fullMeat.Release();
            }
        }

        static void Consumidor_Pastel_Carne ()
        {
            while (true)
            {
                Console.WriteLine("Consuming item...");
                Console.WriteLine(fullOil.CurrentCount + " used items on buffer");
                Console.WriteLine(fullMass.CurrentCount + " used items on buffer");
                Console.WriteLine(fullMeat.CurrentCount + " used items on buffer");
                fullOil.Wait();
                fullMeat.Wait();
                fullMass.Wait();
                mutex.WaitOne();

                Console.WriteLine("Removing item from buffer...");
                Massa_ -= 1; Carne_ -= 1; Oleo_ -= 1;

                mutex.Release();

                emptyOil.Release();
                emptyMeat.Release();
                emptyMass.Release();

                Pastel_Carne += 1;
                Console.WriteLine("Pastel = " + Pastel_Carne);
            }
        }
    }
}

