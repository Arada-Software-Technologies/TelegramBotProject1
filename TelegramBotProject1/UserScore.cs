using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;


namespace TelegramBotProject1
{
    class UserScore
    {
        //the users full name
        public string username;

        //the users number of tries
        public int moves;
        
        public void RegisterScore()
        {
            using (SqlConnection conn = new SqlConnection("Server= localhost; Database= IDPOS_Database; Integrated Security=True;"))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("Record_score", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;                
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@Moves", moves);                
                cmd.ExecuteNonQuery();
            }
        }

        public string TopTen()
        {
            int ctr = 0;
            string msg="";

            //select statement that returns the top 10 scores
            string sql = "SELECT TOP(10)username, Moves FROM HighScore_table ORDER BY Moves ASC ";


            SqlConnection conn = new SqlConnection("Server= localhost; Database= IDPOS_Database; Integrated Security=True;");
            SqlCommand comm = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataAdapter reader = new SqlDataAdapter(comm);
            DataTable dt = new DataTable();

            reader.Fill(dt);
            
            foreach (DataRow row in dt.Rows)
            {
                ctr++;
                msg += ctr.ToString() + ". " + row["username"].ToString() + " in " + row["Moves"].ToString() + " moves \n";                
            }
            
            conn.Close();

            return msg;
        }
    }
}
