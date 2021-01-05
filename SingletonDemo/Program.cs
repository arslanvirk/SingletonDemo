using SingletonDemo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SingletonDemo
{
    class Program
    {
        //DBContect is not Thread Safe
        static SingletonDbEntities db = SingletonDbEntities.GetInstance;
        static void Main(string[] args)
        {
            Parallel.Invoke(
                () => PrintStudentdetails(),
                () => PrintEmployeeDetails(),
                () => PrintEmployeeDetail3()
                );
            //Thread t1 = new Thread(PrintStudentdetails);
            //t1.Name = "Thread1";
            //Thread t2 = new Thread(PrintEmployeeDetails);
            //t2.Name = "Thread2";
            //t1.Start();
            //t2.Start();
            Console.WriteLine($"Main Thread Id: {Thread.CurrentThread.ManagedThreadId}, Name: {Thread.CurrentThread.Name}, IsBackgroun: {Thread.CurrentThread.IsBackground}");
            Console.WriteLine("Hello End");
            Console.ReadLine();
        }
        private static void PrintEmployeeDetails()
        {
            /*
             * Assuming Singleton is created from employee class
             * we refer to the GetInstance property from the Singleton class
             */
            // Singleton fromEmployee = Singleton.GetInstance;
            //fromEmployee.PrintDetails("From Employee");

            Employee emp = new Employee()
            {
                Id = 1,
                Name = "Arslan"
            };
            Console.WriteLine($"Employee Task 2: {emp.Id}, {emp.Name}");
            Console.WriteLine($"Task 2 Id: {Thread.CurrentThread.ManagedThreadId}, Name: {Thread.CurrentThread.Name}, IsBackgroun: {Thread.CurrentThread.IsBackground}");
            //DbSave(emp);
        }

        private static async void PrintStudentdetails()
        {
            /*
                         * Assuming Singleton is created from student class
                         * we refer to the GetInstance property from the Singleton class
                         */
            //  Singleton fromStudent = Singleton.GetInstance;
            //fromStudent.PrintDetails("From Student");
            Employee emp = new Employee()
            {
                Id = 2,
                Name = "Bilal"
            };
            await Task.Run(() =>
            {
               for (Int64 i = 0; i < 10000000001; i++)
               {
                   if (i == 10000000000)
                   {
                       Console.WriteLine($"End Loop Task 1: {i}");
                       Console.WriteLine($"Task 1 Loop Id: {Thread.CurrentThread.ManagedThreadId}, Name: {Thread.CurrentThread.Name}, IsBackgroun: {Thread.CurrentThread.IsBackground}");
                   }
                   Int64 count = i + 100;
               }
           });
            Console.WriteLine($"Employee Task 1: {emp.Id}, {emp.Name}");
            Console.WriteLine($"Task 1 Id: {Thread.CurrentThread.ManagedThreadId}, Name: {Thread.CurrentThread.Name}, IsBackgroun: {Thread.CurrentThread.IsBackground}");
            // DbSave(emp);
        }

        private static void DbSave(Employee emp)
        {
            object _lock = new object();
            lock (_lock)

            {
                db.Employees.Add(emp);
                db.SaveChanges();
            }
        }
        private static async void PrintEmployeeDetail3()
        {
            await Task.Run(() =>
            {
                for (Int64 i = 0; i < 10000000001; i++)
                {
                    if (i == 10000000000)
                    {
                        Console.WriteLine($"End Loop Task 3: {i}");
                        Console.WriteLine($"Task 3 Loop Id: {Thread.CurrentThread.ManagedThreadId}, Name: {Thread.CurrentThread.Name}, IsBackgroun: {Thread.CurrentThread.IsBackground}");
                    }
                    Int64 count = i + 100;
                }
            });
            Console.WriteLine($"Task 3 Id: {Thread.CurrentThread.ManagedThreadId}, Name: {Thread.CurrentThread.Name}, IsBackgroun: {Thread.CurrentThread.IsBackground}");
        }
    }
}
