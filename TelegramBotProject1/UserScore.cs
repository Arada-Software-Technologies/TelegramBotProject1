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
            /*using (SqlConnection conn = new SqlConnection("Server= localhost; Database= EdirManagement_system; Integrated Security=True;"))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spMember", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                // cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userid", userid);
                cmd.Parameters.AddWithValue("@FirstName", Fname);
                cmd.Parameters.AddWithValue("@LastName", Lname);
                cmd.Parameters.AddWithValue("@Phone", phoneNum);
                cmd.Parameters.AddWithValue("@Address", houseNo);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Gender", gender);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data inserted Succesfully");

            }*/






        }
    }
}
