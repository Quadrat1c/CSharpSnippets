/*
 * C# Snippet Code
 */

 // SQL Database Connection in C# Application NON Web/ASP

// 1.) Import System.Configuration

// 2.) Add connection string to App.Config
<connectionStrings>
    <add name="DatabaseNickName" connectionString="Data Source=ServerName\SqlInstance; Database=NameDB; persist security info=True; User Id=UserNameHere; Password=PasswordHere;" providerName="System.Data.SqlClient" />
</connectionStrings>

// 3.) Basic Select all query
string connectionString = ConfigurationManager.ConnectionString["DatabaseNickName"].ConnectionString;
string commandString = "SELECT * FROM TableName Where ColName = 1";

DataTable dt = new DataTable();

using (SqlConnection connection = new SqlConnection(connectionString))
{
    using (SqlCommand command = new SqlCommand(commandString, connection))
    {
        connection.Open();
        using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
        {
            // Fill our dt DataTable with sql query records
            dataAdapter.Fill(dt);
            connection.Close();
        }
    }
}

