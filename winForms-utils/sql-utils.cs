internal class SQLHelper
{
    public static MySqlConnection connection;

    public static string USERNAME;
    public static string DB;
    public static string SERVER;
    public static string PASSWORD;

    public static async Task<MySqlConnection> LoadAsync()
    {
        USERNAME = SQLSettings.USERNAME;
        DB = SQLSettings.DB;
        SERVER = SQLSettings.SERVER;
        PASSWORD = SQLSettings.PASSWORD;

        connection = new MySqlConnection("SERVER=" + SERVER + ";" + "DATABASE=" + DB + ";" +
            "UID=" + USERNAME + ";" + "PASSWORD=" + PASSWORD + ";");
        try
        {
            await connection.OpenAsync().ConfigureAwait(true);

            if (DEBUG_MODE)
            {
                //MessageBox.Show("SQLHelper.cs\nConnected to the server..");
                await connection.CloseAsync();
            }
    
            return connection;
        }
        catch (Exception)
        {
            return null;
        }
    }
    
    public static async Task<int> PostAsync(string command)
    {
        MySqlCommand cmd = new MySqlCommand(command.Replace("'", "\""), connection);
        int affectedObjects = 0;
        try
        {
            affectedObjects = await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
            cmd.Dispose();
            return affectedObjects;
        }
        catch (MySqlException e)
        {
            cmd.Dispose();
            if (DEBUG_MODE)
                MessageBox.Show("SQLHelper.cs\n" + e);
            return 0;
        }
    }
    public static async Task<List<string>> GetAsync(string command, int index)
    {
        MySqlCommand cmd = new MySqlCommand(command, connection);
        MySqlDataReader dataReader = (MySqlDataReader)await cmd.ExecuteReaderAsync();

        List<string> resultset = new List<string>();
        while (await dataReader.ReadAsync())
            resultset.Add(Convert.ToString(dataReader.GetValue(index)));
        
        dataReader.Dispose();
        cmd.Dispose();
        return resultset;
    }

    public struct Methods
    {
        public struct GET
        {
            public static async Task<List<string>> GetVerifiedUsernames()
            {
                List<string> usernames = await GetAsync("SELECT USERNAME FROM USERS WHERE VERIFIED IS TRUE", 0);
                return usernames;
            }
            public static async Task<bool> UsernameExists(string username)
            {
                List<string> countobject = await GetAsync("SELECT COUNT(*) FROM USERS WHERE USERNAME = '" + username + "'", 0);
                return countobject[0] == "1";
            }
            public static async Task<bool> EmailExists(string email)
            {
                List<string> countobject = await GetAsync("SELECT COUNT(*) FROM USERS WHERE EMAIL = '" + email + "'", 0);
                return countobject[0] == "1";
            }
            
            // returns true if password hashes are the same
            public static async Task<bool> CompareAccountHashedPw(string username, string passwordPlain)
            {
                List<string> passwordObject = await GetAsync("SELECT PASSWORD_HASH FROM USERS WHERE USERNAME = '" + username + "'", 0);
                return passwordObject[0] == TextHelper.Hashed(passwordPlain);
            }
        }

        public struct POST
        {
            public static async Task<bool> CreateAccount(string email, string username, string password)
            {
                await PostAsync("INSERT INTO USERS (NAME, SURNAME, USERNAME, PASSWORD_HASH, EMAIL, ROLE, VERIFIED) " +
                                "VALUES ('', '', '" + username + "', '" + TextHelper.Hashed(password) + "', " + 
                                "'" + email + "', 'guest', 'true')");
                return true;
            }

        }
    }
}