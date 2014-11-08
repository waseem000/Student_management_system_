using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using MySql.Data.Types;

using Student_Management_System;
namespace Back_end
{

    public class Conflict_Check
    {

        private static DBConnect database = new DBConnect();
        private static string sql_statment = null;
        private static MySqlDataReader res;


        private Major major = object_initialization.major;
        private static Courses course = object_initialization.course;

        public Conflict_Check ()
        {
            //do nothing
        }
        public Conflict_Check(DBConnect conn)
        {
            database = conn;
        }

        public static bool add_major_conflict (string major_name)
        {
            List<string> major_list = new List<string>();

            sql_statment = "select name from majors ";
            //System.out.println(sql_statment);
            res = database.execute_query(sql_statment);
            try
            {
                while (res.Read())
                {
                    try
                    {

                        major_list.Add(res["name"]+"");

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
            if (major_list.Contains(major_name))
            {
                Console.WriteLine("ERROR major already exists");
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool delete_major_conflict (int major_id)
        {
            int major_students_number = 0;
            int major_courses_number = 0;
            sql_statment = "select number_of_students, number_of_courses from majors where major_id =' " + major_id + "'";
            //System.out.println(sql_statment);
            res = database.execute_query(sql_statment);
            try
            {
                while (res.Read())
                {
                    try
                    {

                        major_students_number =  int.Parse(res["number_of_students"]+"");
                        major_courses_number =  int.Parse(res["number_of_courses"] + "");
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
            if ((major_students_number > 0 && major_courses_number > 0) || (major_students_number == 0 && major_courses_number > 0) || (major_students_number > 0 && major_courses_number == 0))
            {
                Console.WriteLine("ERROR cant delete major, students are registered in this major");
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool add_course_conflict (string course_name)
        {
            List<string> course_list = new List<string>();

            sql_statment = "select name from courses ";
            //System.out.println(sql_statment);
            res = database.execute_query(sql_statment);
            try
            {
                while (res.Read())
                {
                    try
                    {

                        course_list.Add(res["name"] + "");

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
            if (course_list.Contains(course_name))
            {
                Console.WriteLine("ERROR course already exists");
                return false;
            }
            else
            {
                return true;
            }
        }
        public static bool delete_course_conflict (int course_id)
        {
            int course_students_number = 0;

            sql_statment = "select number_of_students from courses where course_id =' " + course_id + "'";
            //System.out.println(sql_statment);
            res = database.execute_query(sql_statment);
            try
            {
                while (res.Read())
                {
                    try
                    {

                        course_students_number = int.Parse(res["number_of_students"] + "");

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
            if (course_students_number > 0)
            {
                Console.WriteLine("ERROR course cant deleted, students are registered in this course");
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool course_room_conflict (string location, string date_time)
        {
            List<string> room_schedule = new List<string>();

            sql_statment = "select date_offered from courses where location ='" + location + "'";
            Console.WriteLine(sql_statment);
            res = database.execute_query(sql_statment);

            try
            {
                while (res.Read())
                {
                    try
                    {

                        room_schedule.Add(res["date_offered"] + "");

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
            if (room_schedule.Contains(date_time))
            {
                Console.WriteLine("Room is not available at that time ");
                return false;
            }
            else
            {

                return true;
            }
        }
    }
}