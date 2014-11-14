using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using MySql.Data.Types;

using Student_Management_System;

namespace Back_end
{
    public class Student_Record
    {

        //private int student_id;
        //private double gpa;
        //private int total_credits;
        //private int finished_credits;
        //private int remaining_credtis;

        private DBConnect database = new DBConnect();
        private string sql_statment = null;
        private MySqlDataReader res;

        
        private Student student = object_initialization.student;
        private Major major = object_initialization.major;
        private Courses course = object_initialization.course;
        private Took_Courses took_course = object_initialization.took_courses;

        public Student_Record ()
        {
            //do nothing
        }
        public Student_Record (DBConnect conn)
        {
           
            database = conn;
        }

        public virtual bool add_student_entry (int student_id, double gpa, int total_credits, int finished_credits, int remaining_credits)
        {
            bool success =true;
            sql_statment = "insert into student_record (Students_student_id,gpa,total_credits,finished_credits,	remaining_credits) values('" + student_id + "','" + gpa + "','" + total_credits + "','" + finished_credits + "','" + remaining_credits + "')";
            Console.WriteLine(sql_statment);
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

        public virtual bool update_student_entry (int student_id, double gpa, int total_credits, int finished_credits, int remaining_credits)
        {
            bool success =true;
            sql_statment = "update student_record SET gpa = '" + gpa + "',total_credits ='" + total_credits + "',finished_credits ='" + finished_credits + "',remaining_credits='" + remaining_credits + "' where Students_student_id ='" + student_id + "'";
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
        public virtual bool delete_student_entry (int student_id)
        {
            bool success =true;
            sql_statment = "delete from student_record where Students_student_id = '" + student_id + "'";
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

        public virtual double get_gpa (int student_id)
        {

            double gpa = 0;
            sql_statment = "select gpa from student_record where Students_student_id ='" + student_id + "'";
            //	System.out.println(sql_statment);
            res = database.execute_query(sql_statment);
            try
            {
                while (res.Read())
                {
                    try
                    {
                        gpa =  double.Parse(res["gpa"] + "");
                    }
                    catch (MySqlException e)
                    {
                        // TODO Auto-generated catch block
                        Console.WriteLine(e.ToString());
                        Console.Write(e.StackTrace);
                    }
                    Console.WriteLine(gpa);
                }
            }
            catch (MySqlException e)
            {
                // TODO Auto-generated catch block
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }
            database.close_data_reader();
            return gpa;
        }

        public virtual void set_gpa (int student_id, double gpa)
        {

            sql_statment = "UPDATE  student_record SET gpa = '" + gpa + "' where Students_student_id= '" + student_id + "'";
            Console.WriteLine(sql_statment);
            database.execute_statment(sql_statment);
        }

        public virtual int get_total_credits (int student_id)
        {


            int total_credits = 0;
            sql_statment = "select total_credits from student_record where Students_student_id ='" + student_id + "'";
            //	System.out.println(sql_statment);
            res = database.execute_query(sql_statment);
            try
            {
                while (res.Read())
                {
                    try
                    {
                        total_credits =  int.Parse(res["total_credits"] + "");
                    }
                    catch (MySqlException e)
                    {
                        // TODO Auto-generated catch block
                        Console.WriteLine(e.ToString());
                        Console.Write(e.StackTrace);
                    }
                    Console.WriteLine(total_credits);
                }
            }
            catch (MySqlException e)
            {
                // TODO Auto-generated catch block
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }
            database.close_data_reader();
            return total_credits;
        }

        public virtual void set_total_credits (int student_id)
        {
            this.student = object_initialization.student;
            int major_total_credits = major.get_total_credits(major.get_major_id(student.get_major(student_id)));


            sql_statment = "UPDATE  student_record SET total_credits = '" + major_total_credits + "' where Students_student_id= '" + student_id + "'";
            Console.WriteLine(sql_statment);
            database.execute_statment(sql_statment);
        }

        public virtual int get_finished_credits (int student_id)
        {

            int finished_credits = 0;
            sql_statment = "select finished_credits from student_record where Students_student_id ='" + student_id + "'";
            //	System.out.println(sql_statment);
            res = database.execute_query(sql_statment);
            try
            {
                while (res.Read())
                {
                    try
                    {
                        finished_credits =  int.Parse(res["finished_credits"] + "");
                    }
                    catch (MySqlException e)
                    {
                        // TODO Auto-generated catch block
                        Console.WriteLine(e.ToString());
                        Console.Write(e.StackTrace);
                    }
                    Console.WriteLine(finished_credits);
                }
            }
            catch (MySqlException e)
            {
                // TODO Auto-generated catch block
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }
            database.close_data_reader();
            return finished_credits;
        }

        public virtual void set_finished_credits (int student_id)
        {
            this.took_course = object_initialization.took_courses;
            List<int> student_finished_courses = took_course.get_student_courses_status(student_id, "finished");
            int finished_credits = 0;

            for (int i = 0; i < student_finished_courses.Count; i++)
            {
                finished_credits = finished_credits + course.get_credits(student_finished_courses[i]);
            }
            sql_statment = "UPDATE  student_record SET finished_credits = '" + finished_credits + "' where Students_student_id= '" + student_id + "'";
            Console.WriteLine(sql_statment);
            database.execute_statment(sql_statment);

        }

        public virtual int get_remaining_credits (int student_id)
        {


            int remaining_credits = 0;
            sql_statment = "select remaining_credits from student_record where Students_student_id ='" + student_id + "'";
            //	System.out.println(sql_statment);
            res = database.execute_query(sql_statment);
            try
            {
                while (res.Read())
                {
                    try
                    {
                        remaining_credits = int.Parse(res["remaining_credits"] + "");
                    }
                    catch (MySqlException e)
                    {
                        // TODO Auto-generated catch block
                        Console.WriteLine(e.ToString());
                        Console.Write(e.StackTrace);
                    }
                    Console.WriteLine(remaining_credits);
                }
            }
            catch (MySqlException e)
            {
                // TODO Auto-generated catch block
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }
            database.close_data_reader();
            return remaining_credits;
        }

        public virtual void set_remaining_credits (int student_id)
        {

            int total_credits = get_total_credits(student_id);
            int finished_credits = get_remaining_credits(student_id);
            int remaining_credits = total_credits - finished_credits;

            sql_statment = "UPDATE  student_record SET remaining_credits = '" + remaining_credits + "' where Students_student_id= '" + student_id + "'";
            Console.WriteLine(sql_statment);
            database.execute_statment(sql_statment);

        }

        public virtual List<string> get_student_record (int student_id)
        {
            List<string> record = new List<string>();
            sql_statment = "select * from student_record where Students_student_id ='" + student_id + "'";
            //System.out.println(sql_statment);
            res = database.execute_query(sql_statment);
            try
            {
                while (res.Read())
                {
                    try
                    {

                        record.Add(Convert.ToString(int.Parse(res["Students_student_id"] + "")));
                        record.Add(Convert.ToString(double.Parse(res["gpa"] + "")));
                        record.Add(Convert.ToString(int.Parse(res["total_credits"] + "")));
                        record.Add(Convert.ToString(int.Parse(res["finished_credits"] + "")));
                        record.Add(Convert.ToString(int.Parse(res["remaining_credits"] + "")));
                        


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
            return record;
        }

    }

}