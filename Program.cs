using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using User = Back_end.User;

namespace Student_Management_System
{
    class Program
    {
        static void Main(string[] args)
        {


            DBConnect db = new DBConnect();
            User user = new User(db);
           
            db.OpenConnection();
            Console.WriteLine("Connection is open");

            Console.WriteLine(user.get_first_name(1));
            db.close_data_reader();
            List<string> temp1 = new List<string>();
            temp1 = user.get_all_user();
            Console.WriteLine(temp1.Count);
            for (int i = 0; i < temp1.Count; i++)
            {
                Console.WriteLine("id =" + temp1[i]);
                Console.WriteLine("fname =" + temp1[i + 1]);
                Console.WriteLine("lname =" + temp1[i + 2]);
                Console.WriteLine("username =" + temp1[i + 3]);
                Console.WriteLine("password =" + temp1[i + 4]);
                Console.WriteLine("email=" + temp1[i + 5]);
                Console.WriteLine("permission =" + temp1[i + 6]);
                Console.WriteLine("-----------------------------");
                i = i + 6;
            }
            Console.WriteLine("--------------------------------------------------------------------------");
          

            db.CloseConnection();
            Console.WriteLine("Connection is closed");
        }
    }
}
