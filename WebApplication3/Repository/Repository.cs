
using System.Data;
using MySqlConnector;
using WebApplication3.Models;

// create a singleton for the database connection
public class DbConnection
{
    private static DbConnection _instance = null;
    private static MySqlConnection _connection = null;
    private DbConnection()
    {
        string connectionString = "server=localhost;user id=root;password=;database=db_ufr_set;";
        _connection = new MySqlConnection(connectionString);
        _connection.Open();
        if (_connection.State == ConnectionState.Open)
        {
            // MessageBox.Show("Connection Open  !");
        }
    }

    public static DbConnection Instance()
    {
        if (_instance == null)
        {
            _instance = new DbConnection();
        }

        return _instance;
    }

    public MySqlConnection getConnection()
    {
        return _connection;
    }
    
    public static MySqlDataReader requete(string rq)
    {
      
        MySqlCommand cmd = new MySqlCommand(rq, Instance().getConnection());
        MySqlDataReader reader = cmd.ExecuteReader();
        return reader;
        
    }
}


    

