using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using MySql.Data.Types;

using Student_Management_System;
namespace Back_end
{
    class Took_Courses
    {
        //private int student_id;
        //private int course_id;
        //private string letter_grade;
        //private string status;
        //private string semester_took;


		private DBConnect database = new DBConnect();
        private string sql_statment = null;
        private MySqlDataReader res;

        
		private Student student = object_initialization.student;
		private Courses course = object_initialization.course;
        private Student_Schedule schedule = object_initialization.student_schedule;
        private Student_Account account = object_initialization.student_account;
        public Took_Courses ()
        {
            //do nothing
        }
 
         public Took_Courses(DBConnect conn)
        {
            database = conn;
        }

		public virtual bool add_student_to_course(int student_id, int course_id)
		{
			string student_major = student.get_major(student_id);
			string course_major = course.get_major(course_id);
			string semester_offered = course.get_semester_offered(course_id);
			bool is_lab = course.is_lab(course_id);
			bool has_lab = course.has_lab(course_id);
            bool no_time_conflict=true;
			int lab_id = course.get_related_course_id(course_id);
			if (!(is_lab))
			{
				List<int> student_courses = new List<int>();
                bool can_registor_course;
                bool can_registor_lab;
				bool success =true;

				if (student_major.Equals(course_major))
				{
					student_courses = get_student_courses(student_id);
					if (!(student_courses.Contains(course_id)))
					{
						no_time_conflict=course_time_conflict(student_id,course_id);
                        if(no_time_conflict)
                        {
						can_registor_course = course.add_student_to_course(course_id);
                        can_registor_lab = course.add_student_to_course(lab_id);
                        if ((!has_lab) && can_registor_course)
                        {
                            sql_statment = "insert into took_courses (Students_student_id,Courses_course_id,semester_took) values('" + student_id + "','" + course_id + "','" + semester_offered + "')";
                            Console.WriteLine(sql_statment);
                            success = database.execute_statment(sql_statment);
                            schedule.add_course(student_id, course_id);

                            if (success)
                            {
                                account.set_student_tuition(student_id);
                                Console.Write(success);
                                return true;
                            }
                            else
                            {
                                Console.Write(success);
                                return false;
                            }
                        }

                        else if (has_lab && can_registor_course && can_registor_lab)
                        {
                            //course
                            sql_statment = "insert into took_courses (Students_student_id,Courses_course_id,semester_took) values('" + student_id + "','" + course_id + "','" + semester_offered + "')";
                            Console.WriteLine(sql_statment);
                            success = database.execute_statment(sql_statment);
                            schedule.add_course(student_id, course_id);
                            //lab
                            sql_statment = "insert into took_courses (Students_student_id,Courses_course_id,semester_took) values('" + student_id + "','" + lab_id + "','" + semester_offered + "')";
                            Console.WriteLine(sql_statment);
                            success = database.execute_statment(sql_statment);
                            schedule.add_course(student_id, lab_id);

                            if (success)
                            {
                                account.set_student_tuition(student_id);
                                Console.Write(success);
                                return true;
                            }
                            else
                            {
                                Console.Write(success);
                                return false;
                            }
                        }
                        else if (!can_registor_lab)
                        {
                            Console.WriteLine("lab is full cant add to course");
                            return false;
                        }
                        else if (!can_registor_course)
                        {
                            Console.WriteLine("cant add student course is FULL");
                            return false;
                        }
                        else
                        {
                            Console.WriteLine("ERROR deadcode");
                            return false;

                        }

					}
                       else
                        {
                        Console.WriteLine("time conflict");
						return false;
                        }
                    
                    } 

					else
					{
						Console.WriteLine("student is registered in this course");
						return false;
					}

				}
				else
				{
					Console.WriteLine("you cant registor this course not open for your major");
					return false;
				}
			}
				else
				{
					Console.WriteLine("cant registor lab without course");
					return false;
				}

		}

		public virtual bool drop_student_from_course(int student_id, int course_id)
		{
			bool success =true;
			List<int> student_courses = new List<int>();
			student_courses = get_student_courses(student_id);
			bool is_lab = course.is_lab(course_id);
			bool has_lab = course.has_lab(course_id);
			int lab_id = course.get_related_course_id(course_id);
			if (!(is_lab))
			{
				if (student_courses.Contains(course_id))
				{
					course.dorp_student_from_course(course_id);
					sql_statment = "delete from took_courses where Courses_course_id = '" + course_id + "' and Students_student_id = '" + student_id + "'";
					success = database.execute_statment(sql_statment);
					schedule.remove_course(student_id, course_id);

					if (has_lab)
					{
						course.dorp_student_from_course(lab_id);
						sql_statment = "delete from took_courses where Courses_course_id = '" + lab_id + "' and Students_student_id = '" + student_id + "'";
						success = database.execute_statment(sql_statment);
						schedule.remove_course(student_id, lab_id);
					}

					if (success)
					{
                        account.set_student_tuition(student_id);
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
					Console.WriteLine("Error");
					return false;
				}
			}
			else
			{
				Console.WriteLine("cant drop lab you need to drop course");
				return false;
			}

		}

		public virtual string get_letter_grade(int student_id, int course_id)
		{

			string letter_grade = null;
			sql_statment = "select letter_grade from took_courses where Students_student_id ='" + student_id + "' and Courses_course_id = '" + course_id + "'";
			//	System.out.println(sql_statment);
			res = database.execute_query(sql_statment);
			try
			{
				while (res.Read())
				{
					try
					{
						letter_grade = res["letter_grade"]+"";
					}
					catch (MySqlException e)
					{
						// TODO Auto-generated catch block
						Console.WriteLine(e.ToString());
						Console.Write(e.StackTrace);
					}
					Console.WriteLine(letter_grade);
				}
			}
			catch (MySqlException e)
			{
				// TODO Auto-generated catch block
				Console.WriteLine(e.ToString());
				Console.Write(e.StackTrace);
			}
            database.close_data_reader();
			return letter_grade;
		}

		public virtual void set_letter_grade(int student_id, int course_id, string letter_grade)
		{

            sql_statment = "UPDATE  took_courses SET letter_grade = '" + letter_grade + "' where Students_student_id ='" + student_id + "' and Courses_course_id = '" + course_id + "'";
			Console.WriteLine(sql_statment);
			database.execute_statment(sql_statment);

		}

		public virtual string get_status(int student_id, int course_id)
		{

			string status = null;
			sql_statment = "select status from took_courses where Students_student_id ='" + student_id + "' and Courses_course_id = '" + course_id + "'";
			//	System.out.println(sql_statment);
			res = database.execute_query(sql_statment);
			try
			{
				while (res.Read())
				{
					try
					{
						status =res["status"]+"";
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

		public virtual void set_status(int student_id, int course_id, string status)
		{

			sql_statment = "UPDATE  took_courses SET status = '" + status + "' where student_id ='" + student_id + "' and Courses_course_id = '" + course_id + "'";
			Console.WriteLine(sql_statment);
			database.execute_statment(sql_statment);

		}

		public virtual string get_semester_took(int student_id, int course_id)
		{


			string semester_took = null;
			sql_statment = "select semester_took from took_courses where Students_student_id ='" + student_id + "' and Courses_course_id = '" + course_id + "'";
			//	System.out.println(sql_statment);
			res = database.execute_query(sql_statment);
			try
			{
				while (res.Read())
				{
					try
					{
						semester_took = res["semester_took"]+"";
					}
					catch (MySqlException e)
					{
						// TODO Auto-generated catch block
						Console.WriteLine(e.ToString());
						Console.Write(e.StackTrace);
					}
					Console.WriteLine(semester_took);
				}
			}
			catch (MySqlException e)
			{
				// TODO Auto-generated catch block
				Console.WriteLine(e.ToString());
				Console.Write(e.StackTrace);
			}
            database.close_data_reader();
			return semester_took;
		}

		public virtual void set_semester_took(int student_id, int course_id, string semester_took)
		{

            sql_statment = "UPDATE  took_courses SET semester_took = '" + semester_took + "'where Students_student_id ='" + student_id + "' and Courses_course_id = '" + course_id + "'";
			Console.WriteLine(sql_statment);
			database.execute_statment(sql_statment);

		}

		public virtual List<int> get_student_courses(int student_id)
		{
			List<int> courses = new List<int>();
			sql_statment = "select Courses_course_id from took_courses where Students_student_id ='" + student_id + "'";
			//System.out.println(sql_statment);
			res = database.execute_query(sql_statment);
			try
			{
				while (res.Read())
				{
					try
					{
						courses.Add(int.Parse(res["Courses_course_id"]+""));

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
			return courses;
		}

		public virtual List<int> get_student_courses_status(int student_id, string status)
		{
			List<string> courses = new List<string>();
			List<int> output = new List<int>();
			sql_statment = "select Courses_course_id, status from took_courses where Students_student_id ='" + student_id + "'";
			//System.out.println(sql_statment);
			res = database.execute_query(sql_statment);
			try
			{
				while (res.Read())
				{
					try
					{


						courses.Add(Convert.ToString(int.Parse(res["Courses_course_id"]+"")));
						courses.Add(res["status"]+"");

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
			if (status == "ongoing")
			{
				for (int i = 0 ; i < courses.Count;i = i + 2)
				{
					if (courses[i + 1].Equals("ongoing"))
					{
						output.Add(int.Parse(courses[i]));
					}
				}
				return output;
			}
			else if (status == "finished")
			{

				for (int i = 0 ; i < courses.Count;i = i + 2)
				{
					if (courses[i + 1].Equals("finished"))
					{
						output.Add(int.Parse(courses[i]));
					}
				}
				return output;
			}
			else if (status == "failed")
			{

				for (int i = 0 ; i < courses.Count;i = i + 2)
				{
					if (courses[i + 1].Equals("failed"))
					{
						output.Add(int.Parse(courses[i]));
					}
				}

				return output;
			}

			return output; // deadcode

		}

		public virtual List<string> get_took_courses_table()
		{
			List<string> table = new List<string>();
			sql_statment = "select * from took_courses ";
			//System.out.println(sql_statment);
			res = database.execute_query(sql_statment);
			try
			{
				while (res.Read())
				{
					try
					{

						table.Add(Convert.ToString(int.Parse(res["Students_student_id"]+"")));
						table.Add(Convert.ToString(int.Parse(res["Courses_course_id"]+"")));
						table.Add(res["letter_grade"]+"");
						table.Add(res["status"]+"");
						table.Add(res["semester_took"]+"");

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

		public  virtual  bool course_time_conflict(int studnet_id, int new_course_id) //add this to add_student_to_course function
		{
			List<int> stuednt_courses = get_student_courses(new_course_id);
			string new_course_time = course.get_date_offered(new_course_id);
			string reg_course_time;
			for (int i = 0; i < stuednt_courses.Count;i++)
			{
				reg_course_time = course.get_date_offered(stuednt_courses[i]);
				if (reg_course_time.Equals(new_course_time))
				{
                    database.close_data_reader();
					return false;
				}
			}
            database.close_data_reader();
			return true;
		}
	}
    }

