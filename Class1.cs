using DBFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DBFirst
{
    internal class Class1
    {
        private static Emp addemployyedetails;

        static void Main(string[] args)
        {
            GetAllEmpDetails();
            Console.WriteLine();
            GetAllEmpDetailsbydeptno();
            GetAllEmpDetailsbyjob();
            GetAllEmpDetailsbyempno();

            AddEmployee();
            deleteEmployee();
            UpdateEmployee();
            getstoredprocedue();
            updatestoredprocedure();

            addstoredprocedue();

        }


        public static void AddEmployee()
        {
            var ctx = new SreedharContext();
            var t = new Emp { Empno = 1234, Ename = "Robert", Job = "ANALYST", Mgr = 7839, Hiredate = DateTime.Now, Sal = 3000, Comm = 200, Deptno = 20 };
            ctx.Emps.Add(t);
            ctx.SaveChanges();
            var empDetails = ctx.Emps;
            foreach (var emp in empDetails)
            {
                Console.WriteLine(emp.Ename + " " + emp.Job + " " + emp.Sal);
            }
        }

        public static void deleteEmployee()
        {
            var ctx = new SreedharContext();
            var deleteemployees = ctx.Emps.Where(emp => emp.Empno == 1234).SingleOrDefault();
            ctx.Remove(deleteemployees);
            Console.WriteLine("Employee deleted");
            ctx.SaveChanges();
        }

        private static void UpdateEmployee()
        {
            var ctx=new SreedharContext();
            var updateemployees = ctx.Emps.Where(emp => emp.Empno == 1234).SingleOrDefault();
            updateemployees.Sal= 10;
            //ctx.Emps.Update(updateemployees);
            ctx.Update(updateemployees);
            ctx.SaveChanges();
            Console.WriteLine("Updated");
          

            //var employees = ctx.Emps;
            //foreach (var employee in employees)
            //{
            //    Console.WriteLine(employee.Ename + "  " + employee.Sal+ "   " );
            //}


        }

        private static void addstoredprocedue()
        {
            var ctx = new SreedharContext();
            var employee = ctx.Emps.FromSqlRaw("Addnewemp @p0,@p1,@p2", 200, "nohit",40);
            Console.WriteLine("Insert Sucessfull");
            var employees = ctx.Emps;
            ctx.SaveChanges();

            foreach(var emp in employees)
            {
                Console.WriteLine(emp.Empno + "  " + emp.Ename);
            }

        }


        private static void updatestoredprocedure()
        {
            var ctx = new SreedharContext();
            var employees = ctx.Emps.FromSqlRaw("updateempname @p0,@p1", 1234, "sreedhar");
            Console.WriteLine("Update sucessfull");
            var employee = ctx.Emps;
            ctx.SaveChanges();

            foreach (var emp in employee)
            {
                Console.WriteLine(emp.Empno + "  " + emp.Ename);
            }




        }

        private static void getstoredprocedue()
        {
            var ctx = new SreedharContext();
            List <Emp> employees = ctx.Set<Emp>().FromSqlRaw("GetEmpDetails").ToList();

            foreach (Emp emp in employees)
            {
                Console.WriteLine(emp.Ename + "  " + emp.Empno);
            }
          
        }

        private static void GetAllEmpDetails()
        {
            var ctx = new SreedharContext();
            var employees = ctx.Emps;
            foreach (var employee in employees)
            {
                Console.WriteLine(employee.Ename + "  " + employee.Sal);
            }
        }
        private static void GetAllEmpDetailsbydeptno()
        {
            var ctx = new SreedharContext();
            var employees = ctx.Emps.Where(emp => emp.Deptno == 10);

            foreach (var employee in employees)
            {
                Console.WriteLine(employee.Ename);
            }
        }

        private static void GetAllEmpDetailsbyjob()
        {
            var ctx = new SreedharContext();
            var employees = ctx.Emps.Where(emp => emp.Job == "clerk");

            foreach (var employee in employees)
            {
                Console.WriteLine(employee.Ename);
            }
        }

        private static void GetAllEmpDetailsbyempno()
        {
            var ctx = new SreedharContext();
            var employees = ctx.Emps.Where(emp => emp.Empno == 7839);

            foreach (var employee in employees)
            {
                Console.WriteLine(employee.Ename);
            }
        }

    }
}


