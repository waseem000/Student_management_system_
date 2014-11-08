using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using MySql.Data.Types;

using Student_Management_System;
namespace Back_end
{
        class Major
        {

            //you can't but major offered false if number of student is >0
            //private int major_id;
            //private string name;
            //private int total_credits;
            //private int number_of_student;
            //private bool is_offered;
            //private int number_of_courses;

            private DBConnect database = new DBConnect();
            private string sql_statment = null;
            private MySqlDataReader res;

            public Major ()
            {
                //do nothing
            }
            public Major (DBConnect conn)
            {
                database = conn;
            }

            public virtual bool add_major (string name, int total_credits, int number_of_students, int number_of_courses, int is_offered)
            {

                bool success = true; ;
                bool conflict;
                conflict = Conflict_Check.add_major_conflict(name);
                if (conflict)
                {
                    sql_statment = "insert into majors(name,credits,number_of_students,number_of_courses,is_offered) values('" + name + "'" + "," + "'" + total_credits + "'," + "'" + number_of_students + "','" + number_of_courses + "','" + is_offered + "')";
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
                else
                {
                    Console.WriteLine("ERROR major already exists");
                    return false;
                }

            }
            public virtual bool update_major (int major_id, string name, int total_credits, int number_of_students, int number_of_courses, int is_offered)
            {
                bool success = true; ;
                sql_statment = "UPDATE  majors SET name = '" + name + "', credits = '" + total_credits + "',number_of_students = '" + number_of_students + "', number_of_courses= '" + number_of_courses + "', is_offered = '" + is_offered + "' WHERE  major_id = '" + major_id + "'";
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
            public virtual bool delete_major (int major_id) // need to make sure that there are no courses and students under the major before the delete
            {
                bool conflict = Conflict_Check.delete_major_conflict(major_id);
                if (conflict)
                {
                    bool success = true; ;
                    sql_statment = "delete from majors where major_id = '" + major_id + "'";
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
                else
                {
                    Console.WriteLine("ERROR  cant delete major, students are registered in this major");
                    return false;
                }
            }
            public virtual int get_number_of_student (int major_id)
            {

                int number_of_student = 0;
                sql_statment = "select number_of_students from majors where major_id ='" + major_id + "'";
                //	System.out.println(sql_statment);
                res = database.execute_query(sql_statment);
                try
                {
                    while (res.Read())
                    {
                        try
                        {
                            number_of_student = int.Parse(res["number_of_students"]+"");
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
                return number_of_student;
            }
            public virtual bool add_student_to_major (int major_id)
            {

                Boolean is_offered = get_is_offered(major_id);
                if (is_offered == true)
                {
                    int number_of_student = get_number_of_student(major_id) + 1;

                    sql_statment = "UPDATE  majors SET number_of_students = '" + number_of_student + "' where major_id= '" + major_id + "'";
                    Console.WriteLine(sql_statment);
                    database.execute_statment(sql_statment);
                    return true;
                }
                else
                {
                    Console.WriteLine("major is not offered");
                    return false;
                }

            }
            public virtual int get_number_of_courses (int major_id)
            {

                int number_of_courses = 0;
                sql_statment = "select number_of_courses from majors where major_id ='" + major_id + "'";
                //	System.out.println(sql_statment);
                res = database.execute_query(sql_statment);
                try
                {
                    while (res.Read())
                    {
                        try
                        {
                            number_of_courses =  int.Parse(res["number_of_courses"] + "");
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
                return number_of_courses;
            }
            public virtual void add_course_to_major (int major_id)
            {

                int number_of_courses = get_number_of_courses(major_id) + 1;
                bool is_offered = get_is_offered(major_id);
                if (is_offered == true)
                {
                    sql_statment = "UPDATE  majors SET number_of_courses = '" + number_of_courses + "' where major_id= '" + major_id + "'";
                    Console.WriteLine(sql_statment);
                    database.execute_statment(sql_statment);
                }
                else
                {
                    Console.WriteLine("major is not offered");
                }

            }
            public virtual Boolean get_is_offered (int major_id)
            {

                Boolean is_offered = true;

                sql_statment = "select is_offered from majors where major_id ='" + major_id + "'";
                //	System.out.println(sql_statment);
                res = database.execute_query(sql_statment);
                try
                {
                    while (res.Read())
                    {
                        try
                        {
                            is_offered =  Boolean.Parse(res["is_offered"] + "");
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
                return is_offered;
            }
            public virtual void set_is_offered (int major_id, int is_offered)
            {

                int number_of_students = get_number_of_student(major_id);
                int number_of_courses = get_number_of_courses(major_id);
                if ((number_of_students > 0 && number_of_courses > 0 && is_offered == 1) || (number_of_students == 0 && number_of_courses > 0 && is_offered == 1) || (number_of_students > 0 && number_of_courses == 0 && is_offered == 1))
                {
                    sql_statment = "UPDATE majors SET is_offered = '" + is_offered + "' where major_id= '" + major_id + "'";
                    Console.WriteLine(sql_statment);
                    database.execute_statment(sql_statment);
                    Console.WriteLine("updated");
                }
                else if (number_of_students == 0 && number_of_courses == 0)
                {
                    sql_statment = "UPDATE majors SET is_offered = '" + is_offered + "' where major_id= '" + major_id + "'";
                    Console.WriteLine(sql_statment);
                    database.execute_statment(sql_statment);
                    Console.WriteLine("updated 0,0 ");
                }
                else
                {
                    Console.WriteLine("student or courses are in this major ");
                }
            }
            public virtual int get_major_id (string major_name)
            {

                int id = 0;
                sql_statment = "select major_id from majors where name ='" + major_name + "'";
                Console.WriteLine(sql_statment);
                res = database.execute_query(sql_statment);
                try
                {
                    while (res.Read())
                    {
                        try
                        {
                            id =  int.Parse(res["major_id"] + "");
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
            public virtual string get_name (int major_id)
            {


                string name = null;
                sql_statment = "select name from majors where major_id ='" + major_id + "'";
                //	System.out.println(sql_statment);
                res = database.execute_query(sql_statment);
                try
                {
                    while (res.Read())
                    {
                        try
                        {
                            name =  res["name"] + "";
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
                return name;
            }
            public virtual int get_total_credits (int major_id)
            {

                int total_credits = 0;
                sql_statment = "select credits from majors where major_id ='" + major_id + "'";
                //	System.out.println(sql_statment);
                res = database.execute_query(sql_statment);
                try
                {
                    while (res.Read())
                    {
                        try
                        {
                            total_credits =  int.Parse(res["credits"] + "");
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
                return total_credits;
            }

            public virtual List<string> get_major (int major_id)
            {
                List<string> major = new List<string>();

                sql_statment = "select * from majors where major_id ='" + major_id + "'";
                //System.out.println(sql_statment);
                res = database.execute_query(sql_statment);
                try
                {
                    while (res.Read())
                    {
                        try
                        {

                            major.Add(Convert.ToString( int.Parse(res["major_id"] + "")));
                            major.Add(res["name"] + "");
                            major.Add(Convert.ToString(int.Parse(res["credits"] + "")));
                            major.Add(Convert.ToString(int.Parse(res["number_of_students"] + "")));
                            major.Add(Convert.ToString(int.Parse(res["number_of_courses"] + "")));
                            major.Add(Convert.ToString( res["is_offered"] + ""));

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
                return major;
            }

            public virtual List<string> get_all_majors ()
            {
                List<string> majors = new List<string>();

                sql_statment = "select * from majors ";
                //System.out.println(sql_statment);
                res = database.execute_query(sql_statment);
                try
                {
                    while (res.Read())
                    {
                        try
                        {

                            majors.Add(Convert.ToString(int.Parse(res["major_id"] + "")));
                            majors.Add(res["name"] + "");
                            majors.Add(Convert.ToString(int.Parse(res["credits"] + "")));
                            majors.Add(Convert.ToString(int.Parse(res["number_of_students"] + "")));
                            majors.Add(Convert.ToString(int.Parse(res["number_of_courses"] + "")));
                            majors.Add(Convert.ToString(Boolean.Parse(res["is_offered"] + "")));

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
                return majors;
            }
            public virtual void drop_student_from_major (int major_id)
            {
                // TODO Auto-generated method stub

                int number_of_student = get_number_of_student(major_id) - 1;

                sql_statment = "UPDATE  majors SET number_of_students = '" + number_of_student + "' where major_id= '" + major_id + "'";
                Console.WriteLine(sql_statment);
                database.execute_statment(sql_statment);

            }
        }
    }
