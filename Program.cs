using System;
using System.Threading;
namespace Program

{
    class Teste {
        static Thread[] t = new Thread[5];
        static Semaphore semaphore = new Semaphore(2,2);
        static void DoThing() {
            Console.WriteLine("{0} = waiting", Thread.CurrentThread.Name);
            semaphore.WaitOne();
            Console.WriteLine("{0} begins!", Thread.CurrentThread.Name);
            Thread.Sleep(1000);
            Console.WriteLine("{0} releasing...", Thread.CurrentThread.Name);
            semaphore.Release();
        }

        static void Main(string[] args) {
            for (int i=0; i<5; i++) {
                t[i] = new Thread(DoThing);
                t[i].Name = "Thread de número: " + i;
                t[i].Start();
            }
            Console.Read();
        }
    }
}