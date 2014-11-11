using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using MySql.Data.Types;

using User = Back_end.User;
using Student_Management_System;
namespace Back_end
{
    public class Staff
    {

        //private int id;
        //private string first_name;
        //private string last_name;
        //private string email;
        //private int permission_type;

        private DBConnect database = new DBConnect();
        private string sql_statment = null;
        private MySqlDataReader res;

        private User user = object_initialization.user;//new User();

        public Staff ()
        {
            //do nothing
        }
        public Staff (DBConnect conn)
        {
            database = conn;
        }

        public virtual bool add_staff (string first_name, string last_name, string email, int permission_type)
        {
           bool success;
            string password = first_name;
            string user_name = first_name + "_" + last_name;


            sql_statment = "insert into users(first_name,last_name,user_name,password,email,permission_type) values('" + first_name + "'" + "," + "'" + last_name + "'," + "'" + user_name + "'," + "'" + password + "'," + "'" + email + "'," + "'" + permission_type + "')";
            Console.WriteLine(sql_statment);
            success = database.execute_statment(sql_statment);

            int user_id = user.get_user_id(user_name, password);
            sql_statment = "insert into staff(first_name,last_name,email,permission_type, Users_user_id) values('" + first_name + "','" + last_name + "','" + email + "','" + permission_type + "','" + user_id + "')";
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

        public virtual bool update_staff (int id, string first_name, string last_name, string email, int permission_type)
        {
           bool success;
            int user_id = get_user_id(id);
            sql_statment = "UPDATE  staff SET first_name = '" + first_name + "', last_name = '" + last_name + "' ,email = '" + email + "' ,	permission_type= '" + permission_type + "', Users_user_id = '" + user_id + "' WHERE  staff_id = '" + id + "'";
            Console.WriteLine(sql_statment);
            success = database.execute_statment(sql_statment);

            sql_statment = "UPDATE  users SET first_name = '" + first_name + "', last_name = '" + last_name + "',user_name = '" + user.get_user_name(user_id) + "', password = '" + user.get_password(user_id) + "' ,email = '" + email + "' ,	permission_type= '" + permission_type + "' WHERE  user_id = '" + user_id + "'";
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

        public virtual bool delete_staff (int id)
        {
           bool success;
            int user_id = get_user_id(id);
            sql_statment = "delete from staff where staff_id = '" + id + "'";
            success = database.execute_statment(sql_statment);
            sql_statment = "delete from users where user_id = '" + user_id + "'";
            success = database.execute_statment(sql_statment);
            if (success )
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
        public virtual int get_user_id (int staff_id)
        {
            int id = 0;
            sql_statment = "select Users_user_id from staff where staff_id ='" + staff_id + "'";
            //	System.out.println(sql_statment);
            res = database.execute_query(sql_statment);
            try
            {
                while (res.Read())
                {
                    try
                    {
                        id = int.Parse(res["Users_user_id"] + "");
                    }
                    catch (MySqlException e)
                    {
                        // TODO Auto-generated catch block
                        Console.WriteLine(e.ToString());
                        Console.Write(e.StackTrace);
                    }
                    Console.WriteLine(id);
                }
            }
            catch (MySqlException e)
            {
                // TODO Auto-generated catch block
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }
            database.close_data_reader();
            return id;
        }
        public virtual string get_first_name (int id)
        {
            string first_name = null;
            sql_statment = "select first_name from Staff where staff_id ='" + id + "'";
            //	System.out.println(sql_statment);
            res = database.execute_query(sql_statment);
            try
            {
                while (res.Read())
                {
                    try
                    {
                        first_name = res["first_name"]+"";
                    }
                    catch (MySqlException e)
                    {
                        // TODO Auto-generated catch block
                        Console.WriteLine(e.ToString());
                        Console.Write(e.StackTrace);
                    }
                    Console.WriteLine(first_name);
                }
            }
            catch (MySqlException e)
            {
                // TODO Auto-generated catch block
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }
            database.close_data_reader();
            return first_name;
        }

        public virtual string get_last_name (int id)
        {
            string last_name = null;
            sql_statment = "select last_name from Staff where staff_id ='" + id + "'";
            //	System.out.println(sql_statment);
            res = database.execute_query(sql_statment);
            try
            {
                while (res.Read())
                {
                    try
                    {
                        last_name =  res["last_name"] + "";
                    }
                    catch (MySqlException e)
                    {
                        // TODO Auto-generated catch block
                        Console.WriteLine(e.ToString());
                        Console.Write(e.StackTrace);
                    }
                    Console.WriteLine(last_name);
                }
            }
            catch (MySqlException e)
            {
                // TODO Auto-generated catch block
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }
            database.close_data_reader();
            return last_name;
        }

        public virtual string get_email (int id)
        {
            string email = null;
            sql_statment = "select email from staff where staff_id ='" + id + "'";
            //	System.out.println(sql_statment);
            res = database.execute_query(sql_statment);
            try
            {
                while (res.Read())
                {
                    try
                    {
                        email = res["email"] + "";
                    }
                    catch (MySqlException e)
                    {
                        // TODO Auto-generated catch block
                        Console.WriteLine(e.ToString());
                        Console.Write(e.StackTrace);
                    }
                    Console.WriteLine(email);
                }
            }
            catch (MySqlException e)
            {
                // TODO Auto-generated catch block
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }
            database.close_data_reader();
            return email;
        }

        public virtual int get_permission (int id)
        {
            int permission_type = -1;
            sql_statment = "select permission_type from staff where staff_id ='" + id + "'";
            //	System.out.println(sql_statment);
            res = database.execute_query(sql_statment);
            try
            {
                while (res.Read())
                {
                    try
                    {
                        permission_type = int.Parse( res["permission_type"] + "");
                    }
                    catch (MySqlException e)
                    {
                        // TODO Auto-generated catch block
                        Console.WriteLine(e.ToString());
                        Console.Write(e.StackTrace);
                    }
                    Console.WriteLine(permission_type);
                }
            }
            catch (MySqlException e)
            {
                // TODO Auto-generated catch block
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }
            database.close_data_reader();
            return permission_type;
        }

        public virtual List<string> get_staff (int id)
        {
            List<string> staff = new List<string>();
            sql_statment = "select * from staff where staff_id ='" + id + "'";
            //System.out.println(sql_statment);
            res = database.execute_query(sql_statment);
            try
            {
                while (res.Read())
                {
                    try
                    {

                        staff.Add(Convert.ToString(int.Parse( res["staff_id"] + "")));
                        staff.Add( res["first_name"] + "");
                        staff.Add( res["last_name"] + "");
                        staff.Add( res["email"] + "");
                        staff.Add(Convert.ToString(int.Parse( res["permission_type"] + "")));
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
            return staff;
        }

        public virtual List<string> get_all_staff ()
        {
            List<string> all_staff = new List<string>();
            sql_statment = "select * from staff";
            //	System.out.println(sql_statment);
            res = database.execute_query(sql_statment);
            try
            {
                while (res.Read())
                {
                    try
                    {
                        all_staff.Add(Convert.ToString(int.Parse(res["staff_id"] + "")));
                        all_staff.Add(res["first_name"] + "");
                        all_staff.Add(res["last_name"] + "");
                        all_staff.Add(res["email"] + "");
                        all_staff.Add(Convert.ToString(int.Parse(res["permission_type"] + "")));

                        // all_users.add(user);

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
            return all_staff;
        }
    }

}