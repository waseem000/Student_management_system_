using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using MySql.Data.Types;

using Student_Management_System;
namespace Back_end
{
    class Student_Account
    {

        private DBConnect database = new DBConnect();
        private string sql_statment = null;
        private MySqlDataReader res;
        private Took_Courses took_courses = object_initialization.took_courses;
        private double fixed_tuition = 4000;
        private double additional_tuition = 1500;
        public Student_Account ()
        {
            //do nothing
        }

        public Student_Account (DBConnect conn)
        {
            database = conn;
        }


        public bool add_student_entry (int student_id)
        {
            bool success = true;
            double student_tuition = generate_student_tuition(student_id);
            double paid_fees =0;
            double remaining_fees = student_tuition;
            bool is_full_paid = false;
            DateTime deadline = object_initialization.get_deadline() ;
            DateTime paid_date = DateTime.MinValue;
            double late_payment_fine=0;
            bool is_fined=false;
            int receipt_id = 0;
            string MySQLFormatDate = deadline.ToString("yyyy-MM-dd HH:mm:ss");
            sql_statment = "insert into student_account (Students_student_id,student_tuition,paid_fees,remaining_fees, is_full_paid,deadline,paid_date,late_payment_fine,is_fined,receipt_id) values('" + student_id + "','" + student_tuition + "','" + paid_fees + "','" + remaining_fees + "','" + is_full_paid + "','" + MySQLFormatDate + "','" + paid_date + "',' " + late_payment_fine + "',' " + is_fined + "','" + receipt_id + "')";
            Console.WriteLine(sql_statment);
            success = database.execute_statment(sql_statment);
            if (success)
            {
                Console.Write(success);
                return true;
            }
            else
            {
                Console.Write(success);
                return false;
            }

        }

        public virtual bool remove_student_enrty (int student_id)
        {
            bool success = true;
            sql_statment = "delete from student_account where Students_student_id= '" + student_id  + "'";
            success = database.execute_statment(sql_statment);
            if (success)
            {
                Console.Write(success);
                return true;
            }
            else
            {
                Console.Write(success);
                return false;
            }
        }
        public double get_paid_fees(int student_id)
        {
            double paid_fees = 0.0;
            sql_statment = "select paid_fees from student_account where Students_student_id ='" + student_id +  "'";
            res = database.execute_query(sql_statment);
            try
            {
                while (res.Read())
                {
                    try
                    {
                        paid_fees = Double.Parse(res["paid_fees"] + "");
                    }
                    catch (MySqlException e)
                    {
                        // TODO Auto-generated catch block
                        Console.WriteLine(e.ToString());
                        Console.Write(e.StackTrace);
                    }
                    Console.WriteLine(paid_fees);
                }
            }
            catch (MySqlException e)
            {
                // TODO Auto-generated catch block
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }
            database.close_data_reader();
            return paid_fees;

        }
        public bool set_paid_fees (int student_id, double amount)
        {
            bool success = false;
            double previous_payment = get_paid_fees(student_id);
            double new_paid_fees = previous_payment + amount;
            sql_statment = "UPDATE  student_account SET paid_fees = '" + new_paid_fees + "'where Students_student_id ='" + student_id + "'";
            Console.WriteLine(sql_statment);
           success= database.execute_statment(sql_statment);

            if (success)
            {
                set_remaining_fees(student_id);
                Console.Write(success);
                return true;
            }
            else
            {
                Console.Write(success);
                return false;
            }
        }

        public double get_remaining_fees (int student_id)
        {
            double remaining_fees = 0.0;
            sql_statment = "select remaining_fees from student_account where Students_student_id ='" + student_id + "'";
            res = database.execute_query(sql_statment);
            try
            {
                while (res.Read())
                {
                    try
                    {
                        remaining_fees = Double.Parse(res["remaining_fees"] + "");
                    }
                    catch (MySqlException e)
                    {
                        // TODO Auto-generated catch block
                        Console.WriteLine(e.ToString());
                        Console.Write(e.StackTrace);
                    }
                    Console.WriteLine(remaining_fees);
                }
            }
            catch (MySqlException e)
            {
                // TODO Auto-generated catch block
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }
            database.close_data_reader();
            return remaining_fees;
        }
        public bool set_remaining_fees (int student_id)
        {
            bool success = false;
            double paid_fees = get_paid_fees(student_id);
            double total_fees = get_student_tuition(student_id);
            double remaining_fees = total_fees - paid_fees;
           // bool fined = is_fined(student_id);
            if (remaining_fees == 0)
            {
                set_is_full_paid(student_id, true);
            }
            else if (remaining_fees > 0)
            {
                set_is_full_paid(student_id, false);
            }
            sql_statment = "UPDATE  student_account SET remaining_fees = '" + remaining_fees + "' where Students_student_id ='" + student_id + "'";
            Console.WriteLine(sql_statment);
             success= database.execute_statment(sql_statment);

            if (success)
            {
                Console.Write(success);
                return true;
            }
            else
            {
                Console.Write(success);
                return false;
            }
        }

        public bool is_full_paid (int student_id)
        {

            bool is_full_paid = false;
            sql_statment = "select is_full_paid from student_account where Students_student_id ='" + student_id + "'";
            res = database.execute_query(sql_statment);
            try
            {
                while (res.Read())
                {
                    try
                    {
                        is_full_paid = Boolean.Parse(res["is_full_paid"] + "");
                    }
                    catch (MySqlException e)
                    {
                        // TODO Auto-generated catch block
                        Console.WriteLine(e.ToString());
                        Console.Write(e.StackTrace);
                    }
                    Console.WriteLine(is_full_paid);
                }
            }
            catch (MySqlException e)
            {
                // TODO Auto-generated catch block
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }
            database.close_data_reader();
            return is_full_paid;
        }
        public bool set_is_full_paid (int student_id, bool is_full_paid)
        {

            bool success = false;

            sql_statment = "UPDATE  student_account SET is_full_paid = '" + is_full_paid + "' where Students_student_id ='" + student_id + "'";
            Console.WriteLine(sql_statment);
             success= database.execute_statment(sql_statment);

            if (success)
            {
                Console.Write(success);
                return true;
            }
            else
            {
                Console.Write(success);
                return false;
            }
        }

        public DateTime get_paid_date (int student_id)
        {

            DateTime paid_date = DateTime.MinValue;

            
            sql_statment = "select paid_date from student_account where Students_student_id ='" + student_id + "'";
            res = database.execute_query(sql_statment);
            try
            {
                while (res.Read())
                {
                    try
                    {
                        paid_date = DateTime.Parse(res["paid_date"] + "");
                    }
                    catch (MySqlException e)
                    {
                        // TODO Auto-generated catch block
                        Console.WriteLine(e.ToString());
                        Console.Write(e.StackTrace);
                    }
                    Console.WriteLine(paid_date);
                }
            }
            catch (MySqlException e)
            {
                // TODO Auto-generated catch block
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }
            database.close_data_reader();
            return paid_date;
        }
        public bool set_paid_date (int student_id, DateTime date)
        {

            bool success = false;
            string MySQLFormatDate = date.ToString("yyyy-MM-dd HH:mm:ss");
            sql_statment = "UPDATE  student_account SET paid_date = '" + MySQLFormatDate + "' where Students_student_id ='" + student_id + "'";
            Console.WriteLine(sql_statment);
             success= database.execute_statment(sql_statment);

            if (success)
            {
                Console.Write(success);
                return true;
            }
            else
            {
                Console.Write(success);
                return false;
            }
        }

        public bool set_late_payment_fine (int student_id, double fine )
        {

            bool success = false;
            set_is_fined(student_id, true);
            sql_statment = "UPDATE  student_account SET late_payment_fine = '" + fine + "' where Students_student_id ='" + student_id + "'";
            Console.WriteLine(sql_statment);
             success= database.execute_statment(sql_statment);

            if (success)
            {
                set_fees(student_id, fine);
                Console.Write(success);
                return true;
            }
            else
            {
                Console.Write(success);
                return false;
            }
        }

        private bool set_fees (int student_id, double amount)
        {
            Double remaining_fees = get_remaining_fees(student_id);
            double new_fees = remaining_fees + amount;
            bool success = false;
          
            if (remaining_fees == 0)
            {
                set_is_full_paid(student_id, true);
            }
            else if (remaining_fees > 0)
            {
                set_is_full_paid(student_id, false);
            }
            sql_statment = "UPDATE  student_account SET remaining_fees = '" + new_fees + "' where Students_student_id ='" + student_id + "'";
            Console.WriteLine(sql_statment);
            success = database.execute_statment(sql_statment);

            if (success)
            {
                Console.Write(success);
                return true;
            }
            else
            {
                Console.Write(success);
                return false;
            }
        }

        public bool is_fined (int student_id)
        {

            bool is_fined = false;
            sql_statment = "select is_fined from student_account where Students_student_id ='" + student_id + "'";
            res = database.execute_query(sql_statment);
            try
            {
                while (res.Read())
                {
                    try
                    {
                        is_fined = Boolean.Parse(res["is_fined"] + "");
                    }
                    catch (MySqlException e)
                    {
                        // TODO Auto-generated catch block
                        Console.WriteLine(e.ToString());
                        Console.Write(e.StackTrace);
                    }
                    Console.WriteLine(is_fined);
                }
            }
            catch (MySqlException e)
            {
                // TODO Auto-generated catch block
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }
            database.close_data_reader();
            return is_fined;
        }
        public bool set_is_fined (int student_id, bool is_fined)
        {

            bool success = false;

            sql_statment = "UPDATE  student_account SET is_fined  = '" + is_fined + "' where Students_student_id ='" + student_id + "'";
            Console.WriteLine(sql_statment);
             success= database.execute_statment(sql_statment);

            if (success)
            {
                Console.Write(success);
                return true;
            }
            else
            {
                Console.Write(success);
                return false;
            }
        }

        public double get_receipt_id (int student_id)
        {
            
            int receipt_id = 0;
            sql_statment = "select receipt_id from student_account where Students_student_id ='" + student_id + "'";
            res = database.execute_query(sql_statment);
            try
            {
                while (res.Read())
                {
                    try
                    {
                        receipt_id = int.Parse(res["receipt_id"] + "");
                    }
                    catch (MySqlException e)
                    {
                        // TODO Auto-generated catch block
                        Console.WriteLine(e.ToString());
                        Console.Write(e.StackTrace);
                    }
                    Console.WriteLine(receipt_id);
                }
            }
            catch (MySqlException e)
            {
                // TODO Auto-generated catch block
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }
            database.close_data_reader();
            return receipt_id;
        }
        public bool set_receipt_id (int student_id, int receipt_id)
        {

            bool success = false;

            sql_statment = "UPDATE  student_account SET receipt_id  = '" + receipt_id + "' where Students_student_id ='" + student_id + "'";
            Console.WriteLine(sql_statment);
             success= database.execute_statment(sql_statment);

            if (success)
            {
                Console.Write(success);
                return true;
            }
            else
            {
                Console.Write(success);
                return false;
            }
        }

 
        private double generate_student_tuition (int student_id)
        {
            took_courses = object_initialization.took_courses;
           
            double tuition=0;
            List<int> student_courses = took_courses.get_student_courses(student_id);
            int num_courses = student_courses.Count;

            tuition = (num_courses * fixed_tuition) + additional_tuition;
            return tuition;


        }

        public double get_student_tuition (int student_id)
        {

            double student_tuition = 0.0;
            sql_statment = "select student_tuition from student_account where Students_student_id ='" + student_id + "'";
            res = database.execute_query(sql_statment);
            try
            {
                while (res.Read())
                {
                    try
                    {
                        student_tuition = Double.Parse(res["student_tuition"] + "");
                    }
                    catch (MySqlException e)
                    {
                        // TODO Auto-generated catch block
                        Console.WriteLine(e.ToString());
                        Console.Write(e.StackTrace);
                    }
                    Console.WriteLine(student_tuition);
                }
            }
            catch (MySqlException e)
            {
                // TODO Auto-generated catch block
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }
            database.close_data_reader();
            return student_tuition;
        }

        public bool set_student_tuition (int student_id)
        {

            bool success = false;
           double student_tuition= generate_student_tuition(student_id);
            sql_statment = "UPDATE  student_account SET student_tuition  = '" + student_tuition + "' where Students_student_id ='" + student_id + "'";
            Console.WriteLine(sql_statment);
            success = database.execute_statment(sql_statment);

            if (success)
            {
                set_remaining_fees(student_id);
                Console.Write(success);
                return true;
            }
            else
            {
                Console.Write(success);
                return false;
            }
        }
        public virtual List<string> get_student_account (int student_id)
        {
            List<string> account_info = new List<string>();
            sql_statment = "select * from student_account where Students_student_id ='" + student_id + "'";
            //System.out.println(sql_statment);
            res = database.execute_query(sql_statment);
            try
            {
                while (res.Read())
                {
                    try
                    {

                        account_info.Add(Convert.ToString(int.Parse(res["Students_student_id"] + "")));
                        account_info.Add(Convert.ToString(Double.Parse(res["student_tuition"] + "")));
                        account_info.Add(Convert.ToString(Double.Parse(res["paid_fees"] + "")));
                        account_info.Add(Convert.ToString(Double.Parse(res["remaining_fees"] + "")));
                        account_info.Add(Convert.ToString(Boolean.Parse(res["is_full_paid"] + "")));
                        account_info.Add(Convert.ToString(DateTime.Parse(res["deadline"] + ""))); ;
                        account_info.Add(Convert.ToString(DateTime.Parse(res["paid_date"] + "")));
                        account_info.Add(Convert.ToString(Double.Parse(res["late_payment_fine"] + "")));
                        account_info.Add(Convert.ToString(Boolean.Parse(res["is_fined"] + "")));
                        account_info.Add(Convert.ToString(int.Parse(res["receipt_id"] + "")));


                    }
                    catch (MySqlException e)
                    {
                        // TODO Auto-generated catch block
                        Console.WriteLine(e.ToString());
                        Console.Write(e.StackTrace);
                    }
                }
            }
            catch (MySqlException e)
            {
                // TODO Auto-generated catch block
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }
            database.close_data_reader();
            return account_info;
        }

    }
}
