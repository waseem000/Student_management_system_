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

        class Student
        {
            //before adding student to major check if the major is offered
            //private string first_name;
            //private string last_name;
            //private string gender;
            //private string email;
            //private string student_major;
            //private int total_credits;
            //private int finished_credits;
            //private double gpa;
            //private string date_of_join;
            //private string status;

            private DBConnect database = new DBConnect();
            private string sql_statment = null;
            private MySqlDataReader res;

            private User user = object_initialization.user;
            private Major major = object_initialization.major;
            private Student_Record record = object_initialization.student_record;//needs implementation
            private Courses course = object_initialization.course;
            private Took_Courses took_courses = object_initialization.took_courses;//needs implementation
            private Student_Account account = object_initialization.student_account;
            public Student ()
            {
                //do nothing
            }
            public Student (DBConnect conn)
            {
                database = conn;
            }

            public virtual bool add_student (string first_name, string last_name, string gender, string email, string student_major, int total_credits, int finished_credits, double gpa, string date_of_join, string status)
            {
                bool success = true;
                string password = first_name;
                string user_name = first_name + "_" + last_name;
                int permission_type = 3;

                int major_id = major.get_major_id(student_major);
                int major_credits = major.get_total_credits(major_id);
                Console.WriteLine(major_id);
                sql_statment = "insert into users(first_name,last_name,user_name,password,email,permission_type) values('" + first_name + "'" + "," + "'" + last_name + "'," + "'" + user_name + "'," + "'" + password + "'," + "'" + email + "'," + "'" + permission_type + "')";
                success = database.execute_statment(sql_statment);

                int user_id = user.get_user_id(user_name, password);
                sql_statment = "insert into students (first_name,last_name,gender,email,major,total_credits,finished_credits,gpa,date_of_join,status,Users_user_id,Majors_major_id) values('" + first_name + "','" + last_name + "','" + gender + "','" + email + "','" + student_major + "','" + total_credits + "','" + finished_credits + "','" + gpa + "','" + date_of_join + "','" + status + "','" + user_id + "','" + major_id + "')";
                Console.WriteLine(sql_statment);
                success = database.execute_statment(sql_statment);

                major.add_student_to_major(major_id);

                record.add_student_entry(get_student_id(user_id), gpa, major_credits, finished_credits, major_credits);
                account.add_student_entry(get_student_id(user_id));
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
            public virtual bool update_student (int id, string first_name, string last_name, string gender, string email, string student_major, int total_credits, int finished_credits, double gpa, string date_of_join, string status)
            {
                bool success = true;
                int permission_type = 3;
                int user_id = get_user_id(id);

                int new_major_id = major.get_major_id(student_major);
                string old_major_name = get_major(id);
                if (old_major_name != student_major)
                {
                    int old_major_id = major.get_major_id(old_major_name);
                    bool added = major.add_student_to_major(new_major_id);
                    if (added)
                    {
                        major.drop_student_from_major(old_major_id);
                    }
                    else
                    {
                        student_major = old_major_name;
                        Console.WriteLine("did not drop student new major not offered");
                    }

                }
                sql_statment = "UPDATE  Students SET first_name = '" + first_name + "', last_name = '" + last_name + "',gender = '" + gender + "' ,email = '" + email + "',major= '" + student_major + "', total_credits = '" + total_credits + "', finished_credits = '" + finished_credits + "',gpa= '" + gpa + "',date_of_join ='" + date_of_join + "' , status ='" + status + "', Users_user_id ='" + user_id + "', Majors_major_id = '" + new_major_id + "' where Student_id= '" + id + "'";
                Console.WriteLine(sql_statment);
                success = database.execute_statment(sql_statment);

                sql_statment = "UPDATE  users SET first_name = '" + first_name + "', last_name = '" + last_name + "',user_name = '" + user.get_user_name(user_id) + "', password = '" + user.get_password(user_id) + "' ,email = '" + email + "' ,	permission_type= '" + permission_type + "' WHERE  user_id = '" + user_id + "'";
                Console.WriteLine(sql_statment);
                success = database.execute_statment(sql_statment);

                record.update_student_entry(id, gpa, total_credits, finished_credits, total_credits - finished_credits);
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
            public virtual bool delete_student (int id)
            {
                bool success = true;
                int user_id = get_user_id(id);

                int major_id = major.get_major_id(get_major(id));
                major.drop_student_from_major(major_id);
                dorp_student_from_courses(id);
                sql_statment = "delete from Students where student_id = '" + id + "'";
                success = database.execute_statment(sql_statment);

                sql_statment = "delete from users where user_id = '" + user_id + "'";
                success = database.execute_statment(sql_statment);
                record.delete_student_entry(id);
                account.remove_student_enrty(id);
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

            public virtual int get_user_id (int student_id)
            {
                int id = 0;
                sql_statment = "select Users_user_id from Students where student_id ='" + student_id + "'";
                //	System.out.println(sql_statment);
                res = database.execute_query(sql_statment);
                try
                {
                    while (res.Read())
                    {
                        try
                        {
                            id =  int.Parse(res["Users_user_id"]+"");
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

            public virtual int get_student_id (int user_id)
            {
                int student_id = 0;
                sql_statment = "select student_id from Students where Users_user_id ='" + user_id + "'";
                //	System.out.println(sql_statment);
                res = database.execute_query(sql_statment);
                try
                {
                    while (res.Read())
                    {
                        try
                        {
                            student_id =  int.Parse(res["student_id"] + "");
                        }
                        catch (MySqlException e)
                        {
                            // TODO Auto-generated catch block
                            Console.WriteLine(e.ToString());
                            Console.Write(e.StackTrace);
                        }
                        Console.WriteLine(student_id);
                    }
                }
                catch (MySqlException e)
                {
                    // TODO Auto-generated catch block
                    Console.WriteLine(e.ToString());
                    Console.Write(e.StackTrace);
                }
                database.close_data_reader();
                return student_id;
            }
            public virtual string get_email (int id)
            {
                string email = null;
                sql_statment = "select email from Students where student_id ='" + id + "'";
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
            public virtual void set_email (int id, string email)
            {

                sql_statment = "UPDATE  Students SET email = '" + email + "' where student_id= '" + id + "'";
                Console.WriteLine(sql_statment);
                database.execute_statment(sql_statment);

                sql_statment = "UPDATE  users SET email = '" + email + "' where user_id= '" + get_user_id(id) + "'";
                database.execute_statment(sql_statment);
            }
            public virtual string get_major (int id)
            {

                string major = null;
                sql_statment = "select major from Students where student_id ='" + id + "'";
                //	System.out.println(sql_statment);
                res = database.execute_query(sql_statment);
                try
                {
                    while (res.Read())
                    {
                        try
                        {
                            major = res["major"] + "";
                        }
                        catch (MySqlException e)
                        {
                            // TODO Auto-generated catch block
                            Console.WriteLine(e.ToString());
                            Console.Write(e.StackTrace);
                        }
                        Console.WriteLine(major);
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
            public virtual void set_major (int id, string new_major)
            {

                string old_major_name = get_major(id);
                int new_major_id = major.get_major_id(new_major);
                if (old_major_name != new_major)
                {
                    int old_major_id = major.get_major_id(old_major_name);
                    bool added = major.add_student_to_major(new_major_id);
                    if (added)
                    {
                        major.drop_student_from_major(old_major_id);
                        sql_statment = "UPDATE  Students SET major = '" + new_major + "' where student_id= '" + id + "'";
                        Console.WriteLine(sql_statment);
                        database.execute_statment(sql_statment);
                    }
                    else
                    {
                        Console.WriteLine("did not drop student new major not offered");
                    }

                }

            }
            public virtual int get_finished_credits (int id)
            {

                int finished_credits = 0;
                sql_statment = "select finished_credits from Students where student_id ='" + id + "'";
                //	System.out.println(sql_statment);
                res = database.execute_query(sql_statment);
                try
                {
                    while (res.Read())
                    {
                        try
                        {
                            finished_credits = int.Parse(res["finished_credits"] + "");
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
            public virtual void set_finished_credits (int id, int finished_credits)
            {
                sql_statment = "UPDATE  Students SET finished_credits = '" + finished_credits + "' where student_id= '" + id + "'";
                Console.WriteLine(sql_statment);
                database.execute_statment(sql_statment);

            }
            public virtual double get_gpa (int id)
            {
                double gpa = 0;
                sql_statment = "select gpa from Students where student_id ='" + id + "'";
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
            public virtual void set_gpa (int id, double gpa)
            {
                sql_statment = "UPDATE  Students SET gpa = '" + gpa + "' where student_id= '" + id + "'";
                Console.WriteLine(sql_statment);
                database.execute_statment(sql_statment);
            }
            public virtual string get_status (int id)
            {

                string status = null;
                sql_statment = "select status from Students where student_id ='" + id + "'";
                //	System.out.println(sql_statment);
                res = database.execute_query(sql_statment);
                try
                {
                    while (res.Read())
                    {
                        try
                        {
                            status = res["status"] + "";
                        }
                        catch (MySqlException e)
                        {
                            // TODO Auto-generated catch block
                            Console.WriteLine(e.ToString());
                            Console.Write(e.StackTrace);
                        }
                        Console.WriteLine(status);
                    }
                }
                catch (MySqlException e)
                {
                    // TODO Auto-generated catch block
                    Console.WriteLine(e.ToString());
                    Console.Write(e.StackTrace);
                }
                database.close_data_reader();
                return status;
            }
            public virtual void set_status (int id, string status)
            {
                sql_statment = "UPDATE  Students SET status = '" + status + "' where student_id= '" + id + "'";
                Console.WriteLine(sql_statment);
                database.execute_statment(sql_statment);
            }
            public virtual string get_first_name (int id)
            {
                string first_name = null;
                sql_statment = "select first_name from Students where student_id ='" + id + "'";
                //	System.out.println(sql_statment);
                res = database.execute_query(sql_statment);
                try
                {
                    while (res.Read())
                    {
                        try
                        {
                            first_name =  res["first_name"] + "";
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
                sql_statment = "select last_name from Students where student_id ='" + id + "'";
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
            public virtual string get_gender (int id)
            {

                string gender = null;
                sql_statment = "select gender from Students where student_id ='" + id + "'";
                //	System.out.println(sql_statment);
                res = database.execute_query(sql_statment);
                try
                {
                    while (res.Read())
                    {
                        try
                        {
                            gender =  res["gender"] + "";
                        }
                        catch (MySqlException e)
                        {
                            // TODO Auto-generated catch block
                            Console.WriteLine(e.ToString());
                            Console.Write(e.StackTrace);
                        }
                        Console.WriteLine(gender);
                    }
                }
                catch (MySqlException e)
                {
                    // TODO Auto-generated catch block
                    Console.WriteLine(e.ToString());
                    Console.Write(e.StackTrace);
                }
                database.close_data_reader();
                return gender;
            }
            public virtual int get_total_credits (int id)
            {

                int total_credits = 0;
                sql_statment = "select total_credits from Students where student_id ='" + id + "'";
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
            public virtual string get_date_of_join (int id)
            {

                string date_of_join = null;
                sql_statment = "select date_of_join from Students where student_id ='" + id + "'";
                //	System.out.println(sql_statment);
                res = database.execute_query(sql_statment);
                try
                {
                    while (res.Read())
                    {
                        try
                        {
                            date_of_join =res["date_of_join"] + "";
                        }
                        catch (MySqlException e)
                        {
                            // TODO Auto-generated catch block
                            Console.WriteLine(e.ToString());
                            Console.Write(e.StackTrace);
                        }
                        Console.WriteLine(date_of_join);
                    }
                }
                catch (MySqlException e)
                {
                    // TODO Auto-generated catch block
                    Console.WriteLine(e.ToString());
                    Console.Write(e.StackTrace);
                }
                database.close_data_reader();
                return date_of_join;
            }
            private void dorp_student_from_courses (int student_id)
            {
                List<int> course_list = took_courses.get_student_courses(student_id);

                for (int i = 0; i < course_list.Count; i++)
                {
                    course.dorp_student_from_course(course_list[i]);
                }

            }

            public virtual List<string> get_student (int id)
            {
                List<string> student = new List<string>();
                sql_statment = "select * from Students where student_id ='" + id + "'";
                //System.out.println(sql_statment);
                res = database.execute_query(sql_statment);
                try
                {
                    while (res.Read())
                    {
                        try
                        {

                            student.Add(Convert.ToString(int.Parse(res["student_id"] + "")));
                            student.Add( res["first_name"] + "");
                            student.Add(res["last_name"] + "");
                            student.Add(res["gender"] + "");
                            student.Add(res["email"] + "");
                            student.Add( res["major"] + "");
                            student.Add(Convert.ToString(int.Parse(res["total_credits"] + "")));
                            student.Add(Convert.ToString(int.Parse(res["finished_credits"] + "")));
                            student.Add(Convert.ToString(double.Parse(res["gpa"] + "")));
                            student.Add( res["date_of_join"] + "");
                            student.Add(res["status"] + "");
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
                return student;

            }
            public virtual List<string> get_all_students ()
            {
                List<string> students = new List<string>();
                sql_statment = "select * from Students";
                //System.out.println(sql_statment);
                res = database.execute_query(sql_statment);
                try
                {
                    while (res.Read())
                    {
                        try
                        {

                            students.Add(Convert.ToString(int.Parse(res["student_id"] + "")));
                            students.Add(res["first_name"] + "");
                            students.Add(res["last_name"] + "");
                            students.Add(res["gender"] + "");
                            students.Add(res["email"] + "");
                            students.Add(res["major"] + "");
                            students.Add(Convert.ToString(int.Parse(res["total_credits"] + "")));
                            students.Add(Convert.ToString(int.Parse(res["finished_credits"] + "")));
                            students.Add(Convert.ToString(double.Parse(res["gpa"] + "")));
                            students.Add(res["date_of_join"] + "");
                            students.Add(res["status"] + "");
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
                return students;

            }
        }

    }
