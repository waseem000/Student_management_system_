using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using MySql.Data.Types;

namespace Back_end
{


    public class User : DBConnect
    {

        //private int id;
        //private string first_name;
        //private string last_name;
        //private string user_name;
        //private string password;
        //private string email;
        //private int permission_type; // 0 admin, 1 staff, 2 student

        private DBConnect database = new DBConnect();
        private string sql_statment = null;
        private MySqlDataReader res;

        public User ()
        {
            //do nothing
        }
        public User(DBConnect conn)
        {
            database = conn;
        }

        public virtual bool create_user(string first_name, string last_name, string user_name, string password, string email, int permission_type)
        {
            bool success = true;
            sql_statment = "insert into users(first_name,last_name,user_name,password,email,permission_type) values('" + first_name + "'" + "," + "'" + last_name + "'," + "'" + user_name + "'," + "'" + password + "'," + "'" + email + "'," + "'" + permission_type + "')";
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
        public virtual bool update_user(int id, string first_name, string last_name, string user_name, string password, string email, int permission_type)
        {
            bool success = true;
            sql_statment = "UPDATE  users SET first_name = '" + first_name + "', last_name = '" + last_name + "',user_name = '" + user_name + "', password = '" + password + "' ,email = '" + email + "' ,	permission_type= '" + permission_type + "' WHERE  user_id = '" + id + "'";
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
        public virtual bool delete_user(int id)
        {
            bool success = true;
            sql_statment = "delete from users where user_id = '" + id + "'";
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
        public virtual int get_user_id(string email, string user_name, int permission)
        {
            int id = 0;
            sql_statment = "select user_id from users where email ='" + email + "' and user_name ='" + user_name + "' and permission_type = '" + permission + "'";
            //	System.out.println(sql_statment);
            res = database.execute_query(sql_statment);
            try
            {
                while (res.Read())
                {
                    try
                    {
                        id = int.Parse((res["user_id"] + ""));
                    }
                    catch (MySqlException e)
                    {
                        // TODO Auto-generated catch block
                        Console.WriteLine(e.ToString());
                        Console.Write(e.StackTrace);
                    }
                    //	 System.out.println(first_name);
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
        public virtual string get_first_name(int id)
        {
            string first_name = null;
            sql_statment = "select first_name from users where user_id ='" + id + "'";
            Console.WriteLine(sql_statment);
            res = database.execute_query(sql_statment);
            try
            {
                while (res.Read())
                {
                    try
                    {
                        first_name = res["first_name"] + "";
                    }
                    catch (MySqlException e)
                    {
                        // TODO Auto-generated catch block
                        Console.WriteLine(e.ToString());
                        Console.Write(e.StackTrace);
                    }
                    //	 System.out.println(first_name);
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

        public virtual string get_last_name(int id)
        {
            string last_name = null;
            sql_statment = "select last_name from users where user_id ='" + id + "'";
            //	System.out.println(sql_statment);
            res = database.execute_query(sql_statment);
            try
            {
                while (res.Read())
                {
                    try
                    {
                        last_name = res["last_name"] + "";
                    }
                    catch (MySqlException e)
                    {
                        // TODO Auto-generated catch block
                        Console.WriteLine(e.ToString());
                        Console.Write(e.StackTrace);
                    }
                    // System.out.println(last_name);
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

        public virtual string get_user_name(int id)
        {

            string user_name = null;
            sql_statment = "select user_name from users where user_id ='" + id + "'";
            //	System.out.println(sql_statment);
            res = database.execute_query(sql_statment);
            try
            {
                while (res.Read())
                {
                    try
                    {
                        user_name = res["user_name"] + "";
                    }
                    catch (MySqlException e)
                    {
                        // TODO Auto-generated catch block
                        Console.WriteLine(e.ToString());
                        Console.Write(e.StackTrace);
                    }
                    // System.out.println(user_name);
                }
            }
            catch (MySqlException e)
            {
                // TODO Auto-generated catch block
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }
            database.close_data_reader();
            return user_name;
        }

        public virtual string get_password(int id)
        {

            string password = null;
            sql_statment = "select password from users where user_id ='" + id + "'";
            //	System.out.println(sql_statment);
            res = database.execute_query(sql_statment);
            try
            {
                while (res.Read())
                {
                    try
                    {
                        password = res["password"] + "";
                    }
                    catch (MySqlException e)
                    {
                        // TODO Auto-generated catch block
                        Console.WriteLine(e.ToString());
                        Console.Write(e.StackTrace);
                    }
                    /// System.out.println(password);
                }
            }
            catch (MySqlException e)
            {
                // TODO Auto-generated catch block
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }
            database.close_data_reader();
            return password;
        }

        public virtual string get_email(int id)
        {
            string email = null;
            sql_statment = "select email from users where user_id ='" + id + "'";
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
                    // System.out.println(email);
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

        public virtual int get_permission(int id)
        {
            int permission_type = -1;
            sql_statment = "select permission_type from users where user_id ='" + id + "'";
            //	System.out.println(sql_statment);
            res = database.execute_query(sql_statment);
            try
            {
                while (res.Read())
                {
                    try
                    {
                        permission_type = int.Parse(res["permission_type"] + "");
                    }
                    catch (MySqlException e)
                    {
                        // TODO Auto-generated catch block
                        Console.WriteLine(e.ToString());
                        Console.Write(e.StackTrace);
                    }
                    // System.out.println(permission_type);
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
        public virtual List<string> get_user(int id)
        {
            List<string> user = new List<string>();
            sql_statment = "select * from users where user_id ='" + id + "'";
            //System.out.println(sql_statment);
            res = database.execute_query(sql_statment);
            try
            {
                while (res.Read())
                {
                    try
                    {

                        user.Add(Convert.ToString(int.Parse(res["user_id"] + "")));
                        user.Add(res["first_name"] + "");
                        user.Add(res["last_name"] + "");
                        user.Add(res["user_name"] + "");
                        user.Add(res["password"] + "");
                        user.Add(res["email"] + "");
                        user.Add(Convert.ToString(int.Parse(res["permission_type"] + "")));
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
            return user;
        }

        public virtual List<string> get_all_user()
        {
            List<string> users = new List<string>();
            //	ArrayList<ArrayList<String>> all_users= new ArrayList<ArrayList<String>>();
            sql_statment = "select * from users";
            //	System.out.println(sql_statment);
            res = database.execute_query(sql_statment);
            try
            {
                while (res.Read())
                {
                    try
                    {
                        users.Add(Convert.ToString(int.Parse(res["user_id"] + "")));
                        users.Add(res["first_name"] + "");
                        users.Add(res["last_name"] + "");
                        users.Add(res["user_name"] + "");
                        users.Add(res["password"] + "");
                        users.Add(res["email"] + "");
                        users.Add(Convert.ToString(int.Parse(res["permission_type"] + "")));

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
            return users;
        }
    }


}