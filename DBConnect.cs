using MySql.Data.MySqlClient;
using MySql.Data.Types;
using System;
public class DBConnect
{
    private MySqlConnection connection;
    private string server;
    private string database;
    private string uid;
    private string password;
    private MySqlDataReader result;
    private MySqlCommand cmd;
    //Constructor
    public DBConnect()
    {
        Initialize();
    }

    //Initialize values
    private void Initialize()
    {
        server = "localhost";
        database = "student_management_system_db";
        uid = "root";
        password = "";
        string connectionString;
        connectionString = "SERVER=" + server + ";" + "DATABASE=" +
        database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

        connection = new MySqlConnection(connectionString);
    }

    //open connection to database
    public bool OpenConnection()
    {
        try
        {
            Console.WriteLine("openning connection to database");
            connection.Open();
            //  return true;
        }
        catch (MySqlException ex)
        {
            //When handling errors, you can your application's response based 
            //on the error number.
            //The two most common error numbers when connecting are as follows:
            //0: Cannot connect to server.
            //1045: Invalid user name and/or password.
            switch (ex.Number)
            {
                case 0:
                    Console.WriteLine("Cannot connect to server.  Contact administrator");
                    // Message.Show("Cannot connect to server.  Contact administrator");
                    break;

                case 1045:
                    Console.WriteLine("Invalid username/password, please try again");
                    //  MessageBox.Show("Invalid username/password, please try again");
                    break;
                default:
                    Console.WriteLine("Invalid, eeror number ="+ ex.Number);
                    break;
            }
            return false;
        }
        Console.WriteLine("open");
        return true;
    }

    //Close connection
    public bool CloseConnection()
    {
        try
        {
            connection.Close();
            return true;
        }
        catch (MySqlException ex)
        {
            Console.WriteLine(ex.Message);
            // MessageBox.Show(ex.Message);
            return false;
        }
    }

    public bool execute_statment(string query)
    {
         cmd = new MySqlCommand(query, connection);
        cmd.ExecuteNonQuery();
        return true;
    }
    public MySqlDataReader execute_query(string sql)
    {
        cmd = new MySqlCommand(sql, connection);
       // MySqlDataReader result;
        result = cmd.ExecuteReader();
      //  Console.WriteLine(result);
        return result;
    }
    public void close_data_reader()
    {
        result.Close();//close datareader
      //  Console.WriteLine("data reader is closed");
    }
}