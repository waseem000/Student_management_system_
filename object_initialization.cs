using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using User = Back_end.User;
using Staff = Back_end.Staff;
using Major = Back_end.Major;
using Student = Back_end.Student;
using Student_Record = Back_end.Student_Record;
using Took_Courses = Back_end.Took_Courses;
using Courses = Back_end.Courses;
using Student_Schedule = Back_end.Student_Schedule;
using Conflict_Check = Back_end.Conflict_Check;
using Student_Account = Back_end.Student_Account;
using Log_in = Back_end.Log_in;
using Log_out = Back_end.Log_out;
namespace Student_Management_System
{
    class object_initialization
    {

       private static DateTime deadline;
     public  static DBConnect db = new DBConnect();

     public static User user = new User(db);
     public static Staff staff = new Staff(db);
     public static Major major = new Major(db);
     public static Courses course = new Courses(db);
     public static Conflict_Check Cconflict_check = new Conflict_Check(db);
        //place holder for the needed classes
     public static Student_Record student_record = new Student_Record(db);
     public static Student_Account student_account = new Student_Account(db);
     public static Student_Schedule student_schedule = new Student_Schedule(db);
     public static Student student = new Student(db);
     public static Took_Courses took_courses = new Took_Courses(db);
     
     public static Log_in log_in = new Log_in(db);
     public static Log_out log_out = new Log_out(db);

     
    public static DateTime get_deadline()
    {
        return deadline;
    }
    public static void set_deadline (DateTime deadline_date)
    {
        deadline = deadline_date;
    }
    }
}
