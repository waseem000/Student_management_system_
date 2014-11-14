using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using MySql.Data.Types;

using Student_Management_System;
namespace Back_end
{


    public class Student_Schedule
    {

        //private int student_id;
        //private int course_id;
        //private string abbreviation;
        //private string start_date;
        //private string end_date;
        //private string day_time;
        //private string location;

        private Courses course = object_initialization.course;

        private DBConnect database = new DBConnect();
        private string sql_statment = null;
        private MySqlDataReader res;


        public Student_Schedule ()
        {
            //do nothing
        }

        public Student_Schedule (DBConnect conn)
        {
            database = conn;
        }

        public virtual bool add_course (int student_id, int course_id)
        {
            string abbreviation = course.get_abbreviation(course_id);
            string start_date = course.get_start_date(course_id);
            string end_date = course.get_end_date(course_id);
            string day_time = course.get_date_offered(course_id);
            string location = course.get_location(course_id);
            bool success =true;

            sql_statment = "insert into student_schedule (Students_student_id,course_id,abbreviation,start_date, end_date,day_time,room) values('" + student_id + "','" + course_id + "','" + abbreviation + "','" + start_date + "','" + end_date + "','" + day_time + "','" + location + "')";
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

        public virtual bool remove_course (int student_id, int course_id)
        {
           bool success =true;
            sql_statment = "delete from student_schedule where Students_student_id= '" + student_id + "' and course_id = '" + course_id + "'";
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

        public virtual List<string> get_student_schedule (int student_id)
        {
            List<string> schedule = new List<string>();
            sql_statment = "select * from student_schedule where Students_student_id ='" + student_id + "'";
            //System.out.println(sql_statment);
            res = database.execute_query(sql_statment);
            try
            {
                while (res.Read())
                {
                    try
                    {

                        schedule.Add(Convert.ToString( int.Parse(res["Students_student_id"]+"")));
                        schedule.Add(Convert.ToString(int.Parse(res["course_id"]+"")));
                        schedule.Add(res["abbreviation"]+"");
                        schedule.Add(res["start_date"]+"");
                        schedule.Add(res["end_date"]+"");
                        schedule.Add(res["day_time"]+"");
                        schedule.Add(res["room"]+"");


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
            return schedule;
        }

        public virtual List<string> get_student_schedule_table ()
        {
            List<string> table = new List<string>();
            sql_statment = "select * from student_schedule ";
            //System.out.println(sql_statment);
            res = database.execute_query(sql_statment);
            try
            {
                while (res.Read())
                {
                    try
                    {

                        table.Add(Convert.ToString(int.Parse(res["Students_student_id"] + "")));
                        table.Add(Convert.ToString(int.Parse(res["course_id"] + "")));
                        table.Add(res["abbreviation"] + "");
                        table.Add(res["start_date"] + "");
                        table.Add(res["end_date"] + "");
                        table.Add(res["day_time"] + "");
                        table.Add(res["room"] + "");

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
            return table;
        }
    }

}