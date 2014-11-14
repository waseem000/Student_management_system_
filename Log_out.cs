using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using MySql.Data.Types;

using Student_Management_System;
namespace Back_end
{
    class Log_out
    {

        private DBConnect database = new DBConnect();
        private string sql_statment = null;



        public Log_out ()
        {
            //do nothing
        }
        public Log_out (DBConnect conn)
        {
            database = conn;
        }

        public virtual bool log_out_user (int session_id)
        {
            bool success = true;
            sql_statment = "delete from session where session_id = '" + session_id + "'";
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
    }
}
