using System;
using System.Threading;
namespace Program

{
    class Teste {
        static Thread[] t = new Thread[5];
        static Semaphore semaphore = new Semaphore(2,2);

        static Thread producerThread;
        static Thread consumerThread;
        // static Semaphore empty = new Semaphore(10, 10);
        // static Semaphore full = new Semaphore(0, 10);
        static SemaphoreSlim empty = new SemaphoreSlim(10, 10);
        static SemaphoreSlim full = new SemaphoreSlim(0, 10);
        static Semaphore mutex = new Semaphore(1, 1);
        static void DoThing() {
            Console.WriteLine("{0} = waiting", Thread.CurrentThread.Name);
            semaphore.WaitOne();
            Console.WriteLine("{0} begins!", Thread.CurrentThread.Name);
            Thread.Sleep(1000);
            Console.WriteLine("{0} releasing...", Thread.CurrentThread.Name);
            semaphore.Release();
        }

        static void Producer() {
            while(true) {
                Console.WriteLine("Producing item...");
                Console.WriteLine(empty.CurrentCount + " empty items on buffer");
                empty.Wait();
                mutex.WaitOne();
                Thread.Sleep(500);
                Console.WriteLine("Putting item in the buffer...");
                mutex.Release();
                full.Release();
            }
        }

        static void Consumer() {
            while(true) {
                Console.WriteLine("Consuming item...");
                Console.WriteLine(full.CurrentCount + " used items on buffer");
                full.Wait();
                mutex.WaitOne();
                Console.WriteLine("Removing item from buffer...");
                mutex.Release();
                empty.Release();
            }
        }

        static void Main(string[] args) {
            // for (int i=0; i<5; i++) {
            //     t[i] = new Thread(DoThing);
            //     t[i].Name = "Thread de número: " + i;
            //     t[i].Start();
            // }
            // Console.Read();

            producerThread = new Thread(Producer);
            consumerThread = new Thread(Consumer);
            producerThread.Start();
            consumerThread.Start();
        }
    }
}