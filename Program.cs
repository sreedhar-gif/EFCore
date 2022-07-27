using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeFirstApporach.Models;
using CodeFirstApporach.Data;
using codefirstapproach.Model;
using Microsoft.EntityFrameworkCore;


namespace LearnCodeFirstApproach
{
    class Program
    {
        static void Main(string[] args)
        {
            //GetAllBookDetails();
            //AddBook();
            //DeleteBook();
            // UpdateBook();

            // AddCustomerandOrder();

            // GetAllCustomerandorder_Eagerloading();

            //GetAllCustomerandorder_Explicitloading();
            //jointables();
            joinprocedure();


            //DissconnectedArchitecture();
        }
        public static void AddBook()
        {
            var ctx = new BookContext();
            var b1 = new Book { BookID = 1, BookName = "Harry Potter", author = "JK Rowling", price = 2000, BookAge = 5 };
            var b2 = new Book { BookID = 2, BookName = "Mahabhrata", author = "Vedavyasa", price = 20000, BookAge = 10000 };
            var b3 = new Book { BookID = 3, BookName = "Ramayana", author = "Valmiki", price = 15000, BookAge = 15000 };
            var b4 = new Book { BookID = 4, BookName = "Ramacharita Manasa", author = "Kalidasa", price = 5000, BookAge = 1000 };
            ctx.Books.Add(b1);
            ctx.Books.Add(b2); ctx.SaveChanges();
            ctx.Books.Add(b3); ctx.SaveChanges();
            ctx.Books.Add(b4);
            ctx.SaveChanges();
            var BookDetails = ctx.Books;
            foreach (var bk in BookDetails)
            {
                Console.WriteLine(bk.BookName + " " + bk.author + " " + bk.price);
            }
        }
        public static void DeleteBook()
        {
            var ctx = new BookContext();
            var deleteBook = ctx.Books.Where(x => x.BookName == "Harry Potter").SingleOrDefault();
            ctx.Remove(deleteBook);
            Console.WriteLine("Deleted");
            ctx.SaveChanges();
            var empDetails = ctx.Books;
            var BookDetails = ctx.Books;
            foreach (var bk in BookDetails)
            {
                Console.WriteLine(bk.BookName + " " + bk.author + " " + bk.price);
            }
        }
        public static void UpdateBook()
        {
            var ctx = new BookContext();
            var updateBook = ctx.Books.Where(x => x.BookID == 2).SingleOrDefault();
            updateBook.BookName = "Vyasa Mahabharata";
            ctx.Books.Update(updateBook);
            Console.WriteLine("Updated");
            ctx.SaveChanges();
            var BookDetails = ctx.Books;
            foreach (var bk in BookDetails)
            {
                Console.WriteLine(bk.BookName + " " + bk.author + " " + bk.price);
            }

        }

        public static void GetAllBookDetails()
        {
            var ctx = new BookContext();
            var BookDetails = ctx.Books;
            foreach (var bk in BookDetails)
            {
                Console.WriteLine(bk.BookName + " " + bk.author + " " + bk.price);
            }

        }

        public static void AddCustomerandOrder()
        {
            var ctx = new BookContext();
            var Customer = new Customer();
            Customer.ID = 3;
            Customer.Name = "sreedhar";
            Customer.Age = 22;

            //var order=new Order();
            //order.Order_ID = 1110;
            //order.OrderDate = DateTime.Now;
            //order.Amount = 1000;

            //List<Order> myorders =new List<Order>();
            //myorders.Add(order);
            //Customer.Orders = myorders;

            //order.cust = Customer;

            //order.cust = ctx.Customers.Where(a => a.ID == 1).SingleOrDefault();


            try
            {
                ctx.Customers.Add(Customer);

                //ctx.Orders.Add(order);
                ctx.SaveChanges();
                Console.WriteLine("Customer and order is created");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.ToString());
            }
        }

        //eager loading or early loading
        public static void GetAllCustomerandorder_Eagerloading()
        {
            var ctx = new BookContext();
            try
            {
                var Customers = ctx.Customers.Include(o => o.Orders);

                foreach (var customer in Customers)
                {
                    Console.WriteLine(customer.Name);
                    Console.WriteLine("----------");

                    foreach (var order in customer.Orders)
                    {
                        Console.WriteLine(order.Amount + "  " + order.Order_ID);
                    }
                    Console.WriteLine("----------");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //Explicit loading
        public static void GetAllCustomerandorder_Explicitloading()
        {
            var ctx = new BookContext();
            try
            {
                var Customers = ctx.Customers;
                foreach (var customer in Customers)
                {
                    Console.WriteLine(customer.Name);
                    Console.WriteLine("----------");

                    ctx.Entry(customer).Collection(o => o.Orders).Load();

                    foreach (var order in customer.Orders)
                    {
                        Console.WriteLine(order.Amount.ToString() + "   " + order.OrderDate.ToString());
                    }

                    Console.WriteLine("----------");

                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void jointables()
        {
            var ctx = new BookContext();
            try
            {
                //    var result = ctx.Customers.GroupJoin(ctx.Orders, c => c.ID, o => o.cust.ID, (c, o) => new
                //    {
                //       c,
                //       generic=o.FirstOrDefault()
                //    });

                //    foreach (var item in result)
                //    {
                //        Console.WriteLine(item.cust + " " + item.o);

                //    }
                //}
                //catch (Exception ex)
                //{
                //    Console.WriteLine(ex.Message);
                //}
                var result = (from c in ctx.Customers
                              join o in ctx.Orders on c.ID equals o.cust.ID
                              select new
                              {
                                  Name = c.Name,
                                  Price = o.Amount
                              });
                foreach (var item in result)
                {
                    Console.WriteLine(item.Name + " " + item.Price);
                }




            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        //public void jointableslambda()
        //{
        //    var data = new List<Data>();
        //    var customerdetails = new List<CustomerList>();
        //    var orderlist=new List<OrderList>();

        //    var result = data.Join(customerdetails,
        //        d => d.CUSTOMER_NAME,
        //        g => g.ID,
        //        (d, g) => new
        //        {
        //            d,
        //            customer = g.FirstOrDefault()
        //        }).GroupJoin(orderlist,
        //        dd => dd.d.CUSTOMER_NAME,
        //        i => i.cust,
        //        (d, i) => new
        //        {
        //            data = d.d,
        //            orderlist = i.FirstOrDefault()
        //        });


        //}

        private static void DissconnectedArchitecture()
        {
            var ctx=new BookContext();
            var customer = ctx.Customers.Where(c => c.ID == 1).SingleOrDefault();
            ctx.Dispose();
            UpdateCustomerName(customer);
        }

        private static void UpdateCustomerName(Customer customer)
        {
            var ctx =new BookContext();
            customer.Name = "Peter";
            Console.WriteLine(ctx.Entry(customer).State.ToString());

            ctx.Attach(customer).State=EntityState.Modified;
            ctx.SaveChanges();
            Console.WriteLine("customer name is updated");

        }

        private static void joinprocedure()
        {
            var ctx=new BookContext();
            List<CustData> datas = ctx.Set<CustData>().FromSqlRaw("jointables").ToList();
            Console.WriteLine("Join Sucessfull");

            foreach (var item in datas) 
            {
                Console.WriteLine(item.NAME+"  "+item.OrderAmt);
            }          
            
        }
    }
}