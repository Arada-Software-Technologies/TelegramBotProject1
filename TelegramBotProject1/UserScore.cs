using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;


namespace TelegramBotProject1
{
    class UserScore
    {
        string username;
        int moves;
        
        public void RegisterScore()
        {
            using (SqlConnection conn = new SqlConnection("Server= localhost; Database= IDPOS_Database; Integrated Security=True;"))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("Record_score", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                // cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@Moves", moves);                
                cmd.ExecuteNonQuery();
            }
        }
    }
}
