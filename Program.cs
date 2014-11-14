using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using User = Back_end.User;
using Staff = Back_end.Staff;
using Major = Back_end.Major;
using Courses = Back_end.Courses;
using Student = Back_end.Student;
using Student_Record = Back_end.Student_Record;
using Student_Schedule = Back_end.Student_Schedule;
using Took_Courses = Back_end.Took_Courses;
using Student_Account = Back_end.Student_Account;
using Log_in = Back_end.Log_in;
using Log_out = Back_end.Log_out;
namespace Student_Management_System
{
    class Program
    {
        static void Main (string[] args)
        {
            Console.WriteLine(DateTime.Now);
            object_initialization.set_deadline(DateTime.Parse("2014/11/15 12:00:00 AM"));
            DBConnect db = object_initialization.db;//new DBConnect();
            User user = object_initialization.user; //new User(db);
            Staff staff = object_initialization.staff;//new Staff(db);
            Major major = object_initialization.major;
            Courses course = object_initialization.course;
            Student s = object_initialization.student;
            Student_Record record = object_initialization.student_record;
            Student_Schedule schedule = object_initialization.student_schedule;
            Took_Courses tc = object_initialization.took_courses;
            Student_Account account = object_initialization.student_account;
            Log_in log_in = object_initialization.log_in;
            Log_out log_out = object_initialization.log_out;
            db.OpenConnection();
            Console.WriteLine("Connection is open");

            //----------------testing USER class Start---------------//
            //user.create_user("qqww2", "zzz", "zzxx", "09898", "zxxcv", 3);
            //user.update_user(7, "qscvgy", "efbg", "yhjk", "14785200", "eddfl", 2);
            //user.delete_user(8);
            //Console.WriteLine(user.get_first_name(1));
            //Console.WriteLine(user.get_last_name(1));
            //Console.WriteLine(user.get_email(1));
            //Console.WriteLine(user.get_password(1));
            //Console.WriteLine(user.get_user_name(1));
            //Console.WriteLine(user.get_permission(1));
            //Console.WriteLine("-----------------------------");
            //List<string> temp = user.get_user(5);

            //for (int i = 0; i < temp.Count; i++)
            //{
            //    Console.WriteLine(temp[i]);
            //}
            //Console.WriteLine("-----------------------------");
            //Console.WriteLine("printing users");
            //List<string> temp1 = new List<string>();
            //temp1 = user.get_all_user();

            //for (int i = 0; i < temp1.Count; i++)
            //{
            //    Console.WriteLine("id =" + temp1[i]);
            //    Console.WriteLine("fname =" + temp1[i + 1]);
            //    Console.WriteLine("lname =" + temp1[i + 2]);
            //    Console.WriteLine("username =" + temp1[i + 3]);
            //    Console.WriteLine("password =" + temp1[i + 4]);
            //    Console.WriteLine("email=" + temp1[i + 5]);
            //    Console.WriteLine("permission =" + temp1[i + 6]);
            //    Console.WriteLine("-----------------------------");
            //    i = i + 6;
            //}
            //Console.WriteLine("--------------------------------------------------------------------------");

            //----------------testing USER class End---------------//
            //----------------testing Staff class Start---------------//

            //staff.add_staff("clack", "john", "c.j@gamail.com", 2);
            //staff.update_staff(4, "jaks1", "jaks1", "jaks11@jak", 2);
            //staff.delete_staff(8);
            //staff.get_email(9);
            //staff.get_first_name(9);
            //staff.get_last_name(9);
            //staff.get_permission(9);

            //Console.WriteLine("-----------------------------");

            //List<String> temp2=	staff.get_staff(9);

            //for(int j =0; j<temp2.Count;j++)
            //{
            //    Console.WriteLine(temp2[j]);
            //}
            //Console.WriteLine("-----------------------------");

            //List<string> temp3 = new List<string>();
            //temp3 = staff.get_all_staff();

            //for (int i = 0; i < temp3.Count; i++)
            //{
            //    Console.WriteLine("id =" + temp3[i]);
            //    Console.WriteLine("fname =" + temp3[i + 1]);
            //    Console.WriteLine("lname =" + temp3[i + 2]);
            //    Console.WriteLine("email =" + temp3[i + 3]);
            //    Console.WriteLine("permission =" + temp3[i + 4]);
            //    Console.WriteLine("-----------------------------");
            //    i = i + 4;
            //}

            //----------------testing Staff class End---------------//
            //----------------testing Major class Start---------------//

            //major.set_is_offered(9, 0);
            //major.set_is_offered(8, 0);
            //major.set_is_offered(5, 0);


            //major.set_is_offered(3, 1);
            //major.drop_student_from_major(1);

            //	major.add_major("computer3", 40, 10,5, 1);
            //major.update_major(5, "SOEN", 45, 0,0, 1); 
            //	major.add_major("SSSS", 40, 10,3, 1);
            //major.delete_major(12);
            //major.delete_major(9);
            //major.delete_major(8);
            //major.get_major(3);
            //major.get_name(3);
            //major.get_number_of_student(3);
            //major.get_number_of_courses(3);
            //major.get_total_credits(3);
            //major.get_is_offered(3);

            //major.set_is_offered(1, 1);
            // major.add_student_to_major(5);
            // major.add_course_to_major(5);
            //List<String> temp7=	major.get_major(3);

            //for(int j =0; j<temp7.Count;j++)
            //{
            //     Console.WriteLine(temp7[j]);
            //}
            //Console.WriteLine("-----------------------------");
            //List<String> temp8=major.get_all_majors();
            //for (int i = 0; i < temp8.Count; i++)
            //{
            //     Console.WriteLine("id ="+ temp8[i]);
            //     Console.WriteLine("name ="+ temp8[i+1]);
            //     Console.WriteLine("credits ="+ temp8[i+2]);
            //     Console.WriteLine("number_of_students="+ temp8[i+3]);
            //     Console.WriteLine("number_of_courses="+ temp8[i+4]);
            //     Console.WriteLine("is_offered ="+ temp8[i+5]);
            //     Console.WriteLine("-----------------------------");
            //    i=i+5;
            //}


            //----------------testing Major class End---------------//
            //----------------testing Courses class Start---------------//

            // course.add_course("Software Managment 1000", "software eng", 4, 25, 0, "fall 2014", "software management", "W 5:54pm to 8:15", "jak", "l-122", "SONE 6611", "1/9/2013", "30/12/2013", 0, 0,"");
            // course.add_course("Software Managment 20", "software eng", 4, 25, 0, "fall 2014", "software management", "W 5:54pm to 8:16", "jak", "H-122", "SONE 6611", "1/9/2013", "30/12/2013", 0, 0,"");
            //  course.add_course("Software Managment 220", "software eng", 4, 25, 0, "fall 2014", "software management", "R 5:54pm to 8:15", "jak", "H-122", "SONE 6611", "1/9/2013", "30/12/2013", 0, 0,"");

            //course.add_course("Software Managment 10", "software eng", 4, 25, 0, "fall 2014", "software management", "W 5:54pm to 8:15", "jak", "H-122", "SONE 6611", "1/9/2013", "30/12/2013", 0, 0,"");
            // course.update_course(3,"Software Managment 113", "software eng", 3, 25, 20, "fall 2014", "software management", "M 5:54pm to 8:15", "joe", "FF-1222", "SONE 6611", "1/9/2013", "30/12/2013", 0, 0,"");
            // course .add_student_to_course(5);
            //  course.add_student_to_course(5);
            // course.dorp_student_from_course(5);
            // course.delete_course(13);
            //course.add_student_to_course(9);
            //course.add_student_to_course(13);


            //Console.WriteLine(course.get_course_id("Software Managment"));
            //Console.WriteLine(course.get_name(4));
            //Console.WriteLine(course.get_major(4));
            //Console.WriteLine(course.get_credits(4));
            //Console.WriteLine(course.get_total_number_students(4));
            //Console.WriteLine(course.get_number_of_students(4));
            //Console.WriteLine(course.get_semester_offered(4));
            //Console.WriteLine(course.get_description(4));
            //Console.WriteLine(course.get_date_offered(4));
            //Console.WriteLine(course.get_instructor(4));
            //Console.WriteLine(course.get_location(4));
            //Console.WriteLine(course.get_abbreviation(4));
            //Console.WriteLine(course.get_start_date(4));
            //Console.WriteLine(course.get_end_date(4));
            //Console.WriteLine(course.is_lab(4));
            //Console.WriteLine(course.has_lab(4));
            //Console.WriteLine(course.get_related_course_id(4));

            //course.set_date_offered(7, "T");
            //course.set_description(7, "12121212");
            //course.set_end_date(7, "yofa");
            //course.set_is_lab(7, 0);
            //course.set_has_lab(7, 1);
            //course.set_instructor(7, "lucy");
            //course.set_location(7, "EV 123");
            //course.set_related_course_id(7, 3);
            //course.set_semester_offered(7, "Spring");
            //course.set_start_date(7, "gg");
            //course.set_total_number_student(7, 100);
            //course.add_student_to_course(7);
            //course.set_location(14, "HHH");
            //course.set_date_offered(14, "T");

            //List<String> temp9=course.get_course(14);

            //for (int j = 0; j < temp9.Count; j++)
            //{
            //     Console.WriteLine(temp9[j]);
            //}
            //List<String> temp10 = course.get_all_courses();
            //for (int i = 0; i< temp10.Count; i++)
            //{
            //     Console.WriteLine("id ="+ temp10[i]);
            //     Console.WriteLine("name ="+ temp10[i+1]);
            //     Console.WriteLine("major ="+ temp10[i+2]);
            //     Console.WriteLine("credits ="+ temp10[i+3]);
            //     Console.WriteLine("total_number_students="+ temp10[i+4]);
            //     Console.WriteLine("number_of_students="+ temp10[i+5]);
            //     Console.WriteLine("semester_offered ="+ temp10[i+6]);
            //     Console.WriteLine("description ="+ temp10[i+7]);
            //     Console.WriteLine("get_date_offred ="+ temp10[i+8]);
            //     Console.WriteLine("instructor ="+ temp10[i+9]);
            //     Console.WriteLine("start_date ="+ temp10[i+10]);
            //     Console.WriteLine("end_date ="+ temp10[i+11]);
            //     Console.WriteLine("location ="+ temp10[i+12]);
            //     Console.WriteLine("abbreviation ="+ temp10[i+13]);
            //     Console.WriteLine("is_lab =" + temp10[i + 14]);
            //     Console.WriteLine("has_lab ="+ temp10[i+15]);
            //     Console.WriteLine("related_course_id ="+ temp10[i+16]);
            //     Console.WriteLine("Majors_major_id ="+ temp10[i+17]);
            //     Console.WriteLine("-----------------------------");
            //    i=i+17;
            //}

            //----------------testing Courses class End---------------//
            //----------------testing  Student class Start---------------// 

            //s.add_student("ahmad101", "ahmad100", "male", "a000@gmail.com", "SOEN", 40, 5, 3.8, "10/10/2013", "fulltime");
            // s.update_student(30, "ahmad13330", "ahmad133330", "male", "a133331000@gmail.com", "SOEN", 40, 13, 2.3, "13/15/2013", "fulltime");
            //  s.delete_student(14);
            // s.set_email(13, "newnewnew");
            // s.set_finished_credits(13, 33);
            // s.set_gpa(13, 4.0);
            // s.set_major(13, "COMP");
            // s.set_status(13, "part_time");
            // s.update_student(6, "ahmad13", "ahmad13", "male", "a1331@gmail.com", "COMP11", 40, 13, 2.3, "13/15/2013", "fulltime");
            // s.set_major(6, "SOEN");
            // s.set_major(23, "COMP11");
            //s.get_user_id(22);
            //s.get_first_name(22);
            //s.get_last_name(22);
            //s.get_gender(22);
            //s.get_email(22);
            //s.get_major(22);
            //s.get_finished_credits(22);
            //s.get_total_credits(22);
            //s.get_gpa(22);
            //s.get_date_of_join(22);
            //s.get_status(22);

            //Console.WriteLine("----------------------------");
            //List<String> temp5 = s.get_student(12);
            //for (int j = 0; j < temp5.Count; j++)
            //{
            //    Console.WriteLine(temp5[j]);
            //}
            //Console.WriteLine("----------------------------");
            //List<String> temp6 = s.get_all_students();

            //for (int i = 0; i < temp6.Count; i++)
            //{
            //    Console.WriteLine("id =" + temp6[i]);
            //    Console.WriteLine("fname =" + temp6[i + 1]);
            //    Console.WriteLine("lname =" + temp6[i + 2]);
            //    Console.WriteLine("gender=" + temp6[i + 3]);
            //    Console.WriteLine("email=" + temp6[i + 4]);
            //    Console.WriteLine("major =" + temp6[i + 5]);
            //    Console.WriteLine("total_credits =" + temp6[i + 6]);
            //    Console.WriteLine("finished_credits =" + temp6[i + 7]);
            //    Console.WriteLine("gpa =" + temp6[i + 8]);
            //    Console.WriteLine("date_of_join =" + temp6[i + 9]);
            //    Console.WriteLine("status =" + temp6[i + 10]);
            //    Console.WriteLine("----------------------------");
            //    i = i + 10;
            //}
            //----------------testing Student class End---------------//
            //----------------testing  Student_record class Start---------------// 

            //record.add_student_entry(6, 3.0, 40, 15, 35);
            //record.add_student_entry(13, 3.5, 45, 15, 30);
            //record.update_student_entry(29, 1.0, 5, 10, 40);
            //record.delete_student_entry(30);
            //record.get_gpa(29);
            //record.get_total_credits(29);
            //record.get_finished_credits(29);
            //record.get_remaining_credits(29);
            //record.set_total_credits(13);
            //record.set_finished_credits(13);
            //record.set_gpa(13, 4.0);
            //record.set_remaining_credits(13);



            //List<string> temp20 = record.get_student_record(6);
            //for (int i = 0; i < temp20.Count; i++)
            //{
            //    Console.WriteLine("student_id =" + temp20[i]);
            //    Console.WriteLine("gpa =" + temp20[i + 1]);
            //    Console.WriteLine("total_credits =" + temp20[i + 2]);
            //    Console.WriteLine("finished_credits =" + temp20[i + 3]);
            //    Console.WriteLine("remaining_credits =" + temp20[i + 4]);

            //    i = i + 4;
            //}
            //----------------testing Student_record class End---------------//
            //----------------testing  Student_schedule class Start---------------// 

            //  schedule.add_course(30, 4);

            //schedule.get_student_schedule(30);
            //List<string> temp21 = schedule.get_student_schedule(30);
            //for (int i = 0; i < temp21.Count; i++)
            //{
            //    Console.WriteLine("student_id =" + temp21[i]);
            //    Console.WriteLine("course_id =" + temp21[i + 1]);
            //    Console.WriteLine("abbreviation =" + temp21[i + 2]);
            //    Console.WriteLine("start_date =" + temp21[i + 3]);
            //    Console.WriteLine("end_date =" + temp21[i + 4]);
            //    Console.WriteLine("day_time =" + temp21[i + 5]);
            //    Console.WriteLine("room =" + temp21[i + 6]);

            //    i = i + 6;
            //}


            //List<string> temp22 = schedule.get_student_schedule_table();
            //for (int i = 0; i < temp22.Count; i++)
            //{
            //    Console.WriteLine("student_id =" + temp22[i]);
            //    Console.WriteLine("course_id =" + temp22[i + 1]);
            //    Console.WriteLine("abbreviation =" + temp22[i + 2]);
            //    Console.WriteLine("start_date =" + temp22[i + 3]);
            //    Console.WriteLine("end_date =" + temp22[i + 4]);
            //    Console.WriteLine("day_time =" + temp22[i + 5]);
            //    Console.WriteLine("room =" + temp22[i + 6]);

            //    i = i + 6;
            //}
            //schedule.remove_course(6, 5);
            //----------------testing Student class End---------------//
            //----------------testing  Took_courses class Start---------------// 
            // tc.drop_student_from_course(6, 1);
            //  tc.add_student_to_course(6, course.get_course_id("Software Managment 113"));
            //tc.add_student_to_course(6, course.get_course_id("Software Managment 333"));


            //tc.add_student_to_course(6, course.get_course_id("Software Managment101"));
            //tc.drop_student_from_course(6, course.get_course_id("Software Managment"));
            //tc.drop_student_from_course(6, course.get_course_id("Software Managment 113"));
            //tc.drop_student_from_course(6, course.get_course_id("Software Managment 101"));
            //tc.drop_student_from_course(6, course.get_course_id("Software Managment 333"));
            //tc.drop_student_from_course(6, course.get_course_id("Software Managment"));


            //tc.get_letter_grade(6, course.get_course_id("Software Managment 113"));
            //tc.get_semester_took(6, course.get_course_id("Software Managment 113"));
            //tc.get_status(6, course.get_course_id("Software Managment 113"));


            //   List<int> temp11=tc.get_student_courses(6);

            //  for (int k = 0; k < temp11.Count; k++)
            //  {
            //      Console.WriteLine(course.get_name(temp11[k]));
            //  }


            //  Console.WriteLine("-----------------------------");
            //  List<String> temp12=	tc.get_took_courses_table();

            //for (int i = 0; i < temp12.Count; i++)
            //  {
            //      Console.WriteLine("student id ="+ temp12[i]);
            //      Console.WriteLine("course id ="+ temp12[i+1]);
            //      Console.WriteLine("letter grade ="+ temp12[i+2]);
            //      Console.WriteLine("status ="+ temp12[i+3]);
            //      Console.WriteLine("semester took ="+ temp12[i+4]);

            //      Console.WriteLine("-----------------------------");
            //      i=i+4;
            //  }	

            //log_in.authenticate_user("ahmad_ahmad", "ahmad");
          //  log_in.authenticate_user("joe_john", "joe");
        //    log_in.authenticate_user("ahmad_ahmad", "ahmad");
        // Console.WriteLine(  log_in.get_log_in_time(30));
        //  Console.WriteLine(log_in.get_session_id(30));

        //  List<String> temp15 = log_in.logged_in_user(30);

        //  for (int k = 0; k < temp15.Count; k++)
        //  {
        //      Console.WriteLine(temp15[k]);
        //  }
        //  Console.WriteLine("-----------------------------");
        //  List<String> temp16 = log_in.logged_in_users();

        //  for (int i = 0; i < temp16.Count; i++)
        //  {
        //      Console.WriteLine("session id =" + temp16[i]);
        //      Console.WriteLine("user id =" + temp16[i + 1]);
        //      Console.WriteLine("premission type  =" + temp16[i + 2]);
        //      Console.WriteLine("log_in_time =" + temp16[i + 3]);

        //      Console.WriteLine("-----------------------------");
        //      i = i + 3;
        //  }

          //  log_out.log_out_user(6);

         //  account.add_student_entry(6);
          //  account.remove_student_enrty(6);
            //account.is_fined(6);
            //account.is_full_paid(6);
            //account.get_remaining_fees(6);
            //account.get_student_tuition(6);
            //account.get_paid_fees(6);

            //account.set_paid_fees(6, 2500);
            //account.set_paid_date(6, DateTime.Now);

            //account.get_remaining_fees(6);


            //List<String> temp16 = account.get_student_account(6);

            //for (int i = 0; i < temp16.Count; i++)
            //{
            //    Console.WriteLine("sstudent id =" + temp16[i]);
            //    Console.WriteLine("student_tuition  =" + temp16[i + 1]);
            //    Console.WriteLine("paid_fees   =" + temp16[i + 2]);
            //    Console.WriteLine("remaining_fees =" + temp16[i + 3]);
            //    Console.WriteLine("is_full_paid id =" + temp16[i+4]);
            //    Console.WriteLine("deadline  =" + temp16[i + 5]);
            //    Console.WriteLine("paid_date   =" + temp16[i + 6]);
            //    Console.WriteLine("late_payment_fine =" + temp16[i + 7]);
            //    Console.WriteLine("is_fined   =" + temp16[i + 8]);
            //    Console.WriteLine("receipt_id =" + temp16[i + 9]);

                
            //    Console.WriteLine("-----------------------------");
            //    i = i + 9;
            //}
           // s.add_student("jakson", "lan", "male", "J.lan@gmail.com", "SOEN", 40, 0, 0.0, "11/11/2014", "full Time");

          //  tc.add_student_to_course(37, 3);
         //  tc.add_student_to_course(37, 4);
           // tc.add_student_to_course(21, 4);
          // account.set_paid_fees(37, 1500);
         //  account.set_paid_date(37, DateTime.Now);
            db.CloseConnection();
          Console.WriteLine("Connection is closed");
        }
    }
}
