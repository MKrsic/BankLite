using BankLite.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace BankLite.Data.Repository
{
    public class UserRepository : RepositoryBase<User>
    {

        public List<User> GetList()
        {
            return this.DbContext.User
                .Include(u => u.Role)
                .Include(u => u.UserData)
                .OrderBy(u => u.ID)
                .ToList();
        }

        public override User Find(int id)
        {
            return this.DbContext.User
                .Include(u => u.Role)
                .Include(u => u.UserData)
                .Where(u => u.ID == id)
                .FirstOrDefault();
        }

        public string CheckUser(string username, string password)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["BankLiteDbContext"].ConnectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "dbo.CheckLogin";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("UserName", username);
                    cmd.Parameters.AddWithValue("Password", password);
                    //SqlParameter output = new SqlParameter("@responseMessage", SqlDbType.NVarChar);
                    //output.Direction = ParameterDirection.Output;
                    //cmd.Parameters.Add(output);
                    var returnParameter = cmd.Parameters.Add("@responseMessage", SqlDbType.NVarChar, 250);
                    returnParameter.Direction = ParameterDirection.Output;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    var result = returnParameter.Value;
                    return result.ToString();
                }
            }
            catch(Exception ex)
            {
                return "Dogodila se greška";
            }
        }
    }
}
