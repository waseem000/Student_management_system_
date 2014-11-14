using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using MySql.Data.Types;


using Student_Management_System;
namespace Back_end
{
    class Log_in
    {


        User user = object_initialization.user;
        private DBConnect database = new DBConnect();
        private string sql_statment = null;
        private MySqlDataReader res;


        public Log_in ()
        {
            //do nothing
        }
        public Log_in (DBConnect conn)
        {
            database = conn;
        }

        public virtual bool authenticate_user (string user_name, string password)
        {
            int user_id;
            bool success = true;
            bool can_log_in=true;
            user_id = user.get_user_id(user_name, password);

            if (user_id > 0)
            {
                can_log_in = check_user_log_in_status(user_id);
                if (can_log_in)
                {
                    success = log_in_user(user_id);

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
                else
                {
                    Console.WriteLine("user already loged_in");
                    return false;
                }
            }
            else
            {
                Console.Write("error in user name or password");
                return false;
            }


        }
        private bool log_in_user (int user_id)
        {

            int permission_type = -1;
            bool success = true;
            DateTime log_in_time;

            permission_type = user.get_permission(user_id);

            log_in_time = DateTime.Now;
            string MySQLFormatDate = log_in_time.ToString("yyyy-MM-dd HH:mm:ss");
            sql_statment = "insert into session(user_id,permission_type,log_in_time) values('" + user_id + "'" + "," + "'" + permission_type + "'," + "'" + MySQLFormatDate + "')";
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

        public virtual int get_session_id (int user_id)
        {
            int session_id = 0;
            sql_statment = "select session_id from session where user_id ='" + user_id + "'";
            //	System.out.println(sql_statment);
            res = database.execute_query(sql_statment);
            try
            {
                while (res.Read())
                {
                    try
                    {
                        session_id = int.Parse(res["session_id"] + "");
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
            return session_id;
        }
        public virtual DateTime get_log_in_time (int user_id)
        {
            DateTime log_in_time = DateTime.Now;
            sql_statment = "select log_in_time from session where user_id ='" + user_id + "'";
            //	System.out.println(sql_statment);
            res = database.execute_query(sql_statment);
            try
            {
                while (res.Read())
                {
                    try
                    {
                        log_in_time = DateTime.Parse(res["log_in_time"] + "");
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
            return log_in_time;
        }

        public bool check_user_log_in_status (int user_id)
        {
            List<string> users = new List<string>();

            users = logged_in_users();

            if(users.Contains(Convert.ToString(user_id)))
            {
                Console.WriteLine("user already loged_in");
                return false;
            }
            else
            {
                Console.WriteLine("user not loged_in");
                return true;
            }

        }
        public virtual List<String> logged_in_user (int user_id)
        {
            List<string> logged_in_user = new List<string>();
            sql_statment = "select * from session where user_id ='" + user_id + "'";
            //System.out.println(sql_statment);
            res = database.execute_query(sql_statment);
            try
            {
                while (res.Read())
                {
                    try
                    {

                        logged_in_user.Add(Convert.ToString(int.Parse(res["session_id"] + "")));
                        logged_in_user.Add(res["user_id"] + "");
                        logged_in_user.Add(res["permission_type"] + "");
                        logged_in_user.Add(Convert.ToString(DateTime.Parse(res["log_in_time"] + "")));
                       
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
            return logged_in_user;
        }

        public virtual List<string> logged_in_users ()
        {
            List<string> logged_in_users = new List<string>();
            sql_statment = "select * from session ";
            //System.out.println(sql_statment);
            res = database.execute_query(sql_statment);
            try
            {
                while (res.Read())
                {
                    try
                    {

                        logged_in_users.Add(Convert.ToString(int.Parse(res["session_id"] + "")));
                        logged_in_users.Add(res["user_id"] + "");
                        logged_in_users.Add(res["permission_type"] + "");
                        logged_in_users.Add(Convert.ToString(DateTime.Parse(res["log_in_time"] + "")));

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
            return logged_in_users;
        }
    }
}
