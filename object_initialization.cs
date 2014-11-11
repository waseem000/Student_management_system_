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

namespace Student_Management_System
{
    class object_initialization
    {
     public  static DBConnect db = new DBConnect();

     public static User user = new User(db);
     public static Staff staff = new Staff(db);
     public static Major major = new Major(db);
     public static Courses course = new Courses(db);
     public static Conflict_Check Cconflict_check = new Conflict_Check(db);
        //place holder for the needed classes
   //  public static Student_Record student_record = new Student_Record(db);
     public static Student student = new Student(db);
   //  public static Student_Schedule schedule = new Student_Schedule(db);
   //  public static Took_Courses took_courses = new Took_Courses(db);
   
     
    
    }
}
