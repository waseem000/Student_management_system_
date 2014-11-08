using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using MySql.Data.Types;

using Student_Management_System;
namespace Back_end
{

    public class Courses
    {

        //private int course_id;
        //private string major_name;
        //private int credits;
        //private int number_of_students;
        //private int total_number_students;
        //private string semester_offered;
        //private string description;
        //private string date_offered;
        //private string instructor;
        //private string location;
        //private string abbreviation;
        //private string start_date;
        //private string end_date;
        //private int has_lab;
        //private int related_course_id;
        //private int is_lab;

        private DBConnect database = new DBConnect();
        private string sql_statment = null;
        private MySqlDataReader res;

        private Major major = object_initialization.major;

         public Courses ()
        {
            //do nothing
        }
         public Courses (DBConnect conn)
        {
            database = conn;
        }

        public virtual bool add_course (string name, string major_name, int credits, int total_number_students, int number_of_students, string semester_offered, string description, string date_offered, string instructor, string location, string abbreviation, string start_date, string end_date, int is_lab, int has_lab, String related_course_name)
        {
            bool success =true;
            int major_id = major.get_major_id(major_name);
            bool can_add_course = Conflict_Check.add_course_conflict(name);
            bool can_use_room = Conflict_Check.course_room_conflict(location, date_offered);
            int related_course_id = get_course_id(related_course_name);
            if (can_add_course && can_use_room)
            {
                sql_statment = "insert into courses ( name, major ,credits, total_number_students, number_of_students,  semester_offered, description,date_offered, instructor, location,  abbreviation, start_date, end_date,is_lab, has_lab, related_course_id, Majors_major_id) values('" + name + "','" + major_name + "','" + credits + "','" + total_number_students + "','" + number_of_students + "','" + semester_offered + "','" + description + "','" + date_offered + "','" + instructor + "','" + location + "','" + abbreviation + "','" + start_date + "','" + end_date + "','" + is_lab + "','" + has_lab + "','" + related_course_id + "','" + major_id + "')";
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
                Console.WriteLine("ERROR course already exists or room problems");
                return false;
            }
        }
        public virtual bool update_course (int course_id, string name, string major_name, int credits, int total_number_students, int number_of_students, string semester_offered, string description, string date_offered, string instructor, string location, string abbreviation, string start_date, string end_date, int is_lab, int has_lab, string related_course_name)
        { //check student who reg in the updated course to update info
            bool success =true;
            bool can_use_room = Conflict_Check.course_room_conflict(location, date_offered);
            int major_id = major.get_major_id(major_name);
            int related_course_id = get_course_id(related_course_name);
            if (can_use_room)
            {
                sql_statment = "UPDATE  courses SET name = '" + name + "', credits = '" + credits + "',total_number_students = '" + total_number_students + "', number_of_students = '" + number_of_students + "',semester_offered = '" + semester_offered + "', description = '" + description + "', date_offered = '" + date_offered + "', instructor = '" + instructor + "', location= '" + location + "', abbreviation ='" + abbreviation + "' ,start_date='" + start_date + "', end_date = '" + end_date + "', is_lab ='" + "', has_lab = '" + has_lab + "' ,related_course_id = '" + related_course_id + "', Majors_major_id ='" + major_id + "' WHERE  course_id = '" + course_id + "'";
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
                Console.WriteLine("Room is not available at that time ");
                return false;
            }

        }
        public virtual bool delete_course (int course_id)
        {
            bool success =true;

            bool conflict = Conflict_Check.delete_course_conflict(course_id);

            if (conflict)
            {
                sql_statment = "delete from courses where course_id = '" + course_id + "'";
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
                Console.WriteLine("ERROR  cant delete course, students are registered in this course");
                return false;
            }
        }
        public virtual int get_number_of_students (int course_id)
        {

            int number_of_students = 0;
            sql_statment = "select number_of_students from courses where course_id ='" + course_id + "'";
            //	System.out.println(sql_statment);
            res = database.execute_query(sql_statment);
            try
            {
                while (res.Read())
                {
                    try
                    {
                        number_of_students = int.Parse(res["number_of_students"] + "");
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
            return number_of_students;
        }
        public virtual bool add_student_to_course (int course_id)
        {

            int total_number_students = get_total_number_students(course_id);
            int number_of_student_registered = get_number_of_students(course_id);
            if (!(number_of_student_registered == total_number_students))
            {
                number_of_student_registered = get_number_of_students(course_id) + 1;

                sql_statment = "UPDATE  courses SET number_of_students = '" + number_of_student_registered + "' where course_id= '" + course_id + "'";
                Console.WriteLine(sql_statment);
                database.execute_statment(sql_statment);
                return true;
            }
            else
            {
                Console.WriteLine("cant add student course is FULL");
                return false;
            }
        }
        public virtual int get_total_number_students (int course_id)
        {

            int total_number_of_students = 0;
            sql_statment = "select total_number_students from courses where course_id ='" + course_id + "'";
            //	System.out.println(sql_statment);
            res = database.execute_query(sql_statment);
            try
            {
                while (res.Read())
                {
                    try
                    {
                        total_number_of_students =  int.Parse(res["total_number_students"] + "");
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
            return total_number_of_students;
        }
        public virtual void set_total_number_student (int course_id, int total_number_students)
        {

            int number_of_students = get_total_number_students(course_id) + 1;

            sql_statment = "UPDATE  courses SET number_of_students = '" + number_of_students + "' where course_id= '" + course_id + "'";
            Console.WriteLine(sql_statment);
            database.execute_statment(sql_statment);
        }
        public virtual string get_semester_offered (int course_id)
        {

            string semester_offered = null;
            sql_statment = "select semester_offered from courses where course_id ='" + course_id + "'";
            //	System.out.println(sql_statment);
            res = database.execute_query(sql_statment);
            try
            {
                while (res.Read())
                {
                    try
                    {
                        semester_offered = res["semester_offered"] + "";
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
            return semester_offered;
        }
        public virtual void set_semester_offered (int course_id, string semester_offered)
        {

            sql_statment = "UPDATE  courses SET semester_offered = '" + semester_offered + "' where course_id= '" + course_id + "'";
            Console.WriteLine(sql_statment);
            database.execute_statment(sql_statment);
        }
        public virtual string get_description (int course_id)
        {

            string description = null;
            sql_statment = "select description from courses where course_id ='" + course_id + "'";
            //	System.out.println(sql_statment);
            res = database.execute_query(sql_statment);
            try
            {
                while (res.Read())
                {
                    try
                    {
                        description =  res["description"] + "";
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
            return description;
        }
        public virtual void set_description (int course_id, string description)
        {

            sql_statment = "UPDATE  courses SET description = '" + description + "' where course_id= '" + course_id + "'";
            Console.WriteLine(sql_statment);
            database.execute_statment(sql_statment);
        }
        public virtual string get_date_offered (int course_id)
        {

            string date_offered = null;
            sql_statment = "select date_offered from courses where course_id ='" + course_id + "'";
            //	System.out.println(sql_statment);
            res = database.execute_query(sql_statment);
            try
            {
                while (res.Read())
                {
                    try
                    {
                        date_offered =  res["date_offered"] + "";
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
            return date_offered;
        }
        public virtual bool set_date_offered (int course_id, string date_offered)
        {

            bool can_use_room = Conflict_Check.course_room_conflict(get_location(course_id), date_offered);
            if (can_use_room)
            {
                sql_statment = "UPDATE  courses SET date_offered = '" + date_offered + "' where course_id= '" + course_id + "'";
                Console.WriteLine(sql_statment);
                database.execute_statment(sql_statment);
                return true;
            }
            else
            {
                Console.WriteLine("Room is not available at that time ");
                return false;
            }
        }
        public virtual string get_instructor (int course_id)
        {

            string instructor = null;
            sql_statment = "select instructor from courses where course_id ='" + course_id + "'";
            //	System.out.println(sql_statment);
            res = database.execute_query(sql_statment);
            try
            {
                while (res.Read())
                {
                    try
                    {
                        instructor =  res["instructor"] + "";
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
            return instructor;
        }
        public virtual void set_instructor (int course_id, string instructor)
        {

            sql_statment = "UPDATE  courses SET instructor = '" + instructor + "' where course_id= '" + course_id + "'";
            Console.WriteLine(sql_statment);
            database.execute_statment(sql_statment);
        }
        public virtual string get_location (int course_id)
        {


            string location = null;
            sql_statment = "select location from courses where course_id ='" + course_id + "'";
            //	System.out.println(sql_statment);
            res = database.execute_query(sql_statment);
            try
            {
                while (res.Read())
                {
                    try
                    {
                        location =  res["location"] + "";
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
            return location;
        }
        public virtual void set_location (int course_id, string location)
        {

            bool can_use_room = Conflict_Check.course_room_conflict(location, get_date_offered(course_id));
            if (can_use_room)
            {
                sql_statment = "UPDATE  courses SET location = '" + location + "' where course_id= '" + course_id + "'";
                Console.WriteLine(sql_statment);
                database.execute_statment(sql_statment);
            }
            else
            {
                Console.WriteLine("Room is not available at that time ");
            }
        }
        public virtual string get_start_date (int course_id)
        {

            string start_date = null;
            sql_statment = "select start_date from courses where course_id ='" + course_id + "'";
            //	System.out.println(sql_statment);
            res = database.execute_query(sql_statment);
            try
            {
                while (res.Read())
                {
                    try
                    {
                        start_date =  res["start_date"] + "";
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
            return start_date;
        }
        public virtual void set_start_date (int course_id, string start_date)
        {

            sql_statment = "UPDATE  courses SET start_date = '" + start_date + "' where course_id= '" + course_id + "'";
            Console.WriteLine(sql_statment);
            database.execute_statment(sql_statment);
        }
        public virtual string get_end_date (int course_id)
        {

            string end_date = null;
            sql_statment = "select end_date from courses where course_id ='" + course_id + "'";
            //	System.out.println(sql_statment);
            res = database.execute_query(sql_statment);
            try
            {
                while (res.Read())
                {
                    try
                    {
                        end_date =  res["end_date"] + "";
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
            return end_date;
        }
        public virtual void set_end_date (int course_id, string end_date)
        {

            sql_statment = "UPDATE  courses SET end_date = '" + end_date + "' where course_id= '" + course_id + "'";
            Console.WriteLine(sql_statment);
            database.execute_statment(sql_statment);
        }
        public virtual bool is_lab (int course_id)
        {

            Boolean is_lab=  false;
            sql_statment = "select is_lab from courses where course_id ='" + course_id + "'";
            //	System.out.println(sql_statment);
            res = database.execute_query(sql_statment);
            try
            {
                while (res.Read())
                {
                    try
                    {
                        is_lab= Boolean.Parse(res["is_lab"] + "");
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
            return is_lab;
        }
        public virtual void set_is_lab (int course_id, int is_lab)
        {

            sql_statment = "UPDATE  courses SET is_lab = '" + is_lab + "' where course_id= '" + course_id + "'";
            Console.WriteLine(sql_statment);
            database.execute_statment(sql_statment);
        }
        public virtual bool has_lab (int course_id)
        {

            bool has_lab = false;
            sql_statment = "select has_lab from courses where course_id ='" + course_id + "'";
            //	System.out.println(sql_statment);
            res = database.execute_query(sql_statment);
            try
            {
                while (res.Read())
                {
                    try
                    {
                        has_lab =  Boolean.Parse(res["has_lab"] + "");
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
            return has_lab;

        }
        public virtual void set_has_lab (int course_id, int has_lab)
        {

            sql_statment = "UPDATE  courses SET has_lab = '" + has_lab + "' where course_id= '" + course_id + "'";
            Console.WriteLine(sql_statment);
            database.execute_statment(sql_statment);
        }
        public virtual int get_related_course_id (int course_id)
        {

            int related_course_id = 0;
            sql_statment = "select related_course_id from courses where course_id ='" + course_id + "'";
            //	System.out.println(sql_statment);
            res = database.execute_query(sql_statment);
            try
            {
                while (res.Read())
                {
                    try
                    {
                        related_course_id =  int.Parse(res["related_course_id"] + "");
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
            return related_course_id;
        }
        public virtual void set_related_course_id (int course_id, int related_course_id)
        {

            sql_statment = "UPDATE  courses SET related_course_id = '" + related_course_id + "' where course_id= '" + course_id + "'";
            Console.WriteLine(sql_statment);
            database.execute_statment(sql_statment);
        }
        public virtual int get_course_id (string course_name)
        {
            if (course_name == "")
            {
                return 0;
            }
            int course_id = 0;
            sql_statment = "select course_id from courses where name ='" + course_name + "'";
            //	System.out.println(sql_statment);
            res = database.execute_query(sql_statment);
            try
            {
                while (res.Read())
                {
                    try
                    {
                        course_id =  int.Parse(res["course_id"] + "");
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
            return course_id;
        }
        public virtual string get_major (int course_id)
        {
            string major_name = null;
            sql_statment = "select major from courses where course_id ='" + course_id + "'";
            //	System.out.println(sql_statment);
            res = database.execute_query(sql_statment);
            try
            {
                while (res.Read())
                {
                    try
                    {
                        major_name = res["major"] + "";
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
            return major_name;
        }
        public virtual int get_credits (int course_id)
        {

            int credits = 0;
            sql_statment = "select credits from courses where course_id ='" + course_id + "'";
            //	System.out.println(sql_statment);
            res = database.execute_query(sql_statment);
            try
            {
                while (res.Read())
                {
                    try
                    {
                        credits =  int.Parse(res["credits"] + "");
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
            return credits;
        }
        public virtual string get_abbreviation (int course_id)
        {

            string abbreviation = null;
            sql_statment = "select abbreviation from courses where course_id ='" + course_id + "'";
            //	System.out.println(sql_statment);
            res = database.execute_query(sql_statment);
            try
            {
                while (res.Read())
                {
                    try
                    {
                        abbreviation = res["abbreviation"] + "";
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
            return abbreviation;
        }

        public virtual string get_name (int course_id)
        {
            string name = null;
            sql_statment = "select name from courses where course_id ='" + course_id + "'";
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

        public virtual void dorp_student_from_course (int course_id)
        {
            int number_of_student_registered = get_number_of_students(course_id);

            number_of_student_registered = get_number_of_students(course_id) - 1;

            sql_statment = "UPDATE  courses SET number_of_students = '" + number_of_student_registered + "' where course_id= '" + course_id + "'";
            Console.WriteLine(sql_statment);
            database.execute_statment(sql_statment);

        }
        public virtual List<string> get_course (int course_id)
        {
            List<string> course = new List<string>();
            sql_statment = "select * from courses where course_id ='" + course_id + "'";
            //System.out.println(sql_statment);
            res = database.execute_query(sql_statment);
            try
            {
                while (res.Read())
                {
                    try
                    {

                        course.Add(Convert.ToString( int.Parse(res["course_id"] + "")));
                        course.Add(res["name"] + "");
                        course.Add(res["major"] + "");
                        course.Add(Convert.ToString(int.Parse(res["credits"] + "")));
                        course.Add(Convert.ToString(int.Parse( res["total_number_students"] + "")));
                        course.Add(Convert.ToString(int.Parse(res["number_of_students"] + "")));
                        course.Add(res["semester_offered"] + "");
                        course.Add(res["description"] + "");
                        course.Add(res["date_offered"] + "");
                        course.Add(res["instructor"] + "");
                        course.Add(res["location"] + "");
                        course.Add(res["abbreviation"] + "");
                        course.Add(res["start_date"] + "");
                        course.Add(res["end_date"] + "");
                        course.Add(Convert.ToString(Boolean.Parse(res["is_lab"] + "")));
                        course.Add(Convert.ToString(Boolean.Parse(res["has_lab"] + "")));
                        course.Add(Convert.ToString(int.Parse(res["related_course_id"] + "")));
                        course.Add(Convert.ToString(int.Parse(res["Majors_major_id"] + "")));

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
            return course;
        }

        public virtual List<string> get_all_courses ()
        {
            List<string> courses = new List<string>();
            sql_statment = "select * from courses";
            //System.out.println(sql_statment);
            res = database.execute_query(sql_statment);
            try
            {
                while (res.Read())
                {
                    try
                    {

                        courses.Add(Convert.ToString(int.Parse(res["course_id"] + "")));
                        courses.Add(res["name"] + "");
                        courses.Add(res["major"] + "");
                        courses.Add(Convert.ToString(int.Parse(res["credits"] + "")));
                        courses.Add(Convert.ToString(int.Parse(res["total_number_students"] + "")));
                        courses.Add(Convert.ToString(int.Parse(res["number_of_students"] + "")));
                        courses.Add(res["semester_offered"] + "");
                        courses.Add(res["description"] + "");
                        courses.Add(res["date_offered"] + "");
                        courses.Add(res["instructor"] + "");
                        courses.Add(res["location"] + "");
                        courses.Add(res["abbreviation"] + "");
                        courses.Add(res["start_date"] + "");
                        courses.Add(res["end_date"] + "");
                        courses.Add(Convert.ToString(Boolean.Parse(res["is_lab"] + "")));
                        courses.Add(Convert.ToString(Boolean.Parse(res["has_lab"] + "")));
                        courses.Add(Convert.ToString(int.Parse(res["related_course_id"] + "")));
                        courses.Add(Convert.ToString(int.Parse(res["Majors_major_id"] + "")));

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

    }

}